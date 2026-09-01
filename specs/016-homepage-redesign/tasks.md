# Tasks: Homepage Redesign

## Phase 1: Setup

**Purpose**: Project initialization and basic structure

- [x] T001 [P] Locate or create 3-4 dummy images to use within the carousel and save them to `wowmenfashions/wwwroot/images/banners/`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

*(No foundational backend or infrastructure tasks needed as this is a frontend layout feature)*

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Single Screen Viewing (Priority: P1) 🎯 MVP

**Goal**: Users should be able to view the entire homepage content without scrolling vertically on desktop.

**Independent Test**: Can be fully tested by loading the homepage on a standard desktop viewport (e.g. 1080p) and ensuring no vertical scrollbars appear and all key elements are visible.

### Implementation for User Story 1

- [x] T002 [US1] Modify `wowmenfashions/Components/Pages/Home.razor` to use a `100vh` container with `MudCarousel` displaying the dummy images.
- [x] T003 [US1] Update `Home.razor` CSS to hide overflow on desktop screens, while allowing `overflow-y: auto` on mobile screens if the content exceeds viewport height.

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T004 Run manual validation using the scenarios defined in `quickstart.md`
