# CountryPicker UWP → WinUI 3 Upgrade Tasks

## Overview

This document tracks the execution of converting the CountryPicker UWP custom control project to WinUI 3 on Windows App SDK. The single project will be upgraded atomically from classic UWP to SDK-style targeting net8.0-windows10.0.22000.0.

**Progress**: 0/2 tasks complete (0%) ![0%](https://progress-bar.xyz/0)

---

## Tasks

### [▶] TASK-001: Verify prerequisites
**References**: Plan §4 Migration Steps

- [ ] (1) Verify .NET 8 SDK installed
- [ ] (2) SDK version meets minimum requirements (Verify)
- [ ] (3) Verify Windows App SDK 1.8 workload available
- [ ] (4) Workload installed (Verify)

---

### [ ] TASK-002: Atomic project conversion and dependency upgrade
**References**: Plan §4 Migration Steps, Plan §4 Package Update Table, Plan §4 Expected Breaking Changes

- [ ] (1) Convert CountryPicker.UWP.csproj to SDK-style per Plan §4 step 2 (Sdk="Microsoft.NET.Sdk", TargetFramework=net8.0-windows10.0.22000.0, UseWinUI=true)
- [ ] (2) Project file converted to SDK-style (Verify)
- [ ] (3) Update global.json if present to target .NET 8 SDK
- [ ] (4) Remove packages: Microsoft.NETCore.UniversalWindowsPlatform, Win2D.uwp per Plan §4 Package Update Table
- [ ] (5) Add packages: Microsoft.WindowsAppSDK 1.8.251106002, Microsoft.Graphics.Win2D 1.1.0, Microsoft.Windows.Compatibility 10.0.1 per Plan §4 Package Update Table
- [ ] (6) Upgrade package: Newtonsoft.Json to 13.0.4 per Plan §4 Package Update Table
- [ ] (7) All package references updated (Verify)
- [ ] (8) Restore all dependencies
- [ ] (9) All dependencies restored successfully (Verify)
- [ ] (10) Update XAML namespaces per Plan §4 step 4 (Windows.UI.Xaml → Microsoft.UI.Xaml, add Microsoft.UI.Xaml.Controls usings)
- [ ] (11) Update App.xaml.cs bootstrap per Plan §4 step 4 (use Microsoft.UI.Xaml.Application, Window activation model)
- [ ] (12) Update resource dictionaries and assets per Plan §4 step 5 (Generic.xaml for WinUI 3, ensure UseWinUI behavior)
- [ ] (13) Fix namespace imports per Plan §4 step 6 (Windows.* → Microsoft.* where applicable, use Windows.Compatibility for gaps)
- [ ] (14) Build solution and fix all compilation errors per Plan §4 Expected Breaking Changes (namespace changes, Window activation model, UWP-only API substitutions)
- [ ] (15) Solution builds with 0 errors (Verify)
- [ ] (16) Commit changes with message: "TASK-002: Complete atomic WinUI 3 upgrade and migration"

---