CREATE PROCEDURE GetResourceTaskDetails
AS
BEGIN
Select * from Resource r join ResourceTask rt on r.ResourceId = rt.ResourceId ;
END;