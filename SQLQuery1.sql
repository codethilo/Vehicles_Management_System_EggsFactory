CREATE DATABASE VehicleDb2;
USE VehicleDb2;

CREATE TABLE Vehicle (
    
    ID INT IDENTITY(1,1) PRIMARY KEY,
	
    Register_No VARCHAR(50) NOT NULL,
    Chassis_No VARCHAR(50) NOT NULL,
    Engine_No VARCHAR(50) NOT NULL,
    OwnerName VARCHAR(50) NOT NULL,
    Address VARCHAR(255) NOT NULL,
    Date_Of_Registration DATE NOT NULL,
    Register_Valid DATE NOT NULL,
    Owner_Sr_no INT NOT NULL,
    Year_of_Mfg DATE NOT NULL, -- Changed "Year_of_Mgf" to "Year_of_Mfg"
    WheelBase INT NOT NULL,
    CubicCapacity INT NOT NULL,
    No_of_Cylinders INT NOT NULL,
    Laden VARCHAR(100) NOT NULL,
    MakerName VARCHAR(100) NOT NULL,
    ModelName VARCHAR(100) NOT NULL,
    Colors VARCHAR(255) NOT NULL,
    Body_Type VARCHAR(100) NOT NULL,
    Seating INT NOT NULL,
    TaxiPaid VARCHAR(100),
    Vehicle_Class VARCHAR(100) NOT NULL,
    RcNo VARCHAR(100)  NOT NULL,
    Fuel_Use VARCHAR(100) NOT NULL

);

CREATE TABLE Spares_Table (
    ID INT,
    Register_No VARCHAR(50) NOT NULL,
    Spares_Type VARCHAR(100) NOT NULL,
    Spares_Name VARCHAR(255) NOT NULL,
    Spares_No INT NOT NULL,
    Date_Of_Purchase DATE NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_Spares_ID PRIMARY KEY (ID), -- Changed the constraint name to PK_Spares_ID
    CONSTRAINT FK_Spares_ID FOREIGN KEY (ID) REFERENCES Vehicle(ID)
);

CREATE TABLE Trip_Table (
    
    Trip_No INT IDENTITY(1,1) PRIMARY KEY,
    Register_No VARCHAR(50) NOT NULL,
    Driver_Name VARCHAR(255) NOT NULL,
    TimeIn DATETIME NOT NULL,
    TimeOut DATETIME NOT NULL,
    Supervisor VARCHAR(255) NOT NULL,
    KM_From DECIMAL(10, 2) NOT NULL,
    Km_To DECIMAL(10, 2) NOT NULL,
    KM_Distance DECIMAL(10, 2) NOT NULL,
    Trip_Place VARCHAR(255) NOT NULL,
    Complaint VARCHAR(255) NOT NULL,
    CONSTRAINT FK_Trip_Trip_No  FOREIGN KEY (Trip_No ) REFERENCES Vehicle(ID) -- Changed the foreign key reference to Register_No
);

CREATE TABLE Tyre_Table (
    ID INT,
    Register_No VARCHAR(50) NOT NULL,
    Type_Size VARCHAR(100) NOT NULL,
    Manufacture VARCHAR(100) NOT NULL,
    Tyre_Type VARCHAR(50) NOT NULL,
    Purchased_From VARCHAR(100) NOT NULL,
    Date_Of_Purchase DATE NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_Tyre_ID PRIMARY KEY (ID), -- Changed the constraint name to PK_Tyre_ID
    CONSTRAINT FK_Tyre_ID FOREIGN KEY (ID) REFERENCES Vehicle(ID)
);

CREATE TABLE Service_Table (
    Service_No INT IDENTITY(1,1) PRIMARY KEY,
    Register_No VARCHAR(50) NOT NULL,
    Spares_Type VARCHAR(100),
    Date_Of_Service DATE NOT NULL,
    Mechanic_Name VARCHAR(50),
    Status1 VARCHAR(100) NOT NULL,
    CONSTRAINT FK_Service_Service_No FOREIGN KEY (Service_No) REFERENCES Vehicle(ID) 
);
INSERT INTO Service_Table (Register_No, Spares_Type, Date_Of_Service, Mechanic_Name, Status1)
VALUES ('RegisterNoValue', 'SparesTypeValue', '2024-06-07', 'MechanicNameValue', 'StatusValue');

CREATE TABLE Vehicle_Table1 (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Reason NVARCHAR(MAX) NOT NULL
);

CREATE TABLE LoginTable1 (
    username NVARCHAR(50),
    password NVARCHAR(50)
);

CREATE TABLE Spares_Table1 (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Reason NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Tyre_Table1 (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Reason NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Trip_Table1 (
    Trip_No INT IDENTITY(1,1) PRIMARY KEY,
    Reason NVARCHAR(MAX) NOT NULL
);

CREATE TABLE Service_Table1 (
    Service_No INT IDENTITY(1,1) PRIMARY KEY,
    Reason NVARCHAR(MAX) NOT NULL
);
