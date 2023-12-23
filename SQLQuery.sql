Create database MvcAndAngularTask;

use MvcAndAngularTask;

create table doctors(
	id int identity(1,1) primary key ,
	name varchar(100),
	doctorId varchar(100)
	
);


create table patient(
	id int identity(1,1) primary key ,
	name varchar(100),
	gender bit,
	nationalId varchar(100)	
);


create table schedule(
	id int identity(1,1) primary key ,
	doctorId int foreign key references doctors(id),
	[from] time,
	[to] time,
	[day] varchar(25),
);

create table reservation(
	id int identity(1,1) primary key ,
	doctorId int foreign key references doctors(id),
	patientId int foreign key references patient(id),
	[from] time,
	[to] time,
	[date] date,
);






insert doctors values ('Ahmed' , 'DR12345');
insert doctors values ('Ali' , 'DR98765');
insert doctors values ('Aya' , 'DR24680');
insert doctors values ('Khaled' , 'DR54321');
insert doctors values ('Asmaa' , 'DR13579');




-- Insert schedule for Doctor 1
INSERT INTO schedule (doctorId, [from], [to], [day])
VALUES (1, '16:00', '20:00', 'Monday'),
       (1, '16:00', '20:00', 'Tuesday'),
       (1, '16:00', '20:00', 'Wednesday'),
       (1, '16:00', '20:00', 'Thursday'),
       (1, '16:00', '20:00', 'Saturday'),
       (1, '16:00', '20:00', 'Sunday');

-- Insert schedule for Doctor 2
INSERT INTO schedule (doctorId, [from], [to], [day])
VALUES (2, '09:00', '13:00', 'Monday'),
       (2, '00:00', '05:00', 'Tuesday'),
       (2, '08:00', '12:00', 'Wednesday'),
       (2, '00:00', '05:00', 'Thursday'),
       (2, '13:00', '17:00', 'Friday'),
       (2, '09:00', '15:00', 'Saturday');

-- Insert schedule for Doctor 3
INSERT INTO schedule (doctorId, [from], [to], [day])
VALUES (3, '13:00', '17:00', 'Monday'),
       (3, '09:00', '13:00', 'Wednesday'),
       (3, '08:00', '12:00', 'Friday');

-- Insert schedule for Doctor 4
INSERT INTO schedule (doctorId, [from], [to], [day])
VALUES (4, '08:00', '12:00', 'Monday'),
       (4, '02:00', '07:00', 'Tuesday'),
       (4, '13:00', '17:00', 'Wednesday'),
       (4, '03:00', '07:00', 'Thursday'),
       (4, '09:00', '13:00', 'Friday');

-- Insert schedule for Doctor 5
INSERT INTO schedule (doctorId, [from], [to], [day])
VALUES (5, '09:00', '13:00', 'Monday'),
       (5, '08:00', '12:00', 'Wednesday'),
       (5, '13:00', '17:00', 'Friday');





CREATE VIEW getDoctorReservationNumber AS
select doctors.name , [count] from doctors
join (
	select doctors.id  , COUNT(reservation.id) AS [count]
	from doctors 
	left join reservation on reservation.doctorId = doctors.id 
	GROUP BY doctors.id

) as tbl on tbl.id = doctors.id;



Alter table reservation add unique ([doctorId] , [date] , [from]);
