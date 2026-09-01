# Quickstart Validation Guide: dynamic-homepage

## Prerequisites
- Application running locally (`dotnet run`).
- Admin access to the portal.

## Validation Steps

1. **Access Settings**: Login as Admin and navigate to `http://localhost:5124/admin/homepage`.
2. **Modify Announcement**:
   - Change the announcement text to "Mega Sale: 50% Off Everything!"
   - Save.
3. **Modify Carousel**:
   - Add a new image URL to the carousel configuration.
   - Save.
4. **Verify Live Updates**:
   - Navigate to `http://localhost:5124/`.
   - Ensure the announcement bar at the top displays the new text.
   - Ensure the carousel includes the newly added image URL.
