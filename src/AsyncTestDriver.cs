using System;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace AsyncTest
{
    [GcServer(true)]
    public class AsyncTestDriver
    {
        private const int MaxLoadFactor = 1600;
        private static readonly int CpuCount = Environment.ProcessorCount;
        private static readonly ParallelOptions RecommendedParallelism = new ParallelOptions { MaxDegreeOfParallelism = (int)Math.Ceiling(CpuCount * 1.618) };

        private int _loadFactor = 100;
        private readonly AsyncTest _test;
        private Task[] _tasksCache;

        public AsyncTestDriver()
        {
            _test = new AsyncTest();
        }

        [Params(50, 100, 200, 400, 800, 1600)]
        public int LoadFactor
        {
            get
            {
                return _loadFactor;
            }

            set
            {
                if (value < 1 || value > MaxLoadFactor)
                {
                    throw new ArgumentOutOfRangeException(nameof(LoadFactor), value, "Overload factor is out of range.");
                }

                _loadFactor = value;
            }
        }

        [GlobalSetup]
        public void Initialize() => _test.Initialize();

        [Benchmark(Baseline = true)]
        public void TestSync()
        {
            Parallel.For(
                0, 
                Parallelism, 
                RecommendedParallelism, 
                _ => _test.TestSync());
        }

        [Benchmark]
        public Task TestAsync()
        {
            var tasks = CreateTasks();
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = _test.TestAsync();
            }

            return Task.WhenAll(tasks);
        }

        private Task[] CreateTasks()
        {
            var count = Parallelism;
            var tasks = _tasksCache;
            return tasks?.Length == count
                ? tasks
                : _tasksCache = tasks = new Task[count];
        }

        private int Parallelism => (int)Math.Ceiling(CpuCount * LoadFactor / 100.0);
    }
}
