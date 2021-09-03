CREATE PROCEDURE [dbo].[spUserLookup]
	@Id NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON

	SELECT FirstName,LastName,EmailAddress, BirthDate, LastLogin
	FROM dbo.Users
	WHERE Id = @Id;
END