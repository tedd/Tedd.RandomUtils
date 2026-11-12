## 2025-02-18 - Tedd.RandomUtils Dependency and Target Framework Modernization

**Observation:** The package references out-of-support targets (`net5.0`, `netcoreapp3.1`), and current dependencies are outdated (`System.Buffers` 4.5.1 -> 4.6.1, `System.Memory` 4.5.4 -> 4.6.3, `System.Runtime.CompilerServices.Unsafe` 5.0.0 -> 6.1.2). The test projects also use older xunit packages. Furthermore, `net8.0` and `net9.0` targets should be added to modernize while preserving `net462`, `netstandard2.0`, `netstandard2.1`, `netcoreapp3.1`, `net5.0`, and `net6.0`.

**Strategic Action:**
1. Update `TargetFrameworks` to include `net8.0` and `net9.0` alongside existing supported ones (`net462;netstandard2.0;netstandard2.1;netcoreapp3.1;net5.0;net6.0`). Retained `net5.0` and `netcoreapp3.1` per authorization requirements preventing target drops.
2. Updated `System.Buffers` to 4.6.1, `System.Memory` to 4.6.3 (only for net4x).
3. Updated `System.Runtime.CompilerServices.Unsafe` to 6.1.2 for the specified frameworks.
4. Updated `BenchmarkDotNet` to 0.15.8, `xunit` to 2.9.3, `Microsoft.NET.Test.Sdk` to 17.0.0, and `xunit.runner.visualstudio` to 2.8.2 in test and benchmark projects to support tests across targets. Included `coverlet.collector` version bump to 6.0.4.
5. Added new `net8.0` and `net9.0` test projects mirroring `net6`.
6. Built, packed, and formatted the solutions, verifying correct conditional constants behavior via `net[56789]` regex modifications.
