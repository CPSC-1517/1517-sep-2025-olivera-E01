// this is the built-in namespace/collection of data validators
using System.ComponentModel.DataAnnotations;

// to use them, we can look at all the options in the docs:
//    https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-9.0
//
//    and attach them to a property by calling any in this list ^
//    without the "Attribute" part at the end

namespace Books.Models
{
    public class Book
    {
        [Required]  // <-- here's the validator, attached to Title; we're using RequiredAttribute
        public string? Title { get; set; }
        public int Pages { get; set; }
        public DateOnly PublishDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Genre { get; set; } = "Fiction";
        public bool InStock { get; set; }
    }
}
