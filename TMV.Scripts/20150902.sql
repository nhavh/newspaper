alter table Banner add IsMobile bit
go

ALTER proc [dbo].[BannerInsert]
	 @Title nvarchar(256)
	,@ImagePath nvarchar(256)
	,@Priority tinyint
	,@StartDate datetime
	,@EndDate datetime
	,@IsMobile bit
as
insert into Banner
(
	 [Title]
	,[ImagePath]
	,[Priority]
	,[StartDate]
	,[EndDate]
	,[IsMobile]
) values
(
	 @Title
	,@ImagePath
	,@Priority
	,@StartDate
	,@EndDate
	,@IsMobile
)
select SCOPE_IDENTITY()
go

ALTER proc [dbo].[BannerUpdate]
	 @BannerId int
	,@Title nvarchar(256)
	,@ImagePath nvarchar(256)
	,@Priority tinyint
	,@StartDate datetime
	,@EndDate datetime
	,@IsMobile bit
as
update Banner
set [Title] = @Title,
	[ImagePath] = @ImagePath,
	[Priority] = @Priority,
	[StartDate] = @StartDate,
	[EndDate] = @EndDate,
	[IsMobile] = @IsMobile
where [BannerId] = @BannerId
go