Use DVDLibrary;
GO

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='SelectAllDVDS')
Drop Procedure SelectAllDVDS
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='SelectByDVDID')
Drop Procedure SelectByDVDID
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='GetDVDsByYear')
Drop Procedure GetDVDsByYear
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='GetDVDsbyDirector')
Drop Procedure GetDVDsbyDirector
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='GetDVDsByTitle')
Drop Procedure GetDVDsByTitle
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='InsertDVD')
Drop Procedure InsertDVD
Go
if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='addnote')
Drop Procedure addnote
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='UpdateDVD')
Drop Procedure UpdateDVD
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='DeleteDVD')
Drop Procedure DeleteDVD
Go




select * from DVD
go

Create Procedure SelectAllDVDS as
begin
select DVDID, Title, realeaseyear,DVD.RatingID, Director, RatingName from DVD
left join Ratings r on r.RatingID=dvd.RatingID
end
GO

Create Procedure  SelectByDVDID(@DVDID int) AS
begin
select DVD.DVDID, Title, realeaseYear, Director, RatingName, DVD.RatingID, Note from DVD
left join Ratings r on r.RatingID=DVD.RatingID
left join Notes n on n.DVDID=dvd.DVDID
where dvd.DVDID= @DVDID
end
GO

Create Procedure GetDVDsByYear(@realeaseyear int) as
begin
select d.DVDID, Title, realeaseYear, Director,d.RatingID, RatingName, Note from DVD d
left join Ratings r on r.RatingID=d.RatingID
left join Notes n on n.DVDID=d.DVDID
where @realeaseyear=d.realeaseYear
end
GO

Create Procedure GetDVDsbyDirector (@Director nvarchar(50)) As
begin
select d.DVDID, Title, realeaseYear, Director,d.RatingID, RatingName, Note from DVD d
left join Ratings r on r.RatingID=d.RatingID
left join Notes n on n.DVDID=d.DVDID
where d.Director like concat('%',@Director,'%')  
end
GO

Create Procedure GetDVDsByTitle (@title nvarchar(50)) as
begin
select d.DVDID, Title, realeaseYear, Director,d.RatingID, RatingName, Note from DVD d
left join Ratings r on r.RatingID=d.RatingID
left join Notes n on n.DVDID=d.DVDID
where d.Title Like concat('%',@title,'%')
end
GO

Create Procedure InsertDVD (@DVDID int output, @title nvarchar(50), @Director nvarchar(50), @RatingID int, @realeaseyear int) as
begin
Insert into DVD (Title, Director, RatingID, realeaseYear) Values (@title, @Director, @RatingID, @realeaseyear);
set @DVDID=SCOPE_IDENTITY()
end
Go

create procedure addnote (@DVDID int, @note nvarchar(500)) As
begin
insert into notes (DVDID, Note) Values (@DVDID,@note)
End
 GO
 
 Create Procedure UpdateDVD (@DVDID int,  @title nvarchar(50), @Director nvarchar(50), @RatingID int, @realeaseyear int, @Note nvarchar(500)) as
 Begin
 Update DVD SET
 Title=@title,
 Director=@Director,
 RatingID=@RatingID,
 realeaseYear=@realeaseyear
 Where DVDID=@DVDID

 Update Notes Set
 Note=@Note
 Where 
 Notes.DVDID=@DVDID
 End
 GO

Create Procedure DeleteDVD (@DVDID int) AS
begin 
begin transaction 
Delete from Notes where Notes.DVDID=@DVDID
Delete from DVD Where DVD.DVDID=@DVDID
Commit Transaction
END
GO

 select* from DVD
 select* from Ratings
 select * from Notes