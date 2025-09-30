using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ObjectOrientedReview
{
    public class Employment
    {
        private string _Title = "";
        private double _Years;

        public string Title
        {
            get => _Title;
            set {
                // validate: job title must not be empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Title cannot be empty", nameof(Title));
                }
                _Title = value;
            }
        }

        public double Years
        {
            get => _Years;
            set
            {
                // we need Years value to >= 0
                if (!Utilities.IsZeroOrPositive(value))  // !true == false
                {
                    throw new ArgumentOutOfRangeException("Years must be 0:", nameof(Years));
                }
                _Years = value;
            }
        }

        // private setter to protect invariant value; change via methods/constructors if needed
        public DateTime StartDate { get; private set; } = DateTime.Now;

        public SupervisoryLevel Level { get; set; } = SupervisoryLevel.Entry;

        public Employment()
        // default a.k.a. "no-argument" constructor
        {
            // initialise all properties to some default values
            Title = "Unknown";
            Years = 0.0;
            StartDate = DateTime.Today;
            Level = SupervisoryLevel.TeamMember;
        }

        public Employment(string title, double years, DateTime startDate, SupervisoryLevel level)
        // greedy constructor: creating a new instance with all properties defined
        {
            Title = title;
            Years = years;
            StartDate = startDate;  // even though its setter is private, value is assignable here
            Level = level;
        }

        public override string ToString()
        {
            // $"static text {someVariable}" -> string interpolation
            return $"Title: {Title}, Years: {Years}, StartDate: {StartDate.ToString("yyyy-MM-dd")}, Level: {Level}";
        }
    }
}
