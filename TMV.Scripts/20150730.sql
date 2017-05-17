create table Rating
(
	 [RatingId] int identity(1, 1) primary key
	,[UserId] int
	,[ItemId] int
	,[ItemType] int
	,[Price] float
	,[Quality] float
	,[Doctor] float
	,[Attitude] float
	,[Facility] float
	,[Customer] float
	,[CreatedDate] datetime
)
alter table Rating add default(getdate()) for [CreatedDate]
go

alter table Article add AveragePrice float
alter table Article add AverageQuality float
alter table Article add AverageDoctor float
alter table Article add AverageAttitude float
alter table Article add AverageFacility float
alter table Article add AverageCustomer float
alter table Article add TotalRating float
go

create proc RatingInsert
	 @UserId int
	,@ItemId int
	,@ItemType tinyint
	,@Price float
	,@Quality float
	,@Doctor float
	,@Attitude float
	,@Facility float
	,@Customer float
AS

INSERT INTO Rating (
	 [UserId]
	,[ItemId]
	,[ItemType]
	,[Price]
	,[Quality]
	,[Doctor]
	,[Attitude]
	,[Facility]
	,[Customer]
) VALUES (
	 @UserId
	,@ItemId
	,@ItemType
	,@Price
	,@Quality
	,@Doctor
	,@Attitude
	,@Facility
	,@Customer
)

declare @AveragePrice float
declare @AverageQuality float
declare @AverageDoctor float
declare @AverageAttitude float
declare @AverageFacility float
declare @AverageCustomer float
declare @AverageRating float
declare @TotalVote int

set @AveragePrice = (select ROUND(CAST(SUM(Price)/COUNT(Price) as float), 1) from Rating where ItemId = @ItemId and ItemType = @ItemType)
set @AverageQuality = (select ROUND(CAST(SUM(Quality)/COUNT(Quality) as float), 1) from Rating where ItemId = @ItemId and ItemType = @ItemType)
set @AverageDoctor = (select ROUND(CAST(SUM(Doctor)/COUNT(Doctor) as float), 1) from Rating where ItemId = @ItemId and ItemType = @ItemType)
set @AverageAttitude = (select ROUND(CAST(SUM(Attitude)/COUNT(Attitude) as float), 1) from Rating where ItemId = @ItemId and ItemType = @ItemType)
set @AverageFacility = (select ROUND(CAST(SUM(Facility)/COUNT(Facility) as float), 1) from Rating where ItemId = @ItemId and ItemType = @ItemType)
set @AverageCustomer = (select ROUND(CAST(SUM(Customer)/COUNT(Customer) as float), 1) from Rating where ItemId = @ItemId and ItemType = @ItemType)

set @AverageRating = ROUND(CAST((@AveragePrice + @AverageQuality + @AverageDoctor + @AverageAttitude + @AverageFacility + @AverageCustomer) / 6 as float), 1)
set @TotalVote = (select COUNT(*) from Rating where ItemId = @ItemId and ItemType = @ItemType)

if (@ItemType = 1)
begin
	update Article 
	set AveragePrice = @AveragePrice
	   ,AverageQuality = @AverageQuality
	   ,AverageDoctor = @AverageDoctor
	   ,AverageAttitude = @AverageAttitude
	   ,AverageFacility = @AverageFacility
	   ,AverageCustomer = @AverageCustomer
	   ,TotalRating = @AverageRating
	   ,TotalVote = @TotalVote
	where ArticleId = @ItemId	
	
	select  @AveragePrice AveragePrice, 
			@AverageQuality AverageQuality, 
			@AverageDoctor AverageDoctor, 
			@AverageAttitude AverageAttitude, 
			@AverageFacility AverageFacility, 
			@AverageCustomer AverageCustomer, 
			@AverageRating AverageRating, 
			@TotalVote TotalVote
end
else if (@ItemType = 2)
begin
	update Article 
	set TotalRating = @AverageRating
	   ,TotalVote = @TotalVote
	where Article.ArticleId = @ItemId	
	
	select  @AverageRating AverageRating, 
			@TotalVote TotalVote
end
go
create proc ArticleGetByCategorySlug
	@CategorySlug varchar(256)
as
select Article.*, Category.CategoryName, Category.Slug CategorySlug
from Article
	inner join Category on Category.CategoryId = Article.CategoryId
where Category.Slug = @CategorySlug 
	and RoleId = 0 
	and GETDATE() >= PublishDate
	and Category.IsShowDetail = 1
go

ALTER proc [dbo].[ArticleGetBySlug]
	@Slug varchar(128)
as
select Article.*, Category.CategoryName, Category.Slug CategorySlug
from Article
	inner join Category on Category.CategoryId = Article.CategoryId
where Article.Slug = @Slug 
	and RoleId = 0 
	and GETDATE() >= PublishDate
	and Category.IsShowDetail = 0
go

alter table Category add IsShowDetail bit
go
ALTER PROCEDURE [dbo].[CategoryInsert]
	 @ParentId int
	,@CategoryName nvarchar(128)
	,@Slug varchar(128)
	,@SeoH1 nvarchar(128)
	,@SeoTitle nvarchar(256)
	,@SeoKeyword nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@Group int
	,@Avatar nvarchar(256)
	,@IsShowDetail bit
AS
INSERT INTO Category (
	 [ParentId]
	,[CategoryName]
	,[Slug]
	,[SeoH1]
	,[SeoTitle]
	,[SeoKeyword]
	,[SeoDescription]
	,[Group]
	,[Avatar]	
	,[IsShowDetail]
) VALUES (
	 @ParentId
	,@CategoryName
	,@Slug
	,@SeoH1
	,@SeoTitle
	,@SeoKeyword
	,@SeoDescription
	,@Group
	,@Avatar
	,@IsShowDetail
)

select SCOPE_IDENTITY()
go
ALTER PROCEDURE [dbo].[CategoryUpdate]
	@CategoryId int, 
	@ParentId int, 
	@CategoryName nvarchar(128), 
	@Slug varchar(128), 
	@SeoH1 nvarchar(128), 
	@SeoTitle nvarchar(256), 
	@SeoKeyword nvarchar(256), 
	@SeoDescription nvarchar(512),
	@Group int,
	@Avatar nvarchar(256),
	@IsShowDetail bit
AS
UPDATE Category SET
	    [ParentId] = @ParentId
	   ,[CategoryName] = @CategoryName
	   ,[Slug] = @Slug
	   ,[SeoH1] = @SeoH1
	   ,[SeoTitle] = @SeoTitle
	   ,[SeoKeyword] = @SeoKeyword
	   ,[SeoDescription] = @SeoDescription
	   ,[Group] = @Group
	   ,[Avatar] = @Avatar
	   ,[IsShowDetail] = @IsShowDetail
WHERE [Category].[CategoryId] = @CategoryId
go