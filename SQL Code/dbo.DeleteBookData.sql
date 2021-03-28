CREATE PROCEDURE [dbo].[DeleteBookData](
	@reg bigint
)
AS
BEGIN
	IF(EXISTS(SELECT * FROM [dbo].[RecordRegistration] WHERE Book_Register_Number=@reg))
	BEGIN
		RETURN (-1);
	END;

	DELETE [dbo].[Book]
	WHERE Register_Number=@reg;
	DBCC CHECKIDENT ('dbo.Book', RESEED, 0);
END