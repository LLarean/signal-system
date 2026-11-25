# Contributing

Thanks for taking the time to contribute!

## Ground rules

- Commit messages follow [Conventional Commits](https://www.conventionalcommits.org/):
  `fix: skip cleared subscriber slots during raise`, `docs: fix manifest snippet`.
  Types: `feat`, `fix`, `refactor`, `perf`, `test`, `docs`, `style`, `build`,
  `ci`, `chore`.
- Keep changes small and self-contained; every commit must leave the build green.
- The public API in `Runtime/` is documented with XML doc comments — keep them
  accurate.
- The runtime must stay compatible with Unity 2021.3 (.NET Standard 2.1):
  no newer BCL APIs.

## Running the checks

The package is engine-independent C#, so everything runs on plain .NET:

| Task | Command |
| --- | --- |
| Build with analyzers (zero warnings required) | `dotnet build ci/SignalSystem.Runtime.csproj` |
| Tests | `dotnet test ci/SignalSystem.Tests.csproj` |
| Tests with coverage floor | `dotnet test ci/SignalSystem.Tests.csproj -p:CollectCoverage=true -p:Threshold=90` |

CI runs the same checks on every push and pull request.

## Working in an IDE

Open `SignalSystem.sln` in Rider or Visual Studio: both `ci/` projects
load with the analyzer ruleset active, and the NUnit suite runs from the
IDE test runner (.NET 10 SDK required).

## New source files

Unity requires a `.meta` file next to every `.cs` file. Copy a sibling
`.meta`, generate a fresh 32-hex-digit `guid`, and commit both files.
