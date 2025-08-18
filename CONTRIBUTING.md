# Contributing Guidelines

Thank you for considering contributing! This document explains the process for proposing, implementing, and reviewing changes.

## Workflow Overview

1. **Open an issue**  
   - Go to the `Issues` tab and create a new issue describing the feature or fix (see [Creating Issues](#creating-issues)).  
   - The issue will automatically appear in the `Backlog` on the project board.  

2. **Start work**  
   - Move the issue to `In Progress` and assign it to yourself.  
   - If you're not ready to implement yet, simply leave the issue in the backlog.  

3. **Implement the feature**  
   - Clone the repository (or `git pull` if already cloned).  
   - Create and check out a new branch named `{issue-id}-{short-description-in-kebab-case}`  
     (e.g. `1-create-feature-set-docs`).  
   - Make your changes, committing them according to the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0) spec.  

4. **Open a pull request**  
   - Push your branch to the remote.  
   - Open a pull request (see [Opening Pull Requests](#opening-pull-requests)).  

5. **Review & merge**  
   - A pull request requires approval from **3 contributors** before merging.  
   - Once approved, merge the PR and delete the branch (see [Reviewing Pull Requests](#reviewing-pull-requests)).  

---

## Creating Issues

- **Clear titles**  
  - Bad: `Update readme`  
  - Good: `Update README.md with licensing information`  

- **Use the issue template** (it should be automatically applied).  

- **Fill in required sections**  
  - *Why?* → Why the feature/change is needed.  
  - *What?* → A full description of the feature/change.  

- **Organize issues**  
  - Add the issue to the project.  
  - Apply appropriate labels before submission.  

---

## Opening Pull Requests

- **Clear titles**  
  - Bad: `Create file`  
  - Good: `Create LICENSE.md with license details`  

- **Use the PR template** (required).  

- **Reference issues**  
  - At the top of your PR, list which issues it closes.  

- **Describe your work**  
  - In the *What was done?* section, provide a clear summary of the changes.  
  - This helps reviewers assess if the PR is ready to merge.  

---

## Reviewing Pull Requests

- Each PR requires **3 approvals** to be merged.  
- To review, go to the PR page and click **Add your review**.  
- Be constructive: point out issues, suggest improvements, or approve when ready.  

---

## Task Board

- Issues automatically appear in the `Backlog`.  
- When you begin work, move your issue to `In Progress` and assign it to yourself.  
- **Only pull requests** are moved to `In Review`. The linked issue should remain in `In Progress` until the PR is fully approved and merged.  
- Draft pull requests should also stay in `In Progress` until they are ready for review.  
- Issues and pull requests are automatically moved to `Done` once merged/closed.  
