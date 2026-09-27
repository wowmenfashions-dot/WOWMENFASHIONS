# Product Reviews Feature Specification

## Overview
**Feature Name:** Product Reviews
**Description:** A complete end-to-end review system allowing users to leave ratings and text reviews on products, edit/delete their own reviews, and allowing administrators to manage (delete, edit, or reply to) all reviews.

## Target Audience
- **Customers:** To read reviews before purchasing, and to write reviews after purchasing.
- **Administrators:** To moderate reviews, respond to customer feedback, and maintain the quality of the review section.

## User Scenarios
1. **Writing a Review:** A logged-in customer visits a product page, clicks "Write a review", selects a 1-5 star rating, writes a description, and submits it.
2. **Reading Reviews:** Any visitor on the product page sees the average rating, total number of reviews, and a paginated list of all approved reviews.
3. **Editing/Deleting (Customer):** A customer views their past review and can update the text/rating or delete it entirely.
4. **Moderation (Admin):** An admin logs into the dashboard, navigates to "Manage Reviews", and can delete inappropriate reviews, edit them if necessary, and leave an official "Admin Reply" that appears under the customer's review.

## Functional Requirements
- **FR1 (Database):** Create a `Reviews` table linked to `Products` and `Users`, containing Rating, Comment, CreatedDate, and AdminReply fields.
- **FR2 (Product Page UI):** Display aggregate review data (average rating, count) on `ProductCard.razor` and `ProductDetails.razor`.
- **FR3 (Review Submission):** Provide a modal or inline form for authenticated users to submit a review. Prevent multiple reviews from the same user on the same product.
- **FR4 (Admin Dashboard):** Create a new page `AdminReviews.razor` for administrators to view all reviews across the site, with actions to delete, edit, and reply.
- **FR5 (API/Service):** Implement `IReviewService` to handle all CRUD operations safely.

## Success Criteria
- Users can successfully create, read, update, and delete their own reviews.
- Administrators can successfully view all reviews, reply to them, and delete them.
- Average ratings dynamically calculate and update on product cards without significantly slowing down page load times.

## Assumptions & Exclusions
- **Exclusion:** Users do not need to have purchased the item to review it (for simplicity in this phase).
- **Assumption:** Reviews are instantly public (no pre-approval required), but admins can delete them post-publishing.
