CREATE TABLE HomepageImages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ImageData VARBINARY(MAX) NOT NULL,
    ContentType NVARCHAR(50) NOT NULL DEFAULT 'image/avif',
    DisplayOrder INT NOT NULL
);
