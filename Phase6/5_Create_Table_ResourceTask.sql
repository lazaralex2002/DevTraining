USE TaskManagement
CREATE TABLE ResourceTask
(
	RelationshipId int PRIMARY KEY,
	ResourceId int,
	TaskId int,
	FOREIGN KEY (ResourceId) REFERENCES TaskManagement.dbo.Resource(ResourceId) ON DELETE CASCADE,
	FOREIGN KEY (TaskId) REFERENCES TaskManagement.dbo.Task(TaskId) ON DELETE CASCADE
);