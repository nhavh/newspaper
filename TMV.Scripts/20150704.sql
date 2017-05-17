alter table Category add Avatar nvarchar(256)
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
	@Avatar nvarchar(256)
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
WHERE [Category].[CategoryId] = @CategoryId
go

alter table Article add Intro nvarchar(max)
alter table Article add Advantage nvarchar(max)
alter table Article add Process nvarchar(max)
alter table Article add QA nvarchar(max)
alter table Article add FrontBack nvarchar(max)
alter table Article add Video nvarchar(max)
alter table Article add Review nvarchar(max)
alter table Article add Result nvarchar(max)
go

ALTER PROCEDURE [dbo].[ArticleInsert]
	 @CategoryId int
	,@Title nvarchar(128)
	,@Slug varchar(128)
	,@Avatar nvarchar(256)
	,@Description nvarchar(512)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
	,@Tags nvarchar(256)
	,@SeoTitle nvarchar(128)
	,@SeoDescription nvarchar(512)
	,@Intro nvarchar(max)
	,@Advantage nvarchar(max)
	,@Process nvarchar(max)
	,@QA nvarchar(max)
	,@FrontBack nvarchar(max)
	,@Video nvarchar(max)
	,@Review nvarchar(max)
	,@Result nvarchar(max)
AS

INSERT INTO Article (
	 [CategoryId]
	,[Title]
	,[Slug]
	,[Avatar]
	,[Description]
	,[Content]
	,[PublishDate]
	,[RoleId]
	,[AuthorId]
	,[EditorId]
	,[ApproverId]
	,[AdminNote]
	,[Tags]
	,[SeoTitle]
	,[SeoDescription]
	,[Intro]
	,[Advantage]
	,[Process]
	,[QA]
	,[FrontBack]
	,[Video]
	,[Review]
	,[Result]
) VALUES (
	 @CategoryId
	,@Title
	,@Slug
	,@Avatar
	,@Description
	,@Content
	,@PublishDate
	,@RoleId
	,@AuthorId
	,@EditorId
	,@ApproverId
	,@AdminNote
	,@Tags
	,@SeoTitle
	,@SeoDescription
	,@Intro
	,@Advantage
	,@Process
	,@QA
	,@FrontBack
	,@Video
	,@Review
	,@Result
)

declare @ArticleId int = SCOPE_IDENTITY()
if exists (select ArticleId from Article where Slug = @Slug and ArticleId <> @ArticleId)
	update Article set Slug = @Slug + '-' + CONVERT(varchar(8), @ArticleId) where ArticleId = @ArticleId
go

ALTER PROCEDURE [dbo].[ArticleUpdate]
	@ArticleId int, 
	@CategoryId int, 
	@Title nvarchar(128), 
	@Slug varchar(128), 
	@Avatar nvarchar(256), 
	@Description nvarchar(512), 
	@Content nvarchar(max), 
	@PublishDate datetime, 
	@RoleId int, 
	@AuthorId int, 
	@EditorId int, 
	@ApproverId int, 
	@AdminNote nvarchar(512), 
	@Tags nvarchar(256), 
	@SeoTitle nvarchar(128), 
	@SeoDescription nvarchar(512),
	@Intro nvarchar(max),
	@Advantage nvarchar(max),
	@Process nvarchar(max),
	@QA nvarchar(max),
	@FrontBack nvarchar(max),
	@Video nvarchar(max),
	@Review nvarchar(max),
	@Result nvarchar(max)
AS

if exists (select ArticleId from Article where Slug = @Slug and ArticleId <> @ArticleId)
	set @Slug = @Slug + '-' + CONVERT(varchar(8), @ArticleId)

UPDATE Article SET
	   [CategoryId] = @CategoryId
	   ,[Title] = @Title
	   ,[Slug] = @Slug
	   ,[Avatar] = @Avatar
	   ,[Description] = @Description
	   ,[Content] = @Content
	   ,[PublishDate] = @PublishDate
	   ,[RoleId] = @RoleId
	   ,[AuthorId] = @AuthorId
	   ,[EditorId] = @EditorId
	   ,[ApproverId] = @ApproverId
	   ,[AdminNote] = @AdminNote
	   ,[Tags] = @Tags
	   ,[SeoTitle] = @SeoTitle
	   ,[SeoDescription] = @SeoDescription
	   ,[Intro] = @Intro
	   ,[Advantage] = @Advantage
	   ,[Process] = @Process
	   ,[QA] = @QA
	   ,[FrontBack] = @FrontBack
	   ,[Video] = @Video
	   ,[Review] = @Review
	   ,[Result] = @Result
WHERE [Article].[ArticleId] = @ArticleId
go