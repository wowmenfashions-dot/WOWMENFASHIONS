IF NOT EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'SelectedColor' AND Object_ID = Object_ID(N'CartItems')) 
BEGIN 
    ALTER TABLE CartItems ADD SelectedColor NVARCHAR(50) NULL; 
END
