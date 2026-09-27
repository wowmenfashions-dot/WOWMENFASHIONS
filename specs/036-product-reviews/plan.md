# Product Reviews Implementation Plan

## Goal Description
Implement an end-to-end Product Reviews system. Users need to be able to leave a 1-5 star rating and comment on products. Users can manage their own reviews (CRUD). Administrators can manage all reviews, including deleting, editing, and replying to customer reviews. Product cards and detail pages must display aggregate ratings.

## Proposed Changes

### Data Layer (`wowmenfashions/Data/Entities/Review.cs`)
Create the new `Review` entity.
- Fields: `Id` (int), `ProductId` (int), `UserId` (string), `Rating` (int 1-5), `Comment` (string), `AdminReply` (string), `CreatedAt` (DateTime), `UpdatedAt` (DateTime).
- Add foreign keys to `Product` and `ApplicationUser`.
- Update `ApplicationDbContext` to include `DbSet<Review> Reviews`.

### Service Layer (`wowmenfashions/Services/IReviewService.cs` & `ReviewService.cs`)
Create the service interface and implementation.
- Methods:
  - `Task<List<ReviewDto>> GetReviewsForProductAsync(int productId);`
  - `Task<ReviewDto?> GetUserReviewForProductAsync(int productId, string userId);`
  - `Task<bool> AddReviewAsync(ReviewDto review);`
  - `Task<bool> UpdateReviewAsync(ReviewDto review);`
  - `Task<bool> DeleteReviewAsync(int reviewId, string userId);`
  - `Task<List<ReviewDto>> GetAllReviewsAdminAsync();` // For Admin
  - `Task<bool> AdminReplyAsync(int reviewId, string reply);`
  - `Task<bool> AdminDeleteReviewAsync(int reviewId);`
- Inject into DI container in `Program.cs`.

### Component Layer (Public UI)
#### [MODIFY] `wowmenfashions/Components/Pages/ProductDetails.razor`
- Add a "Reviews" section below the product description.
- Load reviews via `IReviewService`.
- If the user is logged in, show an "Add Review" form or "Edit Review" form (if they already reviewed).
- Show the list of reviews with the star rating, comment, user name, date, and any admin reply.

#### [MODIFY] `wowmenfashions/Components/Shared/ProductCard.razor`
- Fetch the aggregate rating (average) and total review count to replace the currently mocked `(11 reviews)` hardcoded text.

### Component Layer (Admin UI)
#### [NEW] `wowmenfashions/Components/Pages/AdminReviews.razor`
- Create a new Admin page with a `MudTable` to list all reviews across the site.
- Include actions (edit, delete, reply) per row.
- Restrict access to `[Authorize(Roles = "Admin")]`.

### Database Script
Create `wowmenfashions/Data/Scripts/035_create_reviews_table.sql` to generate the new table in SQL Server.

## Verification Plan
1. Ensure the `035_create_reviews_table.sql` script creates the table correctly.
2. Login as a normal user and leave a review on a product. Verify it shows up instantly and calculates the average on the `ProductCard`.
3. Edit and delete the review as the normal user.
4. Login as an Admin, navigate to the new Admin Reviews page, and reply to a user's review.
5. Check the Product Details page to ensure the Admin Reply is visible.
