# Contributing guide for Glowstone Rift
Thanks for your interest in contributing to this project! Follow the steps below to get started:

## How to Contribute
1. **Fork** this repository.
2. **Clone** your fork to your local machine.
3. **Create a new branch** for your changes.
4. Make your changes and **add or update documentation** as needed.
5. **Push** your changes to your fork.
6. Submit a **Pull Request** to the main repository.

Please ensure your code is clean and follows the project style.

---

## Commit Message Format

This project uses the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) format. Please follow this style in your commit messages to keep the history clean and readable.

### Usage Exmaple

| Prefix       | Use for                               |
|--------------|---------------------------------------|
| `"feat"`     | New gameplay feature or mechanic      |
| `"fix"`      | Bug fix                               |
| `"docs"`     | Documentation changes                 |
| `"style"`    | Code formatting (no logic changes)    |
| `"refactor"` | Code cleanup or restructuring         |
| `"chore"`    | Minor tasks (build scripts, configs). |

### Commit Exmaple

```bash
feat: adds melee attack to the player
```

## Branch Structure

The workflow will be based on Git Flow, a structured branching model that clearly differentiates between development code, stable versions, and urgent fixes.

### Main Branches
main
- Contains only stable production-ready code. 
- Every commit in main corresponds to a published version and must be tagged. 
- Only receives merges from release/ or hotfix/ branches.

development
- Contains the most up-to-date development code. 
- Serves as the integration base for all features before creating a release.

### Supporting Branches
feature/
- Created from: develop
- Merged into: develop
- Purpose: Implement new features.
- Examples:
  - feature/player-inventory
  - feature/new-enemy-yarara

release/
- Created from: develop
- Merged into: main and develop
- Purpose: Prepare a new stable version.
- Example:
  - release/1.2.0

hotfix/
- Created from: main
- Merged into: main and develop
- Purpose: Fix critical bugs found in production.
- Example:
  - hotfix/fix-crash-on-start

## Branch Naming Convention

To maintain consistency, we will use kebab-case (all lowercase, words separated by hyphens), because:

It is more readable in the terminal.

Avoids issues with uppercase letters on case-sensitive file systems.

It is a standard among many teams using Git Flow.

### Rules:
1. Mandatory prefix (feature/, release/, hotfix/). 
2. Short, descriptive name in English. 
3. No spaces or special characters—only hyphens.

#### Correct examples:
- feature/player-inventory 
- feature/enemy-ai-patrol 
- release/1.2.0 
- hotfix/fix-save-data-loss

#### Incorrect examples:
- feature/PlayerInventory (PascalCase)
- feature_enemy_ai (snake_case)
- feature/new stuff (spaces)