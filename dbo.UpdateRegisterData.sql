CREATE PROCEDURE [dbo].[UpdateRegisterData](
	@id int,
	@reg bigint,
	@pwd bigint,
	@dateIssue date,
	@dateReturn date
)
AS
BEGIN

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

	UPDATE RecordRegistration
	SET Book_Register_Number=@reg
	WHERE ID=@id;

	UPDATE RecordRegistration
	SET Reader_Password_Data=@pwd
	WHERE ID=@id;

	UPDATE RecordRegistration
	SET Date_Issue=@dateIssue
	WHERE ID=@id;

	UPDATE RecordRegistration
	SET Date_Return=@dateReturn
	WHERE ID=@id;

	RETURN 0;
END;