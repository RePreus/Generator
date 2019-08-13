namespace Generator.Domain
{
    public class Picture
    {
        public Picture(int id, string image)
        {
            Id = id;
            Image = image;
        }
        public int Id { get; set; }
        public string Image { get; set; }
    }
}
