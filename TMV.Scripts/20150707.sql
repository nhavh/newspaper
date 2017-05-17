create table QuestionAnswer
(
	[QuestionAnswerId] int identity(1, 1) primary key,
	[ArticleId] int,
	[Title] nvarchar(256),
	[Content] nvarchar(max),
	[CreatedDate] datetime,
	[PublishDate] datetime,
	[RoleId] int,
	[AuthorId] int,
	[EditorId] int,
	[ApproverId] int,
	[AdminNote] nvarchar(512),
	[IsDeleted] bit
)
alter table QuestionAnswer add default(getdate()) for [CreatedDate]
alter table QuestionAnswer add default(0) for [IsDeleted]
go

create table CustomerReview
(
	[CustomerReviewId]  int identity(1, 1) primary key,
	[ArticleId] int,
	[Title] nvarchar(256),
	[Avatar] nvarchar(256),
	[Content] nvarchar(max),
	[CreatedDate] datetime,
	[PublishDate] datetime,
	[RoleId] int,
	[AuthorId] int,
	[EditorId] int,
	[ApproverId] int,
	[AdminNote] nvarchar(512),
	[IsDeleted] bit
)
alter table CustomerReview add default(getdate()) for CreatedDate
alter table CustomerReview add default(0) for [IsDeleted]
go

create proc QuestionAnswerInsert
	 @ArticleId int
	,@Title nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
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
)
go

create proc QuestionAnswerUpdate
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
as
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
where [QuestionAnswerId] = @QuestionAnswerId
go

create proc QuestionAnswerGet
	@QuestionAnswerId int
as
select * from QuestionAnswer where [QuestionAnswerId] = @QuestionAnswerId and [IsDeleted] = 0
go

create proc QuestionAnswerListByRole
	 @Status int
	,@UserId int
AS
If @Status=-1 /*Editting*/
	SELECT * 
	FROM QuestionAnswer
	WHERE EditorId=@UserId and RoleId is null 
	ORDER BY CreatedDate DESC
else if @Status=0 /*Online*/
	SELECT *
	FROM QuestionAnswer
	WHERE RoleId=0
	ORDER BY PublishDate DESC
else if @Status=1 /*WaittingApprove*/
	SELECT * 
	FROM QuestionAnswer
	WHERE EditorId is null and RoleId in (select RoleId from AdminUserRole where UserId = @UserId) and RoleId <> 0
	ORDER BY CreatedDate DESC
else if @Status=2 /*Approving*/
	SELECT * 
	FROM QuestionAnswer
	WHERE [EditorId] = @UserId and RoleId > 0 
	ORDER BY CreatedDate DESC
go

create proc QuestionAnswerDelete
	@QuestionAnswerId int
as
update QuestionAnswer set [IsDeleted] = 1 where QuestionAnswerId = @QuestionAnswerId
go

create proc QuestionAnswerListByArticle
	@ArticleId int
as
select * from QuestionAnswer where @ArticleId in (0, [ArticleId])  and [IsDeleted] = 0 and PublishDate <= GETDATE()
go

create proc CustomerReviewInsert
	 @ArticleId int
	,@Title nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
as
insert into CustomerReview (
	 [ArticleId]
	,[Title]
	,[Content]
	,[PublishDate]
	,[RoleId]
	,[AuthorId]
	,[EditorId]
	,[ApproverId]
	,[AdminNote]
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
)
go

create proc CustomerReviewUpdate
	 @CustomerReviewId int
	,@ArticleId int
	,@Title nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
as
update CustomerReview
set  [ArticleId] = @ArticleId
	,[Title] = @Title
	,[Content] = @Content
	,[PublishDate] = @PublishDate
	,[RoleId] = @RoleId
	,[AuthorId] = @AuthorId
	,[EditorId] = @EditorId
	,[ApproverId] = @ApproverId
	,[AdminNote] = @AdminNote
where [CustomerReviewId] = @CustomerReviewId
go

create proc CustomerReviewGet
	@CustomerReviewId int
as
select * from CustomerReview where [CustomerReviewId] = @CustomerReviewId and [IsDeleted] = 0
go

create proc CustomerReviewListByRole
	 @Status int
	,@UserId int
AS
If @Status=-1 /*Editting*/
	SELECT * 
	FROM CustomerReview
	WHERE EditorId=@UserId and RoleId is null 
	ORDER BY CreatedDate DESC
else if @Status=0 /*Online*/
	SELECT *
	FROM CustomerReview
	WHERE RoleId=0
	ORDER BY PublishDate DESC
else if @Status=1 /*WaittingApprove*/
	SELECT * 
	FROM CustomerReview
	WHERE EditorId is null and RoleId in (select RoleId from AdminUserRole where UserId = @UserId) and RoleId <> 0
	ORDER BY CreatedDate DESC
else if @Status=2 /*Approving*/
	SELECT * 
	FROM CustomerReview
	WHERE [EditorId] = @UserId and RoleId > 0 
	ORDER BY CreatedDate DESC
go

create proc CustomerReviewDelete
	@CustomerReviewId int
as
update CustomerReview set [IsDeleted] = 1 where CustomerReviewId = @CustomerReviewId
go

create proc CustomerReviewListByArticle
	@ArticleId int
as
select * from CustomerReview where @ArticleId in (0, [ArticleId])  and [IsDeleted] = 0 and PublishDate <= GETDATE()
go


create table Customer
(
	[CustomerId]  int identity(1, 1) primary key,
	[FullName] nvarchar(256),
	[Avatar] nvarchar(256),
	[Content] nvarchar(max),
	[CreatedDate] datetime,
	[PublishDate] datetime,
	[RoleId] int,
	[AuthorId] int,
	[EditorId] int,
	[ApproverId] int,
	[AdminNote] nvarchar(512),
	[IsDeleted] bit
)
alter table Customer add default(getdate()) for CreatedDate
alter table Customer add default(0) for [IsDeleted]
go

create proc CustomerInsert
	 @FullName nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
as
insert into Customer (
	 [FullName]
	,[Content]
	,[PublishDate]
	,[RoleId]
	,[AuthorId]
	,[EditorId]
	,[ApproverId]
	,[AdminNote]
) values (
	 @FullName
	,@Content
	,@PublishDate
	,@RoleId
	,@AuthorId
	,@EditorId
	,@ApproverId
	,@AdminNote
)
go

create proc CustomerUpdate
	 @CustomerId int
	,@FullName nvarchar(256)
	,@Content nvarchar(max)
	,@PublishDate datetime
	,@RoleId int
	,@AuthorId int
	,@EditorId int
	,@ApproverId int
	,@AdminNote nvarchar(512)
as
update Customer
set  [FullName] = @FullName
	,[Content] = @Content
	,[PublishDate] = @PublishDate
	,[RoleId] = @RoleId
	,[AuthorId] = @AuthorId
	,[EditorId] = @EditorId
	,[ApproverId] = @ApproverId
	,[AdminNote] = @AdminNote
where [CustomerId] = @CustomerId
go

create proc CustomerGet
	@CustomerId int
as
select * from Customer where [CustomerId] = @CustomerId and [IsDeleted] = 0
go

create proc CustomerList
as
select * from Customer where [IsDeleted] = 0
go

create proc CustomerDelete
	@CustomerId int
as
update Customer set [IsDeleted] = 1 where CustomerId = @CustomerId
go

create table Picture
(
	[PictureId] int identity(1, 1) primary key,
	[ImagePath] nvarchar(256),
	[CreatedDate] datetime
)
alter table Picture add default(getdate()) for [CreatedDate]
go

create proc PictureInsert
	@ImagePath nvarchar(256)
as
insert into Picture ([ImagePath]) values (@ImagePath)
go

create proc PictureUpdate
	 @PictureId int
	,@ImagePath nvarchar(256)
as
update Picture set [ImagePath] = @ImagePath where [PictureId] = @PictureId
go

create proc PictureDelete
	@PictureId int
as
delete from Picture where PictureId = @PictureId
go

create proc PictureList
as
select * from Picture
go

create proc PictureGet
	@PictureId int
as
select * from Picture where PictureId = @PictureId
go

create table Video
(
	[VideoId] int identity(1, 1) primary key,
	[Url] nvarchar(256),
	[Title] nvarchar(256),
	[CreatedDate] datetime
)
alter table Video add default(getdate()) for [CreatedDate]
go

create proc VideoInsert
	 @Url nvarchar(256)
	,@Title nvarchar(256)
as
insert into Video ([Url], [Title]) values (@Url, @Title)
go

create proc VideoUpdate
	 @VideoId int
	,@Url nvarchar(256)
	,@Title nvarchar(256)
as
update Video set [Url] = @Url, [Title] = @Title where [VideoId] = @VideoId
go

create proc VideoDelete
	@VideoId int
as
delete from Video where VideoId = @VideoId
go

create proc VideoList
as
select * from Video
go
