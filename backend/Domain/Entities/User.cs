
namespace Domain.Entities
{
    public sealed class User
    {
        public Guid Id {  get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly Fecha_nacimiento {  get; set; }
        public string Direccion { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha_creacion {  get; set; }
        public DateTime? Fecha_modificacion { get; set; }
    }
}
