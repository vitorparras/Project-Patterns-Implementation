# Contributing to Project Patterns Implementation

Thank you for your interest. Bug reports, new pattern implementations and
improvements to existing ones are all welcome.

By participating you agree to follow the [Code of Conduct](CODE_OF_CONDUCT.md).
Security problems go through [SECURITY.md](SECURITY.md), never through a public
issue.

## Ways to contribute

- **Report a bug** with the [bug report template][bug].
- **Propose a new pattern or example** with the [feature request template][feature]
  before writing code, so we can agree on scope and placement.
- **Improve an existing example**: clearer naming, tests, documentation,
  removing unnecessary complexity.

[bug]: https://github.com/vitorparras/Project-Patterns-Implementation/issues/new?template=bug_report.yml
[feature]: https://github.com/vitorparras/Project-Patterns-Implementation/issues/new?template=feature_request.yml

## Development setup

Each example is self-contained in its own folder, with a README that explains
how to build and run it. In general:

```bash
git clone https://github.com/vitorparras/Project-Patterns-Implementation.git
cd Project-Patterns-Implementation
# follow the README inside the example folder
```

Requirements and versions are listed in each example's README.

## Guidelines for examples

- **One pattern, one folder.** Name the folder after the pattern. Include a
  `README.md` that explains the problem, the pattern, trade-offs and how to run
  the example.
- **Small and focused.** Show the pattern, not a product. Remove anything that
  does not help the reader understand it.
- **Secure by default.** Never commit secrets. Read configuration from
  environment variables or user secrets, keep dependencies up to date and pin
  versions. Examples that are intentionally insecure must say so loudly.
- **Code style.** Follow the conventions of the language (for C#, the .NET
  runtime style and `dotnet format`). Respect the `.editorconfig`.
- **Tests when it makes sense.** A few tests that show the pattern's behavior
  are more valuable than full coverage.

## Pull request workflow

1. Fork the repository and create a branch from `main`:
   `git switch -c <type>/<short-description>` (for example `feat/strategy-pattern`).
2. Keep changes focused; one pattern or fix per pull request.
3. Use clear commit messages in English. [Conventional Commits][cc] are
   appreciated: `feat: add strategy pattern example`.
4. Make sure the build and CI checks pass locally when possible.
5. Open the pull request against `main` and fill in the template. Link the issue
   it resolves.
6. A maintainer will review it. Please respond to comments; stale PRs may be
   closed after 30 days of inactivity.

`main` is protected: changes only land through pull requests, and all automated
checks must pass.

[cc]: https://www.conventionalcommits.org/

## Licensing of contributions

Contributions are licensed under the repository license (see [LICENSE](LICENSE)).
By opening a pull request you confirm that you have the right to license your
contribution under those terms.

## Questions

Use [Discussions](https://github.com/vitorparras/Project-Patterns-Implementation/discussions) for
questions and ideas that are not yet concrete issues.
