namespace Difference {
    public class User {
        public Guid Id { get; set; }
        public string String { get; set; }
        public int Int { get; set; }
        public bool Bool { get; set; }
        public decimal Decimal { get; set; }
        public long Long { get; set; }
        public SubEnum Enum { get; set; }

        public string[] ArrayString { get; set; }
        public int[] ArrayInt { get; set; }

        public HashSet<Guid> HashSetGuid { get; set; }
        public HashSet<string> HashSetstring { get; set; }

        public List<Guid> ListGuid { get; set; }
        public List<string> Liststring { get; set; }
        public List<SubClass> ListObj { get; set; }

        public IDictionary<string, Guid> DictionaryGuid { get; set; }
        public IDictionary<string, SubClass> DictionaryObj { get; set; }

        public IEnumerable<SubClass> EnumerableObj { get; set; }
        public IEnumerable<string> EnumerableString { get; set; }
    }
}