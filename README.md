﻿A simple and naive test to compare sync and async DB access in datacenter-like environment.
When running make sure to initiate an instance of MongoDB (see AsyncTest class constructor).
I do not recommend running this instance on localhost, since it will skew test results greatly, both due to ultra-low latency of localhost TCP and additional load, generated by instance of MongoDB.
On the other hand, do not access the database over the internet, since latency would be too high.

Below are some actual results:

```
BenchmarkDotNet=v0.11.3, OS=ubuntu 16.04
Intel Xeon CPU E5-2630 v2 2.60GHz, 2 CPU, 24 logical and 12 physical cores
.NET Core SDK=2.2.102
  [Host]     : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Job-XKIMZH : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Server=True
```

    Method | LoadFactor |       Mean |     Error |     StdDev | Ratio | RatioSD |
---------- |----------- |-----------:|----------:|-----------:|------:|--------:|
  TestSync |         50 |   8.335 ms | 0.1583 ms |  0.1823 ms |  1.00 |    0.00 |
 TestAsync |         50 |  10.684 ms | 0.2125 ms |  0.2274 ms |  1.28 |    0.04 |
           |            |            |           |            |       |         |
  TestSync |        100 |  10.737 ms | 0.2094 ms |  0.2240 ms |  1.00 |    0.00 |
 TestAsync |        100 |  14.665 ms | 0.2899 ms |  0.6179 ms |  1.38 |    0.08 |
           |            |            |           |            |       |         |
  TestSync |        200 |  20.058 ms | 0.3893 ms |  0.3997 ms |  1.00 |    0.00 |
 TestAsync |        200 |  24.005 ms | 0.4756 ms |  0.7946 ms |  1.21 |    0.05 |
           |            |            |           |            |       |         |
  TestSync |        400 |  38.678 ms | 0.7692 ms |  1.4447 ms |  1.00 |    0.00 |
 TestAsync |        400 |  41.237 ms | 0.8189 ms |  1.4342 ms |  1.06 |    0.06 |
           |            |            |           |            |       |         |
  TestSync |        800 |  74.975 ms | 1.4714 ms |  2.7273 ms |  1.00 |    0.00 |
 TestAsync |        800 | 102.508 ms | 3.2215 ms |  9.3463 ms |  1.40 |    0.14 |
           |            |            |           |            |       |         |
  TestSync |       1600 | 160.166 ms | 3.0882 ms |  3.3043 ms |  1.00 |    0.00 |
 TestAsync |       1600 | 207.163 ms | 6.6909 ms | 19.5177 ms |  1.27 |    0.11 |


```
BenchmarkDotNet=v0.11.3, OS=Windows 8.1 (6.3.9600.0)
Intel Xeon CPU E5-2620 v2 2.10GHz, 2 CPU, 24 logical and 12 physical cores
Frequency=2050782 Hz, Resolution=487.6189 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Job-YJMWXB : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Server=True
```

    Method | LoadFactor |       Mean |      Error |     StdDev | Ratio | RatioSD |
---------- |----------- |-----------:|-----------:|-----------:|------:|--------:|
  TestSync |         50 |   9.021 ms |  0.1145 ms |  0.1071 ms |  1.00 |    0.00 |
 TestAsync |         50 |  12.522 ms |  0.1242 ms |  0.1101 ms |  1.39 |    0.02 |
           |            |            |            |            |       |         |
  TestSync |        100 |  10.139 ms |  0.0701 ms |  0.0622 ms |  1.00 |    0.00 |
 TestAsync |        100 |  17.473 ms |  0.3489 ms |  0.5829 ms |  1.75 |    0.06 |
           |            |            |            |            |       |         |
  TestSync |        200 |  19.160 ms |  0.1233 ms |  0.1030 ms |  1.00 |    0.00 |
 TestAsync |        200 |  32.349 ms |  0.8388 ms |  2.4334 ms |  1.65 |    0.08 |
           |            |            |            |            |       |         |
  TestSync |        400 |  38.093 ms |  0.3910 ms |  0.3466 ms |  1.00 |    0.00 |
 TestAsync |        400 |  56.067 ms |  1.1192 ms |  2.9873 ms |  1.52 |    0.06 |
           |            |            |            |            |       |         |
  TestSync |        800 |  76.588 ms |  1.6567 ms |  1.7013 ms |  1.00 |    0.00 |
 TestAsync |        800 | 164.823 ms |  6.2946 ms | 18.4611 ms |  2.20 |    0.28 |
           |            |            |            |            |       |         |
  TestSync |       1600 | 165.933 ms |  2.3194 ms |  2.1696 ms |  1.00 |    0.00 |
 TestAsync |       1600 | 351.830 ms | 11.0559 ms | 32.5987 ms |  2.10 |    0.17 |
