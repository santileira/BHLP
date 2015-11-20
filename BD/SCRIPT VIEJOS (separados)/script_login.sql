CREATE PROCEDURE [ABSTRACCIONX4].LoginAdministrador
(@Usuario VARCHAR(20), @ContraseniaIngresada VARCHAR(70))
AS
BEGIN
	DECLARE @Contrasenia VARCHAR(70),
			@CantidadIntentos TINYINT,
			@ExisteUsuario BIT

	SELECT @ExisteUsuario = COUNT(*) 
		FROM ABSTRACCIONX4.USUARIOS
		WHERE USERNAME = @Usuario
	

	IF @ExisteUsuario = 0
	BEGIN
		RAISERROR('El nombre de usuario ingresado no existe.', 16, 1)
		RETURN
	END

	SELECT @Contrasenia = PASSWORD, @CantidadIntentos = CANT_INTENTOS
		FROM ABSTRACCIONX4.USUARIOS
		WHERE USERNAME = @Usuario

	IF @CantidadIntentos = 3
	BEGIN
		RAISERROR('Ha ingresado la contraseña 3 veces de forma incorrecta. Contáctese con un administrador para reestablecer su cuenta.', 16, 1)
		RETURN
	END

	IF @ContraseniaIngresada <> @Contrasenia
	BEGIN
		RAISERROR('Contraseña incorrecta.', 16, 1)
		
		UPDATE ABSTRACCIONX4.USUARIOS 
			SET CANT_INTENTOS = CANT_INTENTOS + 1
			WHERE USERNAME = @Usuario

		SELECT @CantidadIntentos = CANT_INTENTOS
			FROM ABSTRACCIONX4.USUARIOS
			WHERE USERNAME = @Usuario
		
		IF @CantidadIntentos = 3
		BEGIN
			RAISERROR('Ha ingresado la contraseña 3 veces de forma incorrecta. Contáctese con un administrador para reestablecer su cuenta.', 16, 1)
		END
		RETURN
	END

	UPDATE ABSTRACCIONX4.USUARIOS 
		SET CANT_INTENTOS = 0
		WHERE USERNAME = @Usuario
END

GO