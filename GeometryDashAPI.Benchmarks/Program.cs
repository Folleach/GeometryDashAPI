using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

BenchmarkSwitcher.FromAssembly(Assembly.GetAssembly(typeof(Program))).Run(args,
    ManualConfig.Create(DefaultConfig.Instance)
        .With(ConfigOptions.DisableOptimizationsValidator));
