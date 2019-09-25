namespace Generator.Application.Dtos
{
    public class PicturesMessageBusDto
    {
        public PicturesMessageBusDto(string chosenPicture, string otherPicture)
        {
            ChosenPicture = chosenPicture;
            OtherPicture = otherPicture;
        }

        public string ChosenPicture { get; }

        public string OtherPicture { get; }
    }
}
