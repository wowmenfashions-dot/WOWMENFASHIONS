-- Keep only 90 products
WITH CTE AS (
    SELECT Id, 
           ROW_NUMBER() OVER (ORDER BY Id) as rn
    FROM Products
)
DELETE FROM Products 
WHERE Id IN (
    SELECT Id FROM CTE WHERE rn > 90
);
