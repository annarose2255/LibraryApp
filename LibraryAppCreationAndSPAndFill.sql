-- DDL SCRIPT CREATE LIBRARY APP

Create Database LibraryApp;

Create Table Roles(
	RoleID int Primary Key Identity(0,1) Not Null,
	RoleName nvarchar(100) Not Null UNIQUE,
	RoleDescription nvarchar(100),
)
--changed
Create Table Users(
	UserID int Primary Key Identity(0,1) Not Null,
	UserName nvarchar(100) Not Null UNIQUE,
	[Password] nvarchar(100) Not Null,
	FirstName nvarchar(100) Null,
	LastName nvarchar(100) Null,
	RoleID int FOREIGN KEY REFERENCES Roles(RoleID) NULL
)
Create Table LoggingExceptions(
	[ExceptionLoggingID] [int] IDENTITY(1,1) NOT NULL,
	[StackTrace] [nvarchar](1000) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Source] [nvarchar](1000) NULL,
	[Url] [nvarchar](1000) NULL,
	[LogDate] [datetime] NOT NULL
)


/** DDL FILE FOR SP **/

--INSERT ROLE SP	
go
CREATE PROCEDURE InsertRole

	-- Add the parameters for the stored procedure here
	  ( @RoleName nvarchar(100),
	   @RoleDescription nvarchar(100) = null, 
	   @ParamOutRoleID int out) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Roles (RoleName, RoleDescription) 
	Values (@RoleName, @RoleDescription)
	
	select @ParamOutRoleID = RoleID 
	from roles 
	where RoleName = @RoleName
END
GO

--Select all ROLES SP
go
CREATE PROCEDURE SelectALLRoles	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Roles 
END
GO

--SELECT ROLE BY ID SP
go
CREATE PROCEDURE SelectRoleByID
	(@RoleID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Roles 
	where RoleID = @RoleID
END
GO

--SELECT ROLE BY NAME SP
go
CREATE PROCEDURE SelectRoleByName
	(@RoleName nvarchar(100))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Roles 
	where RoleName = @RoleName
END
GO

--Update ROLES SP
go
CREATE PROCEDURE UpdateRole	
	  ( @RoleID int,
	  @RoleName nvarchar(100),
	   @RoleDescription nvarchar(100) = null) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Roles
	set RoleName = @RoleName, RoleDescription = @RoleDescription
	where @RoleID = RoleID
END
GO

--DELETE ROLE
go
create PROCEDURE DeleteRoleByID	
	  (@RoleID int) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Roles
	where @RoleID = RoleID
	--DBCC CHECKIDENT ('Roles', RESEED, @RoleID)
END
GO

--DELETE ROLE BY NAME
go
create PROCEDURE DeleteRoleByName	
	  (@RoleName nvarchar(100)) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Roles
	where @RoleName = RoleName
END
GO

--DELETE ALL ROLES
go
create PROCEDURE DeleteAllRoles	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Roles
	DBCC CHECKIDENT ('Roles', RESEED, -1) -- reset the id

END
GO 

--!!USERS!!

--INSERT USER SP	
go
CREATE PROCEDURE InsertUser

	-- Add the parameters for the stored procedure here
	  (@UserName nvarchar(100),
	   @Password nvarchar(100),
	   @FirstName nvarchar(100) = null,
	   @LastName nvarchar(100) = null,
	   @RoleID int = null,
	   @ParamOutUserID int out)
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into Users (UserName, [Password], FirstName, LastName, RoleID) 
	Values (@UserName, @Password, @FirstName, @LastName, @RoleID)

	select @ParamOutUserID = UserID 
	from Users 
	where UserName = @UserName
END
GO

--SELECT ALL USERS SP	
go
CREATE PROCEDURE SelectAllUsers
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Users
END
GO

--SELECT USER BY ID
go
CREATE PROCEDURE SelectUserByID
	(@UserID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Users
	where UserID = @UserID
END
GO

--SELECT USER BY USERNAME
go
CREATE PROCEDURE SelectUserByUsername
	(@Username nvarchar(100))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Users
	where UserName = @Username
END
GO

go
CREATE PROCEDURE SelectUserByUsernameAndPassword
	(@Username nvarchar(100),
	@Password nvarchar(100))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from Users
	where UserName = @Username AND [Password] = @Password 
END
GO

--UPDATE USER  ------ GET RID OF THIS
go
CREATE PROCEDURE UpdateUser

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	  @UserName nvarchar(100),
	   @Password nvarchar(100),
	   @FirstName nvarchar(100),
	   @LastName nvarchar(100),
	   @RoleID int) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set UserName = @UserName, [Password] = @Password, FirstName = @FirstName, LastName = @LastName, RoleID = @RoleID
	where UserID = @UserID OR UserName = @UserName

END
GO

go
CREATE PROCEDURE UpdateUserByUsername

	-- Add the parameters for the stored procedure here
	  (@UserName nvarchar(100),
	   @Password nvarchar(100),
	   @FirstName nvarchar(100),
	   @LastName nvarchar(100),
	   @RoleID int =null) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set [Password] = @Password, FirstName = @FirstName, LastName = @LastName, RoleID = @RoleID
	where UserName = @UserName

END
GO

go
CREATE PROCEDURE UpdateUserByID

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	  @UserName nvarchar(100),
	   @Password nvarchar(100),
	   @FirstName nvarchar(100) = null,
	   @LastName nvarchar(100)= null,
	   @RoleID int = null) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set UserName = @UserName, [Password] = @Password, FirstName = @FirstName, LastName = @LastName, RoleID = @RoleID
	where UserID = @UserID 

END
GO

--better
go
CREATE PROCEDURE UpdateUserUsername

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	  @UserName nvarchar(100)) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set UserName = @UserName
	where UserID = @UserID 

END
GO

go
CREATE PROCEDURE UpdateUserPassword

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	   @Password nvarchar(100)) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set [Password] = @Password
	where UserID = @UserID 

END
GO


go
CREATE PROCEDURE UpdateUserFirstName

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	   @FirstName nvarchar(100) = null) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set FirstName = @FirstName
	where UserID = @UserID 

END
GO

go
CREATE PROCEDURE UpdateUserLastName

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	   @LastName nvarchar(100)= null) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set LastName = @LastName
	where UserID = @UserID 

END
GO

go
CREATE PROCEDURE UpdateUserRoleID

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	   @RoleID int = null) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Users
	set RoleID = @RoleID
	where UserID = @UserID 

END
GO


--DELETE ALL USERS
go
CREATE PROCEDURE DeleteAllUsers
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Users
	where not UserID = 0
	DBCC CHECKIDENT ('Users', RESEED, 0) -- reset the id

END
GO

/*--DELETE USER BY ID or Name
go
CREATE PROCEDURE DeleteUserByIDOrName

	-- Add the parameters for the stored procedure here
	  (@UserID int,
	  @UserName nvarchar(100))
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Users
	where UserID = @UserID OR UserName = @UserName

END
GO*/

--DELETE USER BY ID
go
CREATE PROCEDURE DeleteUserByID

	-- Add the parameters for the stored procedure here
	  (@UserID int)
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Users
	where UserID = @UserID 

END
GO

--DELETE USER BY Name
go
CREATE PROCEDURE DeleteUserByName

	-- Add the parameters for the stored procedure here
	  (@UserName nvarchar(100))
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Users
	where UserName = @UserName

END
GO

--GET USER AND ROLE NAME
go
CREATE PROCEDURE SelectUserandRoleNameByID
	(@UserID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select u.UserID, u.UserName, u.Password, u.FirstName, u.LastName, r.RoleName from Users as u
	inner join Roles as r on r.RoleID = u.RoleID
	where u.UserID = @UserID
END
GO

--SELECT ALL USERS AND ROLES
go
CREATE PROCEDURE SelectAllUserandRoleName
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select u.UserID, u.UserName, u.Password, u.FirstName, u.LastName, r.RoleName from Users as u
	left join Roles as r on r.RoleID = u.RoleID
	
END
GO

--ERROR LOGGING INSERT
go
create  PROCEDURE CreateLogException
-- Add the parameters for the stored procedure here
	(@parmStackTrace nvarchar(1000) = null,
	@parmMessage nvarchar(1000),
	@parmSource nvarchar(1000) = null,
	@parmURL nvarchar(1000) = null,
	@parmLogdate datetime,
	@parmOutExceptionLoggingID int out)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
INSERT INTO LoggingExceptions
           ([StackTrace]
           ,[Message]
           ,[Source]
           ,[Url]
           ,[LogDate])
     VALUES
           (@parmStackTrace
           ,@parmMessage
           ,@parmSource
           ,@parmURL
           ,@parmLogdate
		   )		   -- make an actual **assignment** here...
    SELECT @parmOutExceptionLoggingID = SCOPE_IDENTITY();
END
go

go
create  PROCEDURE SelectAllLogException
-- Add the parameters for the stored procedure here
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
	select * from LoggingExceptions
END
go

--Testing roles sp
exec InsertRole 'Guest', 'Guest of the system', 0
exec InsertRole 'Admin', 'Admin of the system', 0
exec InsertRole 'Member', 'Member of the system', 0

--Testing User Sp
exec InsertUser 'Guest', 'Password', null, null, 0, 0
exec InsertUser 'Tester1', 'Password123', 'T', 'tee', 2, 0
exec InsertUser 'TrueOwner', 'Owner', 'I', 'Own', 1, 0
