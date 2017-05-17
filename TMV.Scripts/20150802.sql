alter proc [dbo].[ArticleListByGroup]
	 @CategoryId int
	,@Group int
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
	union all
	select ArticleId
	from Article
		inner join Category on Category.CategoryId = Article.CategoryId
	where Category.[Group] = @Group
		and PublishDate <= GETDATE()
		and RoleId = 0
) as AB

select *, @Total Total from 
(
	select ROW_NUMBER() over(order by PublishDate desc) RowOrder, * from
	(
		select Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate, 
			Category.CategoryName, Category.Slug CategorySlug, IsShowDetail
		from Sponsor
			inner join Article on Article.ArticleId = Sponsor.ItemId
			inner join Category on Category.CategoryId = Article.CategoryId
		where Sponsor.ItemType = @CategoryId
		union all
		select Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate, 
			Category.CategoryName, Category.Slug CategorySlug, IsShowDetail
		from Article
			inner join Category on Category.CategoryId = Article.CategoryId
		where Category.[Group] = @Group
			and PublishDate <= GETDATE()
			and RoleId = 0
	) as ABC
) as BCD
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go

alter proc [dbo].[ArticleListByCategory]
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
	union all
	select ArticleId
	from Article
	where (@CategoryId = 0 or Article.CategoryId = @CategoryId or Article.CategoryId in (select CategoryId from Category where ParentId = @CategoryId))
		and PublishDate <= GETDATE()
		and RoleId = 0
) as AB

select *, @Total Total from 
(
	select ROW_NUMBER() over(order by PublishDate desc) RowOrder, * from
	(
		select Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate, 
			Category.CategoryName, Category.Slug CategorySlug, IsShowDetail
		from Sponsor
			inner join Article on Article.ArticleId = Sponsor.ItemId
			inner join Category on Category.CategoryId = Article.CategoryId
		where Sponsor.ItemType = @CategoryId
		union all
		select Article.Title, Article.Slug, Article.Avatar, Article.[Description], Article.PublishDate, 
			Category.CategoryName, Category.Slug CategorySlug, IsShowDetail
		from Article
			inner join Category on Category.CategoryId = Article.CategoryId
		where (@CategoryId = 0 or Article.CategoryId = @CategoryId or Article.CategoryId in (select CategoryId from Category where ParentId = @CategoryId))
			and PublishDate <= GETDATE()
			and RoleId = 0
	) as ABC
) as BCD
where @PageSize = -1 or (@Page - 1) * @PageSize < RowOrder and RowOrder <= @Page * @PageSize 
go