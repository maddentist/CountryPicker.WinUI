# CountryPicker UWP ? WinUI 3 Upgrade Plan

## Table of Contents
- [1. Executive Summary](#1-executive-summary)
- [2. Migration Strategy](#2-migration-strategy)
- [3. Detailed Dependency Analysis](#3-detailed-dependency-analysis)
- [4. Project-by-Project Plans](#4-project-by-project-plans)
- [5. Risk Management](#5-risk-management)
- [6. Testing & Validation Strategy](#6-testing--validation-strategy)
- [7. Complexity & Effort Assessment](#7-complexity--effort-assessment)
- [8. Source Control Strategy](#8-source-control-strategy)
- [9. Success Criteria](#9-success-criteria)

## 1. Executive Summary
- **Scenario**: Convert UWP custom control project to WinUI 3 on Windows App SDK; migrate from classic UWP project to SDK-style targeting modern .NET.
- **Scope**: Single project `CountryPicker.UWP\CountryPicker.UWP.csproj`, currently `net5.0`, non-SDK UWP. No project dependencies/dependees.
- **Target State**: SDK-style WinUI 3 project targeting `net8.0-windows10.0.22000.0` (per assessment) with Windows App SDK (`Microsoft.WindowsAppSDK=1.8.251106002`) and modern Win2D (`Microsoft.Graphics.Win2D=1.1.0`), Newtonsoft.Json 13.0.4, Microsoft.Windows.Compatibility 10.0.1.
- **Selected Strategy**: **All-At-Once Strategy** — atomic upgrade of project file, packages, and compilation fixes in a single coordinated pass; then run validation.
- **Complexity Classification**: Simple (1 project, no deps, low risk; package swaps well-known).
- **Critical Issues**: Replace `Microsoft.NETCore.UniversalWindowsPlatform` (UWP meta-package) with Windows App SDK + Win2D + Compatibility; update incompatible `Win2D.uwp`; upgrade `Newtonsoft.Json`.
- **Iteration Plan**: One atomic migration phase + validation.

## 2. Migration Strategy
- **Approach**: All projects upgraded simultaneously (single project here) — atomic update of TargetFramework and packages, then build/fix.
- **Ordering**: (1) Convert to SDK-style and retarget, (2) Replace packages, (3) Update XAML/App bootstrap to WinUI 3, (4) Build/fix, (5) Validate.
- **Parallelism**: Not applicable (single project); everything in one pass.
- **Framework Targeting**: `net8.0-windows10.0.22000.0` (WinUI 3/Windows App SDK 1.8). If minimum OS can be lower, align with `10.0.19041` at execution time, but keep 22000.0 per assessment unless overridden.
- **Package Strategy**: Remove UWP meta-package; add Windows App SDK and Win2D; upgrade Newtonsoft.Json; add Windows.Compatibility for gaps.
- **API Surface Migration**: Migrate `Windows.UI.*` ? `Microsoft.UI.*`, `Windows.*` namespaces to Windows App SDK equivalents where needed.

## 3. Detailed Dependency Analysis
- **Graph Summary**: Single classic UWP project; no dependencies or dependees.
- **Grouping**: Single atomic phase (entire project).
- **Critical Path**: Project file conversion and package replacement unblock all code fixes.
- **Circular Dependencies**: None.

## 4. Project-by-Project Plans

### CountryPicker.UWP\CountryPicker.UWP.csproj
**Current State**: UWP classic project, `net5.0`, non-SDK; packages: `Microsoft.NETCore.UniversalWindowsPlatform 5.3.3`, `Win2D.uwp 1.19.0`, `Newtonsoft.Json 7.0.1`.

**Target State**: SDK-style WinUI 3 project targeting `net8.0-windows10.0.22000.0`; packages: `Microsoft.WindowsAppSDK 1.8.251106002`, `Microsoft.Graphics.Win2D 1.1.0`, `Microsoft.Windows.Compatibility 10.0.1`, `Newtonsoft.Json 13.0.4`.

**Migration Steps (atomic)**:
1) **Prerequisites**: Ensure .NET 8 SDK installed; ensure Windows App SDK 1.8 workload available; update `global.json` if present.
2) **Convert project to SDK-style**:
   - Use `Sdk="Microsoft.NET.Sdk"` with `<TargetFramework>net8.0-windows10.0.22000.0</TargetFramework>`.
   - Replace UWP property set (TargetPlatformVersion/MinVersion) with `TargetPlatformMinVersion` where required for WinUI bootstrap; include `<UseWinUI>true</UseWinUI>`.
   - Preserve entry points: `App.xaml`, `Package.appxmanifest` equivalent moves to `app.manifest`/`Package.appxmanifest` for packaging? For unpackaged, rely on WinAppSDK bootstrap. Keep asset items via `Content`/`None` entries as needed.
3) **Package updates**:
   - Remove `Microsoft.NETCore.UniversalWindowsPlatform` and `Win2D.uwp`.
   - Add `Microsoft.WindowsAppSDK` version `1.8.251106002`.
   - Add `Microsoft.Graphics.Win2D` version `1.1.0`.
   - Add `Microsoft.Windows.Compatibility` version `10.0.1` if any legacy APIs are referenced.
   - Upgrade `Newtonsoft.Json` to `13.0.4`.
4) **XAML / App bootstrap**:
   - Update XAML namespaces: `xmlns:controls="using:Microsoft.UI.Xaml.Controls"`, replace `Windows.UI.Xaml` with `Microsoft.UI.Xaml`.
   - Update `App.xaml.cs` to use `Microsoft.UI.Xaml.Application` and `Window` from WinUI 3; ensure `m_window = new MainWindow(); m_window.Activate();` style activation.
   - Replace `ApplicationView`, `CoreApplication` usages with WinAppSDK equivalents (`AppWindow`, `DispatcherQueue`, etc.) as needed.
5) **Resource dictionaries & assets**:
   - Update merged dictionaries to WinUI styles if using generic.xaml for custom control; ensure `Themes/Generic.xaml` compiled as `Page` or `None` with correct `UseWinUI` behavior.
6) **Compile fixes**:
   - Adjust `using Windows.*` to `using Microsoft.*` where applicable; handle missing APIs via `Windows.Compatibility` or alternative patterns.
7) **Build and verify**:
   - Restore packages, build solution; fix any remaining API mismatches.

**Package Update Table**:
| Package | Current | Target | Reason |
| --- | --- | --- | --- |
| Microsoft.NETCore.UniversalWindowsPlatform | 5.3.3 | (Remove) | UWP meta-package replaced by Windows App SDK
| Win2D.uwp | 1.19.0 | (Remove) | UWP-specific; use Microsoft.Graphics.Win2D
| Microsoft.WindowsAppSDK | — | 1.8.251106002 | WinUI 3 platform
| Microsoft.Graphics.Win2D | — | 1.1.0 | Win2D for WinUI 3
| Microsoft.Windows.Compatibility | — | 10.0.1 | Fill gaps for legacy APIs if needed
| Newtonsoft.Json | 7.0.1 | 13.0.4 | Stay supported

**Expected Breaking Changes / Fixes**:
- Namespace changes (`Windows.UI.*` ? `Microsoft.UI.*`), `Window` activation model changes.
- Some UWP-only APIs (background tasks, restricted capabilities) are not available; ensure not used or refactor.
- Package assets/manifest handling differs; consider unpackaged app model unless MSIX packaging is required.

**Testing / Validation**:
- Build succeeds on `net8.0-windows10.0.22000.0`.
- Run control sample (if available) to validate rendering and interactions; verify resource dictionaries load.
- Spot-check JSON serialization paths after Newtonsoft upgrade.

## 5. Risk Management
- **High-Risk Areas**:
  - UWP-only APIs not available in WinAppSDK: Mitigate by substituting AppWindow/Windowing APIs or Windows.Compatibility.
  - XAML namespace drift: Audit `Windows.UI.*` and update to `Microsoft.UI.*` consistently.
  - Packaging assumptions: If MSIX-specific assets exist, decide unpackaged vs packaged and update project properties accordingly.
- **Contingencies**:
  - If API missing, prefer Windows App SDK equivalents or Windows.Compatibility; if unavailable, redesign feature or conditional compile.
  - If build blocks on Win2D usage, ensure correct namespace (`Microsoft.Graphics.Canvas.*`) and package reference.

## 6. Testing & Validation Strategy
- **Build validation**: Full solution build after atomic upgrade.
- **Runtime validation**: Launch control host/sample (if present) to verify load/activation and visual correctness.
- **Functional checks**: Validate any JSON serialization paths; confirm resources and generic.xaml load without missing resource errors.

## 7. Complexity & Effort Assessment
- **Overall**: Low complexity (single project, well-known migration path).
- **Key drivers**: Package replacement and namespace updates; minimal API incompatibilities expected per assessment.

## 8. Source Control Strategy
- **Branching**: Create feature branch from `master` (e.g., `upgrade/winui3-sdkstyle`).
- **Commits**: Single consolidated commit for atomic upgrade (project file conversion, package updates, code/XAML namespace updates).
- **Review**: One PR with build verification notes and checklist of namespace/package changes.

## 9. Success Criteria
- Project is SDK-style targeting `net8.0-windows10.0.22000.0` with `UseWinUI` set.
- All legacy UWP packages removed; Windows App SDK/Win2D/Compatibility/Newtonsoft updated to specified versions.
- Solution builds with 0 errors/warnings related to migration.
- Control renders/loads successfully in host (if available); no missing resources.
- Strategy followed: All-at-Once atomic upgrade completed and validated.
