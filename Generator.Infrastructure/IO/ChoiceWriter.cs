using System.IO;
using System.Threading.Tasks;
using Generator.Application.Interfaces;
using Generator.Domain.Entities;
using Generator.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Generator.Infrastructure.IO
{
    public class ChoiceWriter : IWriter<Choice>
    {
        private readonly StreamWriter outputFile;

        public ChoiceWriter(IOptions<ChoiceWriterConfiguration> config)
        {
            outputFile = File.AppendText(config.Value.filename ?? "DefaultFilename.csv");
        }

        public async Task Save(Choice input)
        {
            var properties = input.GetType().GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                await outputFile.WriteAsync(properties[i].ToString() + ",");
            }

            await outputFile.WriteAsync(properties[properties.Length - 1] + System.Environment.NewLine);
        }
    }
}
