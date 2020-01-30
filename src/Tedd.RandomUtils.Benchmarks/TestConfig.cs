using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;

namespace Tedd.RandomUtils.Benchmarks
{
    public class TestConfig : ManualConfig
    {

        public TestConfig()
        {
            Add(ConsoleLogger.Default);

            Add(Job.Default
                .WithLaunchCount(1)
                .WithGcForce(true)
                .WithId("x64 .Net Core 3.1 Ryu")
                .With(Platform.X64)
                .With(Jit.RyuJit)
                .With(CoreRuntime.Core31));

            //Add(Job.Default
            //    .WithLaunchCount(1)
            //    .WithGcForce(true)
            //    .WithId("x64 .Net 4.8 Ryu")
            //    .With(Platform.X64)
            //    .With(Jit.RyuJit)
            //    .With(ClrRuntime.Net48));

            //Add(Job.Default
            //    .WithLaunchCount(1)
            //    .WithGcForce(true)
            //    .WithId("x64 Mono Llvm")
            //    .With(Platform.X64)
            //    .With(Jit.Llvm)
            //    .With(MonoRuntime.Default));

            Add(new[] { TargetMethodColumn.Method });
            Add(new[] { new BaselineColumn(), BaselineRatioColumn.RatioMean, BaselineRatioColumn.RatioStdDev });
            Add(new[] { StatisticColumn.StdDev, StatisticColumn.Error, StatisticColumn.Iterations, StatisticColumn.Min, StatisticColumn.Mean, StatisticColumn.Max, StatisticColumn.Median, StatisticColumn.OperationsPerSecond, StatisticColumn.P95, StatisticColumn.P90 });
            //Add(new[] { HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions, HardwareCounter.TotalIssues });
            //// HardwareCounter.CacheMisses, HardwareCounter.BranchMispredictsRetired, HardwareCounter.TotalCycles, HardwareCounter.UnhaltedCoreCycles, HardwareCounter.UnhaltedReferenceCycles, HardwareCounter.BranchInstructionRetired, 
            //Add(ThreadingDiagnoser.Default);
            ////Add(new ConcurrencyVisualizerProfiler());
            //Add(new TailCallDiagnoser());
            Add(MemoryDiagnoser.Default);
            //            Add(DisassemblyDiagnoser.Create(new DisassemblyDiagnoserConfig(printAsm: true, printIL: true, printSource: false, printPrologAndEpilog: true, recursiveDepth: 2, printDiff: false)));

            Add(EnvironmentAnalyser.Default);
            Add(new[] { RPlotExporter.Default, AsciiDocExporter.Default, CsvExporter.Default, CsvMeasurementsExporter.Default, HtmlExporter.Default, PlainExporter.Default });

        }
    }
}
