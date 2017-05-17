alter table Article add IsTab bit
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
	,@IsTab bit
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
	,[IsTab]
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
	,@IsTab
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
	@Result nvarchar(max),
	@IsTab bit
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
	   ,[IsTab] = @IsTab
WHERE [Article].[ArticleId] = @ArticleId
go

ALTER proc [dbo].[CustomerReviewList]
	@ArticleId int
as
select CustomerReviewId, Title, AuthorId, EditorId, CreatedDate, PublishDate 
from CustomerReview 
where @ArticleId in (0, [ArticleId]) and [IsDeleted] = 0 
order by CustomerReviewId desc
go

ALTER proc [dbo].[QuestionAnswerList]
	@ArticleId int
as
select QuestionAnswerId, Title, AuthorId, EditorId, CreatedDate, PublishDate  
from QuestionAnswer 
where @ArticleId in (0, [ArticleId])  and [IsDeleted] = 0 
order by QuestionAnswerId desc
go