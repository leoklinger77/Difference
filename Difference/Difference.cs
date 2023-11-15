namespace Difference {
    using System.Collections;
    using System.Reflection;

    public record class ResultDiff(string attribute, object oldValue, object newValue);
    public static class Difference {
        private static List<ResultDiff> _diff = new List<ResultDiff>();
        private static HashSet<Type> _types = new HashSet<Type>() {
            typeof(string),
            typeof(int),
            typeof(decimal),
            typeof(float),
            typeof(long),
            typeof(char),
            typeof(bool),
            typeof(DateTime),
            typeof(Guid),
            typeof(TimeSpan),
        };

        public static IEnumerable<ResultDiff> Diff<T>(T oldValue, T newValue) where T : class {
            _diff.Clear();
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties) {

                if (_types.Contains(property.PropertyType) || property.PropertyType.IsEnum) {
                    object oldValueProp = property.GetValue(oldValue);
                    object newValueProp = property.GetValue(newValue);

                    if (!oldValueProp.Equals(newValueProp)) {
                        _diff.Add(new ResultDiff(property.Name, oldValueProp, newValueProp));
                    }
                    continue;
                }

                if (!property.PropertyType.IsGenericType && property.PropertyType.IsArray) {
                    Array oldDictionary = (Array)property.GetValue(oldValue);
                    Array newDictionary = (Array)property.GetValue(newValue);

                    if (oldDictionary != null || newDictionary != null) {
                        var differences = GetArrayDifferences(property.Name, oldDictionary, newDictionary);
                        _diff.AddRange(differences);
                    }
                    continue;
                }

                if (property.PropertyType.IsGenericType) {
                    Type genericType = property.PropertyType.GetGenericTypeDefinition();

                    if (genericType.Name.Contains(typeof(List<>).Name) ||
                        genericType.Name.Contains(typeof(HashSet<>).Name) ||
                        genericType.Name.Contains(typeof(ISet<>).Name) ||
                        genericType.Name.Contains(typeof(IList<>).Name) ||
                        genericType.Name.Contains(typeof(IEnumerable<>).Name) ||
                        genericType.Name.Contains(typeof(ICollection<>).Name) ||
                        genericType.Name.Contains(typeof(IReadOnlyCollection<>).Name)) {

                        IEnumerable oldEnumerable = property.GetValue(oldValue) as IEnumerable;
                        IEnumerable newEnumerable = property.GetValue(newValue) as IEnumerable;
                        if (oldEnumerable != null || newEnumerable != null) {
                            var differences = GetEnumerableDifferences(property.Name, oldEnumerable.Cast<object>(), newEnumerable.Cast<object>());
                            _diff.AddRange(differences);
                        }
                        continue;
                    }

                    if (genericType.Name.Contains(typeof(Dictionary<,>).Name) ||
                        genericType.Name.Contains(typeof(IDictionary<,>).Name)) {
                        IDictionary oldDictionary = (IDictionary)property.GetValue(oldValue);
                        IDictionary newDictionary = (IDictionary)property.GetValue(newValue);

                        if (oldDictionary != null || newDictionary != null) {
                            IEnumerable<ResultDiff> differences = GetDictionaryDifferences(property.Name, oldDictionary, newDictionary);
                            _diff.AddRange(differences);
                        }
                        continue;
                    }
                } else {
                    if (property.PropertyType.IsClass) {
                        Diff(property.GetValue(oldValue), property.GetValue(newValue));
                    }
                }
            }

            return _diff;
        }


        private static IEnumerable<ResultDiff> GetArrayDifferences(string propertyMainName, Array oldArray, Array newArray) {
            var differences = new List<ResultDiff>();
            if (oldArray == null && newArray != null) {                
                for (int i = 0; i < newArray.Length; i++) {
                    object newValue = newArray.GetValue(i);
                    differences.Add(new ResultDiff($"[{propertyMainName}-{i}]", newValue, null));                    
                }
                
                return differences;
            }

            if (newArray == null && oldArray != null) {                
                for (int i = 0; i < oldArray.Length; i++) {
                    object oldValue = oldArray.GetValue(i);
                    differences.Add(new ResultDiff($"[{propertyMainName}-{i}]", oldValue, null));                    
                }
                
                return differences;
            }

            int minLength = Math.Min(oldArray.Length, newArray.Length);

            for (int i = 0; i < minLength; i++) {
                object oldValue = oldArray.GetValue(i);
                object newValue = newArray.GetValue(i);

                if (_types.Contains(oldValue.GetType())) {
                    if (!oldValue.Equals(newValue)) {
                        differences.Add(new ResultDiff($"[{propertyMainName}-{i}]", oldValue, newValue));
                    }
                } else if (oldValue.GetType().IsClass) {
                    Diff(oldValue, newValue);
                }
            }

            for (int i = minLength; i < oldArray.Length; i++) {
                differences.Add(new ResultDiff($"[{propertyMainName}-{i}]", oldArray.GetValue(i), null));
            }

            for (int i = minLength; i < newArray.Length; i++) {
                differences.Add(new ResultDiff($"[{propertyMainName}-{i}]", null, newArray.GetValue(i)));
            }

            return differences;
        }

        private static IEnumerable<ResultDiff> GetEnumerableDifferences(string propertyMainName, IEnumerable<object> oldEnumerable, IEnumerable<object> newEnumerable) {
            var differences = new List<ResultDiff>();
            if (oldEnumerable == null && newEnumerable != null) {
                var x = 0;
                foreach (var entry in newEnumerable) {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{x}]", null, entry));
                    x++;
                }
                return differences;
            }

            if (newEnumerable == null && oldEnumerable != null) {
                var x = 0;
                foreach (var entry in oldEnumerable) {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{x}]", entry, null));
                    x++;
                }
                return differences;
            }

            Dictionary<int, object> oldDict = oldEnumerable.Select((item, index) => new { Index = index, Item = item }).ToDictionary(pair => pair.Index, pair => pair.Item);
            Dictionary<int, object> newDict = newEnumerable.Select((item, index) => new { Index = index, Item = item }).ToDictionary(pair => pair.Index, pair => pair.Item);

            IEnumerable<int> allIndices = oldDict.Keys.Union(newDict.Keys);

            foreach (var index in allIndices) {
                object oldItem = oldDict.TryGetValue(index, out var oldDictItem) ? oldDictItem : null;
                object newItem = newDict.TryGetValue(index, out var newDictItem) ? newDictItem : null;

                if (oldItem != null && _types.Contains(oldItem.GetType())) {
                    if (!oldItem.Equals(newItem)) {
                        differences.Add(new ResultDiff($"[{propertyMainName}-{index}]", oldItem, newItem));
                    }
                } else if (oldItem != null && oldItem.GetType().IsClass) {
                    Diff(oldItem, newItem);
                } else if (oldItem == null && newItem != null) {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{index}]", oldItem, newItem));
                }
            }

            return differences;
        }

        private static IEnumerable<ResultDiff> GetDictionaryDifferences(string propertyMainName, IDictionary oldDictionary, IDictionary newDictionary) {
            var differences = new List<ResultDiff>();

            if (oldDictionary == null && newDictionary != null) {
                foreach (DictionaryEntry entry in newDictionary) {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{entry.Key}]", null, entry.Value));
                }
                return differences;
            }

            if (newDictionary == null && oldDictionary != null) {
                foreach (DictionaryEntry entry in oldDictionary) {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{entry.Key}]", entry.Value, null));
                }
                return differences;
            }

            foreach (DictionaryEntry entry in oldDictionary) {
                object key = entry.Key;
                object oldValue = entry.Value;

                if (newDictionary.Contains(key)) {
                    object newValue = newDictionary[key];

                    if (_types.Contains(oldValue.GetType())) {
                        if (!oldValue.Equals(newValue)) {
                            differences.Add(new ResultDiff($"[{propertyMainName}-{key}]", oldValue, newValue));
                        }
                    } else if (oldValue.GetType().IsClass) {
                        Diff(oldValue, newValue);
                    }
                } else {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{key}]", oldValue, null));
                }
            }

            foreach (DictionaryEntry entry in newDictionary) {
                if (!oldDictionary.Contains(entry.Key)) {
                    differences.Add(new ResultDiff($"[{propertyMainName}-{entry.Key}]", null, entry.Value));
                }
            }

            return differences;
        }
    }
}