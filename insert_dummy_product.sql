-- Insert Dummy Triangle Product
DELETE FROM ProductColorImages WHERE ProductId = 99;
DELETE FROM Products WHERE Id = 99;

INSERT INTO Products (Id, Name, CategoryId, Category, Color, Price, OriginalPrice, ImageUrl, Description, AvailableColors) 
VALUES (
    99, 
    'Dummy Triangle', 
    3, 
    'mens', 
    'Red', 
    100.00, 
    NULL, 
    'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMDAiIGhlaWdodD0iMjAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHBvbHlnb24gcG9pbnRzPSI1MCwxMCA5MCw5MCAxMCw5MCIgZmlsbD0iUmVkIiAvPjwvc3ZnPg==', 
    'A dummy product for testing with 5 color options.', 
    'Red, Blue, Green, Orange, Purple'
);

INSERT INTO ProductColorImages (ProductId, ColorName, ImageUrl) VALUES 
(99, 'Red', 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMDAiIGhlaWdodD0iMjAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHBvbHlnb24gcG9pbnRzPSI1MCwxMCA5MCw5MCAxMCw5MCIgZmlsbD0iUmVkIiAvPjwvc3ZnPg=='),
(99, 'Blue', 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMDAiIGhlaWdodD0iMjAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHBvbHlnb24gcG9pbnRzPSI1MCwxMCA5MCw5MCAxMCw5MCIgZmlsbD0iQmx1ZSIgLz48L3N2Zz4='),
(99, 'Green', 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMDAiIGhlaWdodD0iMjAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHBvbHlnb24gcG9pbnRzPSI1MCwxMCA5MCw5MCAxMCw5MCIgZmlsbD0iR3JlZW4iIC8+PC9zdmc+'),
(99, 'Orange', 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMDAiIGhlaWdodD0iMjAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHBvbHlnb24gcG9pbnRzPSI1MCwxMCA5MCw5MCAxMCw5MCIgZmlsbD0iT3JhbmdlIiAvPjwvc3ZnPg=='),
(99, 'Purple', 'data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMDAiIGhlaWdodD0iMjAwIiB2aWV3Qm94PSIwIDAgMTAwIDEwMCI+PHBvbHlnb24gcG9pbnRzPSI1MCwxMCA5MCw5MCAxMCw5MCIgZmlsbD0iUHVycGxlIiAvPjwvc3ZnPg==');
