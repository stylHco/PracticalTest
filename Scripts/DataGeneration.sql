USE PracticalDB;
 
SET IDENTITY_INSERT  Departments ON

INSERT INTO Departments (Id, Name, OfficeLocation) VALUES
(1, 'Department1', 'SampleLocation'),
(2, 'Department2', 'SampleLocation'),
(3, 'Department3', 'SampleLocation'),
(4, 'Department4', 'SampleLocation'),
(5, 'Department5', 'SampleLocation');

SET IDENTITY_INSERT  Departments OFF

SET IDENTITY_INSERT Employees ON;

INSERT INTO Employees (Id, FirstName, LastName, Email, Salary, DepartmentId)
VALUES
(1,  'Test Firstname 1',  'Test Lastname 1',  'testemail1@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(2,  'Test Firstname 2',  'Test Lastname 2',  'testemail2@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(3,  'Test Firstname 3',  'Test Lastname 3',  'testemail3@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(4,  'Test Firstname 4',  'Test Lastname 4',  'testemail4@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(5,  'Test Firstname 5',  'Test Lastname 5',  'testemail5@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(6,  'Test Firstname 6',  'Test Lastname 6',  'testemail6@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(7,  'Test Firstname 7',  'Test Lastname 7',  'testemail7@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(8,  'Test Firstname 8',  'Test Lastname 8',  'testemail8@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(9,  'Test Firstname 9',  'Test Lastname 9',  'testemail9@example.com',  ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3),  FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(10, 'Test Firstname 10', 'Test Lastname 10', 'testemail10@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(11, 'Test Firstname 11', 'Test Lastname 11', 'testemail11@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(12, 'Test Firstname 12', 'Test Lastname 12', 'testemail12@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(13, 'Test Firstname 13', 'Test Lastname 13', 'testemail13@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(14, 'Test Firstname 14', 'Test Lastname 14', 'testemail14@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(15, 'Test Firstname 15', 'Test Lastname 15', 'testemail15@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(16, 'Test Firstname 16', 'Test Lastname 16', 'testemail16@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(17, 'Test Firstname 17', 'Test Lastname 17', 'testemail17@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(18, 'Test Firstname 18', 'Test Lastname 18', 'testemail18@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(19, 'Test Firstname 19', 'Test Lastname 19', 'testemail19@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(20, 'Test Firstname 20', 'Test Lastname 20', 'testemail20@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(21, 'Test Firstname 21', 'Test Lastname 21', 'testemail21@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(22, 'Test Firstname 22', 'Test Lastname 22', 'testemail22@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(23, 'Test Firstname 23', 'Test Lastname 23', 'testemail23@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(24, 'Test Firstname 24', 'Test Lastname 24', 'testemail24@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1),
(25, 'Test Firstname 25', 'Test Lastname 25', 'testemail25@example.com', ROUND(RAND(CHECKSUM(NEWID())) * 100000, 3), FLOOR(RAND(CHECKSUM(NEWID())) * 5) + 1);

SET IDENTITY_INSERT Employees OFF;