# client-template

A template client package with some CI configured and documentation of the manual steps needed to finish setup.

## Steps to finish setup

1. Add a branch protection rule on the main branch in Settings -> Branches.
   1. Require a pull request before merging, require 1 approval
   1. Require status checks to pass before merging. Require the build workflow to pass.
   1. Everything else disabled.
1. Create a time-delayed environment for schema sync workflows in Settings -> Environment.
   1. Name the environment `delayed-schema-sync`.
   1. Add a wait timer protection rule for 5 minutes.
   1. Allow administrators to bypass configuration rules.
1. Configure dependabot.
   1. Open `/github/dependabot.yml`.
   1. Delete the block for the `gitsubmodule` package ecosystem; that's only used to keep the template repo up to date.
      We'll set up a better workflow for syncing submodules in a moment.
   1. Uncomment the block for the target language's package ecosystem. Available options are listed in [the Dependabot docs](https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuration-options-for-the-dependabot.yml-file#package-ecosystem).
1. Finish workflow setup.
   1. Go to Settings -> General and check "Allow Auto-Merge" under the pull request section.
   1. PR the new client package to the `target` matrix in the `dispatch` step of the schema validation workflow: [link](https://github.com/hpackage/hpackage-schema/blob/main/.github/workflows/validate.yml)
   1. Find the `build` workflow in the new repository and add build and test steps for your build system.
1. Replace this readme! A good client package describes how types are generated, where to find documentation, and a brief
   explanation of how to use the library. You can use [the Rust client](https://github.com/hpackage/hpackage-rs) as inspiration.
