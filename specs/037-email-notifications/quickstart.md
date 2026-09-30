# Quickstart & Validation Guide: Order Email Notifications (037)

**Feature**: 037-email-notifications
**Date**: 2026-09-27

---

## Prerequisites

### One-time Azure Portal Setup (must be done before testing)

1. **Create Azure Communication Services resource** in the Azure Portal (same subscription as your App Service).
2. **Create Email Communication Service** inside the ACS resource.
3. **Configure a sender domain**:
   - Option A (quickest): Use the free Azure-managed domain (e.g., `donotreply@<hash>.azurecomm.net`)
   - Option B: Connect your custom domain `noreply@wowmenfashions.com` (requires DNS verification)
4. **Copy the ACS Connection String** from the ACS resource → Keys tab.
5. **Add App Service Application Settings** (Azure Portal → App Service → Configuration → Application Settings):
   - `AzureCommunicationServices__ConnectionString` = `<your-connection-string>`
   - `AzureCommunicationServices__SenderAddress` = `<your-sender-address>`
6. **Run the DB migration** (Azure Query Editor):

```sql
CREATE TABLE [dbo].[EmailNotificationLogs](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [OrderId] [int] NOT NULL,
    [NotificationType] [nvarchar](50) NOT NULL,
    [RecipientEmail] [nvarchar](256) NOT NULL,
    [Status] [nvarchar](20) NOT NULL,
    [SentAt] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
    [ErrorMessage] [nvarchar](max) NULL,
    [OrderStatus] [nvarchar](50) NULL,
    CONSTRAINT [PK_EmailNotificationLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);
```

---

## Local Development Setup

Add the following to `appsettings.Development.json` (do NOT commit real credentials):

```json
{
  "AzureCommunicationServices": {
    "ConnectionString": "<your-acs-connection-string>",
    "SenderAddress": "donotreply@<hash>.azurecomm.net"
  }
}
```

Install the NuGet package:
```bash
dotnet add package Azure.Communication.Email
```

---

## Validation Scenario 1: Order Confirmation Email

**Goal**: Verify a customer receives a confirmation email after successful checkout.

**Steps**:
1. Run the app locally or use the deployed Azure site.
2. Add a product to the cart.
3. Proceed through checkout and complete payment via Razorpay.
4. Check the customer's inbox (the email used during registration/checkout).

**Expected**:
- Email arrives within ~60 seconds.
- Subject line: `Your WOWMENFASHIONS order #<OrderId> has been confirmed!`
- Email body contains: order ID, list of items, quantities, prices, total, and shipping address.

**Verify in DB**:
```sql
SELECT TOP 1 * FROM EmailNotificationLogs ORDER BY SentAt DESC;
-- Status should be 'Sent', NotificationType = 'OrderConfirmation'
```

---

## Validation Scenario 2: Order Status Update Email

**Goal**: Verify a customer receives a status update email when admin changes the order status.

**Steps**:
1. Log in to the Admin Dashboard.
2. Navigate to **Orders**.
3. Find a test order and update its status (e.g., from `Pending` → `Shipped`) and enter tracking details.
4. Click **Save/Update**.
5. Check the customer's inbox.

**Expected**:
- Email arrives within ~60 seconds.
- For "Shipped": Subject: `Your WOWMENFASHIONS order #<OrderId> has been shipped!`
- Email includes courier name, tracking number, and a clickable tracking link.
- For "Delivered": Email thanks customer and invites them to leave a review.

**Verify in DB**:
```sql
SELECT TOP 1 * FROM EmailNotificationLogs ORDER BY SentAt DESC;
-- Status should be 'Sent', NotificationType = 'StatusUpdate', OrderStatus = 'Shipped'
```

---

## Validation Scenario 3: Email Failure Handling

**Goal**: Verify order creation succeeds even when email fails.

**Steps**:
1. Temporarily set `AzureCommunicationServices__ConnectionString` to an invalid value in local `appsettings.Development.json`.
2. Place a test order.

**Expected**:
- Order is created successfully in the database.
- No error is shown to the user on the checkout page.
- In DB: `EmailNotificationLogs` has a row with `Status = 'Failed'` and an `ErrorMessage`.
- Hangfire dashboard shows the job as failed (with retry scheduled).

---

## Hangfire Dashboard

To monitor background email jobs, visit:
- **Local**: `http://localhost:<port>/hangfire`
- **Azure**: `https://wowmenfashions-app-*.azurewebsites.net/hangfire`

Failed email jobs appear under "Failed" tab and will be retried automatically by Hangfire.
