using System.IO;

namespace Generator.Application.Extensions
{
    public static class SaveToFileExtensions
    {
        private static string filename = "Choices.csv";

        public static void SaveToFile(this Domain.Choice choice)
        {
            using (StreamWriter outputFile = File.AppendText(filename))
            {
                foreach (var prop in choice.GetType().GetProperties())
                {
                    outputFile.Write(prop.GetValue(choice));
                    outputFile.Write(',');
                }
            }
        }

        public static void SaveToFile(this string text)
        {
            using (StreamWriter outputFile = File.AppendText(filename))
            {
                outputFile.Write(text);
            }
        }
    }
}
