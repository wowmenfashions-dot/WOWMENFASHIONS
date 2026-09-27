# Product Reviews Tasks

## Phase 1: Database & Entities
- [ ] 1. Create `035_create_reviews_table.sql` with `Reviews` table and run it.
- [ ] 2. Create `wowmenfashions/Data/Entities/Review.cs` entity class.
- [ ] 3. Update `ApplicationDbContext.cs` to include `DbSet<Review> Reviews`.

## Phase 2: Backend Services
- [ ] 4. Create `wowmenfashions/Models/ReviewDto.cs`.
- [ ] 5. Create `wowmenfashions/Services/IReviewService.cs` and `wowmenfashions/Services/ReviewService.cs`.
- [ ] 6. Register `IReviewService` in `Program.cs`.

## Phase 3: Frontend - Customer View
- [ ] 7. Update `ProductDetails.razor` to include the "Reviews" section, displaying a list of reviews.
- [ ] 8. Update `ProductDetails.razor` to allow authenticated users to submit/edit/delete a review.
- [ ] 9. Update `ProductCard.razor` and `ProductDetails.razor` to display the actual average rating instead of the mocked hardcoded values.

## Phase 4: Frontend - Admin View
- [ ] 10. Create `wowmenfashions/Components/Pages/AdminReviews.razor`.
- [ ] 11. Add AdminReviews to the navigation menu if needed, or ensure route is accessible (`/admin/reviews`).
- [ ] 12. Implement admin CRUD functions (delete, edit, reply).
