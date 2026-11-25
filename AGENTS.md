# Agent notes

Instructions for anyone (human or coding agent) changing this repository.

## Layout

UPM package `com.llarean.signal-system`: engine-independent event bus in
`Runtime/` (namespace `GameSignals`), NUnit tests in `Tests/Runtime`, Unity
sample in `Samples~/SaveLoad`. `ci/` holds plain .NET projects that build
and test the package sources without Unity.

## Commands

| Task | Command |
|---|---|
| Build with analyzers (zero warnings) | `dotnet build ci/SignalSystem.Runtime.csproj` |
| Test | `dotnet test ci/SignalSystem.Tests.csproj` |

CI runs the same checks; keep them green.

## Conventions

- Conventional Commits, see [CONTRIBUTING.md](CONTRIBUTING.md).
- Zero analyzer warnings: the ruleset lives in `.editorconfig`; a deviation
  needs an inline comment saying why.
- New `.cs` files need a Unity `.meta` next to them (fresh GUID).
- The runtime must stay compatible with Unity 2021.3 (.NET Standard 2.1):
  no newer BCL APIs.
- Small commits, green build at every step.
