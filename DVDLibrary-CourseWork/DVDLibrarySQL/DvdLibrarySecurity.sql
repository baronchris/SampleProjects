Use master;
Go

Create Login DVDLibraryApp with Password= 'testing1234'
GO

Create user DVDLibraryAPP for Login DVDLibraryApp
GO

 use DVDLibrary;
 Go

 sp_adduser 'DVDLibraryApp', 'DVDLibraryApp','db_datareader'
  exec sp_addrolemember N'db_datawriter',N'DVDLibraryApp'
  GO

 use DVDLibrary;
 Go

Create Role db_executor
Go

grant execute to db_executor
GO

alter role db_executor ADD member DVDLibraryApp 
GO  -- for apo, may want to restict to just this