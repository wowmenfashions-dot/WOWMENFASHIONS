USE wowmenfashions;
GO

IF NOT EXISTS (SELECT * FROM Categories WHERE Slug = 'kids')
BEGIN
    INSERT INTO Categories (Id, Name, Slug, ParentId) VALUES (9, 'Kids', 'kids', NULL)
END

IF NOT EXISTS (SELECT * FROM Categories WHERE Slug = 'toddler')
BEGIN
    INSERT INTO Categories (Id, Name, Slug, ParentId) VALUES (10, 'Toddler', 'toddler', NULL)
END
GO
