USE DVDLibrary;
GO


if exists (select* from sys.tables where name='Notes')
drop Table Notes
GO

if exists (select* from sys.tables where name='DVD')
drop Table DVD
GO

if exists (select* from sys.tables where name='Directors')
drop Table Directors
GO

if exists (select* from sys.tables where name='Ratings')
drop Table Ratings
GO





/*
Create Table Directors 
	(
		DirectorID int identity(1,1) primary key not null,
		DirectorName nvarchar(50) not null
	)

GO*/

Create Table Ratings 
	(
		RatingID int identity(1,1) primary key not null,
		RatingName nvarchar(8)
	)
	Go

Create Table DVD (
	DVDID int identity(1,1) primary key not null,
	Title nvarchar(50) not null,
	--DirectorID int Foreign key references Directors(DirectorID) not null,
	Director nvarchar(50) not null,
	RatingID int Foreign key references Ratings(RatingID) not null,
	realeaseYear int not null
	)
GO

Create Table Notes 
	(
		DVDID int foreign key references DVD(DVDID) not null,
		NoteID int identity(1,1) primary key not null,
		Note nvarchar(500)
	)

GO





