CREATE PROCEDURE [dbo].[addrecord]
    @Id uniqueidentifier,
    @Name NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO [dbo].[Table1] ([Id], [Name])
    VALUES (@Id, @Name)
END
