alter table QuestionAnswer add Question nvarchar(max)
alter table QuestionAnswer add Slug varchar(256)
go

ALTER proc [dbo].[QuestionAnswerInsert]
	 @ArticleId int
	,@Title nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
	,@Question nvarchar(max)
	,@Slug varchar(256)
as
insert into QuestionAnswer (
	 [ArticleId]
	,[Title]
	,[Content]
	,[PublishDate]
	,[RoleId]
	,[AuthorId]
	,[EditorId]
	,[ApproverId]
	,[AdminNote]
	,[Question]
	,[Slug]
) values (
	 @ArticleId
	,@Title
	,@Content
	,@PublishDate
	,@RoleId
	,@AuthorId
	,@EditorId
	,@ApproverId
	,@AdminNote
	,@Question
	,@Slug
)
declare @QuestionAnswerId int = SCOPE_IDENTITY()

if exists (select QuestionAnswerId from QuestionAnswer where Slug = @Slug and QuestionAnswerId <> @QuestionAnswerId)
	update QuestionAnswer set Slug = @Slug + '-' + CONVERT(varchar(8), @QuestionAnswerId) where QuestionAnswerId = @QuestionAnswerId
go

ALTER proc [dbo].[QuestionAnswerUpdate]
	 @QuestionAnswerId int
	,@ArticleId int
	,@Title nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
	,@Question nvarchar(max)
	,@Slug varchar(256)
as
if exists (select QuestionAnswerId from QuestionAnswer where Slug = @Slug and QuestionAnswerId <> @QuestionAnswerId)
	set @Slug = @Slug + '-' + CONVERT(varchar(8), @QuestionAnswerId)

update QuestionAnswer
set  [ArticleId] = @ArticleId
	,[Title] = @Title
	,[Content] = @Content
	,[PublishDate] = @PublishDate
	,[RoleId] = @RoleId
	,[AuthorId] = @AuthorId
	,[EditorId] = @EditorId
	,[ApproverId] = @ApproverId
	,[AdminNote] = @AdminNote
	,[Question] = @Question
	,[Slug] = @Slug
where [QuestionAnswerId] = @QuestionAnswerId
go

ALTER proc [dbo].[ArticleListByOther]
	 @ArticleId int
	,@CategoryID int
	,@PageSize int
	,@Page int
as
declare @Group int
select @Group = [Group] from Category where CategoryId = @CategoryID

select * from 
(
	select ROW_NUMBER() over(order by NEWID()) as RowOrder, Article.Title, Article.[Description], Article.Slug, 
		Category.Slug SlugCategory, Article.Avatar, IsShowDetail
	from Article
		inner join Category on Category.CategoryID = Article.CategoryId
	where Category.[Group] = @Group and Article.ArticleId != @ArticleId
) as ABC
where @PageSize = -1 or RowOrder > (@Page - 1) * @PageSize and RowOrder <= @Page * @PageSize
go

create proc QuestionAnswerGetBySlug
	@Slug varchar(256)
as
select * from QuestionAnswer where Slug = @Slug
go