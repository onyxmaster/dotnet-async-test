namespace AsyncTest
{
    static class Program
    {
        static void Main() => BenchmarkDotNet.Running.BenchmarkRunner.Run<AsyncTestDriver>();
    }
}
