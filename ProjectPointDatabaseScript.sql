USE [ProjectPoint]
GO
/****** Object:  Table [dbo].[User]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](500) NULL,
	[LastName] [varchar](500) NULL,
	[EmailId] [varchar](500) NULL,
	[Address] [varchar](max) NULL,
	[DateOfBirth] [varchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersExperiencedetails]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersExperiencedetails](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NULL,
	[FromDate] [varchar](10) NULL,
	[ToDate] [varchar](10) NULL,
	[Position] [varchar](500) NULL,
	[CompanyName] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UsersExperiencedetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FirstName], [LastName], [EmailId], [Address], [DateOfBirth], [CreatedDate], [UpdatedDate]) VALUES (1, N'Amipara', N'Kartik', N'amiparakartik@gmail.com', N'Vejalpur Ahmedabad
sasdasdasd', N'07/05/2022', CAST(N'2022-07-20T17:44:27.117' AS DateTime), CAST(N'2022-07-20T17:45:09.583' AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersExperiencedetails] ON 

INSERT [dbo].[UsersExperiencedetails] ([Id], [UserId], [FromDate], [ToDate], [Position], [CompanyName], [CreatedDate]) VALUES (10, 1, N'07/07/2022', N'07/07/2022', N'SR. Developer', N'Sobot', CAST(N'2022-07-20T17:45:09.590' AS DateTime))
INSERT [dbo].[UsersExperiencedetails] ([Id], [UserId], [FromDate], [ToDate], [Position], [CompanyName], [CreatedDate]) VALUES (11, 1, N'07/08/2022', N'07/13/2022', N'SR. Developer', N'TCS', CAST(N'2022-07-20T17:45:09.590' AS DateTime))
INSERT [dbo].[UsersExperiencedetails] ([Id], [UserId], [FromDate], [ToDate], [Position], [CompanyName], [CreatedDate]) VALUES (12, 1, N'07/07/2022', N'07/01/2022', N'SR. Manager', N'Rushkar', CAST(N'2022-07-20T17:45:09.590' AS DateTime))
INSERT [dbo].[UsersExperiencedetails] ([Id], [UserId], [FromDate], [ToDate], [Position], [CompanyName], [CreatedDate]) VALUES (13, 1, N'07/13/2022', N'07/15/2022', N'SR. Manager', N'MageWix', CAST(N'2022-07-20T17:45:09.590' AS DateTime))
SET IDENTITY_INSERT [dbo].[UsersExperiencedetails] OFF
GO
/****** Object:  StoredProcedure [dbo].[UserDelete]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[UserDelete]
	@Id bigint = 0
AS
BEGIN
    SET NOCOUNT ON;

	if (@Id > 0)
	begin
		delete from [User] WHERE Id=@Id
		delete from UsersExperiencedetails where UserId = @Id
		select 1 as IsDeleted
	end
	else
	begin
		select 0 as IsDeleted
	end
END
GO
/****** Object:  StoredProcedure [dbo].[UserSelect]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec UserSelect 2
CREATE PROCEDURE [dbo].[UserSelect] 
	@Id bigint = 0 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Code Int = 400,
			@Message varchar(500) = ''

	SET @Code=200
	SET @Message= 'User retrieved successfully'

	SELECT @Code as Code,
		   @Message as [Message]
	select
		Id,
		(select U.* from UsersExperiencedetails U where U.UserId = @Id FOR JSON PATH) as UsersExperiencedetails,
		FirstName,
		LastName,
		EmailId,
		Address,
		DateOfBirth,
		CreatedDate,
		UpdatedDate
	from 
		[User]
	where 
		Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[UserSelectAll]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec [dbo].[UserSelectAll] 'Amipara Kartik',0,0
CREATE Procedure [dbo].[UserSelectAll]
  @Search nvarchar(MAX) = '',
  @OffSet bigint = 0,
  @Limit bigint = 0
AS 
BEGIN
    SET NOCOUNT ON;

	DECLARE @TotalRecords Bigint;

	SET @TotalRecords = (
		                    Select count (*) from [User] 
							WHERE 
							( isnull(@Search,'') = '' or
								(
									(lower(FirstName +' '+ LastName) like '%'+lower(@Search)+'%') 
								)	 
							)
						)

	IF @Limit = 0
	BEGIN
			SET @Limit = @TotalRecords
		
	IF @Limit = 0
	BEGIN
			SET @Limit = 10
    END
	
	END

	SELECT 
		Id,
		FirstName,
		LastName,
		EmailId,
		Address,
		DateOfBirth,
		CreatedDate,
		UpdatedDate
	from 
		[User]
	WHERE 
		( isnull(@Search,'') = '' or
			(
				(lower(FirstName +' '+ LastName) like '%'+lower(@Search)+'%') 
			)	 
		)			

	ORDER BY Id DESC
		
	OFFSET @offset rows fetch next @Limit rows only

	select @TotalRecords as TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[UsersExperienceInsert]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsersExperienceInsert]
	@Id bigint= 0,
	@UserId bigint = 0,
	@FromDate varchar(10) = null,
	@ToDate varchar(100) = null,
	@Position varchar(500) = null,
	@CompanyName varchar(max) = null
AS
BEGIN
	SET NOCOUNT ON;
	declare @Code int = 0,@Message varchar(max) = null

	if @Id = 0
	begin
		insert into [UsersExperiencedetails]
		(
			UserId,
			FromDate,
			ToDate,
			Position,
			CompanyName,
			CreatedDate
		)
		values
		(
			@UserId,
			@FromDate,
			@ToDate,
			@Position,
			@CompanyName,
			GETUTCDATE()
		)

		SET @Id = scope_identity()
		SET @Code = 200
		SET @Message = 'User created success fully'

	end
	
    select @Code as Code,@Message as Message
	select
		Id,
		UserId,
		FromDate,
		ToDate,
		Position,
		CompanyName,
		CreatedDate
	from 
		[UsersExperiencedetails] 
	where 
		Id = @Id


END
GO
/****** Object:  StoredProcedure [dbo].[UserUpsert]    Script Date: 20-07-2022 10:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- exec UserUpsert 0,'Amipara','Kartik','kartik@gmail.com','Ahmedabad','07/05/2000'
CREATE PROCEDURE [dbo].[UserUpsert]
	@Id int= 0,
	@FirstName varchar(500) = null,
	@LastName varchar(500) = null,
	@EmailId varchar(500) = null,
	@Address varchar(max) = null,
	@DateOfBirth varchar(10) = null
AS
BEGIN
	SET NOCOUNT ON;
	declare @Code int = 0,@Message varchar(max) = null

	if @Id = 0
	begin
		if (select count(*) from [User] where EmailId = @EmailId) > 0
		begin
			SET @Code = 400
			SET @Message = 'Email is already exit!'
		end
		else 
		begin
			insert into [User]
			(
				FirstName,
				LastName,
				EmailId,
				Address,
				DateOfBirth,
				CreatedDate
			)
			values
			(
				@FirstName,
				@LastName,
				@EmailId,
				@Address,
				@DateOfBirth,
				GETUTCDATE()
			)

			SET @Id = scope_identity()
			SET @Code = 200
			SET @Message = 'User created success fully'
		end
	end
	else if @Id > 0
	begin
		if (select count(*) from [User] where EmailId = @EmailId and Id != @Id) > 0
		begin
			SET @Code = 400
			SET @Message = 'Email is already exit!'
		end
		else 
		begin
			--upser update delete previous data in UsersExperiencedetails
			delete from UsersExperiencedetails where UserId = @Id

			update [User] set
				FirstName = @FirstName,
				LastName = @LastName,
				EmailId = @EmailId,
				Address = @Address,
				DateOfBirth = @DateOfBirth,
				UpdatedDate = GETUTCDATE()
			where Id = @Id
			SET @Code = 200
			SET @Message = 'User updated success fully'
		end
	end

    select @Code as Code,@Message as Message
	select
		Id,
		FirstName,
		LastName,
		EmailId,
		Address,
		DateOfBirth,
		CreatedDate,
		UpdatedDate
	from 
		[User] 
	where 
		Id = @Id


END
GO
