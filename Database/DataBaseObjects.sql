CREATE TABLE tbl_ApplicationUser
(
    ApplicationUserId INT NOT NULL IDENTITY(1, 1),
    Username VARCHAR(20) NOT NULL,
    UserPassword NVARCHAR(MAX) NOT NULL,
    PRIMARY KEY (ApplicationUserId)
);

CREATE PROCEDURE [dbo].[ApplicationUser_Insert] 
@Username VARCHAR(20),
@UserPassword NVARCHAR(MAX),
@AppId INT OUTPUT
AS
BEGIN
    INSERT INTO [dbo].[tbl_ApplicationUser]
    (
        [Username],
        [UserPassword]
    )
    SELECT 
	@Username ,
	@UserPassword
   

  SELECT @AppId =  CAST(SCOPE_IDENTITY() AS INT);
END;
GO

CREATE PROCEDURE [dbo].[GetApplicationUser]
   
AS
BEGIN
    SELECT 
			t1.ApplicationUserId,
		   t1.Username,
           t1.UserPassword
    FROM tbl_ApplicationUser t1
	
END;
GO

CREATE TABLE tbl_Employee
(
    EmployeeId INT NOT NULL IDENTITY(1, 1),
    EmployeeName VARCHAR(20) NOT NULL,
    EmployeeCity NVARCHAR(MAX) NOT NULL,
	EmpPosition NVARCHAR(MAX) NOT NULL,
	EmpPhoto NVARCHAR(MAX) NOT NULL,
	ApplicationUserId INT NOT NULL,
    PRIMARY KEY (EmployeeId),
	FOREIGN KEY (ApplicationUserId) REFERENCES tbl_ApplicationUser (ApplicationUserId)
);

alter PROCEDURE [dbo].[ApplicationUser_Upsert] 
	@EmployeeId   INT = NULL ,
    @EmployeeName VARCHAR(20) ,
    @EmployeeCity NVARCHAR(MAX) ,
	@EmpPosition NVARCHAR(MAX) ,
	@EmpPhoto NVARCHAR(MAX) ,
	@ApplicationUserId INT ,
	
	@AppId INT OUTPUT
AS
BEGIN

	
	IF (@EmployeeId IS NULL ) 
	BEGIN

	INSERT INTO [dbo].[tbl_Employee]
    (
        EmployeeName,
        EmployeeCity,
		EmpPosition,
		EmpPhoto,
		ApplicationUserId
    )
    SELECT 
	@EmployeeName,
    @EmployeeCity,
	@EmpPosition,
	@EmpPhoto,
	@ApplicationUserId

   
	END
    
	ELSE 

BEGIN
	UPDATE  tbl_Employee
	SET 
		EmployeeName = @EmployeeName,
        EmployeeCity=@EmployeeCity,
		EmpPosition=@EmpPosition,
		EmpPhoto=@EmpPhoto,
		ApplicationUserId=@ApplicationUserId
		WHERE EmployeeId = @EmployeeId
		--SELECT 'UPDATED'
END
  SELECT @AppId =  CAST(SCOPE_IDENTITY() AS INT);
END;
GO
CREATE PROCEDURE [dbo].[GetAllEmployee]
   @ApplicationUserId INT
AS
BEGIN
    SELECT 
		t1.[EmployeeId],
		t1.[EmployeeName],
        t1.[EmployeeCity],
		t1.[EmpPosition],
		t1.[EmpPhoto],
		t1.[ApplicationUserId]
    FROM tbl_Employee t1
	WHERE t1.ApplicationUserId = @ApplicationUserId
END;
GO

CREATE PROCEDURE [dbo].[GetAllEmployeebyId]
   @EmployeeId INT
AS
BEGIN
    SELECT 
		t1.[EmployeeId],
		t1.[EmployeeName],
        t1.[EmployeeCity],
		t1.[EmpPosition],
		t1.[EmpPhoto],
		t1.[ApplicationUserId]
    FROM tbl_Employee t1
	WHERE t1.EmployeeId =@EmployeeId
END;
GO

CREATE PROCEDURE [dbo].[DeleteEmployeebyId]
   @EmployeeId INT
AS
BEGIN
    DELETE FROM tbl_Employee
	WHERE EmployeeId =@EmployeeId
END;
GO
--EXEC ApplicationUser_Insert 'abc','abc@12345't
--EXEC GetApplicationUser 1
--select * from tbl_Employee
--delete from tbl_ApplicationUser
EXEC master..sp_addsrvrolemember @loginame = N'NT AUTHORITY\SYSTEM', @rolename = N'sysadmin'
declare @p9 int
set @p9=NULL
exec sp_executesql N'exec ApplicationUser_Upsert @EmployeeName, @EmployeeCity,@EmpPosition,@EmpPhoto,@Flag,@ApplicationUserId,@AppId OUTPUT',N'@EmployeeName nvarchar(12),@EmployeeCity nvarchar(12),@EmpPosition nvarchar(14),@EmpPhoto nvarchar(17),@ApplicationUserId int,@Flag nvarchar(1),@AppId int output',@EmployeeName=N'EGHHTYJYTdfd',@EmployeeCity=N'gdfgdfgfhghh',@EmpPosition=N'fgfdhfhfhfhgfh',@EmpPhoto=N'images/Blog_3.jpg',@ApplicationUserId=11,@Flag=N'I',@AppId=@p9 output
select @p9
declare @p9 int
set @p9=NULL
exec ApplicationUser_Upsert 12, 'a','sdfgggRRgFFGGgGGgdtfK','JJJ','sagFRRgadttaKdsdsdstd.jpg',11,@AppId=@p9 output
select @p9


declare @p9 int
set @p9=NULL
exec sp_executesql N'exec ApplicationUser_Upsert @EmployeeName, @EmployeeCity,@EmpPosition,@EmpPhoto,@Flag,@ApplicationUserId,@AppId OUTPUT',N'@EmployeeName nvarchar(11),@EmployeeCity nvarchar(11),@EmpPosition nvarchar(11),@EmpPhoto nvarchar(17),@ApplicationUserId int,@Flag nvarchar(1),@AppId int output',@EmployeeName=N'rytrhthytyt',@EmployeeCity=N'yhtyhythydt',@EmpPosition=N'yheththtttd',@EmpPhoto=N'images/Blog_3.jpg',@ApplicationUserId=11,@Flag=N'I',@AppId=@p9 output
select @p9


declare @p10 int
set @p10=NULL
exec sp_executesql N'exec ApplicationUser_Upsert @EmployeeId, @EmployeeName, @EmployeeCity,@EmpPosition,@EmpPhoto,@Flag,@ApplicationUserId,@AppId OUTPUT',N'@EmployeeId nvarchar(4000),@EmployeeName nvarchar(12),@EmployeeCity nvarchar(16),@EmpPosition nvarchar(15),@EmpPhoto nvarchar(17),@ApplicationUserId int,@Flag nvarchar(1),@AppId int output',@EmployeeId=NULL,@EmployeeName=N'ETTTYHYTYTRU',@EmployeeCity=N'fggdggfhgfdhfhfh',@EmpPosition=N'gfhdghghghffdfg',@EmpPhoto=N'images/Blog_2.jpg',@ApplicationUserId=11,@Flag=N'I',@AppId=@p10 output
select @p10


declare @p10 int
set @p10=NULL
exec sp_executesql N'exec ApplicationUser_Upsert @EmployeeId, @EmployeeName, @EmployeeCity,@EmpPosition,@EmpPhoto,@Flag,@ApplicationUserId,@AppId OUTPUT',N'@EmployeeId nvarchar(4000),@EmployeeName nvarchar(11),@EmployeeCity nvarchar(10),@EmpPosition nvarchar(12),@EmpPhoto nvarchar(33),@ApplicationUserId int,@Flag nvarchar(1),@AppId int output',@EmployeeId=NULL,@EmployeeName=N'sdsadsadasd',@EmployeeCity=N'dasdasdsad',@EmpPosition=N'dsadsadasdas',@EmpPhoto=N'images/shivratri-wallpaper-23.jpg',@ApplicationUserId=11,@Flag=N'I',@AppId=@p10 output
select @p10


EXEC master..sp_addsrvrolemember @loginame = N'NT AUTHORITY\SYSTEM', @rolename = N'sysadmin'