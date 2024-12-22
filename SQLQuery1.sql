-- Таблица канцтоваров
CREATE TABLE Stationery (
    StationeryID INT PRIMARY KEY IDENTITY, 
    Name NVARCHAR(100) NOT NULL,                
    TypeID INT NOT NULL,                        
    Price DECIMAL(18, 2) NOT NULL,              
    SoldUnits INT NOT NULL DEFAULT 0,          
    LastSaleDate DATE NULL,                    
    FOREIGN KEY (TypeID) REFERENCES StationeryType(TypeID) 
);

-- Таблица типов канцтоваров
CREATE TABLE StationeryType (
    TypeID INT PRIMARY KEY IDENTITY,
    TypeName NVARCHAR(100) NOT NULL      
);

-- Таблица менеджеров по продажам
CREATE TABLE SalesManager (
    ManagerID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,           
    TotalSales DECIMAL(18, 2) DEFAULT 0     
);

-- Таблица фирм покупателей
CREATE TABLE CustomerFirm (
    FirmID INT PRIMARY KEY IDENTITY,
    FirmName NVARCHAR(100) NOT NULL,       
    TotalPurchase DECIMAL(18, 2) DEFAULT 0   
);

-- Архивные таблицы
CREATE TABLE ArchivedStationery (
    StationeryID INT,                          
    Name NVARCHAR(100),                      
    TypeID INT,                              
    Price DECIMAL(18, 2),                     
    SoldUnits INT,                            
    LastSaleDate DATE,                        
    ArchivedDate DATETIME DEFAULT GETDATE()    
);

CREATE TABLE ArchivedSalesManager (
    ManagerID INT,                            
    Name NVARCHAR(100),                       
    TotalSales DECIMAL(18, 2),               
    ArchivedDate DATETIME DEFAULT GETDATE()    
);

CREATE TABLE ArchivedCustomerFirm (
    FirmID INT,                               
    FirmName NVARCHAR(100),                   
    TotalPurchase DECIMAL(18, 2),             
    ArchivedDate DATETIME DEFAULT GETDATE()    
);