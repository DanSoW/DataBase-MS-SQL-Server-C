CREATE PROCEDURE [dbo].[WriteRegisterData](
	@id bigint,
	@reg bigint,
	@pwd bigint,
	@dateIssue date,
	@dateReturn date
)AS
BEGIN
	IF(EXISTS(SELECT * FROM [dbo].[RecordRegistration] WHERE ID=@id))
	BEGIN
		RETURN (-4);
	END;

	IF(NOT EXISTS(SELECT * FROM [dbo].[Book] WHERE Register_Number=@reg))
	BEGIN
		RETURN (-2);
	END;

	IF(NOT EXISTS(SELECT * FROM [dbo].[Reader] WHERE Password_Data=@pwd))
	BEGIN
		RETURN (-1);
	END;

	IF(@dateIssue > @dateReturn)
	BEGIN
		RETURN (-3);
	END;

	INSERT INTO [dbo].[RecordRegistration] (ID, Book_Register_Number, Reader_Password_Data, Date_Issue, Date_Return)
	VALUES (@id, @reg, @pwd, @dateIssue, @dateReturn);
	RETURN 0;
END;