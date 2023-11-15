namespace Difference.Test {
    internal record class PrimitiviTypesModel(Guid id, int age, string name, DateTime birthdate, bool active, decimal salary, char charValue, TimeSpan timeSpan);
    public class PrimitiviTypesTest {

        [Fact]
        public void Guid_Difference() {            
            var oldValue = new PrimitiviTypesModel(Guid.NewGuid(), 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(Guid.NewGuid(), 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);            
        }

        [Fact]
        public void Int_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 27, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }

        [Fact]
        public void String_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger de Oliveira", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }

        [Fact]
        public void DateTime_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1996, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }

        [Fact]
        public void Bool_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), false, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }

        [Fact]
        public void Decimal_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.100m, 'L', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);
            
            Assert.Single(diffGuid);
        }

        [Fact]
        public void Char_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'K', new TimeSpan(1, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }

        [Fact]
        public void TimeSpan_Difference() {
            var id = Guid.NewGuid();
            var oldValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(1, 1, 1, 1, 1));
            var newValue = new PrimitiviTypesModel(id, 28, "Leandro Klinger", new DateTime(1995, 09, 11), true, 1.000m, 'L', new TimeSpan(2, 1, 1, 1, 1));

            var diffGuid = Difference.Diff(oldValue, newValue);

            Assert.NotEmpty(diffGuid);
            Assert.Single(diffGuid);
        }
    }
}