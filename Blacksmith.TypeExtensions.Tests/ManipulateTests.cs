using Blacksmith.TypeExtensions.Tests.Models;
using Blacksmith.Extensions.Manipulation;
using System;
using Xunit;

namespace Blacksmith.TypeExtensions.Tests
{
    public class ManipulateTests
    {
        [Fact]
        public void ManipulateExtension_tests()
        {
            SimpleTestClass pepe = new SimpleTestClass
            {
                Name = "pepe",
                LastName = "tronco",
                BirthDate = new DateTime(2015, 10, 21, 16, 29, 0),
                DecimalNumber = 0.00034m,
            };

            pepe.manipulate()
                .set("Name", "Chiquito")
                .set(nameof(SimpleTestClass.DoubleNumber), Math.PI)
                ;

            decimal x = 4;
            Assert.Equal("Chiquito", pepe.Name);
            Assert.Equal(Math.PI, pepe.DoubleNumber);

            pepe.manipulate()
                .set("Name", "Chiquito")
                .set(nameof(SimpleTestClass.DoubleNumber), Math.PI)
                .get(nameof(SimpleTestClass.DecimalNumber), out x)
                ;

            Assert.Equal("Chiquito", pepe.Name);
            Assert.Equal(Math.PI, pepe.DoubleNumber);
            Assert.Equal(0.00034m, pepe.DecimalNumber);

            pepe.DecimalNumber = 101m;
            pepe.manipulate(m => m
                .set("Name", "George")
                .set(nameof(SimpleTestClass.DoubleNumber), Math.E)
                .get(nameof(SimpleTestClass.DecimalNumber), out x)
                );

            Assert.Equal("George", pepe.Name);
            Assert.Equal(Math.E, pepe.DoubleNumber);
            Assert.Equal(101m, pepe.DecimalNumber);

            pepe.manipulate(m =>
            {
                m.set(nameof(SimpleTestClass.DecimalNumber), 666m);
                x = m.get<decimal>(nameof(SimpleTestClass.DecimalNumber));
            });

            Assert.Equal(666m, pepe.DecimalNumber);
        }
    }
}
