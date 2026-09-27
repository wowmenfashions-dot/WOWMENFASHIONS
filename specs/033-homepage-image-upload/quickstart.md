# Quickstart Validation: Homepage Image Upload

To validate this feature end-to-end:

1. **Start the application** and log in as an administrator.
2. Navigate to `http://localhost:5124/admin/homepage`.
3. In the "Carousel Images" section, verify that the text inputs for URLs have been replaced with a file upload button.
4. **Upload a JPG/PNG image**. 
5. Click **Save Carousel**.
6. Navigate to the storefront homepage (`http://localhost:5124/`).
7. Verify the uploaded image appears in the carousel.
8. Right-click the image and select "Open image in new tab". Ensure the served URL is returning `image/avif` content (this can be checked in browser dev tools -> Network tab).
9. Refresh the homepage and verify in the application logs that **0 database queries** were made for the images on the subsequent load, confirming the in-memory cache is working.
