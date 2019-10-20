namespace SampleLibrary
{
    public class ConcreteForAbstractExtendedSampleService : AbstractSampleService
    {
        public override int someMethod(decimal a, decimal b)
        {
            return (int)(a * b);
        }
    }
}
