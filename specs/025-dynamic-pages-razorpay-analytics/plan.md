# Implementation Plan: Dynamic Pages, Razorpay, & Analytics

## 1. Database Schema (Dynamic Pages)
- **New Table**: `PageContents` (`Id`, `PageName`, `ContentHtml`, `LastUpdated`).
- **Stored Procs**: `sp_PageContents_GetByName`, `sp_PageContents_Update`.

## 2. Data Access & Services
- **Models**: `PageContent.cs`.
- **Services**: `IPageContentService`, `PageContentService`. Register in `Program.cs`.

## 3. Dynamic Pages UI
- **Admin**: Create `AdminPageContent.razor` to select a page and edit its HTML in a `MudTextField` (Lines="15").
- **Public**: Update `AboutUs.razor` and `ContactUs.razor` to load the HTML from the service and render using `@((MarkupString)ContentHtml)`. Fallback to current HTML if DB is empty.

## 4. Razorpay Integration
- Install `Razorpay` NuGet package (if backend order creation is needed) or just use the frontend JS integration. We will create a `RazorpayService` to generate the backend Order ID using Razorpay API.
- Add placeholder Razorpay Key & Secret to `appsettings.json`.
- In `Checkout.razor`, add a "Pay with Razorpay" button. Inject a JS interop file (`razorpay.js`) to open the Razorpay checkout modal with the order details.
- Upon success, call backend to finalize the `Order`.

## 5. Google Analytics
- Add the `gtag.js` snippet to `App.razor` `<head>`. Use a placeholder ID like `G-XXXXXXXXXX`.

## Verification
- Run DB scripts. Build and launch. Edit AboutUs in Admin. Verify on public page. Check source for GA script.
