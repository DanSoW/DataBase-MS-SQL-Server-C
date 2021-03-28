CREATE PROCEDURE [dbo].[WriteBookData](
	@reg bigint,
	@pages smallint,
	@year smallint,
	@section nvarchar(150)
)
AS
BEGIN
	IF(EXISTS(SELECT * FROM [dbo].[Book] WHERE Register_Number=@reg))
	BEGIN
		RETURN (-1);
	END;

	SET IDENTITY_INSERT [dbo].[Book] ON;
	INSERT INTO [dbo].[Book] (Register_Number, Count_Pages, Year_Publishing, Section)
	VALUES (@reg, @pages, @year, @section);
	SET IDENTITY_INSERT [dbo].[Book] OFF;
	
	RETURN 0;
END;