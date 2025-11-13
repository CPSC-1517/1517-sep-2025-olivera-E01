namespace Books.Models
{
    public class BookWithoutDataAnnotations
    {
        public string? Title { get; set; }

        public int Pages { get; set; }
        public DateOnly PublishDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Genre { get; set; } = "Fiction";
        public bool InStock { get; set; }

        public string ToCsv()
        {
            return $"{Title},{Pages},{PublishDate},{Genre},{InStock}\n";
        }
    }
}
