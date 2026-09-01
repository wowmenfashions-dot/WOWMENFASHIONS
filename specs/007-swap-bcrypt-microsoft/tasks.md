# Implementation Tasks: Swap BCrypt for Microsoft PasswordHasher

**Feature**: `specs/007-swap-bcrypt-microsoft/spec.md`

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Remove the `BCrypt.Net-Next` package reference from `wowmenfashions/wowmenfashions.csproj`.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented
*No specific foundational tasks for this feature.*

---

## Phase 3: User Story 1 - Microsoft Password Hashing (Priority: P1) 🚀 MVP

**Goal**: Replace 3rd-party hashing with built-in ASP.NET Identity hashing.

**Independent Test**: Register a new user and log in successfully.

### Implementation for User Story 1

- [x] T002 [US1] Update `wowmenfashions/Services/PasswordHasher.cs` to instantiate `Microsoft.AspNetCore.Identity.PasswordHasher<string>` and replace the `BCrypt` static method calls with `HashPassword` and `VerifyHashedPassword`.

**Checkpoint**: At this point, the application should build and handle authentication correctly with the new hasher.

---

## Phase 4: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T003 Run quickstart.md validation to ensure the project builds with 0 errors and 0 warnings, and the E2E registration/login flow works.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Must run first.
- **User Stories (Phase 3+)**: Depend on Setup completion.
- **Polish (Final Phase)**: Depends on all desired user stories being complete.
