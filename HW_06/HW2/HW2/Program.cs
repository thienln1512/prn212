using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW2
{
    public delegate string DataProcessor(string input);
    public delegate void ProcessingEventHandler(string stage, string input, string output);

    public class DataProcessingPipeline
    {
        public event ProcessingEventHandler ProcessingStageCompleted;

        protected virtual void OnProcessingStageCompleted(string stage, string input, string output)
        {
            ProcessingStageCompleted?.Invoke(stage, input, output);
        }

        public static string RemoveSpaces(string input)
        {
            return input.Replace(" ", "");
        }

        public static string ToUpperCase(string input)
        {
            return input.ToUpper();
        }

        public static string AddTimestamp(string input)
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {input}";
        }

        public static string ReverseString(string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }

        public static string EncodeBase64(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        public static string ValidateInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Input cannot be null or empty");
            return input;
        }

        public string ProcessData(string input, DataProcessor pipeline)
        {
            string current = input;
            foreach (DataProcessor processor in pipeline.GetInvocationList())
            {
                string stage = processor.Method.Name;
                string output;
                try
                {
                    output = processor(current);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error in stage {stage}: {ex.Message}", ex);
                }
                OnProcessingStageCompleted(stage, current, output);
                current = output;
            }
            return current;
        }
    }

    public class ProcessingLogger
    {
        public void OnProcessingStageCompleted(string stage, string input, string output)
        {
            Console.WriteLine($"[LOG] {stage}: \"{input}\" -> \"{output}\"");
        }
    }

    public class PerformanceMonitor
    {
        private DateTime _lastTime;
        private readonly Dictionary<string, List<double>> _timings = new Dictionary<string, List<double>>();

        public void OnProcessingStageCompleted(string stage, string input, string output)
        {
            var now = DateTime.Now;
            if (_lastTime != default)
            {
                double ms = (now - _lastTime).TotalMilliseconds;
                if (!_timings.ContainsKey(stage))
                    _timings[stage] = new List<double>();
                _timings[stage].Add(ms);
            }
            _lastTime = now;
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("\nPerformance Statistics (ms):");
            foreach (var kv in _timings)
            {
                double avg = kv.Value.Average();
                Console.WriteLine($"{kv.Key}: Count={kv.Value.Count}, Avg={avg:F2}");
            }
        }
    }

    public class DelegateChain
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 2: CUSTOM DELEGATE CHAIN ===");

            var pipeline = new DataProcessingPipeline();
            var logger = new ProcessingLogger();
            var monitor = new PerformanceMonitor();

            pipeline.ProcessingStageCompleted += logger.OnProcessingStageCompleted;
            pipeline.ProcessingStageCompleted += monitor.OnProcessingStageCompleted;

            DataProcessor processingChain = DataProcessingPipeline.ValidateInput;
            processingChain += DataProcessingPipeline.RemoveSpaces;
            processingChain += DataProcessingPipeline.ToUpperCase;
            processingChain += DataProcessingPipeline.AddTimestamp;

            string testInput = "Hello World from C#";
            Console.WriteLine($"\nInput: {testInput}");
            string result = pipeline.ProcessData(testInput, processingChain);
            Console.WriteLine($"\nOutput: {result}");

            processingChain += DataProcessingPipeline.ReverseString;
            processingChain += DataProcessingPipeline.EncodeBase64;
            result = pipeline.ProcessData("Extended Pipeline Test", processingChain);
            Console.WriteLine($"\nExtended Output: {result}");

            processingChain -= DataProcessingPipeline.ReverseString;
            result = pipeline.ProcessData("Without Reverse", processingChain);
            Console.WriteLine($"\nModified Output: {result}");

            monitor.DisplayStatistics();

            try
            {
                result = pipeline.ProcessData(null, processingChain);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError handled: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
