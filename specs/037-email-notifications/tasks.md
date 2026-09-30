# Tasks: Order Email Notifications (037)

**Branch**: `037-email-notifications` | **Feature**: [spec.md](spec.md) | **Plan**: [plan.md](plan.md)

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Install the NuGet package, run the DB migration, and configure Azure Communication Services settings.

- [X] T001 Install `Azure.Communication.Email` NuGet package: run `dotnet add package Azure.Communication.Email` in `wowmenfashions/`
- [X] T002 Run DB migration SQL against Azure and local databases — execute the `EmailNotificationLogs` table script from [data-model.md](data-model.md)
- [X] T003 [P] Add `AzureCommunicationServices` configuration section to `wowmenfashions/appsettings.json` (empty values — real values go in App Service Settings and local `appsettings.Development.json`)
- [ ] T004 [P] Add ACS settings to `wowmenfashions/appsettings.Development.json` (local dev secrets, gitignored) with actual ACS connection string and sender address

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Create the core email service interface, implementation skeleton, and DI registration. All user story phases depend on this.

> **⚠️ CRITICAL**: No user story work can begin until this phase is complete.

- [X] T005 Create `wowmenfashions/Services/IEmailNotificationService.cs` — define interface with two methods: `Task SendOrderConfirmationAsync(int orderId)` and `Task SendOrderStatusUpdateAsync(int orderId, string newStatus)`
- [X] T006 Create `wowmenfashions/Services/EmailNotificationService.cs` — implement `IEmailNotificationService`, inject `IConfiguration`, `ISqlConnectionFactory`, `ILogger<EmailNotificationService>` and `IEncryptionService`; initialize `EmailClient` from ACS connection string in constructor
- [X] T007 Create `wowmenfashions/Data/EmailNotificationLogRepository.cs` — implement a `LogAsync(int orderId, string notificationType, string recipientEmail, string status, string? orderStatus, string? errorMessage)` method that inserts a row into `EmailNotificationLogs` using Dapper
- [X] T008 Register `IEmailNotificationService` → `EmailNotificationService` as `Scoped` in `wowmenfashions/Program.cs`; also register `EmailNotificationLogRepository` as `Scoped`

**Checkpoint**: `dotnet build` must succeed before proceeding.

---

## Phase 3: User Story 1 — Order Confirmation Email (Priority: P1) 🏆 MVP

**Goal**: Customer automatically receives a branded HTML confirmation email after a successful Razorpay payment.

**Independent Test**: Place a test order through checkout → verify email arrives within 60 seconds with correct order details. See [quickstart.md](quickstart.md) — Validation Scenario 1.

### Implementation for User Story 1

- [X] T009 [US1] In `wowmenfashions/Services/EmailNotificationService.cs`, implement `SendOrderConfirmationAsync(int orderId)`:
  - Query order details using `dbo.Order_GetAll` filtered by orderId (or add a `dbo.Order_GetById` stored procedure)
  - Query order items using `dbo.OrderItem_GetByOrderId`
  - Decrypt any encrypted PII fields (shipping address) using `IEncryptionService`
  - Build HTML email body using the `BuildOrderConfirmationHtml()` private method (see T010)
  - Call `EmailClient.SendAsync(WaitUntil.Completed, emailMessage)`
  - On success: call `EmailNotificationLogRepository.LogAsync(..., status: "Sent")`
  - On exception: log to `ILogger`, call `LogAsync(..., status: "Failed", errorMessage: ex.Message)`, do NOT rethrow

- [X] T010 [US1] Add `private string BuildOrderConfirmationHtml(OrderDto order)` method in `EmailNotificationService.cs`:
  - Dark-themed HTML email (background `#1a1a1a`, accent gold `#c9a84c`) consistent with WOWMENFASHIONS brand
  - Include: WOWMENFASHIONS header/logo text, order ID, itemised product table (name, qty, price), total amount, shipping address, friendly footer message
  - Subject line: `"Your WOWMENFASHIONS Order #{{orderId}} is Confirmed! 🎉"`

- [X] T011 [US1] Modify `wowmenfashions/Services/CheckoutService.cs` in `CreatePendingOrderAsync()` — after `transaction.Commit()` and before the return statement, add: `BackgroundJob.Enqueue<IEmailNotificationService>(s => s.SendOrderConfirmationAsync(newOrderId));`

**Checkpoint**: Place a test order → email arrives in inbox → `EmailNotificationLogs` shows `Status = 'Sent'`.

---

## Phase 4: User Story 2 — Order Status Update Email (Priority: P2)

**Goal**: Customer automatically receives a status update email whenever an admin updates their order status in the admin dashboard.

**Independent Test**: Update any order's status in the admin panel → verify customer receives appropriate email within 60 seconds. See [quickstart.md](quickstart.md) — Validation Scenario 2.

### Implementation for User Story 2

- [X] T012 [US2] In `wowmenfashions/Services/EmailNotificationService.cs`, implement `SendOrderStatusUpdateAsync(int orderId, string newStatus)`:
  - Query order details (including customer email) from the database
  - Decrypt PII fields using `IEncryptionService`
  - Determine the correct email template based on `newStatus` (Shipped / Delivered / Cancelled / other)
  - For "Shipped": include `CourierName`, `TrackingNumber`, `TrackingUrl` from the order record
  - Build HTML email using `BuildOrderStatusUpdateHtml()` private method (see T013)
  - Send via `EmailClient.SendAsync(WaitUntil.Completed, emailMessage)`
  - Log result to `EmailNotificationLogs` with `OrderStatus = newStatus`

- [X] T013 [US2] Add `private string BuildOrderStatusUpdateHtml(OrderDto order, string status)` method in `EmailNotificationService.cs`:
  - Branded WOWMENFASHIONS template consistent with confirmation email
  - Subject lines per status:
    - Shipped: `"Your WOWMENFASHIONS Order #{{id}} has been Shipped! 📦"`
    - Delivered: `"Your WOWMENFASHIONS Order #{{id}} has been Delivered! 🎉"`
    - Cancelled: `"Update on your WOWMENFASHIONS Order #{{id}}"`
    - Other: `"Update on your WOWMENFASHIONS Order #{{id}} — {{status}}"`
  - For Shipped: include tracking details section with clickable tracking URL
  - For Delivered: include a "Leave a Review" call-to-action link

- [X] T014 [US2] Modify `wowmenfashions/Services/OrderService.cs` in `UpdateOrderStatusAsync()` — after the `ExecuteAsync` call, add: `BackgroundJob.Enqueue<IEmailNotificationService>(s => s.SendOrderStatusUpdateAsync(orderId, status));`

- [X] T015 [US2] Ensure the `OrderDto` model includes `TrackingNumber`, `TrackingUrl`, `CourierName`, and `CustomerEmail` fields — check `wowmenfashions/Models/OrderDto.cs` and add any missing properties; verify `dbo.Order_GetAll` stored procedure SELECTs these columns

**Checkpoint**: Update an order to "Shipped" in admin panel → customer receives email with tracking info → `EmailNotificationLogs` shows `Status = 'Sent'`, `OrderStatus = 'Shipped'`.

---

## Phase 5: User Story 3 — Admin Email Visibility (Priority: P3)

**Goal**: Admin can see whether a confirmation email was sent for each order in the admin order management view.

**Independent Test**: After placing an order, open the admin Orders page → find the order → verify a sent/failed email indicator is visible.

### Implementation for User Story 3

- [X] T016 [P] [US3] Add an `IEmailNotificationLogRepository` interface and `EmailNotificationLogRepository` implementation in `wowmenfashions/Data/EmailNotificationLogRepository.cs` with a method `Task<IEnumerable<EmailNotificationLog>> GetByOrderIdAsync(int orderId)` to retrieve logs for a given order

- [X] T017 [P] [US3] Create `wowmenfashions/Models/EmailNotificationLogDto.cs` — DTO with properties: `Id`, `OrderId`, `NotificationType`, `RecipientEmail`, `Status`, `SentAt`, `ErrorMessage`, `OrderStatus`

- [X] T018 [US3] Locate the admin Orders management Razor component (likely `wowmenfashions/Components/Pages/Admin/`) — inject `IEmailNotificationLogRepository` and load email logs for each order being displayed

- [X] T019 [US3] In the admin order detail/list view Razor component, add a small email status badge per order:
  - Green badge "✉ Email Sent" with timestamp if `Status = 'Sent'`
  - Red badge "✉ Email Failed" if `Status = 'Failed'`
  - Grey badge "✉ No Email" if no log exists for the order

**Checkpoint**: Admin panel shows email status badges per order correctly.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Final hardening, error resilience, and documentation updates.

- [X] T020 [P] Add `appsettings.Development.json` to `.gitignore` if not already present — prevents accidental commit of ACS credentials
- [X] T021 Add a null/empty check on `AzureCommunicationServices__ConnectionString` in `EmailNotificationService.cs` — if missing/empty, log a warning and return early rather than throwing an unhandled exception at startup
- [X] T022 Add the `EmailNotificationLogs` migration SQL to a new file `036_email_notification_logs.sql` in the project root (alongside the other migration scripts like `035_azure_migration.sql`), so it can be run consistently
- [X] T023 [P] Update `knowledge/okf.md` to document the new email notification pattern (ACS Email + Hangfire dispatch) as a project-wide pattern for future features
- [ ] T024 Run the full [quickstart.md](quickstart.md) validation end-to-end on the deployed Azure environment and confirm all 3 scenarios pass

---

## Dependencies & Execution Order

### Phase Dependencies

- **Phase 1 (Setup)**: No dependencies — start immediately
- **Phase 2 (Foundational)**: Depends on Phase 1 (NuGet package must be installed)
- **Phase 3 (US1 — Confirmation)**: Depends on Phase 2 — can start once foundational service exists
- **Phase 4 (US2 — Status Update)**: Depends on Phase 2 — independent from Phase 3, can run in parallel with it
- **Phase 5 (US3 — Admin Visibility)**: Depends on Phase 2 — can start once log repository exists (T007)
- **Phase 6 (Polish)**: Depends on Phases 3, 4, 5 all complete

### User Story Dependencies

- **US1 (P1)**: Depends only on Foundational (Phase 2). No dependency on US2 or US3.
- **US2 (P2)**: Depends only on Foundational (Phase 2). Independent of US1.
- **US3 (P3)**: Depends on Phase 2 (specifically T007 — log repository). Independent of US1/US2.

### Within Each User Story

- Service implementation (T009/T012) depends on interface (T005) and log repository (T007)
- HTML builder methods (T010/T013) can be written in parallel with service logic
- Hangfire dispatch (T011/T014) depends on service implementation being complete

### Parallel Opportunities

- T003 and T004 (config files) — parallel
- T005 and T007 (interface + log repo) — parallel
- Once Phase 2 complete: US1, US2, US3 can all start simultaneously
- T016 and T017 (log repo interface + DTO) — parallel

---

## Parallel Example: Phase 2

```
Developer A:
  T005 → Create IEmailNotificationService interface
  T006 → Implement EmailNotificationService (depends on T005)

Developer B (parallel):
  T007 → Create EmailNotificationLogRepository
  T008 → Register DI in Program.cs (depends on T005, T007)
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup (T001–T004)
2. Complete Phase 2: Foundational (T005–T008)
3. Complete Phase 3: US1 — Order Confirmation (T009–T011)
4. **STOP and VALIDATE**: Place a test order → confirm email received
5. Deploy `release` branch to Azure → done!

### Incremental Delivery

1. Setup + Foundational → build passes ✅
2. US1 (Confirmation Email) → test independently → deploy (MVP!)
3. US2 (Status Update Email) → test independently → deploy
4. US3 (Admin Visibility) → test → deploy
5. Polish → final deployment

---

## Notes

- `[P]` tasks touch different files with no cross-dependencies — safe to run in parallel
- `[US?]` labels trace each task back to its user story for traceability
- Azure App Service Application Settings **override** `appsettings.json` values — never commit real credentials
- Hangfire retries failed jobs automatically — email failures are recoverable without code changes
- The `EmailNotificationService` must **never throw** — all exceptions must be caught, logged, and swallowed
