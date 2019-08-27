using System.Configuration;
using System.IO;
using Generator.Application.Exceptions;
using Generator.Application.Interfaces;

namespace Generator.Infrastructure.IO
{
    public class FileWriter : IWriter
    {
        private readonly string filename;

        public FileWriter()
        {
            filename = ConfigurationManager.AppSettings["filename"] ?? "DefaultFilename.csv";
        }

        public void Save(string[] pictures, Domain.Choice choice)
        {
            var props = choice.GetType().GetProperties();
            if (pictures.Length != props.Length - 1)
            {
                throw new GeneratorException("Server side error");
            }

            for (int i = 0; i < props.Length; i++)
            {
                SaveToFile(props[i].GetValue(choice).ToString());
                if (i < pictures.Length)
                {
                    SaveToFile(",");
                    SaveToFile(pictures[i]);
                    SaveToFile(",");
                }
                else
                {
                    SaveToFile(System.Environment.NewLine);
                    break;
                }
            }
        }

        private void SaveToFile(string text)
        {
            using (StreamWriter outputFile = File.AppendText(filename))
            {
                outputFile.Write(text);
            }
        }
    }
}
