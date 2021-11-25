


create database Oriontek;
use Oriontek;

create table Clients(

ID int primary key IDENTITY(1,1),
Names varchar(100),
LastNames varchar(120),
Email varchar (120),
Phone varchar (17),
Birthday DateTime,
Description varchar(300)
)


create table Addresses(

ID int primary key IDENTITY(1,1),
ClientID int,
Address1 varchar(60),
Address2 varchar(60),
City varchar (120),
Providence varchar (50),
Zip varchar,
Country varchar(50),
Phone varchar(15)

FOREIGN KEY (ClientID) REFERENCES Clients(ID)
)