# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v8.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [CountryPicker.UWP\CountryPicker.UWP.csproj](#countrypickeruwpcountrypickeruwpcsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 1 | All require upgrade |
| Total NuGet Packages | 3 | 2 need upgrade |
| Total Code Files | 8 |  |
| Total Code Files with Incidents | 1 |  |
| Total Lines of Code | 1940 |  |
| Total Number of Issues | 7 |  |
| Estimated LOC to modify | 0+ | at least 0,0% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [CountryPicker.UWP\CountryPicker.UWP.csproj](#countrypickeruwpcountrypickeruwpcsproj) | net5.0 | 🟢 Low | 5 | 0 |  | Uwp, Sdk Style = False |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 1 | 33,3% |
| ⚠️ Incompatible | 1 | 33,3% |
| 🔄 Upgrade Recommended | 1 | 33,3% |
| ***Total NuGet Packages*** | ***3*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Microsoft.NETCore.UniversalWindowsPlatform | 5.3.3 |  | [CountryPicker.UWP.csproj](#countrypickeruwpcountrypickeruwpcsproj) | Needs to be replaced with Replace with new package Microsoft.WindowsAppSDK=1.8.251106002;Microsoft.Graphics.Win2D=1.1.0;Microsoft.Windows.Compatibility=10.0.1 |
| Newtonsoft.Json | 7.0.1 | 13.0.4 | [CountryPicker.UWP.csproj](#countrypickeruwpcountrypickeruwpcsproj) | NuGet package upgrade is recommended |
| Win2D.uwp | 1.19.0 | 1.27.1 | [CountryPicker.UWP.csproj](#countrypickeruwpcountrypickeruwpcsproj) | ⚠️NuGet package is incompatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>⚙️&nbsp;CountryPicker.UWP.csproj</b><br/><small>net5.0</small>"]
    click P1 "#countrypickeruwpcountrypickeruwpcsproj"

```

## Project Details

<a id="countrypickeruwpcountrypickeruwpcsproj"></a>
### CountryPicker.UWP\CountryPicker.UWP.csproj

#### Project Info

- **Current Target Framework:** net5.0
- **Proposed Target Framework:** net8.0-windows10.0.22000.0
- **SDK-style**: False
- **Project Kind:** Uwp
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 259
- **Number of Files with Incidents**: 1
- **Lines of Code**: 1940
- **Estimated LOC to modify**: 0+ (at least 0,0% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["CountryPicker.UWP.csproj"]
        MAIN["<b>⚙️&nbsp;CountryPicker.UWP.csproj</b><br/><small>net5.0</small>"]
        click MAIN "#countrypickeruwpcountrypickeruwpcsproj"
    end

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 0 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 0 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 0 |  |
| ***Total APIs Analyzed*** | ***0*** |  |

