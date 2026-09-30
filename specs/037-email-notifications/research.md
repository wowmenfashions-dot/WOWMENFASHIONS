# Research: Order Email Notifications (037)

**Feature**: 037-email-notifications
**Date**: 2026-09-27

---

## Decision 1: Email Delivery Provider

**Decision**: Azure Communication Services (ACS) Email via `Azure.Communication.Email` NuGet package.

**Rationale**: The application is already deployed on Azure (App Service + Azure SQL). ACS Email is the native Azure-integrated email service, consistent with the existing infrastructure. It requires no third-party dependencies or external accounts, and the connection string can be stored securely in Azure App Service environment variables (which are already used for the DB connection string).

**Alternatives considered**:
- **SendGrid**: Popular but requires a separate third-party account and API key management.
- **SMTP (System.Net.Mail)**: Simple but lacks reliability, delivery tracking, and Azure-native integration.
- **Mailgun / Postmark**: Good options but add external vendor dependency outside the Azure ecosystem.

---

## Decision 2: Email Sending Architecture (Non-blocking via Hangfire)

**Decision**: Email sending will be dispatched as **Hangfire background jobs** rather than inline `await` calls in the HTTP request pipeline.

**Rationale**: The application already uses Hangfire (visible in the Azure logs for `CartAbandonmentJob`). Dispatching emails as background jobs means:
- An email service outage never blocks order placement.
- Hangfire automatically retries failed jobs.
- No timeout risk from long-running email sends blocking Blazor component rendering.

**Pattern**: In `CheckoutService.CreatePendingOrderAsync()`, after the order is saved, enqueue a Hangfire job: `BackgroundJob.Enqueue<IEmailNotificationService>(s => s.SendOrderConfirmationAsync(orderId))`. The job fetches order details and sends the email.

**Alternatives considered**:
- **Inline `await` in service layer**: Simple but blocks the request and fails the order if email fails.
- **`Task.Run` fire-and-forget**: Loses exceptions, no retry, no monitoring.

---

## Decision 3: Email Template Approach

**Decision**: HTML email templates built as **C# string interpolation methods** inside the `EmailNotificationService`. No external template engine required.

**Rationale**: The project is a Blazor Server app with no existing templating engine. Adding Razor email templates (e.g., via `RazorLight`) adds significant complexity. Given the small number of email types (confirmation + status update), inline HTML string methods are maintainable, deployable without extra build steps, and easy to brand with WOWMENFASHIONS styling.

**Alternatives considered**:
- **RazorLight**: Full Razor templating for emails. Powerful but adds NuGet dependency and build pipeline complexity.
- **Scriban / Fluid**: Lightweight template engines. Unnecessary overhead for 2 email types.
- **Database-stored templates**: Flexible for admins to edit but requires UI, DB table, and caching. Out of scope for this feature.

---

## Decision 4: Email Notification Logging

**Decision**: Use the existing `ErrorLogs` table for email failure logging. Email send success/failure will also be tracked in a new `EmailNotificationLogs` table for admin visibility.

**Rationale**: The existing `ErrorLoggerService` already handles failure logging. A separate lightweight `EmailNotificationLogs` table allows admins to see send history per order without polluting the error log with expected operational records.

**Schema for `EmailNotificationLogs`**:
```
Id (int, PK, identity)
OrderId (int, FK → Orders)
NotificationType (nvarchar(50)) — 'OrderConfirmation' | 'StatusUpdate'
RecipientEmail (nvarchar(256))
Status (nvarchar(20)) — 'Sent' | 'Failed'
SentAt (datetime2)
ErrorMessage (nvarchar(max), nullable)
```

---

## Decision 5: Configuration / Secrets

**Decision**: ACS connection string stored as an **Azure App Service Application Setting** (`AzureCommunicationServices__ConnectionString`), read via `IConfiguration` in the app. Sender address stored as `AzureCommunicationServices__SenderAddress`.

**Rationale**: App Service Application Settings are already used for the SQL connection string. No additional secrets management infrastructure needed.

---

## ACS Email Setup Steps (Pre-Implementation)

Before implementing code, the following one-time Azure Portal setup is needed:

1. Create an **Azure Communication Services** resource in the Azure Portal.
2. Within ACS, create an **Email Communication Service** resource.
3. Configure a **sender domain** — either:
   - Use the free Azure-managed domain (e.g., `donotreply@<hash>.azurecomm.net`), or
   - Connect a custom domain (e.g., `noreply@wowmenfashions.com`) for branded emails.
4. Copy the **ACS connection string** from the "Keys" tab.
5. Add it as an App Service Application Setting: `AzureCommunicationServices__ConnectionString`.
6. Add the sender address as: `AzureCommunicationServices__SenderAddress`.
