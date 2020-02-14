ALTER TABLE USERS 
ALTER COLUMN DrivingLiscenceNumber nvarchar(15) NULL;

CREATE UNIQUE INDEX inunique
ON USERS(DrivingLiscenceNumber) WHERE DrivingLiscenceNumber IS NOT NULL;