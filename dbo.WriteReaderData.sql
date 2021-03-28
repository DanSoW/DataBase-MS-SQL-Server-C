CREATE PROCEDURE [dbo].[WriteReaderData](
	@pwd bigint,
	@home nvarchar(80),
	@fname nvarchar(50)
)
AS
BEGIN
	/*Логика проверки входного параметра на существование в уже созданной таблице Reader*/
	IF(EXISTS(SELECT * FROM [dbo].[Reader] WHERE Password_Data=@pwd))
	BEGIN
		RETURN (-1);
	END;

	/*Добавление записи*/
	SET IDENTITY_INSERT [dbo].[Reader] ON;
	INSERT INTO [dbo].[Reader] (Password_Data, Home_Address, Full_Name)
	VALUES (@pwd, @home, @fname);
	SET IDENTITY_INSERT [dbo].[Reader] OFF;

	RETURN 0;
END;