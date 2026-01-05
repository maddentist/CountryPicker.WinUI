# CountryPicker WinUI 3 .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the CountryPicker UWP project conversion to WinUI 3 targeting .NET 10.0. The single project will be converted to SDK-style format, migrated to Windows App SDK, and validated in one atomic operation.

**Progress**: 0/3 tasks complete (0%) ![0%](https://progress-bar.xyz/0)

---

## Tasks

### [▶] TASK-001: Verify prerequisites
**References**: Plan §2 Migration Strategy, Plan §9 Complexity & Effort Assessment

- [▶] (1) Verify .NET 10 SDK is installed
- [ ] (2) .NET 10 SDK meets minimum requirements (**Verify**)
- [ ] (3) Verify Windows App SDK 1.8 workload is available
- [ ] (4) Windows App SDK 1.8 workload is available (**Verify**)
- [ ] (5) Update `global.json` if present to allow .NET 10
- [ ] (6) `global.json` allows .NET 10 SDK (**Verify**)

---

### [ ] TASK-002: Atomic project conversion and dependency upgrade with compilation fixes
**References**: Plan §4 Project-by-Project Plans (CountryPicker.UWP), Plan §5 Package Update Reference, Plan §6 Breaking Changes Catalog

- [ ] (1) Convert `CountryPicker.UWP\CountryPicker.UWP.csproj` to SDK-style format per Plan §4 Step 2 (use `Sdk="Microsoft.NET.Sdk"`, set `<TargetFramework>net10.0-windows10.0.22000.0</TargetFramework>`, add `<UseWinUI>true</UseWinUI>`, set `<TargetPlatformMinVersion>10.0.22000.0</TargetPlatformMinVersion>`, preserve entry points and assets, remove legacy UWP properties)
- [ ] (2) Project converted to SDK-style format (**Verify**)
- [ ] (3) Remove package references: `Microsoft.NETCore.UniversalWindowsPlatform` 5.3.3, `Win2D.uwp` 1.19.0
- [ ] (4) Add package references: `Microsoft.WindowsAppSDK` 1.8.251106002, `Microsoft.Graphics.Win2D` 1.1.0, `Microsoft.Windows.Compatibility` 10.0.1
- [ ] (5) Update package reference: `Newtonsoft.Json` 7.0.1 → 13.0.4
- [ ] (6) All package references updated per Plan §5 (**Verify**)
- [ ] (7) Update XAML namespace declarations in all XAML files per Plan §4 Step 4 (`Windows.UI.Xaml.*` → `Microsoft.UI.Xaml.*`)
- [ ] (8) Update C# using statements in all C# files per Plan §4 Step 5 (`using Windows.UI.Xaml;` → `using Microsoft.UI.Xaml;` and similar for Controls, Media, Input, Data, Shapes, Composition, Text)
- [ ] (9) Update `App.xaml.cs` to use `Microsoft.UI.Xaml.Application` per Plan §4 Step 6 (update Window activation pattern, replace `ApplicationView`/`CoreApplication` with `AppWindow`/`DispatcherQueue`)
- [ ] (10) Update resource dictionaries to WinUI styles per Plan §4 Step 7 (update `Themes/Generic.xaml` if exists)
- [ ] (11) Restore dependencies: `dotnet restore`
- [ ] (12) All dependencies restored successfully (**Verify**)
- [ ] (13) Build solution and fix all compilation errors per Plan §6 Breaking Changes Catalog (namespace changes, API changes, Win2D compatibility, Newtonsoft.Json compatibility)
- [ ] (14) Solution builds with 0 errors (**Verify**)
- [ ] (15) Commit changes with message: "TASK-002: Convert CountryPicker.UWP to WinUI 3 .NET 10.0"

---

### [ ] TASK-003: Runtime validation
**References**: Plan §4 Validation Checklist, Plan §8 Testing & Validation Strategy

- [ ] (1) Execute runtime validation per Plan §8 Build Validation (`dotnet restore` and `dotnet build` complete successfully, 0 migration-related warnings)
- [ ] (2) Build validation successful (**Verify**)
- [ ] (3) Execute runtime validation per Plan §8 Runtime Validation (control loads without XAML parse errors, control renders correctly, user interactions work, resource dictionaries load)
- [ ] (4) Runtime validation successful (**Verify**)
- [ ] (5) Execute functional validation per Plan §8 Functional Validation (JSON serialization/deserialization works, Win2D graphics render correctly if used, country picker functionality intact)
- [ ] (6) Functional validation successful (**Verify**)
- [ ] (7) Commit validation results with message: "TASK-003: Complete WinUI 3 .NET 10.0 migration validation"

---
