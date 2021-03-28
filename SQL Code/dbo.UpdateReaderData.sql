CREATE PROCEDURE [dbo].[UpdateReaderData](
	@pwd bigint,
	@newHome nvarchar(80),
	@newFName nvarchar(50)
)
AS
BEGIN
	UPDATE Reader
	SET Home_Address=@newHome
	WHERE Password_Data=@pwd;

	UPDATE Reader
	SET Full_Name=@newFName
	WHERE Password_Data=@pwd;

END;