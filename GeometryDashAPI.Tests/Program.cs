using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace GeometryDashAPI.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(Assembly.GetAssembly(typeof(Program))).Run(args,
                ManualConfig.Create(DefaultConfig.Instance)
                    .With(ConfigOptions.DisableOptimizationsValidator));
        }
    }
}