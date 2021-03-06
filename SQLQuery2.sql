CREATE TABLE Reader (
  Password_Data BIGINT  NOT NULL   IDENTITY ,
  Home_Address VARCHAR(80)    ,
  Full_Name VARCHAR(50)      ,
PRIMARY KEY(Password_Data));
GO

CREATE TABLE Book (
  Register_Number BIGINT  NOT NULL   IDENTITY ,
  Count_Pages SMALLINT    ,
  Year_Publishing SMALLINT    ,
  Section VARCHAR(150)      ,
PRIMARY KEY(Register_Number));
GO

CREATE TABLE RecordRegistration (
  ID INTEGER  NOT NULL  ,
  Book_Register_Number BIGINT  NOT NULL  ,
  Reader_Password_Data BIGINT  NOT NULL  ,
  Date_Issue DATE    ,
  Date_Return DATE      ,
PRIMARY KEY(ID)    ,
  FOREIGN KEY(Reader_Password_Data)
    REFERENCES Reader(Password_Data),
  FOREIGN KEY(Book_Register_Number)
    REFERENCES Book(Register_Number));
GO


CREATE INDEX Table_03_FKIndex1 ON RecordRegistration (Reader_Password_Data);
GO
CREATE INDEX Table_03_FKIndex2 ON RecordRegistration (Book_Register_Number);
GO

CREATE INDEX IFK_Reader ON RecordRegistration (Reader_Password_Data);
GO
CREATE INDEX IFK_Storage ON RecordRegistration (Book_Register_Number);
GO
