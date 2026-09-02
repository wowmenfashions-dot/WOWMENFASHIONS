# Implementation Plan: Contact Us Comments & Admin Panel

## 1. Database Schema
- **New Table**: `ContactMessages`
  - `Id` (INT IDENTITY PRIMARY KEY)
  - `Name` (NVARCHAR(100))
  - `Email` (NVARCHAR(100))
  - `Subject` (NVARCHAR(200))
  - `Message` (NVARCHAR(MAX))
  - `IsRead` (BIT, Default 0)
  - `CreatedAt` (DATETIME, Default GETUTCDATE())
- **Stored Procedures**:
  - `sp_ContactMessages_Insert`
  - `sp_ContactMessages_GetAll`
  - `sp_ContactMessages_MarkAsRead`

## 2. Data Access Layer
- Create `ContactMessage` model in `wowmenfashions/Models/ContactMessage.cs`.
- Create `IContactMessageService` and `ContactMessageService` using Dapper to call the stored procedures.
- Register `IContactMessageService` in `Program.cs`.

## 3. UI Updates
- **Contact Us Page**: Update `ContactUs.razor` to include a form on the left ("Leave A Comment") and the information text on the right, matching the screenshot. Use `EditForm` to submit the `ContactMessage` model to the service. Show `MudSnackbar` on success.
- **About Us Page**: Update `AboutUs.razor` to match the screenshot layout (centered titles, specific text blocks, and the 4-column icon layout for features).

## 4. Admin UI
- Create `AdminContactMessages.razor` under `wowmenfashions/Components/Pages/Admin/`.
- Use `MudTabs` for "Unread" and "Read" tabs.
- Use `MudTable` to display the messages in each tab.
- Add a button in the Unread table to call `MarkAsReadAsync` and refresh the table.
- Add link to NavMenu for Admin Dashboard to access this page.

## Verification
- Run DB script. Build the app. Navigate to Contact Us, submit a form, go to Admin, mark as read, verify it moved tabs.
