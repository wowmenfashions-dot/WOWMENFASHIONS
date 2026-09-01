# Quickstart Validation Guide: admin-product-creation

## Prerequisites
- WOWMENFASHIONS backend running locally (`dotnet run`)
- Access to the local SQL database to verify inserts.

## Validation Steps

1. **Access Admin Form**: Navigate to `http://localhost:5124/admin/products/new`.
2. **Fill Details**: 
   - Enter Name: "Test Admin Product"
   - Category: "mens"
   - Price: 199.99
3. **Add Colors**: 
   - Click "Add Color"
   - Enter Color: "Blue"
   - Image URL: "/images/blue.avif"
4. **Submit**: Click "Create Product".
5. **Verify**: Ensure the UI shows a success message. Navigate to the store homepage or mens category and confirm "Test Admin Product" is displayed. Ensure you can view it and switch to the "Blue" color variant.
