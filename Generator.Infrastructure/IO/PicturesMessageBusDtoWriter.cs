using System.IO;
using System.Threading.Tasks;
using Generator.Application.Dtos;
using Generator.Application.Interfaces;
using Generator.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Generator.Infrastructure.IO
{
    public class PicturesMessageBusDtoWriter : IWriter<PicturesMessageBusDto>
    {
        private readonly StreamWriter outputFile;

        public PicturesMessageBusDtoWriter(IOptions<PicturesMessageBusDtoWriterConfiguration> config)
        {
            outputFile = File.AppendText(config.Value.Filename ?? "DefaultFilename.csv");
        }

        public async Task Save(PicturesMessageBusDto input)
        {
            await outputFile.WriteAsync(input.ChosenPicture + ",");
            await outputFile.WriteAsync(input.OtherPicture + System.Environment.NewLine);
            outputFile.Close();
        }
    }
}
