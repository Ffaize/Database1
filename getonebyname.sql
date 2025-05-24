CREATE PROCEDURE [dbo].[getonebyname]
	@Name NVARCHAR(Max)
AS
BEGIN
    SELECT TOP 1 Id, Name
    FROM dbo.Table1
    WHERE Name = @Name
END
