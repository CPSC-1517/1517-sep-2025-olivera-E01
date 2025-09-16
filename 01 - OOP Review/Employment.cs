using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ObjectOrientedReview
{
    public class Employment
    {
        private string _Title;
        private double _Years;

        public string Title
        {
            get => _Title;
            set { _Title = value; }
        }

        public double Years
        {
            get => _Years;
            set { _Years = value; }
        }

        public Employment()
        // "no-argument constructor"
        {
            // initialise all properties to some default values
            Title = "Unknown";
            Years = 0.0;
        }

        public Employment(string title, double years)
        // greedy constructor: creating a new instance with all properties defined
        {
            Title = title;
            Years = years;
        }

        public override string ToString()
        {
            // $"static text {someVariable}" -> string interpolation
            return $"Title: {Title}, Years: {Years}";
        }
    }
}
