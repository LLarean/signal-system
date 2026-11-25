# Agent notes

Instructions for anyone (human or coding agent) changing this repository.

## Layout

UPM package `com.llarean.signal-system`: engine-independent event bus in
`Runtime/` (namespace `GameSignals`), NUnit tests in `Tests/Runtime`, Unity
sample in `Samples~/SaveLoad`. `ci/` holds plain .NET projects that build
and test the package sources without Unity.

## Commands

| Task | Command |
| --- | --- |
| Build with analyzers (zero warnings) | `dotnet build ci/SignalSystem.Runtime.csproj` |
| Test | `dotnet test ci/SignalSystem.Tests.csproj` |

CI runs the same checks; keep them green.

## Conventions

- Conventional Commits, see [CONTRIBUTING.md](CONTRIBUTING.md).
- Zero analyzer warnings: the ruleset lives in `.editorconfig`; a deviation
  needs an inline comment saying why.
- Comments: no inline comments in `Runtime/` (CI enforces) — rationale
  belongs in XML doc comments. `///` docs on the public API are required
  and checked by analyzers. In tests, rare "why" comments are allowed.
- New `.cs` files need a Unity `.meta` next to them (fresh GUID).
- The runtime must stay compatible with Unity 2021.3 (.NET Standard 2.1):
  no newer BCL APIs.
- Small commits, green build at every step.

## Definition of done

Before calling a change finished, run from the repo root:

1. `dotnet build ci/SignalSystem.Runtime.csproj` — zero warnings.
2. `dotnet test SignalSystem.sln` — all tests green.
3. `npx --yes markdownlint-cli2 "*.md"` — clean, when docs changed.
4. Behavior changes get a CHANGELOG.md entry under `## [Unreleased]`.

## Release process

1. Bump `version` in `package.json` (semver: fix → patch, API addition →
   minor, breaking → major).
2. Move the CHANGELOG `Unreleased` entries under the new version with the
   date; update the compare links at the bottom.
3. Commit as `chore(release): X.Y.Z`, tag `v.X.Y.Z`, push both.
4. Publish a GitHub release from the tag with the changelog section as notes.
5. Optionally attach a `.unitypackage` exported from the Unity Editor.

## Traps

- In Git Bash, pass MSBuild properties as `-p:Name=value`; the `/p:` form
  gets rewritten as a path by MSYS and fails with MSB1008.
- StyleCop 1.1.118 predates C# 9 and flags target-typed `new()` (SA1000):
  write constructor types explicitly in `Runtime/`.
- coverlet writes lcov to `ci/TestResults/coverage.info` — the path is
  relative to the test project directory, not the repo root.
- The CI actionlint image bundles shellcheck: unquoted variables in `run:`
  blocks fail the workflow lint step (SC2086).
- `ci/Directory.Build.props` keeps the two projects' obj/bin apart; both
  projects share the `ci/` folder, do not delete it.
- `Runtime/` must compile as netstandard2.1 with LangVersion 9 (Unity
  2021.3): no newer BCL APIs or C# features.
