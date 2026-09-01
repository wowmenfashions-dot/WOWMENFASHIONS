# Tasks: dynamic-homepage

**Feature Branch**: `022-dynamic-homepage`
**Spec**: [spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/022-dynamic-homepage/spec.md)
**Plan**: [plan.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/022-dynamic-homepage/plan.md)

---

## Phase 1: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

- [X] T001 [P] Create `HomepageConfig_Create.sql` to define the `HomepageConfig` table structure
- [X] T002 [P] Implement `HomepageConfigRepository.cs` in `wowmenfashions/Data/` to handle data access
- [X] T003 Implement `HomepageConfigService.cs` in `wowmenfashions/Services/` that uses the repository
- [X] T004 Update `Program.cs` or Service Extensions to register `HomepageConfigRepository` and `HomepageConfigService`

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 2: User Story 1 - Admin Configuration UI (Priority: P1) 🎯 MVP

**Goal**: The admin logs into the system and navigates to the Homepage Configuration area to update the layout components.

**Independent Test**: Navigate to `/admin/homepage` and successfully save text/images for the carousel and announcement.

### Implementation for User Story 1

- [X] T005 [P] [US1] Create empty `AdminHomepageConfig.razor` in `wowmenfashions/Components/Pages/` with `@attribute [Authorize(Roles = "Admin")]`
- [X] T006 [US1] Build the MudBlazor UI in `AdminHomepageConfig.razor` for Announcement text input
- [X] T007 [US1] Build the MudBlazor UI in `AdminHomepageConfig.razor` for Carousel Image list management
- [X] T008 [US1] Wire the "Save" button to `HomepageConfigService` to persist changes
- [X] T009 [P] [US1] Add a navigation link to `/admin/homepage` in `NavMenu.razor`

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 3: User Story 2 - Public Homepage Binding (Priority: P2)

**Goal**: A customer visits the store homepage and sees the active announcement and the latest configured carousel slides exactly as defined by the Admin.

**Independent Test**: Visit the public home page and observe that it matches the latest configurations saved in the Admin view.

### Implementation for User Story 2

- [X] T010 [P] [US2] Modify `Home.razor` to inject `HomepageConfigService` and load configuration in `OnInitializedAsync`
- [X] T011 [US2] Refactor the hardcoded Announcement Bar in `Home.razor` to bind to dynamic data
- [X] T012 [US2] Refactor the hardcoded Carousel in `Home.razor` to iterate over dynamic data

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 4: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [X] T013 Code cleanup and refactoring
- [X] T014 Run quickstart.md validation
