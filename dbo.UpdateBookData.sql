CREATE PROCEDURE [dbo].[UpdateBookData](
	@reg bigint,
	@newPages smallint,
	@newYear smallint,
	@newSection nvarchar(150)
)
AS
BEGIN
	UPDATE Book
	SET Count_Pages=@newPages
	WHERE Register_Number=@reg;

	UPDATE Book
	SET Year_Publishing=@newYear
	WHERE Register_Number=@reg;

	UPDATE Book
	SET Section=@newSection
	WHERE Register_Number=@reg;

END;