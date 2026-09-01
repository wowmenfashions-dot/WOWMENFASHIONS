# Implementation Tasks: Custom Authentication and Customer Account

**Feature Directory**: `specs/003-customer-auth`

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Verify `BCrypt.Net-Next` NuGet package is installed in `wowmenfashions/wowmenfashions.csproj`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T002 [P] Create `Customer` entity model in `wowmenfashions/Data/Entities/Customer.cs`
- [x] T003 [P] Create SQL table creation script in `wowmenfashions/Database/Tables/003_CustomerTables.sql`
- [x] T004 Define `ICustomerRepository` interface in `wowmenfashions/Data/ICustomerRepository.cs`
- [x] T005 Define `ICustomerService` interface in `wowmenfashions/Services/ICustomerService.cs`
- [x] T006 Implement `IPasswordHasher` interface and `PasswordHasher` class in `wowmenfashions/Services/PasswordHasher.cs` using BCrypt
- [x] T007 Register `ICustomerRepository`, `ICustomerService`, `IPasswordHasher`, and ASP.NET Core Cookie Authentication in `wowmenfashions/Program.cs`

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Customer Registration (Priority: P1) 🚀 MVP

**Goal**: A guest user wants to register for a new account.

**Independent Test**: Can be fully tested by submitting the registration form with valid data and verifying a new customer record is created.

### Implementation for User Story 1

- [x] T008 [P] [US1] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Customer_Create.sql`
- [x] T009 [P] [US1] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Customer_GetByEmail.sql`
- [x] T010 [US1] Implement `CreateCustomerAsync` and `GetCustomerByEmailAsync` methods in `wowmenfashions/Data/CustomerRepository.cs` using Dapper
- [x] T011 [US1] Implement `RegisterAsync` business logic in `wowmenfashions/Services/CustomerService.cs` (handling password hashing and unique email validation)
- [x] T012 [US1] Create Blazor page `wowmenfashions/Components/Pages/Account/Register.razor` with MudBlazor form

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Customer Login (Priority: P1)

**Goal**: A registered customer wants to log into their account using their email and password.

**Independent Test**: Can be tested by providing correct credentials and verifying the server issues an authenticated session cookie.

### Implementation for User Story 2

- [x] T013 [P] [US2] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Customer_UpdateLastLogin.sql`
- [x] T014 [US2] Implement `UpdateLastLoginAsync` in `wowmenfashions/Data/CustomerRepository.cs`
- [x] T015 [US2] Implement `LoginAsync` business logic in `wowmenfashions/Services/CustomerService.cs` (validating password hash)
- [x] T016 [US2] Create Blazor page `wowmenfashions/Components/Pages/Account/Login.razor` to handle credential submission and issue auth cookies

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - Customer Logout (Priority: P2)

**Goal**: An authenticated customer wants to log out to secure their account.

**Independent Test**: Can be tested by clicking logout and verifying the session cookie is invalidated.

### Implementation for User Story 3

- [x] T017 [US3] Implement logout logic in `wowmenfashions/Components/Pages/Account/Logout.razor` (clearing authentication cookie)
- [x] T018 [US3] Update Navigation Bar in `wowmenfashions/Components/Layout/MainLayout.razor` or `NavMenu.razor` to dynamically show Login/Register or Profile/Logout based on Authentication State

**Checkpoint**: All core auth user stories should now be independently functional

---

## Phase 6: User Story 4 - Forgot Password (Priority: P3)

**Goal**: A customer who forgot their password needs to request a reset link to regain access.

**Independent Test**: Requesting a reset link generates a valid, expiring token.

### Implementation for User Story 4

- [x] T019 [US4] Create Blazor page `wowmenfashions/Components/Pages/Account/ForgotPassword.razor`
- [x] T020 [US4] Implement token generation mock in `CustomerService.cs` (logging out the reset link for now)

---

## Phase 7: User Story 5 - Profile Management (Priority: P3)

**Goal**: An authenticated customer wants to update their personal details.

**Independent Test**: Updating profile fields reflects the new data on subsequent page loads.

### Implementation for User Story 5

- [x] T021 [P] [US5] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Customer_Update.sql`
- [x] T022 [US5] Implement `UpdateCustomerAsync` in `wowmenfashions/Data/CustomerRepository.cs`
- [x] T023 [US5] Implement `UpdateProfileAsync` in `wowmenfashions/Services/CustomerService.cs`
- [x] T024 [US5] Create Blazor page `wowmenfashions/Components/Pages/Account/Profile.razor` with `[Authorize]` attribute

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T025 Execute all generated SQL scripts against the local SQL Server database
- [x] T026 Run the `quickstart.md` validation scenarios manually to confirm complete functionality

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
  - User stories can then proceed sequentially in priority order (P1 -> P2 -> P3)
- **Polish (Final Phase)**: Depends on all desired user stories being complete
