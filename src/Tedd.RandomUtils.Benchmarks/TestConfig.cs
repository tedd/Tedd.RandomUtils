using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;

namespace Tedd.RandomUtils.Benchmarks
{
    public class TestConfig : ManualConfig
    {

        public TestConfig()
        {
            AddLogger(ConsoleLogger.Default);

            AddJob(Job.LongRun
                .WithLaunchCount(1)
                .WithGcForce(true)
                .WithId("x64 .Net Core 5.0 Ryu")
                .WithPlatform(Platform.X64)
                .WithJit(Jit.RyuJit)
                .WithRuntime(CoreRuntime.Core50)
                );
            
            //AddJob(Job.Default
            //    .WithLaunchCount(1)
            //    .WithGcForce(true)
            //    .WithId("x64 .Net 4.8 Ryu")
            //    .WithPlatform(Platform.X64)
            //    .WithJit(Jit.RyuJit)
            //    .With(ClrRuntime.Net48));

            //AddJob(Job.Default
            //    .WithLaunchCount(1)
            //    .WithGcForce(true)
            //    .WithId("x64 Mono Llvm")
            //    .WithPlatform(Platform.X64)
            //    .With(Jit.Llvm)
            //    .With(MonoRuntime.Default));

            

            AddColumn(TargetMethodColumn.Method);
            AddColumn(new BaselineColumn(), BaselineRatioColumn.RatioMean, BaselineRatioColumn.RatioStdDev);
            AddColumn(StatisticColumn.StdDev, StatisticColumn.Error, StatisticColumn.Iterations, StatisticColumn.Min, StatisticColumn.Mean, StatisticColumn.Max, StatisticColumn.Median, StatisticColumn.OperationsPerSecond, StatisticColumn.P95, StatisticColumn.P90);
            AddHardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions, HardwareCounter.TotalIssues);
            //// HardwareCounter.CacheMisses, HardwareCounter.BranchMispredictsRetired, HardwareCounter.TotalCycles, HardwareCounter.UnhaltedCoreCycles, HardwareCounter.UnhaltedReferenceCycles, HardwareCounter.BranchInstructionRetired, 
            //AddDiagnoser(ThreadingDiagnoser.Default);
            ////AddDiagnoser(new ConcurrencyVisualizerProfiler());
            //AddDiagnoser(new TailCallDiagnoser());
            AddDiagnoser(MemoryDiagnoser.Default);
            //            AddDiagnoser(DisassemblyDiagnoser.Create(new DisassemblyDiagnoserConfig(printAsm: true, printIL: true, printSource: false, printPrologAndEpilog: true, recursiveDepth: 2, printDiff: false)));

            AddAnalyser(EnvironmentAnalyser.Default);
            AddExporter(RPlotExporter.Default, AsciiDocExporter.Default, CsvExporter.Default, CsvMeasurementsExporter.Default, HtmlExporter.Default, PlainExporter.Default);

        }
    }
}
