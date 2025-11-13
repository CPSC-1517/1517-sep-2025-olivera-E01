namespace Books.Models
{
    public class BookWithoutDataAnnotations
    {
        public string? Title { get; set; }

        public int Pages { get; set; }
        public DateOnly PublishDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Genre { get; set; } = "Fiction";
        public bool InStock { get; set; }

        // we need some instance constructors if we're going to create instances from a parsed CSV string
        public BookWithoutDataAnnotations() { }

        public BookWithoutDataAnnotations(string title, int pages, DateOnly publishDate, string genre, bool inStock)
        {
            Title = title;
            Pages = pages;
            PublishDate = publishDate;
            Genre = genre;
            InStock = inStock;
        }

        public string ToCsv()
        {
            return $"{Title},{Pages},{PublishDate},{Genre},{InStock}\n";
        }
    }
}
