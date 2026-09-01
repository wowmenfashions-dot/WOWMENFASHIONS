# Phase 0: Outline & Research

## Technical Decisions

### 1. Guest Cart Identification
- **Decision**: Use a secure HTTP-Only cookie to store a `GuestCartId` (GUID) on the browser.
- **Rationale**: Since Blazor Server runs server-side, a cookie allows the circuit to recognize the returning guest visitor across sessions and tabs.
- **Alternatives considered**: LocalStorage (rejected because Blazor Server circuit instantiation happens before JS interop can read local storage securely for pre-rendering).

### 2. Cart Totals & Pricing
- **Decision**: The database will always calculate the exact totals in the stored procedure `dbo.Cart_Get` by joining the `CartItem` with the `Product` table.
- **Rationale**: The Constitution mandates that SQL Server is the source of truth (Principle 18). Product prices may change. Calculating on the fly in SQL Server ensures accurate pricing.
- **Alternatives considered**: Storing prices in `CartItem` table (rejected as prices might drift and it violates the source of truth principle).

### 3. Inventory Validation
- **Decision**: Validate inventory directly in the `dbo.Cart_AddItem` and `dbo.Cart_UpdateItem` stored procedures. If requested quantity exceeds `StockQuantity`, the procedure should raise an error or return a validation code.
- **Rationale**: Avoids race conditions between reading inventory in C# and updating the cart.

### 4. Database Connection
- **Decision**: The connection string provided (`Data Source=(localdb)\MSSqlLocalDb...`) will be placed in `appsettings.json` and injected into repositories using `IConfiguration`.
