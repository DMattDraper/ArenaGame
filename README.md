# ArenaGame

**Branches**
1. master
2. dev
3. dave
4. james

| Branch | Purpose |
|:-------|:--------|
| master | Fully Functional Versions |
| dev | Completed Mechanics / Content |
| dave | Dave's current progress |
| james | James' current progress |

- Push all changes to your personal branch
- When a feature is complete, merge your branch into dev, delete your branch, and split a new branch (with the same name) off of dev
- When a version is complete and clean, merge dev into master
- Avoid changes the same files to facilitate easier merges

**Rebasing**
- git fetch origin <branch>
- git rebase origin/<branch>
- If there are any merge conflicts, squash them, commit those changes, and run git rebase --continue
Rebase before pulling and after merging! Should be from dev or master
