-- 1- CREATE SCHEMA AUTH
CREATE SCHEMA IF NOT EXISTS auth;
-- 2- ACTIVE UUID PGCRYPTO
CREATE EXTENSION IF NOT EXISTS "pgcrypto";
-- 3- CREATE TABLE USERS
CREATE TABLE IF NOT EXISTS auth.users(
	id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
	nombres varchar(255) NOT NULL,
	apellidos varchar(255) NOT NULL,
	fecha_nacimiento date NOT NULL,
	direccion text NOT NULL,
	password varchar(120) NOT NULL,
	telefono char(8)NOT NULL,
	email varchar(150) NOT NULL,
	estado CHAR(1) NOT NULL DEFAULT 'A',
 	fecha_creacion timestamp NULL,
	fecha_modificacion timestamp NULL,
   CONSTRAINT chk_estado CHECK (estado IN ('A','I'))
);
-- 4- CREATE STORE PROCEDURE
CREATE OR REPLACE FUNCTION auth.update_timestamps()
RETURNS TRIGGER AS $$
BEGIN
	IF TG_OP = 'INSERT' THEN
		NEW.fecha_creacion := COALESCE(NEW.fecha_creacion, NOW());
		NEW.fecha_modificacion := NULL;
	END IF;
	IF TG_OP = 'UPDATE' THEN
		NEW.fecha_modificacion := NOW();
	END IF;

	RETURN NEW;
END;
$$ LANGUAGE plpgsql;
-- 5- INIT TRIGGER
CREATE TRIGGER trg_update_timestamps
BEFORE INSERT OR UPDATE ON auth.users
FOR EACH ROW
EXECUTE FUNCTION auth.update_timestamps();

-- 6- CREATE USERS DEFAULT 
INSERT INTO auth.users(nombres, apellidos, fecha_nacimiento, direccion, email, password, telefono, estado) values('User', 'User', '1999-10-10', 'San Salvador', 'user@user.com', '$2a$11$xOt84VX.8Z.maYrEWq6mO.XLXYRL5/rmpnTdg5ppn54bA6d6juLJG', 'prueba', 'A'),
('User1', 'User1', '1999-10-10', 'Santa tecla', 'user1@user1.com', '$2a$11$xOt84VX.8Z.maYrEWq6mO.XLXYRL5/rmpnTdg5ppn54bA6d6juLJG', 'prueba1', 'I')
