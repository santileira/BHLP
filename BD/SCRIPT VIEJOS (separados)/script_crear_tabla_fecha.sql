CREATE PROCEDURE [ABSTRACCIONX4].crearTablaFecha
	@fecha datetime
AS
	DELETE FROM [ABSTRACCIONX4].[FECHA]

	insert into [ABSTRACCIONX4].[FECHA] values (@fecha)
GO

CREATE FUNCTION [ABSTRACCIONX4].obtenerFechaDeHoy()
RETURNS datetime
AS 
	begin
	return (select top 1 * from [ABSTRACCIONX4].[FECHA])
	end
GO

--AL PRINCIPIO, CREAR LA TABLA (EN LA MIGRACION)
drop procedure [ABSTRACCIONX4].crearTablaFecha
drop function [ABSTRACCIONX4].obtenerFechaDeHoy

select [ABSTRACCIONX4].obtenerFechaDeHoy()
