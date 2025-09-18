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

        // private setter to protect invariant value; change via methods/constructors if needed
        public DateTime StartDate { get; private set; } = DateTime.Now; 
        public Employment()
        // default a.k.a. "no-argument" constructor
        {
            // initialise all properties to some default values
            Title = "Unknown";
            Years = 0.0;
            StartDate = DateTime.Today;
        }

        public Employment(string title, double years, DateTime startDate)
        // greedy constructor: creating a new instance with all properties defined
        {
            Title = title;
            Years = years;
            StartDate = startDate;  // even though its setter is private, value is assignable here
        }

        public override string ToString()
        {
            // $"static text {someVariable}" -> string interpolation
            return $"Title: {Title}, Years: {Years}";
        }
    }
}
