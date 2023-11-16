namespace Difference.Test {
    internal record class DictionaryAndEnumerableModel(IDictionary<string, string> dictionary, HashSet<Guid> hashSet, string[] array, IEnumerable<Guid> enumerable, ICollection<string> collection, List<int> list, IList<char> ilist);
    public class DictionaryAndEnumerableTest {
        [Fact]
        public void Dictionary_Difference_OldNull() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(null, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() {
                { "firstName","Leandro" },
                { "lastName","Klinger" },
                { "fullname","Leandro Klinger" }
            }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Dictionary_Difference_OldOnce() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { { "firstName", "Lydia" } }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() {
                { "firstName","Leandro" },
                { "lastName","Klinger" },
                { "fullname","Leandro Klinger" }
            }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Dictionary_Difference_OldObjNull() {
            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() {
                { "firstName","Leandro" },
                { "lastName","Klinger" },
                { "fullname","Leandro Klinger" }
            }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(null, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void HashSet_Difference_old() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();

            var guid = Guid.NewGuid();

            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>() { Guid.NewGuid(), guid }, new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>() { guid, Guid.NewGuid(), Guid.NewGuid() }, new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void HashSet_Difference_new() {

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var guid = Guid.NewGuid();

            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>() { guid, Guid.NewGuid(), Guid.NewGuid() }, new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();

            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>() { Guid.NewGuid(), guid }, new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Array_Difference_OldOnce() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { "firstName", "Lydia" }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>(), new HashSet<Guid>(), new string[] { "Lydia", "firstName", }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(2, diffGuid.Count());
        }

        [Fact]
        public void Array_Difference_OldLength() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { "firstName", "Lydia", "Leandro" }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>(), new HashSet<Guid>(), new string[] { "Lydia", "firstName", }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Array_Difference_ONewLength() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { "firstName", "Lydia" }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>(), new HashSet<Guid>(), new string[] { "Lydia", "firstName", "Leandro" }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Enumerable_Difference_OldLength() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Enumerable_Difference_NewLength() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void Enumerable_Difference() {
            var guid = Guid.NewGuid();

            IEnumerable<Guid> oldEnumerable = new List<Guid>() { Guid.NewGuid(), guid, Guid.NewGuid() };
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>() { guid, Guid.NewGuid() };
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }


        [Fact]
        public void Collection_Difference_OldLength() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>() { "firstName", "Lydia" };
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>() { "first Name", "Lydia" };
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }

        [Fact]
        public void Collection_Difference_NewLength() {
            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>() { "firstName", "Lydia", "Klinger" };
            IList<char> oldIList = new List<char>();
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>();
            ICollection<string> newCollecton = new List<string>() { "first Name", "Leandro" };
            IList<char> newIList = new List<char>();
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Equal(3, diffGuid.Count());
        }

        [Fact]
        public void IList_Difference() {
            var guid = Guid.NewGuid();

            IEnumerable<Guid> oldEnumerable = new List<Guid>();
            ICollection<string> oldCollecton = new List<string>();
            IList<char> oldIList = new List<char>() { 'B'};
            var oldValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, oldEnumerable, oldCollecton, new List<int>() { }, oldIList);

            IEnumerable<Guid> newEnumerable = new List<Guid>() ;
            ICollection<string> newCollecton = new List<string>();
            IList<char> newIList = new List<char>() { 'A'};
            var newValue = new DictionaryAndEnumerableModel(new Dictionary<string, string>() { }, new HashSet<Guid>(), new string[] { }, newEnumerable, newCollecton, new List<int>() { }, newIList);

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }
    }
}
