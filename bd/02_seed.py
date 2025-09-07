import os
import time
import psycopg2
from faker import Faker

# Utilizo Faker para generar datos falsos
fake = Faker()
# Espera a que la base de datos esté lista
while True:
    try:
        conn = psycopg2.connect(
            host=os.getenv("DB_HOST", "db"),
            port=os.getenv("DB_PORT", "5432"),
            dbname=os.getenv("DB_NAME", "user_management"),
            user=os.getenv("DB_USER", "admin"),
            password=os.getenv("DB_PASS", "admin123"),
        )
        break
    except psycopg2.OperationalError:
        print("Esperando a que PostgreSQL esté listo...")
        time.sleep(2)

cur = conn.cursor()

print("Insertando registros con Faker...")

for _ in range(25):  # Inserta 25 registros de ejemplo
    name = fake.name()
    surname = fake.last_name()
    birthdate = fake.date_of_birth(minimum_age=18)
    address = fake.address()
    password = fake.password()
    phone = str(fake.random_number(digits=8, fix_len=True)).zfill(8)
    email = fake.email()
    estado = fake.random_element(elements=("A", "I"))
    cur.execute(
        "INSERT INTO auth.users (nombres, apellidos, fecha_nacimiento, direccion, password, telefono, email, estado) VALUES (%s, %s, %s, %s, %s, %s, %s, %s);",
        (name, surname, birthdate, address, password, phone, email, estado),
    )

conn.commit()
cur.close()
conn.close()
print("✅ Datos falsos insertados con éxito")
