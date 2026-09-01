# Research Notes: Homepage & Order Workflow Redesign

## Unknowns Resolved

1. **How to implement image zoom in MudBlazor?**
   - *Decision*: We can use CSS-based hover zoom on the image (e.g. `transform: scale(1.5)` on hover within an `overflow: hidden` container), as it avoids heavy JS libraries and works well in Blazor Server.
   
2. **Order Status Workflow State Machine**:
   - *Decision*: The status strings will follow a strict transition: `Placed` -> `Processing` -> `Pickup` -> `In Transit` -> `Delivered`.

3. **Homepage Layout and Custom Fonts**:
   - *Decision*: We will use MudBlazor's grid system (`MudGrid`, `MudItem`) to match the visual layout shown in the screenshot. Banners will be static `<MudImage>` or `<img>` elements pointing to `.avif` images (respecting Principle 29).

4. **Courier Tracking Integration**:
   - *Decision*: We will add `CourierName`, `TrackingNumber`, and `TrackingUrl` as columns in the `Orders` table. The `Orders.razor` UI will render a clickable link to `TrackingUrl` if it is present.
