CREATE PROCEDURE [dbo].[getrecords]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT [Id], [Name]
    FROM [dbo].[Table1];
END
