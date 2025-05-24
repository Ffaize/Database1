CREATE PROCEDURE [dbo].[updaterecord]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(Max)
AS
BEGIN
    UPDATE dbo.Table1 SET Name = @Name WHERE Id = @Id
END

