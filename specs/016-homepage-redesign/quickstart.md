# Quickstart: Homepage Redesign

## Setup Prerequisites

Ensure you have run the database seeding scripts from previous features to populate categories and product data if necessary (though the carousel uses dummy images, category links must point to valid routes).

## Running the Application

1. Start the application:
   ```powershell
   .\run.ps1
   ```
2. Navigate to `http://localhost:5124` in your browser.

## Validation Scenarios

### Scenario 1: Desktop Viewport
- Resize your browser window to a standard desktop size (e.g., 1920x1080).
- **Expected Outcome**: The homepage should occupy exactly 100% of the viewport height. There should be NO vertical scrollbar. The MudCarousel should be visible and cycling images automatically.

### Scenario 2: Mobile Viewport
- Open the browser developer tools and toggle device emulation to a small mobile device (e.g., iPhone SE).
- **Expected Outcome**: The homepage elements will stack. Because the screen height is small, a vertical scrollbar WILL appear, allowing you to scroll down and view all categories.
