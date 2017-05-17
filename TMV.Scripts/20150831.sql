create table HyperLink
(
	[HyperLinkId] int identity(1, 1) primary key,
	[OldChar] nvarchar(256),
	[NewChar] nvarchar(256),
	[CreatedDate] datetime
)
alter table HyperLink add default(getdate()) for [CreatedDate]
go

create proc HyperLinkInsert
	@OldChar nvarchar(256),
	@NewChar nvarchar(256)
as
insert into HyperLink (OldChar, NewChar)
values (@OldChar, @NewChar)
go

create proc HyperLinkUpdate
	@HyperLinkId int,
	@OldChar nvarchar(256),
	@NewChar nvarchar(256)
as
update HyperLink 
set OldChar = @OldChar,
	NewChar = @NewChar
where HyperLinkId = @HyperLinkId
go

create proc HyperLinkDelete
	@HyperLinkId int
as
delete from HyperLink where HyperLinkId = @HyperLinkId
go

create proc HyperLinkList
as
select * from HyperLink order by HyperLinkId desc
go