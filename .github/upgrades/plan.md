# .NET 10.0 Upgrade Plan - CountryPicker UWP ? WinUI 3

## Table of Contents

- [1. Executive Summary](#1-executive-summary)
- [2. Migration Strategy](#2-migration-strategy)
- [3. Detailed Dependency Analysis](#3-detailed-dependency-analysis)
- [4. Project-by-Project Plans](#4-project-by-project-plans)
  - [CountryPicker.UWP](#countrypickeruwp)
- [5. Package Update Reference](#5-package-update-reference)
- [6. Breaking Changes Catalog](#6-breaking-changes-catalog)
- [7. Risk Management](#7-risk-management)
- [8. Testing & Validation Strategy](#8-testing--validation-strategy)
- [9. Complexity & Effort Assessment](#9-complexity--effort-assessment)
- [10. Source Control Strategy](#10-source-control-strategy)
- [11. Success Criteria](#11-success-criteria)

---

## 1. Executive Summary

### Scenario Description
Convert UWP custom control project to WinUI 3 on Windows App SDK; migrate from classic UWP project to SDK-style targeting .NET 10.0.

### Scope
- **Single project**: `CountryPicker.UWP\CountryPicker.UWP.csproj`
- **Current state**: net5.0, non-SDK style UWP project
- **No project dependencies or dependants**

### Target State
- SDK-style WinUI 3 project targeting `net10.0-windows10.0.22000.0`
- Windows App SDK (`Microsoft.WindowsAppSDK` 1.8.251106002)
- Modern Win2D (`Microsoft.Graphics.Win2D` 1.1.0)
- `Microsoft.Windows.Compatibility` 10.0.1
- `Newtonsoft.Json` 13.0.4 (security fix)

### Selected Strategy
**All-At-Once Strategy** — Atomic upgrade of project file, packages, and compilation fixes in a single coordinated pass; then run validation.

**Rationale**:
- 1 project (simple solution)
- No project dependencies
- Clear package replacement path
- All packages have target framework versions available or known replacements

### Complexity Classification
**Simple** — Fast batch approach (2-3 detail iterations)

| Metric | Value | Assessment |
|--------|-------|------------|
| Total Projects | 1 | ? Simple |
| Dependency Depth | 0 | ? No blocking dependencies |
| Lines of Code | 1,940 | ? Small codebase |
| Security Vulnerabilities | 1 | ?? Newtonsoft.Json (will be fixed) |
| Incompatible Packages | 1 | ?? Win2D.uwp (will be replaced) |
| API Issues | 0 | ? No breaking API changes detected |

### Critical Issues to Address
1. **Replace** `Microsoft.NETCore.UniversalWindowsPlatform` (UWP meta-package) with Windows App SDK + Win2D + Compatibility packages
2. **Replace** incompatible `Win2D.uwp` with `Microsoft.Graphics.Win2D`
3. **Upgrade** `Newtonsoft.Json` 7.0.1 ? 13.0.4 (security vulnerability fix)
4. **Convert** project to SDK-style format

### Iteration Plan
One atomic migration phase + validation phase

---

## 2. Migration Strategy

### Selected Approach
**All-At-Once Strategy** — All projects upgraded simultaneously in single operation.

### Rationale
- 1 project (small solution)
- All currently on .NET 5.0 (UWP)
- Clear dependency structure (none)
- All packages have target framework versions available or known replacements
- Low risk profile based on assessment

### Execution Sequence
All operations performed as single coordinated batch:

1. **Convert to SDK-style** — Transform classic UWP project to modern SDK-style format
2. **Update TargetFramework** — Change to `net10.0-windows10.0.22000.0`
3. **Replace packages** — Remove UWP packages, add WinUI 3 equivalents
4. **Update XAML/Code namespaces** — Migrate `Windows.UI.*` ? `Microsoft.UI.*`
5. **Restore and build** — Restore dependencies and compile
6. **Fix compilation errors** — Address any breaking changes
7. **Verify** — Solution builds with 0 errors

### Framework Targeting
- **Target**: `net10.0-windows10.0.22000.0`
- **Platform**: WinUI 3 / Windows App SDK 1.8
- **Minimum OS**: Windows 10 version 22000 (Windows 11)

### Package Strategy
| Action | Packages |
|--------|----------|
| **Remove** | Microsoft.NETCore.UniversalWindowsPlatform, Win2D.uwp |
| **Add** | Microsoft.WindowsAppSDK 1.8.251106002 |
| **Add** | Microsoft.Graphics.Win2D 1.1.0 |
| **Add** | Microsoft.Windows.Compatibility 10.0.1 |
| **Upgrade** | Newtonsoft.Json 7.0.1 ? 13.0.4 |

### API Surface Migration
Key namespace migrations required:
- `Windows.UI.Xaml.*` ? `Microsoft.UI.Xaml.*`
- `Windows.UI.Composition.*` ? `Microsoft.UI.Composition.*`
- Win2D: `Microsoft.Graphics.Canvas.*` (same namespace, different package)

### Parallel vs Sequential
**Not applicable** — Single project; everything in one atomic pass.

---

## 3. Detailed Dependency Analysis

*No dependencies found; see Project Plans for individual project details.*

---

## 4. Project-by-Project Plans

### CountryPicker.UWP\CountryPicker.UWP.csproj

#### Current State
| Property | Value |
|----------|-------|
| Target Framework | net5.0 |
| SDK-style | ? No (Classic UWP) |
| Project Kind | UWP |
| Lines of Code | 1,940 |
| Number of Files | 259 |
| Dependencies | 0 projects |
| Dependants | 0 projects |
| Risk Level | ?? Low |

**Current Packages:**
- `Microsoft.NETCore.UniversalWindowsPlatform` 5.3.3
- `Win2D.uwp` 1.19.0
- `Newtonsoft.Json` 7.0.1 (?? security vulnerability)

#### Target State
| Property | Value |
|----------|-------|
| Target Framework | net10.0-windows10.0.22000.0 |
| SDK-style | ? Yes |
| Project Kind | WinUI 3 (Windows App SDK) |

**Target Packages:**
- `Microsoft.WindowsAppSDK` 1.8.251106002
- `Microsoft.Graphics.Win2D` 1.1.0
- `Microsoft.Windows.Compatibility` 10.0.1
- `Newtonsoft.Json` 13.0.4

#### Migration Steps

**Step 1: Prerequisites**
- Ensure .NET 10 SDK is installed
- Ensure Windows App SDK 1.8 workload is available
- Update `global.json` if present to allow .NET 10

**Step 2: Convert Project to SDK-style**
- Replace project file content with SDK-style format
- Use `Sdk="Microsoft.NET.Sdk"`
- Set `<TargetFramework>net10.0-windows10.0.22000.0</TargetFramework>`
- Add `<UseWinUI>true</UseWinUI>`
- Set `<TargetPlatformMinVersion>10.0.22000.0</TargetPlatformMinVersion>` if required
- Preserve entry points: `App.xaml`, assets, resource files
- Remove legacy UWP properties (TargetPlatformVersion, etc.)

**Step 3: Update Package References**
Remove:
```xml
<PackageReference Include="Microsoft.NETCore.UniversalWindowsPlatform" Version="5.3.3" />
<PackageReference Include="Win2D.uwp" Version="1.19.0" />
```

Add:
```xml
<PackageReference Include="Microsoft.WindowsAppSDK" Version="1.8.251106002" />
<PackageReference Include="Microsoft.Graphics.Win2D" Version="1.1.0" />
<PackageReference Include="Microsoft.Windows.Compatibility" Version="10.0.1" />
```

Update:
```xml
<PackageReference Include="Newtonsoft.Json" Version="13.0.4" />
```

**Step 4: Update XAML Namespaces**
In all XAML files, update namespace declarations:
- `xmlns:controls="using:Windows.UI.Xaml.Controls"` ? `xmlns:controls="using:Microsoft.UI.Xaml.Controls"`
- Replace all `Windows.UI.Xaml` references with `Microsoft.UI.Xaml`

**Step 5: Update Code Namespaces**
In all C# files:
- `using Windows.UI.Xaml;` ? `using Microsoft.UI.Xaml;`
- `using Windows.UI.Xaml.Controls;` ? `using Microsoft.UI.Xaml.Controls;`
- `using Windows.UI.Xaml.Media;` ? `using Microsoft.UI.Xaml.Media;`
- Similar changes for other Windows.UI.* namespaces

**Step 6: Update App Bootstrap (if applicable)**
- Update `App.xaml.cs` to use `Microsoft.UI.Xaml.Application`
- Update Window activation pattern for WinUI 3
- Replace `ApplicationView`, `CoreApplication` usages with WinAppSDK equivalents (`AppWindow`, `DispatcherQueue`)

**Step 7: Resource Dictionaries & Assets**
- Update merged dictionaries to WinUI styles
- Ensure `Themes/Generic.xaml` (if exists) uses correct WinUI namespaces
- Verify asset items via `Content`/`None` entries

**Step 8: Build and Verify**
- Restore packages: `dotnet restore`
- Build solution: `dotnet build`
- Fix all compilation errors related to namespace/API changes
- Rebuild to verify 0 errors

#### Validation Checklist
- [ ] Project builds without errors
- [ ] Project builds without warnings (migration-related)
- [ ] All namespace changes applied
- [ ] All package references updated
- [ ] Control renders correctly (if sample available)
- [ ] JSON serialization paths work (Newtonsoft upgrade validation)

---

## 5. Package Update Reference

### Package Changes Summary

| Package | Current Version | Target Version | Action | Reason |
|---------|-----------------|----------------|--------|--------|
| Microsoft.NETCore.UniversalWindowsPlatform | 5.3.3 | — | ? Remove | UWP meta-package; replaced by Windows App SDK |
| Win2D.uwp | 1.19.0 | — | ? Remove | UWP-specific; replaced by Microsoft.Graphics.Win2D |
| Microsoft.WindowsAppSDK | — | 1.8.251106002 | ? Add | WinUI 3 platform package |
| Microsoft.Graphics.Win2D | — | 1.1.0 | ? Add | Win2D for WinUI 3 |
| Microsoft.Windows.Compatibility | — | 10.0.1 | ? Add | Fill gaps for legacy Windows APIs if needed |
| Newtonsoft.Json | 7.0.1 | 13.0.4 | ?? Upgrade | **Security vulnerability fix** |

### Packages Removed (1 project affected)

| Package | Version | Replacement |
|---------|---------|-------------|
| Microsoft.NETCore.UniversalWindowsPlatform | 5.3.3 | Microsoft.WindowsAppSDK + Microsoft.Graphics.Win2D + Microsoft.Windows.Compatibility |
| Win2D.uwp | 1.19.0 | Microsoft.Graphics.Win2D |

### Packages Added (1 project affected)

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.WindowsAppSDK | 1.8.251106002 | WinUI 3 runtime and APIs |
| Microsoft.Graphics.Win2D | 1.1.0 | 2D graphics (Win2D) for WinUI 3 |
| Microsoft.Windows.Compatibility | 10.0.1 | Compatibility shim for legacy Windows APIs |

### Packages Upgraded (1 project affected)

| Package | Current | Target | Impact |
|---------|---------|--------|--------|
| Newtonsoft.Json | 7.0.1 | 13.0.4 | **Security fix** - addresses known vulnerabilities |

### Security Vulnerabilities Addressed

| Package | CVE/Advisory | Severity | Resolution |
|---------|--------------|----------|------------|
| Newtonsoft.Json | Multiple (7.0.1 is EOL) | Medium-High | Upgrade to 13.0.4 |

## 6. Breaking Changes Catalog

### Framework Breaking Changes (UWP ? WinUI 3)

#### Namespace Changes
| UWP Namespace | WinUI 3 Namespace | Impact |
|---------------|-------------------|--------|
| `Windows.UI.Xaml` | `Microsoft.UI.Xaml` | All XAML/code files |
| `Windows.UI.Xaml.Controls` | `Microsoft.UI.Xaml.Controls` | All control references |
| `Windows.UI.Xaml.Media` | `Microsoft.UI.Xaml.Media` | Brushes, transforms |
| `Windows.UI.Xaml.Input` | `Microsoft.UI.Xaml.Input` | Input handling |
| `Windows.UI.Xaml.Data` | `Microsoft.UI.Xaml.Data` | Data binding |
| `Windows.UI.Xaml.Shapes` | `Microsoft.UI.Xaml.Shapes` | Shape elements |
| `Windows.UI.Composition` | `Microsoft.UI.Composition` | Composition APIs |
| `Windows.UI.Text` | `Microsoft.UI.Text` | Text formatting |

#### API Changes
| UWP API | WinUI 3 Equivalent | Notes |
|---------|-------------------|-------|
| `ApplicationView` | `AppWindow` | Window management |
| `CoreApplication` | `Application` | App lifecycle |
| `CoreWindow` | `Window` | Window access |
| `CoreDispatcher` | `DispatcherQueue` | UI thread dispatching |
| `Window.Current` | `App.Window` or explicit reference | Window access pattern |

#### XAML Declaration Changes
| UWP | WinUI 3 |
|-----|---------|
| `xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"` | Same (unchanged) |
| `xmlns:controls="using:Windows.UI.Xaml.Controls"` | `xmlns:controls="using:Microsoft.UI.Xaml.Controls"` |

### Package API Changes

#### Win2D (Win2D.uwp ? Microsoft.Graphics.Win2D)
- Namespace remains `Microsoft.Graphics.Canvas.*` (no change)
- APIs are compatible; primarily a package swap
- Ensure Win2D device creation uses WinUI 3 patterns

#### Newtonsoft.Json (7.0.1 ? 13.0.4)
- Generally backward compatible
- Some default serialization settings may differ
- `TypeNameHandling` security improvements (more restrictive defaults)

### Configuration Changes

#### Project File
| UWP Property | WinUI 3 Equivalent |
|--------------|-------------------|
| `<TargetPlatformVersion>` | `<TargetFramework>net10.0-windows10.0.22000.0</TargetFramework>` |
| `<TargetPlatformMinVersion>` | `<TargetPlatformMinVersion>10.0.22000.0</TargetPlatformMinVersion>` |
| N/A | `<UseWinUI>true</UseWinUI>` (required) |

### UWP-Specific APIs Not Available in WinUI 3
The following UWP-only features are **not available** in WinUI 3 (verify not used):
- Background tasks (use Windows Runtime background tasks separately)
- Restricted capabilities (app-specific)
- Live tiles (deprecated)
- Cortana integration
- Some SystemMediaTransportControls scenarios

?? **Action**: Verify the CountryPicker control does not use these APIs. Based on assessment (0 API issues), these are likely not used.

---

## 7. Risk Management

### Risk Assessment

| Risk Area | Level | Description | Mitigation |
|-----------|-------|-------------|------------|
| Namespace changes | ?? Low | Well-documented migration path | Systematic find/replace; compiler will catch misses |
| Package replacement | ?? Low | Clear replacements identified | Use exact versions from assessment |
| Win2D compatibility | ?? Medium | Different package, same APIs | Test Win2D rendering after migration |
| Security vulnerability | ?? Low | Newtonsoft.Json upgrade | Direct upgrade with backward compatibility |
| Project conversion | ?? Low | SDK-style conversion is well-documented | Follow standard conversion pattern |

### High-Risk Changes

| Project | Risk Level | Description | Mitigation |
|---------|------------|-------------|------------|
| CountryPicker.UWP | ?? Low | Standard UWP?WinUI migration | Follow documented migration steps |

### Security Vulnerabilities

| Package | Current | Issue | Resolution | Priority |
|---------|---------|-------|------------|----------|
| Newtonsoft.Json | 7.0.1 | Known security vulnerabilities | Upgrade to 13.0.4 | ?? High - Fix during upgrade |

### Contingency Plans

| Issue | Contingency |
|-------|-------------|
| Missing API in WinUI 3 | Use Microsoft.Windows.Compatibility package or redesign feature |
| Win2D rendering issues | Verify Microsoft.Graphics.Win2D APIs; check device creation patterns |
| Build failures | Reference breaking changes catalog; check namespace migrations |
| Resource loading failures | Verify Generic.xaml and resource dictionary namespaces updated |

---

## 8. Testing & Validation Strategy

### Build Validation
After atomic upgrade:
- [ ] `dotnet restore` completes successfully
- [ ] `dotnet build` completes with 0 errors
- [ ] No migration-related warnings

### Runtime Validation
- [ ] Control loads without XAML parse errors
- [ ] Control renders correctly (visual inspection)
- [ ] User interactions work as expected
- [ ] Resource dictionaries load (no missing resource errors)

### Functional Validation
- [ ] JSON serialization/deserialization works (Newtonsoft upgrade)
- [ ] Win2D graphics render correctly (if used by control)
- [ ] Country picker functionality intact

### Smoke Test Checklist
- [ ] Application starts without crash
- [ ] Main control instantiates
- [ ] Visual elements display correctly
- [ ] Basic interactions respond

---

## 9. Complexity & Effort Assessment

### Overall Complexity
**Low** — Single project, well-known migration path, minimal API incompatibilities.

### Complexity Breakdown

| Component | Complexity | Rationale |
|-----------|------------|-----------|
| Project conversion | Low | Standard SDK-style conversion |
| Package updates | Low | Clear replacements; no ambiguity |
| Namespace changes | Low | Systematic replacement |
| API compatibility | Low | 0 API issues in assessment |
| Testing | Low | Small codebase (1,940 LOC) |

### Key Drivers
1. **Package replacement** — UWP packages ? WinUI 3 equivalents (well-documented)
2. **Namespace updates** — Windows.UI.* ? Microsoft.UI.* (mechanical transformation)
3. **Security fix** — Newtonsoft.Json upgrade (direct replacement)

### Resource Requirements
- **.NET 10 SDK** installed
- **Windows App SDK 1.8** workload available
- **Windows 11** (or Windows 10 22000+) for testing

---

## 10. Source Control Strategy

### Branching Strategy
- **Source branch**: `master`
- **Upgrade branch**: `upgrade-to-NET10` (already created)
- **Merge approach**: Single PR from upgrade branch to master after validation

### Commit Strategy
**Single consolidated commit** for the atomic upgrade (All-At-Once Strategy):
- Project file conversion to SDK-style
- Target framework update to net10.0-windows10.0.22000.0
- Package reference updates (remove UWP, add WinUI 3)
- XAML namespace updates
- C# namespace updates
- All compilation fixes

**Commit message format**:
```
Upgrade to .NET 10.0 and WinUI 3

- Convert project to SDK-style format
- Update target framework to net10.0-windows10.0.22000.0
- Replace UWP packages with Windows App SDK
- Replace Win2D.uwp with Microsoft.Graphics.Win2D
- Upgrade Newtonsoft.Json to 13.0.4 (security fix)
- Update namespaces from Windows.UI.* to Microsoft.UI.*
```

### Review Process
- Single PR with all upgrade changes
- Build verification (CI should pass)
- Code review checklist:
  - [ ] All namespace changes applied
  - [ ] All package references correct
  - [ ] No UWP-specific code remaining
  - [ ] Security vulnerability addressed

---

## 11. Success Criteria

### Technical Criteria
- [ ] Project is SDK-style targeting `net10.0-windows10.0.22000.0`
- [ ] `<UseWinUI>true</UseWinUI>` is set
- [ ] All legacy UWP packages removed
- [ ] Windows App SDK 1.8.251106002 installed
- [ ] Microsoft.Graphics.Win2D 1.1.0 installed
- [ ] Microsoft.Windows.Compatibility 10.0.1 installed
- [ ] Newtonsoft.Json upgraded to 13.0.4
- [ ] Solution builds with 0 errors
- [ ] No migration-related warnings

### Quality Criteria
- [ ] All XAML namespaces updated to Microsoft.UI.*
- [ ] All C# using statements updated
- [ ] No deprecated UWP patterns remaining
- [ ] Control functionality preserved

### Security Criteria
- [ ] Newtonsoft.Json security vulnerability resolved
- [ ] No remaining security advisories for packages

### Process Criteria
- [ ] All-At-Once Strategy followed (single atomic upgrade)
- [ ] Single consolidated commit
- [ ] Source control strategy followed
- [ ] Validation checklist completed

### Definition of Done
The migration is **complete** when:
1. ? Project targets `net10.0-windows10.0.22000.0` with WinUI 3
2. ? All packages updated to specified versions
3. ? Build succeeds with 0 errors
4. ? Security vulnerability (Newtonsoft.Json) resolved
5. ? Control functions correctly (runtime validation)
