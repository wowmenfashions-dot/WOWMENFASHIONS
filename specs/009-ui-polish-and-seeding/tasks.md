# Implementation Tasks: UI Polish and Database Seeding Expansion

**Feature**: `specs/009-ui-polish-and-seeding/spec.md`

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure
*No specific setup tasks for this feature.*

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented
*No specific foundational tasks for this feature.*

---

## Phase 3: User Story 1 - Uniform UI Card Redesign (Priority: P1) 🚀 MVP

**Goal**: Fix the broken product grid by enforcing strict, uniform card heights and standardizing the "Add" button location.

**Independent Test**: View the category page on desktop. Verify every product card in a row has exactly the same height regardless of whether its image loads or not.

### Implementation for User Story 1

- [x] T001 [P] [US1] Update `wowmenfashions/Components/Shared/ProductCard.razor` to apply `d-flex flex-column h-100` to the card, strict `height: 200px` with a fallback background to the image container, `mt-auto` to the add button, and increase the title typography size.

**Checkpoint**: At this point, the UI grid should look perfectly aligned and professional.

---

## Phase 4: User Story 2 - Massive Database Seeding (Priority: P2)

**Goal**: Automatically insert 400 total products across 6 categories (including 4 new ones) into the SQL Server database.

**Independent Test**: Run the generated SQL script against the database, then run a `SELECT COUNT(*)` query to ensure 400 products exist.

### Implementation for User Story 2

- [x] T002 [P] [US2] Create `Database/02_SeedDummyData.sql`. This script must define `WHILE` loops to insert the 4 missing categories (Kids, Toddler, Adults, Teenager) and insert exactly 50 products per new category and 100 products for Men's/Women's, generating `.avif` image URLs.

**Checkpoint**: At this point, the application will have massive amounts of data to load and paginate.

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T003 Run quickstart.md validation to ensure the SQL script executes successfully, the UI grid looks pristine, and the new categories are fully functional.

---

## Dependencies & Execution Order

### Phase Dependencies

- **User Stories (Phase 3 & 4)**: T001 and T002 touch completely different technologies (Blazor vs SQL) and can be run simultaneously in parallel.
- **Polish (Final Phase)**: Depends on all desired user stories being complete.
