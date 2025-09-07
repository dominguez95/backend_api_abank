using Application.Common.Security;

using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<User>> GetUsers(int pageNumber, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var offset = (pageNumber - 1) * pageSize;

            var sql = @"
                SELECT COUNT(*) FROM auth.users;
                SELECT *
                FROM auth.users
                ORDER BY id
                LIMIT @PageSize OFFSET @Offset;";

            using var connection = _context.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(sql, new { PageSize = pageSize, Offset = offset });

            var total = await multi.ReadFirstAsync<int>();
            var items = (await multi.ReadAsync<User>()).ToList();

            return new PagedResult<User>
            {
                Items = items,
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<User?> GetUser(Guid id)
        {
            var sql = "SELECT * FROM auth.users WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task CreateUser(User user)
        {
            var sql = @"INSERT INTO auth.users( nombres, apellidos, fecha_nacimiento, direccion, password, telefono, email, estado)
                VALUES (@nombres, @apellidos, @fecha_nacimiento, @direccion, @password, @telefono, @email, @estado)";

            using var connection = _context.CreateConnection();


            var parameters = new
            {
                user.Nombres,
                user.Apellidos,
                fecha_nacimiento = user.Fecha_nacimiento.ToDateTime(TimeOnly.MinValue), // <- conversión
                user.Direccion,
                Password = PasswordHasher.Hash(user.Password),
                user.Telefono,
                user.Email,
                user.Estado
            };

            try
            {
                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                throw MapToFriendlyException(ex);
            }
        }



        public async Task UpdateUser(User user, Guid id)
        {
            var sql = @"UPDATE auth.users 
                SET Nombres = @Nombres,
                    Apellidos = @Apellidos,
                    Fecha_nacimiento = @fecha_nacimiento,
                    Direccion = @Direccion,
                    Password = @Password,
                    Telefono = @Telefono,
                    Email = @Email,
                    Estado = @Estado
                WHERE Id = @Id";

            using var connection = _context.CreateConnection();

            // Objeto anónimo con conversión de DateOnly a DateTime
            var parameters = new
            {
                user.Nombres,
                user.Apellidos,
                fecha_nacimiento = user.Fecha_nacimiento.ToDateTime(TimeOnly.MinValue),
                user.Direccion,
                Password = PasswordHasher.Hash(user.Password),
                user.Telefono,
                user.Email,
                user.Estado,
                Id = id
            };

            try
            {
                await connection.ExecuteAsync(sql, parameters);
            }
            catch (Exception ex)
            {
                throw MapToFriendlyException(ex);
            }
        }


        public async Task DeleteUser(Guid id)
        {
            var sql = "DELETE FROM auth.users WHERE Id=@Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<User?> PhoneUser(string telefono)
        {
            var sql = "SELECT * FROM auth.users WHERE telefono=@Telefono";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Telefono = telefono });
        }

        private static Exception MapToFriendlyException(Exception ex)
        {
            if (ex is NotSupportedException nse && nse.Message.Contains("DateOnly", StringComparison.OrdinalIgnoreCase))
                return new ArgumentException("El campo 'fecha_nacimiento' debe ser una fecha válida con formato yyyy-MM-dd.");

            if (ex is PostgresException pg)
            {
                return pg.SqlState switch
                {
                    "22001" => new ArgumentException("Alguno de los valores excede la longitud permitida."),
                    "23505" => new InvalidOperationException("Ya existe un registro con datos únicos duplicados (por ejemplo, email)."),
                    _ => new InvalidOperationException("No se pudo completar la operación. Verifica los datos enviados.")
                };
            }

            return new InvalidOperationException("No se pudo completar la operación. Inténtalo de nuevo.");
        }

      
    }
}
