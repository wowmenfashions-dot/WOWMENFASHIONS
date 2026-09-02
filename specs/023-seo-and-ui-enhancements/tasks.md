# Implementation Tasks: SEO and UI Enhancements

**Branch**: `[023-seo-and-ui-enhancements]` | **Date**: 2026-09-01
**Spec**: [spec.md](./spec.md) | **Plan**: [plan.md](./plan.md)

============================================================================

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Initialize tasks.md document

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**CRITICAL**: No user story work can begin until this phase is complete

- [x] T002 Download the new logo and save to `wwwroot/logo.png`
- [x] T003 Save the new logo as `wwwroot/favicon.ico`

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Search Engine Visibility (Priority: P1)

**Goal**: Improve site discoverability via Google Search with sitemap, robots.txt, and meta tags.

**Independent Test**: Verify `/robots.txt` and `/sitemap.xml` return valid content. Check `Home.razor` source for SEO tags.

### Implementation for User Story 1

- [x] T004 [P] [US1] Create `SitemapController` in `wowmenfashions/Controllers/SitemapController.cs`
- [x] T005 [P] [US1] Create `robots.txt` in `wowmenfashions/wwwroot/robots.txt`
- [x] T006 [US1] Add `<HeadContent>` and SEO meta tags to `wowmenfashions/Components/Pages/Home.razor`

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Brand Identity & Logo (Priority: P2)

**Goal**: Display the new WOW-LOGO in the browser tab and app navigation bar.

**Independent Test**: Load the site and visually confirm the favicon and app bar logo.

### Implementation for User Story 2

- [x] T007 [US2] Update `wowmenfashions/Components/Layout/MainLayout.razor` to replace text with `<img src="logo.png">`
- [x] T008 [US2] Ensure `wowmenfashions/Components/App.razor` correctly links to the new favicon.ico (cache busting if necessary)

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - UI Polish & Trust Badges (Priority: P2)

**Goal**: Add Guarantee, Free Delivery, RFID Protection, YKK, and LWG badges below the homepage carousel, and align the cart badge.

**Independent Test**: Visually inspect the homepage layout and add an item to the cart to verify badge alignment.

### Implementation for User Story 3

- [x] T009 [P] [US3] Update CSS in `wowmenfashions/Components/Shared/CartWidget.razor` to precisely align the `MudBadge`.
- [x] T010 [US3] Restructure `wowmenfashions/Components/Pages/Home.razor` to move existing Trust/Delivery banners immediately below the carousel.
- [x] T011 [US3] Enhance the new banner in `wowmenfashions/Components/Pages/Home.razor` with YKK and LWG elements.

**Checkpoint**: All user stories should now be independently functional

---

## Phase N: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T012 Run quickstart.md validation steps manually.
- [x] T013 Compile project (`dotnet build`) to ensure 0 errors or warnings.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 -> P2 -> P3)
- **Polish (Final Phase)**: Depends on all desired user stories being complete
