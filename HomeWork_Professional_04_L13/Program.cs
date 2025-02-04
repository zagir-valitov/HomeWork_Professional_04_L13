using System.Diagnostics;

Console.WriteLine("HomeWork Professional 03\n");

Stopwatch stopwatch = new Stopwatch();
int[] arrayInts = { 100_000, 1_000_000, 10_000_0000 };

foreach (int ints in arrayInts)
{ 
    ShowResult(ints); 
}


void ShowResult(int i)
{
    Console.WriteLine($"\nFor {i} elements\n" +
        $"--------------------------");
    int[] array = Enumerable.Range(0, i).ToArray();

    stopwatch.Start();
    long sum = SimplySummation(array);
    stopwatch.Stop();
    Log("Simply Summation", sum, stopwatch);

    stopwatch.Restart();
    sum = ParallelSummation(array);
    stopwatch.Stop();
    Log("Parallel Summation", sum, stopwatch);

    stopwatch.Restart();
    sum = PLinqParallelSummation(array);
    stopwatch.Stop();
    Log("PLinq Parallel Summation", sum, stopwatch);
}

long SimplySummation(int[] array)
{
    long sum = 0;
    foreach (long i in array)
        sum += i;
    return sum;
}

long PLinqParallelSummation(int[] array)
{
    return array.AsParallel().Sum(x => (long)x);
}

long ParallelSummation(int[] array)
{
    long sum = 0;
    Parallel.ForEach(array, i => 
    {
        Interlocked.Add(ref sum, (long)i);
        //Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId} \t Sum: {sum}");
    });
    return sum;
}

void Log(string method, long sum, Stopwatch sw)
{
    Console.WriteLine($"{method}:" +
        $"\n * Sum:      {sum}" +
        $"\n * Time, ms: {sw.Elapsed.TotalMilliseconds}\n");
}


