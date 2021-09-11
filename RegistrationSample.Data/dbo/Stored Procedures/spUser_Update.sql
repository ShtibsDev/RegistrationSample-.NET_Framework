CREATE PROCEDURE [dbo].[spUser_Update]
	@Id NVARCHAR(128), 
    @FirstName NVARCHAR(50), 
    @LastName NVARCHAR(50), 
    @EmailAddress NVARCHAR(256), 
    @BirthDate DATETIME2,
    @LastLogin DATETIME2
AS
BEGIN
    SET NOCOUNT ON

	UPDATE dbo.Users 
    SET FirstName = @FirstName,
        LastName = @LastName,
        BirthDate = @BirthDate
    WHERE Id = @Id;
END
