<!--
Sync Impact Report:
- Version changed: 1.3.0 -> 1.4.0
- Added Sections: Principle 29 (AVIF Image Format Only)
- Deferred Intents: UI Polish and Product Seeding (via /speckit-specify)
-->

# Shopping Cart Application Constitution

**Version:** 1.4.0
**Status:** Active
**Application:** Blazor Server Shopping Cart
**UI Framework:** MudBlazor
**Architecture:** Modular Monolith
**Data Access:** Dapper
**Database:** SQL Server
**Database Access Pattern:** Stored Procedures Only
**Authentication:** Custom Authentication
**Development Method:** Spec-Driven Development with Spec Kit

---

# Non-Negotiable Technology Rules

The following rules are mandatory and MUST NOT be violated unless the constitution is explicitly amended.

---

## Principle 1 — SQL Server Is the Only Application Database

The application MUST use **Microsoft SQL Server** as its relational database.

No other database technology may be introduced as the primary application database without an explicit architectural decision.

The application data model MUST be designed for SQL Server.

---

## Principle 2 — Dapper Is the Only Data Access Library

**Dapper MUST be the only data-access library used by the application.**

All application database operations MUST go through Dapper.

The project MUST NOT use:

* Entity Framework Core ( Except for the authentication mechanism only)
* Entity Framework
* NHibernate
* LINQ-to-SQL
* RepoDb
* ServiceStack.OrmLite
* Any other ORM
* Any other database abstraction library that executes database operations

Dapper is the single approved data-access mechanism.

Required dependency:

```text
Application
     ↓
Application Services
     ↓
Data Access Layer
     ↓
Dapper
     ↓
SQL Server
```

---

# Principle 3 — Stored Procedures Only

**ALL database operations MUST be performed through SQL Server Stored Procedures.**

This is a strict requirement.

Application code MUST NOT execute ad-hoc SQL statements.

The following are prohibited:

```csharp
connection.QueryAsync(
    "SELECT * FROM Products"
);
```

```csharp
connection.ExecuteAsync(
    "UPDATE Products SET Price = @Price WHERE Id = @Id"
);
```

```csharp
connection.QueryAsync(
    "INSERT INTO Products ..."
);
```

Raw SQL strings MUST NOT be used for application database operations.

Instead, Dapper MUST execute stored procedures.

Example:

```csharp
await connection.QueryAsync<Product>(
    "dbo.Product_GetById",
    new { Id = productId },
    commandType: CommandType.StoredProcedure
);
```

---

# Principle 4 — No Direct SQL From Application Code

Application code MUST NOT contain SQL statements.

SQL MUST exist only inside SQL Server stored procedures.

The application code may contain:

* Stored procedure names
* Parameter definitions
* Dapper mapping
* Result handling

The application code MUST NOT contain:

* SELECT statements
* INSERT statements
* UPDATE statements
* DELETE statements
* MERGE statements
* CREATE TABLE statements
* ALTER TABLE statements
* Other executable SQL statements

---

# Principle 5 — Database Access Must Be Isolated

Blazor components MUST NOT directly access Dapper or SQL Server.

The required dependency flow is:

```text
Blazor Component
       ↓
Application Service
       ↓
Repository / Data Access
       ↓
Dapper
       ↓
Stored Procedure
       ↓
SQL Server
```

For example:

```text
ProductPage
    ↓
IProductService
    ↓
IProductRepository
    ↓
Dapper
    ↓
dbo.Product_GetById
    ↓
SQL Server
```

The UI MUST never directly create a SQL connection.

---

# Principle 6 — Repository Layer

Database operations SHOULD be isolated behind repository/data-access interfaces.

Examples:

```text
IProductRepository
ICategoryRepository
ICartRepository
ICustomerRepository
IAddressRepository
IOrderRepository
IInventoryRepository
IPaymentRepository
```

Repositories are responsible for:

* Opening database connections
* Executing stored procedures through Dapper
* Passing parameters
* Mapping results
* Handling database-specific concerns

Repositories MUST NOT contain business logic.

---

# Principle 7 — Application Services Own Business Logic

Application services MUST contain business rules.

Example:

```text
ProductService
CartService
CustomerService
CheckoutService
OrderService
InventoryService
PaymentService
```

The service layer may coordinate multiple repositories.

Example:

```text
CheckoutService
       ↓
CartRepository
       ↓
InventoryRepository
       ↓
OrderRepository
       ↓
PaymentRepository
```

Business rules MUST NOT be placed inside repositories.

---

# Principle 8 — Stored Procedures Own Database Logic

Stored procedures are responsible for database-side operations such as:

* SELECT
* INSERT
* UPDATE
* DELETE
* Joins
* Filtering
* Sorting
* Pagination queries
* Database transactions
* Database-level validation
* Data aggregation

Example stored procedures:

```text
dbo.Product_GetById
dbo.Product_Search
dbo.Product_GetPaged
dbo.Category_GetAll

dbo.Cart_Get
dbo.Cart_AddItem
dbo.Cart_UpdateItem
dbo.Cart_RemoveItem
dbo.Cart_Clear

dbo.Customer_Create
dbo.Customer_GetByEmail
dbo.Customer_GetById
dbo.Customer_Update

dbo.Address_Create
dbo.Address_GetByCustomer
dbo.Address_Update
dbo.Address_Delete

dbo.Order_Create
dbo.Order_GetById
dbo.Order_GetByCustomer
dbo.Order_UpdateStatus

dbo.Inventory_Get
dbo.Inventory_Update
```

Stored procedures MUST be version controlled alongside the application.

---

# Principle 9 — No Entity Framework

Entity Framework MUST NOT be introduced.

The following packages MUST NOT be used for application data access:

```text
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Relational
```

The application MUST NOT use:

```text
DbContext
DbSet<T>
EF Core migrations
EF Core LINQ queries
```

---

# Principle 10 — No Code-Generated Database Queries

Database queries MUST NOT be dynamically generated by an ORM or query builder.

Dapper is responsible only for:

```text
Parameter Passing
Stored Procedure Execution
Result Mapping
```

SQL execution logic belongs to SQL Server stored procedures.

---

# Principle 11 — Stored Procedure Naming Convention

Stored procedures MUST use a consistent naming convention.

Recommended format:

```text
dbo.<Module>_<Operation>
```

Examples:

```text
dbo.Product_GetById
dbo.Product_Search
dbo.Product_GetPaged

dbo.Cart_GetByCustomer
dbo.Cart_AddItem
dbo.Cart_UpdateItem
dbo.Cart_RemoveItem

dbo.Customer_Create
dbo.Customer_GetByEmail

dbo.Order_Create
dbo.Order_GetById
dbo.Order_GetByCustomer
```

The naming convention MUST be applied consistently.

---

# Principle 12 — Stored Procedure Parameters

Stored procedure parameters MUST be explicitly defined.

Avoid ambiguous parameter names.

Example:

```text
@ProductId
@CustomerId
@Quantity
@PageNumber
@PageSize
```

Application code MUST pass parameters through Dapper parameter objects.

---

# Principle 13 — Database Transactions

Transactions involving multiple related database operations MUST be handled appropriately.

For example, order creation may require:

```text
Create Order
     ↓
Create Order Items
     ↓
Update Inventory
     ↓
Clear Cart
```

Where these operations need to be atomic, the transaction SHOULD be managed at the database/stored-procedure level or through an explicitly designed Dapper transaction boundary.

The implementation MUST ensure that partial order creation cannot leave the database in an inconsistent state.

---

# Principle 14 — Database Schema Changes

Database schema changes MUST be version controlled.

Database changes include:

```text
Tables
Indexes
Constraints
Stored Procedures
Views
Functions
```

Schema deployment scripts MUST be maintained as part of the source code.

Entity Framework migrations MUST NOT be used.

---

# Principle 15 — Database Constraints

Important data integrity rules MUST be enforced by SQL Server where appropriate.

Examples:

```text
Unique SKU
Unique Customer Email
Unique Order Number
Foreign Keys
Non-negative quantities
Required fields
```

Application validation is still required, but database constraints provide the final data-integrity boundary.

---

# Principle 16 — Pagination Must Be Performed in SQL Server

Pagination MUST be performed by SQL Server.

The application MUST NOT:

```text
Load all products
      ↓
Dapper
      ↓
C#
      ↓
Skip()
Take()
```

Instead:

```text
Blazor
   ↓
ProductService
   ↓
ProductRepository
   ↓
Dapper
   ↓
dbo.Product_GetPaged
   ↓
SQL Server
   ↓
Only requested records
```

The stored procedure MUST perform the pagination.

---

# Principle 17 — Filtering and Sorting Must Be Performed in SQL Server

Product filtering and sorting MUST be performed by SQL Server.

The application MUST NOT load a large dataset and then perform filtering in memory.

For example:

```text
Category
Price Range
Availability
Search
Sort
Pagination
```

should be handled by the appropriate stored procedure.

---

# Principle 18 — SQL Server Is the Source of Truth

SQL Server is the authoritative source for persistent application data.

The application MUST NOT maintain a separate competing source of truth for:

```text
Products
Prices
Inventory
Customers
Orders
Payments
```

Caching MAY be introduced later, but cached data MUST NOT become the authoritative source.

---

# Principle 19 — Security and SQL Parameters

All stored procedure parameters MUST be passed through Dapper parameterization.

The application MUST NOT construct SQL statements by string concatenation.

Even though application SQL is prohibited, stored procedures MUST still validate and safely handle their parameters.

---

# Principle 20 — Database Connection Management

Database connections MUST be managed efficiently.

The application SHOULD:

* Create connections when required
* Execute the required stored procedure
* Dispose connections promptly
* Avoid unnecessarily long-lived connections

Connection strings MUST come from configuration/secrets management.

Connection strings MUST NOT be hardcoded in source code.

---

# Principle 21 — Data Access Testing

Repositories and stored procedures MUST be tested appropriately.

Important database operations SHOULD have integration tests covering:

```text
Product retrieval
Product search
Product pagination
Cart creation
Cart modification
Customer creation
Authentication lookup
Order creation
Inventory update
Order retrieval
```

Tests MUST verify both:

```text
Dapper mapping
Stored Procedure behavior
```

---

# Principle 22 — Architecture

The final architecture MUST follow:

```text
┌──────────────────────────────┐
│        Blazor Server         │
│          MudBlazor           │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│     Application Services     │
│                              │
│ Product / Cart / Auth /      │
│ Checkout / Order / Payment   │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│       Repository Layer       │
│                              │
│ Dapper                       │
└──────────────┬───────────────┘
               │
               ▼
┌──────────────────────────────┐
│      Stored Procedures       │
│         SQL Server           │
└──────────────────────────────┘
```

No layer may bypass the layer immediately below it.

---

# Principle 23 — Technology Stack

The mandatory technology stack is:

```text
Frontend:
    Blazor Server
    MudBlazor

Application:
    C#
    .NET

Data Access:
    Dapper ONLY

Database:
    Microsoft SQL Server ONLY

Database Operations:
    Stored Procedures ONLY

Authentication:
    Custom Authentication

Architecture:
    Modular Monolith

Development:
    Spec Kit
```

---

# Principle 24 — Prohibited Technologies

The following are explicitly prohibited unless this constitution is formally amended:

```text
Entity Framework Core
Entity Framework
LINQ-to-SQL
NHibernate
Other ORMs
Raw SQL from C#
SQL query builders
Direct SQL statements from application code
Database access from Razor components
MongoDB as primary database
PostgreSQL as primary database
SQLite as production database
```

---

# Principle 25 — Five-Stage Development

The application MUST be developed in five stages.

```text
Stage 1
Product Catalog
       ↓
Stage 2
Shopping Cart + Guest Cart
       ↓
Stage 3
Custom Authentication + Customer Account
       ↓
Stage 4
Checkout + Orders + Inventory + Administration
       ↓
Stage 5
Payments + Advanced E-Commerce + Production Readiness
```

Each stage MUST follow:

```text
/specify
    ↓
/clarify
    ↓
/plan
    ↓
/tasks
    ↓
Implementation
    ↓
Testing
    ↓
Validation
```

---

# Principle 26 — No Unnecessary Architecture

The application MUST remain a modular monolith.

The project MUST NOT introduce:

```text
Microservices
Message Brokers
CQRS
Event Sourcing
Distributed Systems
Multiple Databases
```

unless a future requirement explicitly justifies the additional complexity.

---

# Principle 27 — Definition of Done

A feature is complete only when:

* Specification is complete.
* Implementation is complete.
* Database changes are complete.
* Stored procedures are implemented.
* Dapper integration is implemented.
* Validation exists.
* Authorization exists where required.
* Unit tests exist where appropriate.
* Database integration tests exist where appropriate.
* Application compiles.
* Tests pass.
* Existing functionality continues to work.

---

# Principle 28 — Error-Free and Warning-Free Builds

Every feature MUST build without errors and without warnings after implementation.

The application MUST compile successfully before a feature can be considered complete.
Any compiler warnings or analyzer warnings (such as MudBlazor component warnings or obsolete method usages) MUST be resolved before moving to the next task.
Suppressing warnings is generally discouraged unless explicitly justified in the code.

---

# Principle 29 — AVIF Image Format Only

All images used within the application MUST be in the **AVIF** file format.

The application MUST NOT use other image formats (such as JPEG, PNG, WebP, or GIF) for primary visual content unless strictly required by a specific technical limitation. This ensures optimal compression and modern web performance.

---

# Governance

This constitution is mandatory for all development.

The following are considered **non-negotiable architectural constraints**:

```text
1. Blazor Server
2. MudBlazor
3. C# / .NET
4. SQL Server
5. Dapper ONLY
6. Stored Procedures ONLY
7. Custom Authentication
8. Modular Monolith
9. Spec Kit development
10. AVIF File Format Only
```

Any implementation that violates these constraints MUST be rejected or redesigned.

A change to any of these constraints requires an explicit constitution amendment and architectural justification.
