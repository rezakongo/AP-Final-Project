CREATE TABLE [dbo].[clients]
(
	[num] int NOT NULL,
	firstname varchar(50) NOT NULL, 
    [lastname] VARCHAR(50) NOT NULL, 
    [phonenumber] INT NOT NULL, 
    [nationalid] INT NOT NULL, 
    [adress] VARCHAR(MAX) NOT NULL, 
    [email] VARCHAR(MAX) NOT NULL, 
    [password] VARCHAR(MAX) NOT NULL,
)
