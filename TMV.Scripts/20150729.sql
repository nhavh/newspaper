ALTER proc [dbo].[ArticleListByCategory]
	 @CategoryId int
	,@Page int
	,@PageSize int
as
declare @Total int
select @Total = count(*) from
(
	select Article.ArticleId
	from Sponsor
		inner join Article on Article.ArticleId = Sponsor.ItemId
	where Sponsor.ItemType = @CategoryId
		and GETDATE() between StartDate and EndDate
	union all
	select ArticleId
	from Article
	where @CategoryId in (0, Article.CategoryId)
		and PublishDate <= GETDATE()
		and RoleId = 0
) as AB

select *, @Total Total from 
(
	select ROW_NUMBER() over(order by PublishDate desc) RowOrder, * from
	(
		select Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate, 
			Category.CategoryName, Category.Slug CategorySlug 
		from Sponsor
			inner join Article on Article.ArticleId = Sponsor.ItemId
			inner join Category on Category.CategoryId = Article.CategoryId
		where Sponsor.ItemType = @CategoryId
			and GETDATE() between StartDate and EndDate
		union all
		select Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate, 
			Category.CategoryName, Category.Slug CategorySlug
		from Article
			inner join Category on Category.CategoryId = Article.CategoryId
		where @CategoryId in (0, Article.CategoryId)
			and PublishDate <= GETDATE()
			and RoleId = 0
	) as ABC
) as BCD
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go

ALTER proc [dbo].[SponsorList]
as
select [Sponsor].*, Title
from [Sponsor]
	left join Article on Article.ArticleId = Sponsor.ItemId
order by EndDate desc, StartDate desc, Article.Title
go

create proc SponsorListByType
	@CategoryId int
as
select Article.Title, Article.[Description], Article.Slug, Article.Avatar, Category.Slug CategorySlug
from Sponsor
	inner join Article on Sponsor.ItemId = Article.ArticleId
	inner join Category on Category.CategoryId = Article.CategoryId
where Sponsor.ItemType = @CategoryId
	and GETDATE() between StartDate and EndDate
go