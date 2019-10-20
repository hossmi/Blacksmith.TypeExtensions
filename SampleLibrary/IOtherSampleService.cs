namespace SampleLibrary
{
    public interface IOtherSampleService : ISampleService
    {
        string getSomeOtherValue(int value1, int value2);
    }
}
