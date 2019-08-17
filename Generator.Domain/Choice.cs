namespace Generator.Domain
{
    public class Choice
    {
        public Choice(int pictureAId, int pictureBId, int userChoiceId)
        {
            PictureAId = pictureAId;
            PictureBId = pictureBId;
            UserChoiceId = userChoiceId;
        }
        public int PictureAId { get; }

        public int PictureBId { get; }

        public int UserChoiceId { get; }
    }
}
