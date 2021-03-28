ALTER PROCEDURE [dbo].[TaskOneT]
	@monthValue int
AS
BEGIN
	SELECT [dbo].[Reader].Full_Name, [dbo].[Reader].Password_Data, [dbo].[RecordRegistration].Book_Register_Number, [dbo].[RecordRegistration].Date_Issue, [dbo].[RecordRegistration].Date_Return
	FROM [dbo].[Reader], [dbo].[RecordRegistration]
	WHERE ([dbo].[Reader].Password_Data = [dbo].[RecordRegistration].Reader_Password_Data) AND
	((MONTH([dbo].[RecordRegistration].Date_Return) - MONTH([dbo].[RecordRegistration].Date_Issue)) > @monthValue)
	OR ((YEAR([dbo].[RecordRegistration].Date_Return) - YEAR([dbo].[RecordRegistration].Date_Issue)) > (@monthValue % 12));
END;