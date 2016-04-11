create database if not exists ShowManagement;
use ShowManagement;

create table if not exists spectacol(
	showId int unique primary key auto_increment,
    showName varchar(100),
    regia varchar(100),
    distributia varchar(200),
    data_premierei datetime,
    tickets int,
    availabletickets int);
    
create table if not exists user(
	idUser int unique primary key auto_increment,
    username varchar(20) unique,
    password varchar(40),
    usertype varchar(10));
    
create table if not exists ticket(
	ticketId int unique primary key auto_increment,
    rand int,
    loc int,
    showId int,
    idUser int,
    foreign key (showId) references spectacol(showId) on delete cascade,
    foreign key (idUser) references user(idUser) on delete cascade);
    
