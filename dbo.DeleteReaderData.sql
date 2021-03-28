CREATE PROCEDURE [dbo].[DeleteReaderData](
	@pwd bigint
)
AS
BEGIN
	IF(EXISTS(SELECT * FROM [dbo].[RecordRegistration] WHERE Reader_Password_Data=@pwd))
	BEGIN
		RETURN (-1);
	END;

	DELETE [dbo].[Reader]
	WHERE Password_Data=@pwd;
	DBCC CHECKIDENT ('dbo.Reader', RESEED, 0);
	RETURN 0;
END;