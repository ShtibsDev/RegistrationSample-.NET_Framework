CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id NVARCHAR(128), 
    @FirstName NVARCHAR(50), 
    @LastName NVARCHAR(50), 
    @EmailAddress NVARCHAR(256), 
    @BirthDate DATETIME2,
    @LastLogin DATETIME2
AS
BEGIN
    INSERT INTO Users(Id, FirstName, LastName, EmailAddress, BirthDate)
    VALUES(@Id, @FirstName, @LastName, @EmailAddress, @BirthDate)
END
