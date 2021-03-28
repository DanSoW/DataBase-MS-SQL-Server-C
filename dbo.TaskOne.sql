CREATE PROCEDURE [dbo].[TaskOne]
	@valueMonth int
AS
BEGIN
	/*Решение первой задачи*/
	IF((SELECT COUNT(*) FROM [dbo].[RecordRegistration] WHERE (((MONTH(Date_Return) - MONTH(Date_Issue)) > @valueMonth)) OR ((YEAR(Date_Return) - YEAR(Date_Issue)) > MOD(@valueMonth, 12))) = 0)
	BEGIN
		RETURN 0;
	END;

	(SELECT [dbo].[Reader].Full_Name FROM [dbo].[Reader] WHERE
	(EXISTS(SELECT * FROM [dbo].[RecordRegistration] WHERE
	[dbo].[RecordRegistration].Reader_Password_Data=[dbo].[Reader].Password_Data)))

END;
