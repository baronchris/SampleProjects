if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='DbReset')
Drop Procedure DbReset
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='ratingLoad')
Drop Procedure ratingLoad
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='DVDLOAD')
Drop Procedure DVDLOAD
Go

if exists( select * from INFORMATION_SCHEMA.ROUTINES where ROUTINE_NAME='NoteLoad')
Drop Procedure NoteLoad
Go



Create Procedure ratingLoad as
begin 
Set Identity_Insert Ratings On;
insert into Ratings (RatingID, RatingName) values (1,'G'),(2,'PG'),(3,'PG-13'),(4,'R'),(5,'NC-17')
Set Identity_Insert Ratings On;
end
go



Create Procedure DVDLOAD as
begin 
Set Identity_Insert DVD On;
Insert into DVD (DVDID, title, Director, RatingID, realeaseYear) values (1, 'A Super Tale','Sam Jones',2,2015),(2,'A Great Tale','Joe Smith',3,2012)
Set Identity_Insert DVD Off;
end
go

Create Procedure NoteLoad as
begin
Set Identity_Insert Notes On;
Insert into Notes (NoteID, DVDID, Note) Values(1,1,'A super tale with a longer note'),(2,2,'A great tale')
Set Identity_Insert Notes Off;
end



go


CREATE PROCEDURE DbReset AS
Begin
Delete from Notes;
Delete from DVD;
Delete from Ratings;
exec ratingLoad;
select * from Ratings;
exec DVDLOAD;
Exec NoteLoad;
select* from DVD d
left join Notes n on d.DVDID=n.DVDID;  
DBCC CHECKIDENT (DVD, reseed, 2);
DBCC CHECKIDENT (Notes, reseed, 2);
end

Go





--
select*from Ratings

exec DbReset