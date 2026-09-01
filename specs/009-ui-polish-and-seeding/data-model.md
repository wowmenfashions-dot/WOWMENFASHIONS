# Data Model: UI Polish and Database Seeding Expansion

## Seed Data Requirements

The following logical structure will be built via SQL insertion loops:

### `Categories` Table Additions:
- Name: "Kids", Slug: "kids"
- Name: "Toddler", Slug: "toddler"
- Name: "Adults", Slug: "adults"
- Name: "Teenager", Slug: "teenager"

### `Products` Table Constraints for Dummy Data:
- `CategoryId`: Mapped via lookup to the appropriate category.
- `Name`: E.g., "Kids Product 1", "Teenager Product 22".
- `Description`: "Generated dummy product description."
- `Price`: Randomly generated between 10.00 and 200.00.
- `OriginalPrice`: NULL or randomly higher than Price.
- `ImageUrl`: `https://picsum.photos/seed/` + NEWID() + `/400/400.avif` (ensuring .avif constraint).
- `IsActive`: 1
- `Color`: Random assignment (Red, Blue, Black, White).
