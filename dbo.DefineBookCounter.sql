CREATE PROCEDURE [dbo].[DefineBookCounter](
	@pwd bigint
)
AS
BEGIN
	/*Определение, какое количество книг было зарегистрировано под определённым читателем*/
	DECLARE @countBook int;
	SET @countBook=(SELECT COUNT(*) FROM [dbo].[RecordRegistration] WHERE Reader_Password_Data=@pwd);
	RETURN @countBook;
END;