USE TaskManagement
CREATE TABLE Task
(
TaskID int PRIMARY KEY,
Name varchar(50),
Duration numeric(8,1),
Start datetime,
Finish datetime, 
TaskMode int,
ProjectId int NOT NULL ,
FOREIGN KEY (ProjectId) REFERENCES TaskManagement.dbo.Project(ProjectId) ON DELETE CASCADE
);