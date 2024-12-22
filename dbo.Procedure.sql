-- Хранимая процедура для вставки канцтовара
CREATE PROCEDURE InsertStationery
    @Name NVARCHAR(100),
    @TypeID INT,
    @Price DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Stationery (Name, TypeID, Price)
    VALUES (@Name, @TypeID, @Price);
END;

-- Хранимая процедура для вставки типа канцтовара
CREATE PROCEDURE InsertStationeryType
    @TypeName NVARCHAR(100)
AS
BEGIN
    INSERT INTO StationeryType (TypeName)
    VALUES (@TypeName);
END;

-- Хранимая процедура для вставки менеджера по продажам
CREATE PROCEDURE InsertSalesManager
    @Name NVARCHAR(100)
AS
BEGIN
    INSERT INTO SalesManager (Name)
    VALUES (@Name);
END;

-- Хранимая процедура для вставки фирмы покупателя
CREATE PROCEDURE InsertCustomerFirm
    @FirmName NVARCHAR(100)
AS
BEGIN
    INSERT INTO CustomerFirm (FirmName)
    VALUES (@FirmName);
END;
-- Хранимая процедура для обновления канцтовара
CREATE PROCEDURE UpdateStationery
    @StationeryID INT,
    @Name NVARCHAR(100),
    @Price DECIMAL(18, 2)
AS
BEGIN
    UPDATE Stationery
    SET Name = @Name, Price = @Price
    WHERE StationeryID = @StationeryID;
END;

-- Хранимая процедура для обновления фирмы покупателя
CREATE PROCEDURE UpdateCustomerFirm
    @FirmID INT,
    @FirmName NVARCHAR(100)
AS
BEGIN
    UPDATE CustomerFirm
    SET FirmName = @FirmName
    WHERE FirmID = @FirmID;
END;

-- Хранимая процедура для обновления менеджера
CREATE PROCEDURE UpdateSalesManager
    @ManagerID INT,
    @Name NVARCHAR(100)
AS
BEGIN
    UPDATE SalesManager
    SET Name = @Name
    WHERE ManagerID = @ManagerID;
END;

-- Хранимая процедура для показа популярных канцтоваров
CREATE PROCEDURE GetPopularStationery
AS
BEGIN
    SELECT TOP 5 Name, SoldUnits
    FROM Stationery
    ORDER BY SoldUnits DESC;
END;

-- Хранимая процедура для получения канцтоваров, которые не продавались заданное количество дней
CREATE PROCEDURE GetUnsoldStationery
    @Days INT
AS
BEGIN
    SELECT Name
    FROM Stationery
    WHERE DATEDIFF(DAY, LastSaleDate, GETDATE()) >= @Days;
END;

-- Хранимая процедура для обновления типа канцтовара
CREATE PROCEDURE UpdateStationeryType
    @TypeID INT,
    @TypeName NVARCHAR(100)
AS
BEGIN
    UPDATE StationeryType
    SET TypeName = @TypeName
    WHERE TypeID = @TypeID;
END;

-- Хранимая процедура для удаления канцтовара
CREATE PROCEDURE DeleteStationery
    @StationeryID INT
AS
BEGIN
    -- Архивируем данные перед удалением
    INSERT INTO ArchivedStationery (StationeryID, Name, TypeID, Price, SoldUnits, LastSaleDate)
    SELECT StationeryID, Name, TypeID, Price, SoldUnits, LastSaleDate
    FROM Stationery
    WHERE StationeryID = @StationeryID;

    -- Удаляем канцтовар
    DELETE FROM Stationery
    WHERE StationeryID = @StationeryID;
END;

-- Хранимая процедура для удаления менеджера
CREATE PROCEDURE DeleteSalesManager
    @ManagerID INT
AS
BEGIN
    -- Архивируем данные перед удалением
    INSERT INTO ArchivedSalesManager (ManagerID, Name, TotalSales)
    SELECT ManagerID, Name, TotalSales
    FROM SalesManager
    WHERE ManagerID = @ManagerID;

    -- Удаляем менеджера
    DELETE FROM SalesManager
    WHERE ManagerID = @ManagerID;
END;

-- Хранимая процедура для удаления фирмы покупателя
CREATE PROCEDURE DeleteCustomerFirm
    @FirmID INT
AS
BEGIN
    -- Архивируем данные перед удалением
    INSERT INTO ArchivedCustomerFirm (FirmID, FirmName, TotalPurchase)
    SELECT FirmID, FirmName, TotalPurchase
    FROM CustomerFirm
    WHERE FirmID = @FirmID;

    -- Удаляем фирму покупателя
    DELETE FROM CustomerFirm
    WHERE FirmID = @FirmID;
END;

-- Хранимая процедура для удаления типа канцтовара
CREATE PROCEDURE DeleteStationeryType
    @TypeID INT
AS
BEGIN
    -- Выполняем аналогичные действия, как и выше
    DELETE FROM StationeryType WHERE TypeID = @TypeID;
END;


-- Хранимая процедура для показа менеджера с наибольшими продажами по единицам
CREATE PROCEDURE GetTopSalesManagerByUnits
AS
BEGIN
    SELECT TOP 1 Name, SUM(SoldUnits) AS TotalUnits
    FROM SalesManager s
    JOIN Stationery st ON s.ManagerID = st.ManagerID
    GROUP BY Name
    ORDER BY TotalUnits DESC;
END;

-- Хранимая процедура для показа менеджера по наибольшей общей сумме прибыли
CREATE PROCEDURE GetTopSalesManagerByProfit
AS
BEGIN
    SELECT TOP 1 Name, SUM(st.Price * st.SoldUnits) AS TotalProfit
    FROM SalesManager s
    JOIN Stationery st ON s.ManagerID = st.ManagerID
    GROUP BY Name
    ORDER BY TotalProfit DESC;
END;

-- Хранимая процедура для показа менеджера по сумме прибыли за определенный промежуток времени
CREATE PROCEDURE GetSalesManagerByProfitInTime
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT TOP 1 Name, SUM(st.Price * st.SoldUnits) AS TotalProfit
    FROM SalesManager s
    JOIN Stationery st ON s.ManagerID = st.ManagerID
    WHERE st.LastSaleDate BETWEEN @StartDate AND @EndDate
    GROUP BY Name
    ORDER BY TotalProfit DESC;
END;

-- Хранимая процедура для показа информации о фирме покупателе с самой большой суммой покупок
CREATE PROCEDURE GetTopCustomerFirm
AS
BEGIN
    SELECT TOP 1 FirmName, TotalPurchase
    FROM CustomerFirm
    ORDER BY TotalPurchase DESC;
END;

-- Хранимая процедура для показа информации о типе канцтоваров с наибольшим количеством продаж
CREATE PROCEDURE GetTopStationeryTypeBySales
AS
BEGIN
    SELECT TOP 1 st.TypeName, SUM(s.SoldUnits) AS TotalUnits
    FROM StationeryType st
    JOIN Stationery s ON st.TypeID = s.TypeID
    GROUP BY st.TypeName
    ORDER BY TotalUnits DESC;
END;

-- Хранимая процедура для показа информации о самом прибыльном типе канцтоваров
CREATE PROCEDURE GetMostProfitableStationery
AS
BEGIN
    SELECT TOP 1 s.Name, (s.Price * s.SoldUnits) AS TotalProfit
    FROM Stationery s
    WHERE s.SoldUnits > 0
    ORDER BY TotalProfit DESC;
END;