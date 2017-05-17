alter table Picture add [Title] nvarchar(256)
alter table Picture add [Description] nvarchar(512)
alter table Picture add [Tags] nvarchar(512)
alter table Picture add [SeoTitle] nvarchar(256)
alter table Picture add [SeoDescription] nvarchar(512)
alter table Picture add [IsHome] bit
alter table Picture add [StartDate] datetime
alter table Picture add [EndDate] datetime
alter table Picture add [CreatedBy] int
alter table Picture add [UpdatedBy] int
alter table Picture add [UpdatedDate] datetime
alter table Picture add Slug varchar(256)
go

alter table Video add [Description] nvarchar(512)
alter table Video add [Tags] nvarchar(512)
alter table Video add [SeoTitle] nvarchar(256)
alter table Video add [SeoDescription] nvarchar(512)
alter table Video add [IsHome] bit
alter table Video add [StartDate] datetime
alter table Video add [EndDate] datetime
alter table Video add [CreatedBy] int
alter table Video add [UpdatedBy] int
alter table Video add [UpdatedDate] datetime
alter table Video add [Slug] varchar(256)
go

ALTER proc [dbo].[PictureInsert]
	 @ImagePath nvarchar(256)
	,@Title nvarchar(256)
	,@Description nvarchar(512)
	,@Tags nvarchar(512)
	,@SeoTitle nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@IsHome bit
	,@StartDate datetime
	,@EndDate datetime
	,@CreatedBy int
	,@Slug varchar(256)
as
insert into Picture (
	 [ImagePath]
	,[Title]
	,[Description]
	,[Tags]
	,[SeoTitle]
	,[SeoDescription]
	,[IsHome]
	,[StartDate]
	,[EndDate]
	,[CreatedBy]
	,[Slug]
) values (
	 @ImagePath
	,@Title
	,@Description
	,@Tags
	,@SeoTitle
	,@SeoDescription
	,@IsHome
	,@StartDate
	,@EndDate
	,@CreatedBy
	,@Slug
)
go

ALTER proc [dbo].[PictureUpdate]
	 @PictureId int
	,@ImagePath nvarchar(256)
	,@Title nvarchar(256)
	,@Description nvarchar(512)
	,@Tags nvarchar(512)
	,@SeoTitle nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@IsHome bit
	,@StartDate datetime
	,@EndDate datetime
	,@CreatedBy int
	,@UpdatedBy int
	,@Slug varchar(256)
as
update Picture 
set  [ImagePath] = @ImagePath
	,[Title] = @Title
	,[Description] = @Description
	,[Tags] = @Tags
	,[SeoTitle] = @SeoTitle
	,[SeoDescription] = @SeoDescription
	,[IsHome] = @IsHome
	,[StartDate] = @StartDate
	,[EndDate] = @EndDate
	,[CreatedBy] = @CreatedBy
	,[UpdatedBy] = @UpdatedBy
	,[UpdatedDate] = GETDATE()
	,[Slug] = @Slug
where [PictureId] = @PictureId
go

ALTER proc [dbo].[PictureDelete]
	@PictureId int
as
delete from Picture where PictureId = @PictureId
go

ALTER proc [dbo].[PictureList]
as
select * from Picture order by PictureId desc
go

ALTER proc [dbo].[VideoInsert]
	 @Url nvarchar(256)
	,@Title nvarchar(256)
	,@Description nvarchar(512)
	,@Tags nvarchar(512)
	,@SeoTitle nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@IsHome bit
	,@StartDate datetime
	,@EndDate datetime
	,@CreatedBy int
	,@Slug nvarchar(256)
as
insert into Video (
	 [Url]
	,[Title]
	,[Description]
	,[Tags]
	,[SeoTitle]
	,[SeoDescription]
	,[IsHome]
	,[StartDate]
	,[EndDate]
	,[CreatedBy]
	,[Slug]
) values (
	 @Url
	,@Title
	,@Description
	,@Tags
	,@SeoTitle
	,@SeoDescription
	,@IsHome
	,@StartDate
	,@EndDate
	,@CreatedBy
	,@Slug
)
go

ALTER proc [dbo].[VideoUpdate]
	 @VideoId int
	,@Url nvarchar(256)
	,@Title nvarchar(256)
	,@Description nvarchar(512)
	,@Tags nvarchar(512)
	,@SeoTitle nvarchar(256)
	,@SeoDescription nvarchar(512)
	,@IsHome bit
	,@StartDate datetime
	,@EndDate datetime
	,@CreatedBy int
	,@UpdatedBy int
	,@Slug varchar(256)
as
update Video 
set  [Url] = @Url
	,[Title] = @Title
	,[Description] = @Description
	,[Tags] = @Tags
	,[SeoTitle] = @SeoTitle
	,[SeoDescription] = @SeoDescription
	,[IsHome] = @IsHome
	,[StartDate] = @StartDate
	,[EndDate] = @EndDate
	,[CreatedBy] = @CreatedBy
	,[UpdatedBy] = @UpdatedBy
	,[UpdatedDate] = GETDATE()
	,[Slug] = @Slug
where [VideoId] = @VideoId
go