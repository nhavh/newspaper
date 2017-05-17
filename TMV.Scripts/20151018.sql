alter table Category add IsShowMenuTop bit
alter table Category add IsShowMenuBot bit
alter table Category add OrderBy int
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
	,@IsShowMenuTop bit
	,@IsShowMenuBot bit
	,@OrderBy int
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
	,[IsShowMenuTop]
	,[IsShowMenuBot]
	,[OrderBy]
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
	,@IsShowMenuTop
	,@IsShowMenuBot
	,@OrderBy
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
	@IsShowDetail bit,
	@IsShowMenuTop bit,
	@IsShowMenuBot bit,
	@OrderBy int
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
	   ,[IsShowMenuTop] = @IsShowMenuTop
	   ,[IsShowMenuBot] = @IsShowMenuBot
	   ,[OrderBy] = @OrderBy
WHERE [Category].[CategoryId] = @CategoryId
go

create proc CategoryListTop
as
select * from
(
	select * from Category where IsShowMenuTop = 1
	union
	select * 
	from Category
	where [Group] in (select CategoryId from Category where IsShowMenuTop = 1)
) as ABC
order by OrderBy
go

create proc CategoryListBot
as
select * from
(
	select * from Category where IsShowMenuBot = 1
	union
	select * 
	from Category
	where [Group] in (select CategoryId from Category where IsShowMenuBot = 1)
) as ABC
order by OrderBy
go