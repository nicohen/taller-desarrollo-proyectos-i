USE [ZoosManagementSystem]
GO

DECLARE @environmentID UNIQUEIDENTIFIER
INSERT INTO [dbo].[Environment] ([Name], [Description], [Surface], [Type]) VALUES ('Jaula de Leones', 'Jaula para tres leones', 1800, 'Mamiferos')
INSERT INTO [dbo].[Environment] ([Name], [Description], [Surface], [Type]) VALUES ('Jaula de Osos', 'Jaula para dos osos', 2000, 'Mamiferos')
INSERT INTO [dbo].[Environment] ([Name], [Description], [Surface], [Type]) VALUES ('Jaula de Cebras', 'Jaula para cuatro cebras', 3000, 'Mamiferos')
INSERT INTO [dbo].[Environment] ([Name], [Description], [Surface], [Type]) VALUES ('Jaula de Elefantes', 'Jaula para dos elefantes', 3200, 'Mamiferos')
SET @environmentID = (SELECT TOP 1 [Id] FROM [dbo].[Environment] WHERE [Name] = 'Jaula de Leones')

DECLARE @feedingID UNIQUEIDENTIFIER
INSERT INTO [dbo].[Feeding] ([Name], [Description]) VALUES ('Carne de Cebra, Búfalo y Antílope', 'Mezcla de carnes de cebras, búfalos y antílopes')
SET @feedingID = (SELECT TOP 1 [Id] FROM [dbo].[Feeding])

DECLARE @responsibleID UNIQUEIDENTIFIER
INSERT INTO [dbo].[Responsible] ([Name], [LastName], [Email], [Phone]) VALUES ('John', 'Doe', 'joe.doe@zoo.com', '011-15-6116-1661')
SET @responsibleID = (SELECT TOP 1 [Id] FROM [dbo].[Responsible])

DECLARE @animalID UNIQUEIDENTIFIER
INSERT INTO [dbo].[Animal] ([EnvironmentId], [ResponsibleId], [Name], [Sex], [Species], [BirthDate], [BornInCaptivity], [Cost], [NextHealthMeasure]) VALUES (@environmentID, @responsibleID, 'Simba', 'M','Phantera Leo', '2007-05-08', 1, NULL, '2010-02-15')
SET @animalID = (SELECT TOP 1 [Id] FROM [dbo].[Animal])

DECLARE @sensorID UNIQUEIDENTIFIER
INSERT INTO [dbo].[Sensor] ([EnvironmentId], [Name], [Type], [Manufacturer], [SerialNumber]) VALUES (@environmentID, 'SensorLuz-Ph1127-1', 'Luz', 'Phidgets Inc.', 'AB285-DE855-EDDR5-ADRR1-15DDR')
SET @sensorID = (SELECT TOP 1 [Id] FROM [dbo].[Sensor])

INSERT INTO [dbo].[FeedingTime] ([FeedingId], [AnimalId], [Amount], [Time]) VALUES (@feedingID, @animalID, 3500, '12:00:00.0')
INSERT INTO [dbo].[FeedingTime] ([FeedingId], [AnimalId], [Amount], [Time]) VALUES (@feedingID, @animalID, 3500, '20:30:00.0')

INSERT INTO [dbo].[TimeSlot] ([EnvironmentId], [InitialTime], [FinalTime], [TemperatureMin], [TemperatureMax], [HumidityMin], [HumidityMax], [LuminosityMin], [LuminosityMax]) VALUES (@environmentID, '20:00:00.0', '07:00:00.0', 20.0, 30.0, 53.0, 68.0, 0.0, 3.0)
INSERT INTO [dbo].[TimeSlot] ([EnvironmentId], [InitialTime], [FinalTime], [TemperatureMin], [TemperatureMax], [HumidityMin], [HumidityMax], [LuminosityMin], [LuminosityMax]) VALUES (@environmentID, '07:00:00.0', '12:30:00.0', 24.0, 33.0, 55.0, 78.0, 5000.0, 30000.0)
INSERT INTO [dbo].[TimeSlot] ([EnvironmentId], [InitialTime], [FinalTime], [TemperatureMin], [TemperatureMax], [HumidityMin], [HumidityMax], [LuminosityMin], [LuminosityMax]) VALUES (@environmentID, '12:30:00.0', '20:00:00.0', 25.0, 35.0, 60.0, 80.0, 30000.0, 100000.0)