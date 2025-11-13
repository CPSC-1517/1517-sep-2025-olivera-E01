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

        public static BookWithoutDataAnnotations ParseFromCsv(string csvLine)
        {
            if (string.IsNullOrWhiteSpace(csvLine))
            {
                throw new ArgumentNullException(nameof(csvLine), "Line cannot be blank.");
            }

            // shape of data: "title,pages,publishDate,genre,inStock"
            //                 0     1     2           3     4
            // we're going to split by comma into a list; ^these^ will be each property's
            // numerical index in that list
            string[] fields = csvLine.Split(",");  // "split the string by commas into a list of elements"

            if (fields.Length != 5)
            {
                throw new FormatException($"Incorrect number of input terms in:\n{csvLine}");
            }

            // naive implementation: cast each array element into a variable
            string title = fields[0];
            int pages = int.Parse(fields[1]);
            DateOnly publishDate = DateOnly.Parse(fields[2]);
            string genre = fields[3];
            bool inStock = bool.Parse(fields[4]);

            // finally, use the greedy constructor to return an instance hydrated with the above values
            return new BookWithoutDataAnnotations(title, pages, publishDate, genre, inStock);
        }
    }
}
