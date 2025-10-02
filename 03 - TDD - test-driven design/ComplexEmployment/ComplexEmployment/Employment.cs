namespace ComplexEmployment.ClassLibrary
{
    public class Employment
    {

        private string _Title;
        private double _Years;

        public string Title
        {
            get
            {
                return _Title;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title", "Title cannot be empty or just blank");
                }
                else
                {
                    _Title = value.Trim();
                }
            }
        }

        public double Years
        {
            get { return _Years; }

            set
            {
                if (!Utilities.IsZeroOrPositive(value))
                {
                    throw new ArgumentException($"The years of {value} is incorrect. Years must be 0 or greater");
                }
                else
                {
                    _Years = value;
                }
            }
        }


        public DateTime StartDate { get; private set; }


        public SupervisoryLevel Level { get; set; }


        // default constructor
        public Employment()
        {

            Title = "Unknown";
            Level = SupervisoryLevel.TeamMember;
            StartDate = DateTime.Today;

            Years = 0.0;

        }


        // greedy constructor
        public Employment(string title, SupervisoryLevel level,
                            DateTime startdate, double years = 0.0)
        {
            Title = title;
            Level = level;

            if (CheckDate(startdate))
                StartDate = startdate;

            if (years != 0.0)
            {
                Years = years;
            }
            else
            {
                if (startdate == DateTime.Today)
                {
                    Years = 0.0;
                }
                else
                {
                    //calculate the actual years from startdate to today
                    TimeSpan timediff = DateTime.Today - startdate;
                    Years = Math.Round((timediff.Days / 365.2), 1);
                }
            }
        }


        public override string ToString()
        {
            return $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years}";
        }

        // Writing out some methods for "what-if" scenarios:
        
        // What if we wanted to change the supervisory level, despite the private set?
        // A method in *this class*, since it's private, would do it.
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            Level = level;
        }


        // We could also abstract out the date-checking code into a method here.
        public void CorrectStartDate(DateTime startdate)
        {
            if (CheckDate(startdate))
                StartDate = startdate;

            TimeSpan timediff = DateTime.Today - startdate;
            Years = Math.Round((timediff.Days / 365.2), 1);
        }

        // "helper" method for the above checker.
        // Not the most beautifully organised code; just here to demonstrate the what-if scenarios.
        private bool CheckDate(DateTime startdate)
        {
            if (startdate >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The start date of {startdate} is invalid, date cannot be in the future");
            }
            return true;
        }
    }
}