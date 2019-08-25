namespace Generator.Application.Interfaces
{
    public interface IFileWriter
    {
        void SaveToFile(Domain.Choice choice);

        void SaveToFile(string text);
    }
}
