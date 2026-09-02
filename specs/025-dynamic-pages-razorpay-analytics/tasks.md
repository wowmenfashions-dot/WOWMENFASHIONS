# Tasks: Dynamic Pages, Razorpay, Analytics

- [ ] T001: Execute SQL script to create `PageContents` table and stored procedures.
- [ ] T002: Create `PageContent.cs` model and `IPageContentService`/`PageContentService`. Register in `Program.cs`.
- [ ] T003: Create `AdminPageContent.razor` for Admins to edit HTML content. Update `NavMenu.razor` to include it.
- [ ] T004: Refactor `AboutUs.razor` and `ContactUs.razor` to load content dynamically from the DB, with fallback to current content.
- [ ] T005: Add Google Analytics placeholder script to `App.razor`.
- [ ] T006: Add Razorpay API keys to `appsettings.json` and install Razorpay NuGet package.
- [ ] T007: Create `IRazorpayService` and `RazorpayService` to generate Razorpay Order IDs.
- [ ] T008: Add `razorpay.js` to `wwwroot` for frontend JS interop, and update `Checkout.razor` to trigger the Razorpay modal.
