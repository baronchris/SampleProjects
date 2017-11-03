if EXISTS(select * from sys.databases where name ='DVDLibrary')
drop database DVDLibrary
GO

Create Database DVDLibrary
GO

use DVDLibrary
GO