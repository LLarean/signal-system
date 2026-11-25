# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.1.0] - 2026-11-25

### Added

- `SignalSystemException`: handler failures during `Raise` are wrapped in
  it instead of a generic `Exception`.
- `SubscribersList.Count` and `SubscribersList.GetAt` for index-based
  iteration during dispatch.
- Zero-warning quality gate: `ci/` .NET projects (runtime built as
  netstandard2.1/C# 9 for Unity 2021.3 parity, tests on .NET 10),
  `.editorconfig` ruleset, GitHub Actions CI with a 90% line coverage
  floor, markdownlint and Conventional Commits checks.
- CHANGELOG.md, CONTRIBUTING.md, AGENTS.md, .gitignore, .gitattributes.

### Fixed

- `Raise` no longer leaves the subscriber list in executing state when a
  handler throws; unsubscribing afterwards works normally again.
- `Raise` no longer crashes when a subscriber is unsubscribed during
  dispatch: cleared slots are skipped, and subscribers added mid-raise are
  picked up on the next event.
- README: corrected the package name in the manifest.json snippet, repaired
  the code fence and badges, credited the upstream project.
- package.json: canonical repository URLs after the rename, declared
  minimum Unity version 2021.3, removed the dead `testables` key.

### Changed

- Runtime is annotated for nullable reference types;
  `SubscribersList.Subscribers` is now `IReadOnlyList<TSubscriber?>`
  (source-compatible, no binary change).

## [2.0.0] - 2025-07-31

### Changed

- Package renamed from EventBus to SignalSystem; core class renamed to
  `SignalSystem`; `RaiseEvent` renamed to `Raise`; namespace is `GameSignals`.
- Thread-safe subscription management.
- XML documentation across the public API.
- License changed to MIT.

### Added

- NUnit test suite.

## [1.1.2] - 2024-06-24

### Fixed

- EventBus class call; original license text restored; package information
  updated.

## [1.1.1] - 2024-06-23

### Added

- Basic implementation, first packaged release.

[Unreleased]: https://github.com/LLarean/signal-system/compare/v.2.1.0...HEAD
[2.1.0]: https://github.com/LLarean/signal-system/compare/v.2.0.0...v.2.1.0
[2.0.0]: https://github.com/LLarean/signal-system/compare/v.1.1.2...v.2.0.0
[1.1.2]: https://github.com/LLarean/signal-system/compare/v.1.1.1...v.1.1.2
[1.1.1]: https://github.com/LLarean/signal-system/releases/tag/v.1.1.1
