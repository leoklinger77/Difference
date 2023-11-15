namespace Difference {
    using Newtonsoft.Json;
    using System.Diagnostics;

    internal class Program {
        static void Main(string[] args) {
            var oldValue = new User {
                Id = Guid.NewGuid(),
                String = "John Doe",
                Int = 25,
                Bool = true,
                Decimal = 123.45m,
                Long = 987654321,
                Enum = SubEnum.Value1,
                ArrayString = new string[] { "Leandro", "Klinger", "banana" },
                ArrayInt = new int[] { 11, 22, 33 },
                HashSetGuid = new HashSet<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                HashSetstring = new HashSet<string> { "red", "green", "blue" },
                ListGuid = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                Liststring = new List<string> { "cat", "dog", "fish", "sda", "12312312" },
                ListObj = new List<SubClass> {
                new SubClass { Property1 = "A", Property2 = "B" },
                new SubClass { Property1 = "X", Property2 = "Y" }
                },
                DictionaryGuid = new Dictionary<string, Guid> {
                    { "one", Guid.NewGuid() },
                    { "tw1o", Guid.NewGuid() },
                    { "two2", Guid.NewGuid() },
                    { "two23", Guid.NewGuid() },
                    { "two3", Guid.NewGuid() },
                    { "two", Guid.NewGuid() },
                },
                DictionaryObj = new Dictionary<string, SubClass> {
                    { "obj1", new SubClass { Property1 = "M", Property2 = "N" } },
                    { "obj2", new SubClass { Property1 = "P", Property2 = "Q" } }
                },
                EnumerableObj = new List<SubClass> {
                    new SubClass { Property1 = "C", Property2 = "D" },
                    new SubClass { Property1 = "E", Property2 = "F" }
                },
                EnumerableString = new List<string> { "sun", "moon", "stars" }
            };

            var newValue = new User {
                Id = Guid.NewGuid(),
                String = "Leandro Klinger",
                Int = 25,
                Bool = true,
                Decimal = 123.45m,
                Long = 987654321,
                Enum = SubEnum.Value1,
                ArrayString = new string[] { "apple", "orange", "banana" },
                ArrayInt = new int[] { 1, 2, 3 },
                HashSetGuid = new HashSet<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                HashSetstring = new HashSet<string> { "red", "green", "blue" },
                ListGuid = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
                Liststring = new List<string> { "cat", "dog", "fish" },
                ListObj = new List<SubClass> {
                new SubClass { Property1 = "A", Property2 = "B" },
                new SubClass { Property1 = "X", Property2 = "Y" }
                },
                DictionaryGuid = new Dictionary<string, Guid> {
                    { "one", Guid.NewGuid() },
                    { "two", Guid.NewGuid() }
                },
                DictionaryObj = new Dictionary<string, SubClass> {
                    { "obj1", new SubClass { Property1 = "M", Property2 = "N" } },
                    { "obj2", new SubClass { Property1 = "P", Property2 = "Q" } }
                },
                EnumerableObj = new List<SubClass> {
                    new SubClass { Property1 = "C", Property2 = "D" },
                    new SubClass { Property1 = "E", Property2 = "F" }
                },
                EnumerableString = new List<string> { "sun", "moon", "stars" }
            };

            Stopwatch ws = Stopwatch.StartNew();

            var list = Difference.Diff(oldValue, newValue);

            ws.Stop();

            Console.WriteLine(ws.Elapsed.TotalMilliseconds);

            File.WriteAllText("C:\\temp\\file.json", JsonConvert.SerializeObject(list));
        }
    }    
}