using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HW3
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double GPA { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }

    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public double Grade { get; set; }
        public string Semester { get; set; }
        public string Instructor { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class StudentStatistics
    {
        public double MeanGPA { get; set; }
        public double MedianGPA { get; set; }
        public double StandardDeviationGPA { get; set; }
        public double CorrelationAgeGPA { get; set; }
    }

    public static class StudentExtensions
    {
        public static IEnumerable<Student> FilterByAgeRange(this IEnumerable<Student> students, int minAge, int maxAge)
        {
            return students.Where(s => s.Age >= minAge && s.Age <= maxAge);
        }

        public static Dictionary<string, double> AverageGPAByMajor(this IEnumerable<Student> students)
        {
            return students
                .GroupBy(s => s.Major)
                .ToDictionary(g => g.Key, g => g.Average(s => s.GPA));
        }

        public static string ToGradeReport(this Student student)
        {
            var report = $"Grade report for {student.Name}:\n";
            foreach (var c in student.Courses)
            {
                report += $"  {c.Code} ({c.Name}): Grade = {c.Grade:F2}\n";
            }
            report += $"GPA: {student.GPA:F2}";
            return report;
        }

        public static StudentStatistics CalculateStatistics(this IEnumerable<Student> students)
        {
            var list = students.ToList();
            var gpas = list.Select(s => s.GPA).OrderBy(g => g).ToList();
            int n = gpas.Count;
            double mean = gpas.Average();
            double median = n % 2 == 1 ? gpas[n / 2] : (gpas[n / 2 - 1] + gpas[n / 2]) / 2.0;
            double var = gpas.Select(x => (x - mean) * (x - mean)).Sum() / n;
            double std = Math.Sqrt(var);
            var ages = list.Select(s => (double)s.Age).ToList();
            double meanAge = ages.Average();
            double cov = list.Select(s => (s.Age - meanAge) * (s.GPA - mean)).Sum() / n;
            double stdAge = Math.Sqrt(ages.Select(a => (a - meanAge) * (a - meanAge)).Sum() / n);
            double corr = stdAge > 0 && std > 0 ? cov / (stdAge * std) : 0;
            return new StudentStatistics
            {
                MeanGPA = mean,
                MedianGPA = median,
                StandardDeviationGPA = std,
                CorrelationAgeGPA = corr
            };
        }
    }

    public class LinqDataProcessor
    {
        private readonly List<Student> _students;

        public LinqDataProcessor()
        {
            _students = GenerateSampleData();
        }

        public void BasicQueries()
        {
            Console.WriteLine("=== BASIC LINQ QUERIES ===");

            var highGPA = _students.Where(s => s.GPA > 3.5);
            Console.WriteLine("Students with GPA > 3.5:");
            foreach (var s in highGPA) Console.WriteLine($"- {s.Name} ({s.GPA:F2})");

            Console.WriteLine("\nAverage GPA per major:");
            var avgMajor = _students
                .GroupBy(s => s.Major)
                .Select(g => new { Major = g.Key, AvgGPA = g.Average(s => s.GPA) });
            foreach (var g in avgMajor) Console.WriteLine($"- {g.Major}: {g.AvgGPA:F2}");

            Console.WriteLine("\nStudents enrolled in CS101:");
            var cs101 = _students.Where(s => s.Courses.Any(c => c.Code == "CS101"));
            foreach (var s in cs101) Console.WriteLine($"- {s.Name}");

            Console.WriteLine("\nStudents sorted by enrollment date:");
            var sorted = _students.OrderBy(s => s.EnrollmentDate);
            foreach (var s in sorted) Console.WriteLine($"- {s.Name}: {s.EnrollmentDate:yyyy-MM-dd}");

            Console.WriteLine();
        }

        public void CustomExtensionMethods()
        {
            Console.WriteLine("=== CUSTOM EXTENSION METHODS ===");

            var ageRange = _students.FilterByAgeRange(20, 25);
            Console.WriteLine("Students aged 20 to 25:");
            foreach (var s in ageRange) Console.WriteLine($"- {s.Name} ({s.Age})");

            Console.WriteLine("\nAverage GPA by major:");
            var avgByMajor = _students.AverageGPAByMajor();
            foreach (var kv in avgByMajor) Console.WriteLine($"- {kv.Key}: {kv.Value:F2}");

            Console.WriteLine("\nGrade reports:");
            foreach (var s in _students)
            {
                Console.WriteLine(s.ToGradeReport());
                Console.WriteLine();
            }

            var stats = _students.CalculateStatistics();
            Console.WriteLine($"Statistics: Mean={stats.MeanGPA:F2}, Median={stats.MedianGPA:F2}, StdDev={stats.StandardDeviationGPA:F2}, Corr(Age,GPA)={stats.CorrelationAgeGPA:F2}");
            Console.WriteLine();
        }

        public void DynamicQueries()
        {
            Console.WriteLine("=== DYNAMIC QUERIES ===");

            var filtered = ApplyDynamicFilter("GPA", ">", "3.5");
            Console.WriteLine("Dynamic filter GPA > 3.5:");
            foreach (var s in filtered) Console.WriteLine($"- {s.Name} ({s.GPA:F2})");

            var sortedDesc = ApplyDynamicSort(_students.AsQueryable(), "Age", asc: false);
            Console.WriteLine("\nDynamic sort by Age desc:");
            foreach (var s in sortedDesc) Console.WriteLine($"- {s.Name} ({s.Age})");

            var groups = ApplyDynamicGroup("Major");
            Console.WriteLine("\nDynamic group by Major:");
            foreach (var g in groups)
            {
                Console.WriteLine($"Major: {g.Key}");
                foreach (var s in g) Console.WriteLine($"  - {s.Name}");
            }

            Console.WriteLine();
        }

        private IQueryable<Student> ApplyDynamicFilter(string property, string op, string value)
        {
            var param = Expression.Parameter(typeof(Student), "s");
            var member = Expression.PropertyOrField(param, property);
            Expression constant;
            Expression body;

            if (member.Type == typeof(string))
            {
                constant = Expression.Constant(value);
                body = op == "=="
                    ? Expression.Equal(member, constant)
                    : Expression.Call(member, nameof(string.Contains), null, constant);
            }
            else
            {
                var converted = Convert.ChangeType(value, member.Type);
                constant = Expression.Constant(converted);
                switch (op)
                {
                    case ">": body = Expression.GreaterThan(member, constant); break;
                    case "<": body = Expression.LessThan(member, constant); break;
                    case ">=": body = Expression.GreaterThanOrEqual(member, constant); break;
                    case "<=": body = Expression.LessThanOrEqual(member, constant); break;
                    case "==": body = Expression.Equal(member, constant); break;
                    default: throw new NotSupportedException();
                }
            }

            var lambda = Expression.Lambda<Func<Student, bool>>(body, param);
            return _students.AsQueryable().Where(lambda);
        }

        private IOrderedQueryable<Student> ApplyDynamicSort(IQueryable<Student> source, string property, bool asc)
        {
            var param = Expression.Parameter(typeof(Student), "s");
            var member = Expression.PropertyOrField(param, property);
            var keySelector = Expression.Lambda(member, param);
            string method = asc ? "OrderBy" : "OrderByDescending";
            var sorted = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == method && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(Student), member.Type)
                .Invoke(null, new object[] { source, keySelector });
            return (IOrderedQueryable<Student>)sorted;
        }

        private IEnumerable<IGrouping<object, Student>> ApplyDynamicGroup(string property)
        {
            var param = Expression.Parameter(typeof(Student), "s");
            var member = Expression.PropertyOrField(param, property);
            var keySelector = Expression.Lambda<Func<Student, object>>(Expression.Convert(member, typeof(object)), param);
            return _students.AsQueryable().GroupBy(keySelector);
        }

        public void StatisticalAnalysis()
        {
            Console.WriteLine("=== STATISTICAL ANALYSIS ===");

            var stats = _students.CalculateStatistics();
            Console.WriteLine($"Mean GPA: {stats.MeanGPA:F2}");
            Console.WriteLine($"Median GPA: {stats.MedianGPA:F2}");
            Console.WriteLine($"Std Dev GPA: {stats.StandardDeviationGPA:F2}");
            Console.WriteLine($"Correlation Age vs GPA: {stats.CorrelationAgeGPA:F2}");

            var gpas = _students.Select(s => s.GPA).OrderBy(x => x).ToList();
            int n = gpas.Count;
            Console.WriteLine($"25th percentile GPA: {gpas[(int)(0.25 * (n - 1))]:F2}");
            Console.WriteLine($"75th percentile GPA: {gpas[(int)(0.75 * (n - 1))]:F2}");

            var outliers = _students.Where(s => s.GPA < stats.MeanGPA - 2 * stats.StandardDeviationGPA || s.GPA > stats.MeanGPA + 2 * stats.StandardDeviationGPA);
            Console.WriteLine("\nOutlier students by GPA:");
            foreach (var s in outliers) Console.WriteLine($"- {s.Name} ({s.GPA:F2})");

            Console.WriteLine();
        }

        public void PivotOperations()
        {
            Console.WriteLine("=== PIVOT OPERATIONS ===");

            var gpaRanges = new[] {
                (Min: 0.0, Max: 3.5, Label: "<3.5"),
                (Min: 3.5, Max: 4.0, Label: "3.5-4.0")
            };
            var pivot1 = _students
                .SelectMany(s => gpaRanges.Select(r => new { s.Major, Range = r.Label, Count = (s.GPA >= r.Min && s.GPA < r.Max) ? 1 : 0 }))
                .GroupBy(x => new { x.Major, x.Range })
                .Select(g => new { g.Key.Major, g.Key.Range, Count = g.Sum(x => x.Count) });
            Console.WriteLine("Students by Major and GPA range:");
            foreach (var p in pivot1) Console.WriteLine($"- {p.Major}, {p.Range}: {p.Count}");

            var pivot2 = _students
                .SelectMany(s => s.Courses.Select(c => new { s.Major, c.Semester }))
                .GroupBy(x => new { x.Semester, x.Major })
                .Select(g => new { g.Key.Semester, g.Key.Major, Count = g.Count() });
            Console.WriteLine("\nCourse enrollment by Semester and Major:");
            foreach (var p in pivot2) Console.WriteLine($"- {p.Semester}, {p.Major}: {p.Count}");

            var gradeRanges = new[] {
                (Min: 0.0, Max: 2.0, Label: "0-2"),
                (Min: 2.0, Max: 3.0, Label: "2-3"),
                (Min: 3.0, Max: 4.0, Label: "3-4")
            };
            var pivot3 = _students
                .SelectMany(s => s.Courses.SelectMany(c => gradeRanges.Select(r => new { c.Instructor, Range = r.Label, Count = (c.Grade >= r.Min && c.Grade < r.Max) ? 1 : 0 })))
                .GroupBy(x => new { x.Instructor, x.Range })
                .Select(g => new { g.Key.Instructor, g.Key.Range, Count = g.Sum(x => x.Count) });
            Console.WriteLine("\nGrade distribution by Instructor:");
            foreach (var p in pivot3) Console.WriteLine($"- {p.Instructor}, {p.Range}: {p.Count}");

            Console.WriteLine();
        }

        private List<Student> GenerateSampleData()
        {
            return new List<Student>
            {
                new Student { Id=1, Name="Alice Johnson", Age=20, Major="Computer Science", GPA=3.8, EnrollmentDate=new DateTime(2022,9,1), Email="alice.j@university.edu", Address=new Address{City="Seattle",State="WA",ZipCode="98101"}, Courses=new List<Course>{ new Course{Code="CS101",Name="Intro to Programming",Credits=3,Grade=3.7,Semester="Fall 2022",Instructor="Dr. Smith"}, new Course{Code="MATH201",Name="Calculus II",Credits=4,Grade=3.9,Semester="Fall 2022",Instructor="Prof. Johnson"} } },
                new Student { Id=2, Name="Bob Wilson", Age=22, Major="Mathematics",    GPA=3.2, EnrollmentDate=new DateTime(2021,9,1), Email="bob.w@university.edu", Address=new Address{City="Portland",State="OR",ZipCode="97201"}, Courses=new List<Course>{ new Course{Code="MATH301",Name="Linear Algebra",Credits=3,Grade=3.3,Semester="Spring 2023",Instructor="Dr. Brown"}, new Course{Code="STAT101",Name="Statistics",Credits=3,Grade=3.1,Semester="Spring 2023",Instructor="Prof. Davis"} } },
                new Student { Id=3, Name="Carol Davis", Age=19, Major="Computer Science", GPA=3.9, EnrollmentDate=new DateTime(2023,9,1), Email="carol.d@university.edu", Address=new Address{City="San Francisco",State="CA",ZipCode="94101"}, Courses=new List<Course>{ new Course{Code="CS102",Name="Data Structures",Credits=4,Grade=4.0,Semester="Fall 2023",Instructor="Dr. Smith"}, new Course{Code="CS201",Name="Algorithms",Credits=3,Grade=3.8,Semester="Fall 2023",Instructor="Prof. Lee"} } }
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== HOMEWORK 3: LINQ DATA PROCESSOR ===\n");

            var processor = new LinqDataProcessor();
            processor.BasicQueries();
            processor.CustomExtensionMethods();
            processor.DynamicQueries();
            processor.StatisticalAnalysis();
            processor.PivotOperations();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
