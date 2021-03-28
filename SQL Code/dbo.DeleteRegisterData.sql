CREATE PROCEDURE [dbo].[DeleteRegisterData](
	@id int
)
AS
BEGIN
	DELETE [dbo].[RecordRegistration]
	WHERE ID=@id;
END