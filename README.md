# Project Patterns Implementation

[![CI](https://github.com/vitorparras/Project-Patterns-Implementation/actions/workflows/ci.yml/badge.svg)](https://github.com/vitorparras/Project-Patterns-Implementation/actions/workflows/ci.yml)
[![Code: AGPL-3.0](https://img.shields.io/badge/Code-AGPL--3.0-blue.svg)](LICENSE)
[![Content: CC BY-SA 4.0](https://img.shields.io/badge/Content-CC%20BY--SA%204.0-blue.svg)](LICENSE-CONTENT)
[![OpenSSF Scorecard](https://api.scorecard.dev/projects/github.com/vitorparras/Project-Patterns-Implementation/badge)](https://scorecard.dev/viewer/?uri=github.com/vitorparras/Project-Patterns-Implementation)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg)](CODE_OF_CONDUCT.md)
[![PRs welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

Hands-on implementations of architectural and design patterns in C# and .NET.
Each pattern lives in its own folder with a runnable example and an explanation
of the problem it solves and its trade-offs.

> **Status:** active. One architectural pattern is implemented; the design
> patterns listed in the [roadmap](#roadmap) are planned.

## Table of contents

- [Implemented patterns](#implemented-patterns)
- [Running the Minimal API example](#running-the-minimal-api-example)
- [Roadmap](#roadmap)
- [Repository layout](#repository-layout)
- [Contributing](#contributing)
- [Security](#security)
- [Code of conduct](#code-of-conduct)
- [License](#license)

## Implemented patterns

### Architectural: Minimal API with layered architecture

[`Architectural Patterns/MinimalApi`](<Architectural Patterns/MinimalApi>) is an
ASP.NET Core 8 **Minimal API** organized in four layers:

| Project | Responsibility |
|---|---|
| `API` | HTTP endpoints grouped by feature (`AuthEndpoints`, `UserEndpoints`), extension methods for DI, Swagger and JWT authentication, and a token-validation middleware |
| `Application` | Use cases: `AuthService` (login, JWT issuing and validation, token history) and `UserService` |
| `Domain` | Entities (`User`, `TokenHistory`, `BaseEntity`) and DTOs |
| `Infrastructure` | EF Core `DbContext` (in-memory provider) and a generic repository |

It shows dependency inversion between layers, endpoint grouping, JWT bearer
authentication with token revocation, and Swagger documentation.

## Running the Minimal API example

Requirements: [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
git clone https://github.com/vitorparras/Project-Patterns-Implementation.git
cd "Project-Patterns-Implementation/Architectural Patterns/MinimalApi"

# The JWT signing key is NOT in the repository. Set it once with user secrets
# (any random string with at least 32 characters):
dotnet user-secrets set "Jwt:SecretKey" "$(openssl rand -hex 32)" --project API

dotnet run --project API
```

Open <http://localhost:5019/swagger>, create a user with `POST /users`, log in
with `POST /auth/login` and use the returned token in the **Authorize** button.

The key can also be provided through the environment variable
`Jwt__SecretKey`, which is how the Docker image expects it:

```bash
docker build -t minimal-api -f API/Dockerfile .
docker run --rm -p 8080:8080 -e Jwt__SecretKey="$(openssl rand -hex 32)" minimal-api
```

The application refuses to start without a key. Never commit one.

## Roadmap

Planned examples, each in its own folder with a README. Open a
[feature request](https://github.com/vitorparras/Project-Patterns-Implementation/issues/new?template=feature_request.yml)
to propose another or to volunteer for one of these.

**Architectural**: layered architecture (standalone), clean architecture,
microservices with messaging, CQRS.

**Creational**: Singleton, Factory Method, Abstract Factory, Builder.

**Structural**: Adapter, Decorator, Facade, Proxy, Composite.

**Behavioral**: Strategy, Observer, Command, Mediator, Chain of Responsibility.

## Repository layout

```text
Project-Patterns-Implementation/
├── Architectural Patterns/
│   └── MinimalApi/
│       ├── API/                 # entry point, endpoints, Dockerfile
│       ├── Application/
│       ├── Domain/
│       ├── Infrastructure/
│       ├── MinimalApi.sln
│       └── nuget.config         # nuget.org as the only package source
├── .github/                     # CI, Scorecard, dependency review, Dependabot, templates
├── .editorconfig
├── CODE_OF_CONDUCT.md
├── CONTRIBUTING.md
├── SECURITY.md
└── LICENSE
```

## Contributing

New patterns and improvements to existing examples are welcome. Please read
[CONTRIBUTING.md](CONTRIBUTING.md) first. The `main` branch is protected: changes
land only through pull requests with a green build.

## Security

Found a vulnerability in an example or a problem in this repository's
configuration? Follow [SECURITY.md](SECURITY.md) and report it privately.

## Code of conduct

This project follows the [Contributor Covenant](CODE_OF_CONDUCT.md). By
participating you agree to uphold it.

## License

This repository uses two licenses:

- **Code** (programs, scripts, configuration files and code samples):
  [GNU Affero General Public License v3.0](LICENSE) (AGPL-3.0).
- **Content** (documents, explanations, images and other non-code material):
  [Creative Commons Attribution-ShareAlike 4.0 International](LICENSE-CONTENT)
  (CC BY-SA 4.0).

Both require attribution, and both require that modified versions you
distribute, sell or run as a network service remain under the same license.
Copyright © Vitor Hugo Parras.
