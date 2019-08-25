using System.IO;
using Generator.Application.Interfaces;

namespace Generator.Infrastructure.IO
{
    public class FileWriter : IFileWriter
    {
        private const string Filename = "Choices.csv";

        public void SaveToFile(Domain.Choice choice)
        {
            using (StreamWriter outputFile = File.AppendText(Filename))
            {
                foreach (var prop in choice.GetType().GetProperties())
                {
                    outputFile.Write(prop.GetValue(choice));
                    outputFile.Write(',');
                }
            }
        }

        public void SaveToFile(string text)
        {
            using (StreamWriter outputFile = File.AppendText(Filename))
            {
                outputFile.Write(text);
            }
        }
    }
}
