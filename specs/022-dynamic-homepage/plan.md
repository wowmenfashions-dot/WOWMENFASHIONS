# Implementation Plan: dynamic-homepage

**Feature Branch**: `[022-dynamic-homepage]`

## User Review Required

> [!IMPORTANT]
> The admin page will be added to `/admin/homepage` to allow managing the homepage content. 
> The configuration will be stored in a new database table `HomepageConfig`.

## Proposed Changes

### Database Layer
#### [NEW] [HomepageConfig_Create.sql](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/HomepageConfig_Create.sql)
- Creates `HomepageConfig` table to store:
  - `Key` (e.g., Carousel, Announcement, Offers)
  - `Value` (JSON or serialized structure for the content)

#### [NEW] [HomepageConfigRepository.cs](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Data/HomepageConfigRepository.cs)
- Manages reading/writing from the `HomepageConfig` table.

### Services Layer
#### [NEW] [HomepageConfigService.cs](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Services/HomepageConfigService.cs)
- Provides high-level methods to get and set the homepage configuration data.

### UI Layer
#### [NEW] [AdminHomepageConfig.razor](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Components/Pages/AdminHomepageConfig.razor)
- A MudBlazor UI under `/admin/homepage` that allows the admin to edit the announcement bar, upload/link carousel images, and specify latest products/offers.

#### [MODIFY] [Home.razor](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Components/Pages/Home.razor)
- Refactor the current hardcoded carousel, announcement, and featured products sections to pull data dynamically from the `HomepageConfigService`.

#### [MODIFY] [NavMenu.razor](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Components/Layout/NavMenu.razor)
- Add a navigation link for `Homepage Settings` under the Admin area.

## Verification Plan

### Manual Verification
- Navigate to `/admin/homepage`.
- Modify the Announcement Text and click Save.
- Navigate to the store homepage (`/`) and verify the announcement updates immediately.
- Modify the Carousel images and save.
- Navigate back to the homepage and verify the carousel renders the new images.
