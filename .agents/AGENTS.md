# Antigravity Rules

## SpecKit Workflow Enforcement
Always use the SpecKit process for any feature implementation or significant changes. 
Instead of executing plans manually in standard planning mode, follow these strict steps by recommending the corresponding slash commands or utilizing the skills:
1. **Specify Requirements:** Use the `speckit-specify` skill to define the requirement (`spec.md`).
2. **Plan:** Use the `speckit-plan` skill to create the architecture and design (`plan.md`).
3. **Tasks:** Use the `speckit-tasks` skill to generate actionable steps (`tasks.md`).
4. **Implement:** Use the `speckit-implement` skill to execute the tasks systematically.

Do not bypass this workflow for project code modifications.

## Open Knowledge Format (OKF) Enforcement
**CRITICAL RULE:** All agents (Antigravity and any prompt agents) MUST always refer to the Open Knowledge Format (OKF) files located in the `knowledge/` directory (specifically `knowledge/okf.md`) BEFORE planning, making changes, or answering questions about the architecture or project context. 
- Ensure any new project decisions or patterns are updated in the OKF files.
