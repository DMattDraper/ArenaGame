# Bomb, Bow & Blade

**Branches**
1. master
2. dev
3. feature/[branch-name]
4. bug/[branch-name]
5. chore/[branch-name]

| Branch | Purpose |
|:-------|:--------|
| master | Fully Functional Versions |
| dev | Completed Mechanics / Content |
| feature | New features/elements of the game |
| bug | Changes addressing and fixing bugs |
| chore | Changes related to project organization and workflow |

- Push all changes to your feature branch
- When a feature is complete, merge your branch into dev, delete your branch, and split a new branch (with the same name) off of dev
- When a version is complete and clean, merge dev into master
- Avoid changes the same files to facilitate easier merges

**Updating a Remote Branch**
1. Checkout dev branch (git checkout dev)
2. Pull remote changes (git pull dev)
3. Checkout working branch (git checkout feature/xxx, git checkout bug/xxx, etc.)
4. Merge dev into working branch (git merge develop)
