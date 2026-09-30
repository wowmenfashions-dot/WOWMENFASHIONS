# Data Model: Order Email Notifications (037)

**Feature**: 037-email-notifications
**Date**: 2026-09-27

---

## New Entities

### EmailNotificationLog

Tracks every email notification attempt for an order. Used for admin visibility and operational auditing.

| Column | Type | Nullable | Description |
|--------|------|----------|-------------|
| `Id` | `int IDENTITY(1,1)` | No | Primary key |
| `OrderId` | `int` | No | FK → `Orders.Id` |
| `NotificationType` | `nvarchar(50)` | No | `'OrderConfirmation'` or `'StatusUpdate'` |
| `RecipientEmail` | `nvarchar(256)` | No | Customer email address |
| `Status` | `nvarchar(20)` | No | `'Sent'` or `'Failed'` |
| `SentAt` | `datetime2(7)` | No | Timestamp of the attempt |
| `ErrorMessage` | `nvarchar(max)` | Yes | Error details if failed |
| `OrderStatus` | `nvarchar(50)` | Yes | The order status at time of sending (for status update emails) |

**Relationships**:
- `EmailNotificationLog.OrderId` → `Orders.Id` (no cascade delete, keep log even if order deleted)

---

## Existing Entities (No Schema Changes Required)

### Orders
No changes to the schema required. Email sending will be driven by events in the service layer (after order creation and after status update), not by additional columns on the `Orders` table.

### OrderItems
No changes required. Order confirmation emails will query `OrderItem_GetByOrderId` stored procedure to get item details.

---

## New Service Interfaces

### `IEmailNotificationService`

```csharp
public interface IEmailNotificationService
{
    Task SendOrderConfirmationAsync(int orderId);
    Task SendOrderStatusUpdateAsync(int orderId, string newStatus);
}
```

---

## New Configuration Sections

Added to `appsettings.json` (values overridden by Azure App Service Application Settings in production):

```json
{
  "AzureCommunicationServices": {
    "ConnectionString": "",
    "SenderAddress": "noreply@wowmenfashions.azurecomm.net"
  }
}
```

---

## SQL Migration Required

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
GO
```
