namespace ComplexEmployment.ClassLibrary
{
    public class Person
    {

        public string FullName => $"{LastName}, {FirstName}";

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(
                        nameof(FirstName),
                        "FirstName cannot be blank."
                    );
                }

                _firstName = value.Trim();
            }
        }

        public string LastName { get; set; }
        public ResidentAddress? Address { get; set; }
        public List<Employment> EmploymentPositions { get; private set; }

        // default construct
        public Person()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
            EmploymentPositions = new List<Employment>();
            // choosing not to deal with an address in default constructor
        }

        // greedy constructor
        public Person(
            string firstname,
            string lastname,
            ResidentAddress? address,              // let's make this optional
            List<Employment>? employmentpositions  // let's make this optional too
        )
        {
            FirstName = firstname.Trim();
            LastName = lastname.Trim();
            Address = address;
            EmploymentPositions = employmentpositions ?? new List<Employment>();
            // null-coalesce:    a ?? b   -->  a 'else' b  -->  A if not null, else B
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
        }
    }
}