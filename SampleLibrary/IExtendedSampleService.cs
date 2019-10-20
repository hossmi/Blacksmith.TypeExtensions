namespace SampleLibrary
{
    public interface IExtendedSampleService : ISampleService
    {
        string getSomeValue(int value1, int value2);
    }
}
