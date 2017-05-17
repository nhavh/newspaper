create proc SponsorListByHome
as
select Article.Title, Article.[Description], Article.Slug, Article.Avatar, Category.Slug CategorySlug
from Sponsor
	inner join Article on Sponsor.ItemId = Article.ArticleId
	inner join Category on Category.CategoryId = Article.CategoryId
where GETDATE() between StartDate and EndDate
go

create proc VideoGet
	@VideoId int
as
select * from Video where VideoId = @VideoId
go

create proc VideoListByHome
as
select top 1 * 
from Video 
where IsHome = 1 and GETDATE() between StartDate and EndDate
order by VideoId desc
go

create proc PictureListByHome
as
select * 
from Picture 
where IsHome = 1 and GETDATE() between StartDate and EndDate
order by PictureId  desc
go

create proc CustomerListByHome
as
select *
from Customer
where IsDeleted = 0 and RoleId = 0 and PublishDate <= GETDATE()
order by CustomerId desc
go

ALTER proc [dbo].[ArticleListByView]
as
select top 4 Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate,
	Category.CategoryName, Category.Slug CategorySlug
from Article
	inner join Category on Category.CategoryId = Article.CategoryId
where PublishDate <= GETDATE() and PublishDate between GETDATE() - 7 and GETDATE() and RoleId = 0
order by TotalView desc, PublishDate desc
go

ALTER proc [dbo].[QuestionAnswerListByArticle]
	 @ArticleId int
	,@Page int
	,@PageSize int
as
declare @Total int
select @Total = count(QuestionAnswerId)
from QuestionAnswer
where @ArticleId in (0, [ArticleId]) and [IsDeleted] = 0 and PublishDate <= GETDATE()

select *, @Total Total from (
	select ROW_NUMBER() over(order by PublishDate desc) RowOrder, *
	from QuestionAnswer 
	where @ArticleId in (0, [ArticleId]) and [IsDeleted] = 0 and PublishDate <= GETDATE()
) as ABC
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go

ALTER proc [dbo].[CustomerReviewListByArticle]
	 @ArticleId int
	,@Page int
	,@PageSize int
as
declare @Total int
select @Total = count(CustomerReviewId)
from CustomerReview
where @ArticleId in (0, [ArticleId]) and [IsDeleted] = 0 and PublishDate <= GETDATE()

select *, @Total Total from (
	select ROW_NUMBER() over(order by PublishDate desc) RowOrder, *
	from CustomerReview 
	where @ArticleId in (0, [ArticleId]) and [IsDeleted] = 0 and PublishDate <= GETDATE()
) as ABC
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go