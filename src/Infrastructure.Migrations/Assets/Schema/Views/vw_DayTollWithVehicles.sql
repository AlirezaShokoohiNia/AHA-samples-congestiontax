CREATE VIEW vw_DayTollWithVehicles AS
    SELECT 
        v.Id As VehicleId,
        v.LicensePlate,
        dt.Date,
        dt.City,
        dt.TotalFee
    FROM DayTolls dt
    JOIN Vehicles v ON dt.VehicleId = v.Id;