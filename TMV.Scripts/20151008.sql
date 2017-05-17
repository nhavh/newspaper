alter table Banner add Url nvarchar(256)
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
	[Url] = @Url
where [BannerId] = @BannerId
go
