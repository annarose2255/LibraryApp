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
	--Email  nvarchar(200) Null,
	--Phone  nvarchar(200) Null,
	RoleID int FOREIGN KEY REFERENCES Roles(RoleID) NULL
)
Create Table LoggingExceptions(
	[ExceptionLoggingID] [int] IDENTITY(1,1) NOT NULL,
	[StackTrace] [nvarchar](1000) NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Source] [nvarchar](1000) NULL,
	[LogDate] [datetime] NOT NULL
)
Create Table [Permissions](
	PermissionID int Primary Key Identity(0,1) Not Null,
	PermissionName nvarchar(100) Not Null UNIQUE,
	PermissionDescription nvarchar(100),
	RoleID int FOREIGN KEY REFERENCES Roles(RoleID) NULL
)

/** DDL FILE FOR SP **/
--Permissions SPs

--insert
go
CREATE PROCEDURE InsertPermission

	-- Add the parameters for the stored procedure here
	  ( @PermissionName nvarchar(100),
	   @PermissionDescription nvarchar(100) = null, 
	   @ParamOutPermID int out) 
		
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into [Permissions] (PermissionName, PermissionDescription) 
	Values (@PermissionName,  @PermissionDescription)
	
	select @ParamOutPermID = PermissionID 
	from [Permissions] 
	where PermissionName = @PermissionName
END
GO

--select all 
go
CREATE PROCEDURE SelectALLPermissions	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from [Permissions] 
END
GO

--update
go
CREATE PROCEDURE UpdatePermission
	  ( @PermissionID int,
		@PermissionName nvarchar(100),
	   @PermissionDescription nvarchar(100) = null,
	   @RoleID int = null) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update [Permissions]
	set PermissionName = @PermissionName, PermissionDescription = @PermissionDescription, RoleID = @RoleID
	where @PermissionID = PermissionID
END
GO

--delete 
go
create PROCEDURE DeletePermissionByName	
	  (@PermissionName nvarchar(100)) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from [Permissions]
	where @PermissionName = PermissionName
END
GO


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

go
CREATE PROCEDURE SelectRoleNameByName
	(@RoleName nvarchar(100))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select RoleName from Roles 
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
CREATE PROCEDURE SelectUserNameByUsername
	(@Username nvarchar(100))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select UserName from Users
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
	--where not UserID = 0
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
	Select u.UserID, u.UserName, u.[Password], u.FirstName, u.LastName, r.RoleName from Users as u
	left join Roles as r on r.RoleID = u.RoleID
	where u.UserID = @UserID
END
GO

go
CREATE PROCEDURE SelectUserAndRoleIDByRoleID
	(@RoleID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select u.UserID, u.UserName, u.[Password], u.FirstName, u.LastName, u.RoleID from Users as u
	left join Roles as r on r.RoleID = u.RoleID
	where u.RoleID = @RoleID
END
GO


go
CREATE PROCEDURE SelectUserAndPermissionIDByUserID
	(@UserID int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select u.UserID, u.UserName, u.[Password], u.FirstName, u.LastName, u.RoleID, p.PermissionID, p.PermissionName, p.PermissionDescription from Users as u
	left join Roles as r on r.RoleID = u.RoleID
	left join [Permissions] as p on p.RoleID = r.RoleID
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
	Select u.UserID, u.UserName, u.[Password], u.FirstName, u.LastName, r.RoleName from Users as u
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
           ,[LogDate])
     VALUES
           (@parmStackTrace
           ,@parmMessage
           ,@parmSource
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
exec SelectALLRoles
exec InsertRole 'Guest', 'Guest of the system', 0
exec InsertRole 'Admin', 'Admin of the system', 0
exec InsertRole 'Member', 'Member of the system', 0
exec SelectALLRoles
exec SelectRoleByID 4

exec SelectALLRoles
exec UpdateRole 0,'Guest', 'guest profile'
exec SelectALLRoles

exec SelectALLRoles
--exec DeleteRole 1
exec SelectALLRoles


--Exec InsertRole 'admin', null
exec SelectALLRoles

exec SelectALLRoles
Exec DeleteAllRoles
exec SelectALLRoles;

--Testing User Sp
exec SelectAllUsers
exec InsertUser 'Guest', 'Password', null, null, 0, 0
exec InsertUser 'Tester1', 'Password123', 'T', 'tee', 2, 0
exec InsertUser 'TrueOwner', 'Owner', 'I', 'Own', 1, 0
exec SelectAllUsers

exec SelectAllUsers
exec UpdateUser 0, 'HiyaGirls', 'password', null, 'apple', 4
exec SelectAllUsers

exec SelectAllUsers
exec UpdateUser 1, 'HiyaGirls', 'password', 'Fire', null, 4
exec SelectAllUsers
exec SelectUserByID 3

exec SelectAllUsers
--exec InsertUser  'HiyaGirls2', 'password', 'free', null, 6
exec SelectAllUsers
--exec DeleteUserByIDOrName 3, 'HiyaGirls'
exec SelectAllUsers


--exec InsertUser  'HiyaGirls2', 'password', 'free', null, 6
exec SelectAllUsers
exec DeleteAllUsers
exec SelectAllUsers

exec SelectAllUsers
--exec InsertUser 'Hiyay', 'Password', null, null, 4
--exec InsertUser 'Hiyay2', 'Password', null, null, null
exec SelectUserandRoleNameByID 3
exec SelectAllUserandRoleName
exec SelectALLPermissions

exec InsertPermission 'Admin', 'Can do anything', 0
exec SelectALLPermissions
exec UpdatePermission 0, 'Admin', 'Can do anything', 1
exec InsertPermission 'Guest', 'Guest account: cannot edit profile or add roles', 0
exec UpdatePermission 1, 'Guest', 'Guest account: cannot edit profile or add roles', 0
exec InsertPermission 'Member', 'Member account: Can edit and add but not see all user profiles', 0
exec UpdatePermission 2, 'Member', 'Member account: Can edit and add but not see all user profiles', 2

exec SelectUserAndPermissionIDByUserID 0