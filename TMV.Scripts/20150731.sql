create proc PictureListByPaging
	 @Page int
	,@PageSize int
as
declare @Total int
select @Total = COUNT(PictureId)
from Picture
where GETDATE() between StartDate and EndDate

select * from (
	select ROW_NUMBER() over(order by EndDate desc, StartDate desc) RowOrder, *, @Total Total
	from Picture
	where GETDATE() between StartDate and EndDate
) as ABC
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go

create proc VideoListByPaging
	 @Page int
	,@PageSize int
as
declare @Total int
select @Total = COUNT(VideoId)
from Video
where GETDATE() between StartDate and EndDate

select * from (
	select ROW_NUMBER() over(order by EndDate desc, StartDate desc) RowOrder, *, @Total Total
	from Video
	where GETDATE() between StartDate and EndDate
) as ABC
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go

create proc PictureGetBySlug
	@Slug varchar(256)
as
select * 
from Picture
where Slug = @Slug
	and GETDATE() between StartDate and EndDate
go

create proc VideoGetBySlug
	@Slug varchar(256)
as
select * 
from Video
where Slug = @Slug
	and GETDATE() between StartDate and EndDate
go

ALTER proc [dbo].[VideoInsert]
	 @Url nvarchar(256)
	,@Title nvarchar(256)
	,@Description nvarchar(512)
	,@Tags nvarchar(512)
	,@SeoTitle nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@IsHome bit
	,@StartDate datetime
	,@EndDate datetime
	,@CreatedBy int
	,@Slug nvarchar(256)
as
insert into Video (
	 [Url]
	,[Title]
	,[Description]
	,[Tags]
	,[SeoTitle]
	,[SeoDescription]
	,[IsHome]
	,[StartDate]
	,[EndDate]
	,[CreatedBy]
	,[Slug]
) values (
	 @Url
	,@Title
	,@Description
	,@Tags
	,@SeoTitle
	,@SeoDescription
	,@IsHome
	,@StartDate
	,@EndDate
	,@CreatedBy
	,@Slug
)
declare @VideoId int = SCOPE_IDENTITY()

if exists (select VideoId from Video where Slug = @Slug and VideoId <> @VideoId)
	update Video set Slug = @Slug + '-' + CONVERT(varchar(8), @VideoId) where VideoId = @VideoId
go

ALTER proc [dbo].[PictureInsert]
	 @ImagePath nvarchar(256)
	,@Title nvarchar(256)
	,@Description nvarchar(512)
	,@Tags nvarchar(512)
	,@SeoTitle nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@IsHome bit
	,@StartDate datetime
	,@EndDate datetime
	,@CreatedBy int
	,@Slug varchar(256)
as
insert into Picture (
	 [ImagePath]
	,[Title]
	,[Description]
	,[Tags]
	,[SeoTitle]
	,[SeoDescription]
	,[IsHome]
	,[StartDate]
	,[EndDate]
	,[CreatedBy]
	,[Slug]
) values (
	 @ImagePath
	,@Title
	,@Description
	,@Tags
	,@SeoTitle
	,@SeoDescription
	,@IsHome
	,@StartDate
	,@EndDate
	,@CreatedBy
	,@Slug
)
declare @PictureId int = SCOPE_IDENTITY()

if exists (select PictureId from Picture where Slug = @Slug and PictureId <> @PictureId)
	update Picture set Slug = @Slug + '-' + CONVERT(varchar(8), @PictureId) where PictureId = @PictureId
go

create table Banner
(
	[BannerId] int identity(1, 1) primary key,
	[Title] nvarchar(256),
	[ImagePath] nvarchar(256),
	[Priority] tinyint,
	[StartDate] datetime,
	[EndDate] datetime,
	[CreatedDate] datetime
)
alter table Banner add default(getdate()) for [CreatedDate]
go

create proc BannerInsert
	 @Title nvarchar(256)
	,@ImagePath nvarchar(256)
	,@Priority tinyint
	,@StartDate datetime
	,@EndDate datetime
as
insert into Banner
(
	 [Title]
	,[ImagePath]
	,[Priority]
	,[StartDate]
	,[EndDate]
) values
(
	 @Title
	,@ImagePath
	,@Priority
	,@StartDate
	,@EndDate
)
select SCOPE_IDENTITY()
go

create proc BannerUpdate
	 @BannerId int
	,@Title nvarchar(256)
	,@ImagePath nvarchar(256)
	,@Priority tinyint
	,@StartDate datetime
	,@EndDate datetime
as
update Banner
set [Title] = @Title,
	[ImagePath] = @ImagePath,
	[Priority] = @Priority,
	[StartDate] = @StartDate,
	[EndDate] = @EndDate
where [BannerId] = @BannerId
go

create proc BannerDelete
	@BannerId int
as
delete from Banner where BannerId = @BannerId
go

create proc BannerGet
	@BannerId int
as
select * from Banner where BannerId = @BannerId
go

create proc BannerList
as
select * 
from Banner 
where [Priority] <> 1
order by EndDate desc, StartDate desc, BannerId desc
go

create proc BannerListPrice
as
select * 
from Banner
where [Priority] = 1 
order by EndDate desc, StartDate desc, BannerId desc
go

create proc BannerListByPriority
	@Priority tinyint
as
select * 
from Banner
where [Priority] = @Priority
	and GETDATE() between StartDate and EndDate
go