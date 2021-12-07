USE [LibraryApp]
GO
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 12/7/2021 10:45:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DeleteRole]	
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
