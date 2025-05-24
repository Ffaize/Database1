CREATE PROCEDURE [dbo].[getrecordsbyname]
	@Name NVARCHAR(Max)
AS
BEGIN
    SELECT Id, Name
    FROM dbo.Table1
    WHERE Name = @Name
END
