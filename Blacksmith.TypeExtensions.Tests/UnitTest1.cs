using SampleLibrary;
using System;
using Xunit;
using Blacksmith.Extensions.Types;
using Blacksmith.TypeExtensions.Exceptions;

namespace Blacksmith.TypeExtensions.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Type concreteType, concreteFromAbstract;

            concreteType = typeof(ConcreteExtendedSampleService);
            concreteFromAbstract = typeof(ConcreteForAbstractExtendedSampleService);

            Assert.True(concreteType.implementsInterface(typeof(IExtendedSampleService)));
            Assert.True(concreteType.implementsInterface<IExtendedSampleService>());

            Assert.False(concreteType.implementsInterface(typeof(IOtherSampleService)));
            Assert.False(concreteType.implementsInterface<IOtherSampleService>());

            Assert.True(concreteType.implementsInterface(typeof(ISampleService)));
            Assert.True(concreteType.implementsInterface<ISampleService>());

            Assert.False(concreteType.extends(typeof(AbstractSampleService)));
            Assert.False(concreteType.extends<AbstractSampleService>());

            Assert.True(concreteFromAbstract.extends(typeof(AbstractSampleService)));
            Assert.True(concreteFromAbstract.extends<AbstractSampleService>());

            Assert.True(typeof(IOtherSampleService).extends<ISampleService>());
            Assert.False(typeof(ISampleService).extends<IOtherSampleService>());

            Assert.Throws<TypeException>(() => typeof(IOtherSampleService).extends<AbstractSampleService>());
            Assert.Throws<TypeException>(() => typeof(ConcreteExtendedSampleService).extends<ISampleService>());
        }
    }
}
