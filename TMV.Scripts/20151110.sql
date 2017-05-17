alter table Banner add CategoryId int
go

ALTER proc [dbo].[BannerInsert]
	 @Title nvarchar(256)
	,@ImagePath nvarchar(256)
	,@Priority tinyint
	,@StartDate datetime
	,@EndDate datetime
	,@IsMobile bit
	,@Content nvarchar(max)
	,@Type int
	,@Slug varchar(256)
	,@Url nvarchar(256)
	,@CategoryId int
as
insert into Banner
(
	 [Title]
	,[ImagePath]
	,[Priority]
	,[StartDate]
	,[EndDate]
	,[IsMobile]
	,[Content]
	,[Type]
	,[Slug]
	,[Url]
	,[CategoryId]
) values
(
	 @Title
	,@ImagePath
	,@Priority
	,@StartDate
	,@EndDate
	,@IsMobile
	,@Content
	,@Type
	,@Slug
	,@Url
	,@CategoryId
)
declare @BannerId int = SCOPE_IDENTITY()

if exists (select BannerId from Banner where Slug = @Slug and BannerId <> @BannerId)
	update Banner set Slug = @Slug + '-' + CONVERT(varchar(8), @BannerId) where BannerId = @BannerId

select @BannerId
go

ALTER proc [dbo].[BannerUpdate]
	 @BannerId int
	,@Title nvarchar(256)
	,@ImagePath nvarchar(256)
	,@Priority tinyint
	,@StartDate datetime
	,@EndDate datetime
	,@IsMobile bit
	,@Content nvarchar(max)
	,@Type int
	,@Slug varchar(256)
	,@Url nvarchar(256)
	,@CategoryId int
as
if exists (select BannerId from Banner where Slug = @Slug and BannerId <> @BannerId)
	set @Slug = @Slug + '-' + CONVERT(varchar(8), @BannerId)

update Banner
set [Title] = @Title,
	[ImagePath] = @ImagePath,
	[Priority] = @Priority,
	[StartDate] = @StartDate,
	[EndDate] = @EndDate,
	[IsMobile] = @IsMobile,
	[Content] = @Content,
	[Type] = @Type,
	[Slug] = @Slug,
	[Url] = @Url,
	[CategoryId] = @CategoryId
where [BannerId] = @BannerId
go

------------Create------------------

create proc BannerGetByCategoryId
	@CategoryId int,
	@IsMobile bit
as
select Slug, BannerId
from Banner 
where (CategoryId = @CategoryId or CategoryId in (select ParentId from Category where CategoryId = @CategoryId))
	and [Priority] = 1
	and GETDATE() between StartDate and EndDate
	and IsMobile = @IsMobile
go

create table Register	
(
	 [RegisterId] int identity(1, 1) primary key
	,[Gender] bit
	,[FullName] nvarchar(256)
	,[PhoneNumber] varchar(16)
	,[Email] nvarchar(128)
	,[Content] nvarchar(max)
	,[CreatedDate] datetime
	,[Status] int
)
alter table Register add default(GETDATE()) for [CreatedDate]
alter table Register add default(1) for [Status]
go

create proc RegisterInsert
	 @Gender bit
	,@FullName nvarchar(256)
	,@PhoneNumber varchar(16)
	,@Email nvarchar(128)
	,@Content nvarchar(max)
as
insert into Register
(
	 [Gender]
	,[FullName]
	,[PhoneNumber]
	,[Email]
	,[Content]
)
values
(
	 @Gender
	,@FullName
	,@PhoneNumber
	,@Email
	,@Content
)
select SCOPE_IDENTITY()
go

create proc RegisterUpdate
	 @RegisterId int
	,@Gender bit
	,@FullName nvarchar(256)
	,@PhoneNumber varchar(16)
	,@Email nvarchar(128)
	,@Content nvarchar(max)
as
update Register
set  [Gender] = @Gender
	,[FullName] = @FullName
	,[PhoneNumber] = @PhoneNumber
	,[Email] = @Email
	,[Content] = @Content
where [RegisterId] = @RegisterId
go

create proc RegisterUpdateStatus
	 @RegisterId int
	,@Status int
as
update Register set [Status] = @Status where RegisterId = @RegisterId
go

create proc RegisterListByStatus
	@Status int
as
select * from Register where [Status] = @Status
go