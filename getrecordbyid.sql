CREATE PROCEDURE [dbo].[getrecordbyid]
	@Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT Id, Name
    FROM dbo.Table1
    WHERE Id = @Id
END
