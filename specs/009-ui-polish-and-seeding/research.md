# Research: UI Polish and Database Seeding Expansion

## Decision: Flexbox Card Layout
**Decision**: We will apply `d-flex flex-column h-100` to the `MudCard` itself. We will wrap the product information in an element with `flex-grow-1` and apply `mt-auto` to the "Add" button's container.
**Rationale**: This is the standard web pattern for equal height cards. It guarantees the card spans the full height of the grid cell provided by the parent `MudGrid`, and forces the CTA (call to action) to the bottom edge.

## Decision: Image Fallback
**Decision**: We will wrap the `MudImage` in a `<div style="height: 200px; background-color: #f0f0f0;">` (or a mudblazor equivalent grey surface).
**Rationale**: If the image URL is broken, the container will still reserve the 200px block, preventing the entire card from collapsing.

## Decision: SQL Seed Generation
**Decision**: We will create a `02_SeedDummyData.sql` script that uses standard T-SQL variable loops (`WHILE @i <= 50`) to insert product rows into the `Products` table. We will `NEWID()` to randomize prices and image identifiers.
**Rationale**: 400 `INSERT INTO` statements is unwieldy. A dynamic loop ensures we can scale the dummy data efficiently while maintaining database integrity. All generated URLs will look like `https://example.com/images/product-` + random ID + `.avif`.
