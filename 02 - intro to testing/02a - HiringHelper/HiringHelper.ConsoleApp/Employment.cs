using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiringHelper
{
    public class Employment
    {
        // Define backing fields for properties
        private string _employeeId = "";
        private string _fullName = "";
        private decimal _hourlyRate;
        private double _hoursPerWeek;

        public const decimal Minimal_HourlyRate = 15.0m;
        public const decimal Maximum_HourlyRate = 250.0m;
        public const double Minimal_HoursPerWeek = 0;
        public const double Maximum_HoursPerWeek = 60;
        // ^ for catch-up on notation above, see: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types
        // e.g.
        //var f = 0f; // float
        //var d = 0d; // double
        //var m = 0m; // decimal (money)
        //var u = 0u; // unsigned int
        //var l = 0l; // long
        //var ul = 0ul; // unsigned long


        // Define fully implemented properties with backing fields
        public string EmployeeId
        {
            get => _employeeId;
            set
            {
                // Required pattern: two uppercase letters + four digits (e.g., AB1234).
                // Invalid values throw ArgumentException with a helpful message.

                //var regex = new Regex(@"[A-Z][A-Z]\d\d\d\d");
                //if (!regex.IsMatch(value))
                //{
                //    throw new ArgumentException("EmployeeId pattern must contain two uppercase letters + four digits");
                //}
                //_employeeId = value.Trim();

                // for practice/reference/validation with regex: https://regex101.com/

                // Modifed code to use helper methods from Utitilies class
                _employeeId = Utilities.RequirePattern(
                    value,
                    @"[A-Z][A-Z]\d\d\d\d",
                    "EmployeeId pattern must contain two uppercase letters + four digits"
                    );
            }
        }
        public string FullName
        {
            get => _fullName;
            set
            {
                // Must not be blank; trim whitespace
                // Invalid values throw ArgumentException.
                //if (string.IsNullOrWhiteSpace(value))
                //{
                //    throw new ArgumentException("FullName must not be blank.");
                //}
                //_fullName = value.Trim();

                // Modifed code to use helper methods from Utitilies class
                _fullName = Utilities.RequireNotBlank(value, "FullName must not be blank.");
            }
        }
        public decimal HourlyRate
        {
            get => _hourlyRate;
            set
            {
                // Range: 15.00 to 250.00 inclusive.
                // Out-of-range throws ArgumentOutOfRangeException.
                //if (value < Minimal_HourlyRate || value > Maximum_HourlyRate)
                //{
                //    throw new ArgumentOutOfRangeException($"HourRate must be between {Minimal_HourlyRate} and {Maximum_HourlyRate}");
                //}
                //_hourlyRate = value;

                // Modifed code to use helper methods from Utitilies class
                _hourlyRate = Utilities.RequireRange(
                    value,
                    Minimal_HourlyRate,
                    Maximum_HourlyRate,
                    $"HourRate must be between {Minimal_HourlyRate} and {Maximum_HourlyRate}"
                    );
            }
        }
        public double HoursPerWeek
        {
            get => _hoursPerWeek;
            set
            {
                // Range: 0 to 60 inclusive.
                // Out-of-range throws ArgumentOutOfRangeException.
                //if (value < Minimal_HoursPerWeek || value > Maximum_HoursPerWeek)
                //{
                //    throw new ArgumentOutOfRangeException($"HoursPerWeek must be between {Minimal_HoursPerWeek} and {Maximum_HoursPerWeek}");
                //}
                //_hoursPerWeek = value;

                // Modifed code to use helper methods from Utitilies class
                _hoursPerWeek = Utilities.RequireRange(
                    value,
                    Minimal_HoursPerWeek,
                    Maximum_HoursPerWeek,
                    $"HoursPerWeek must be between {Minimal_HoursPerWeek} and {Maximum_HoursPerWeek}"
                    );
            }
        }
        // Define auto-implemented properties without backing field
        public SupervisoryLevel Level { get; set; }
        // Define computed properties
        public decimal WeeklyPay
        {
            get => HourlyRate * (decimal)HoursPerWeek;
        }

        // Define constructors to initialize data
        public Employment()
        {
            //EmployeeId = "";
            //FullName = "";
            //Level = SupervisoryLevel.Staff;
            //HourlyRate = Minimal_HourlyRate;
            //HoursPerWeek = Minimal_HoursPerWeek;
        }
        public Employment(
            string employmentId,
            string fullName,
            SupervisoryLevel level,
            decimal rate,
            double hours)
        {
            EmployeeId = employmentId;
            FullName = fullName;
            Level = level;
            HourlyRate = rate;
            HoursPerWeek = hours;
        }
        // Define instance level methods
        public override string ToString()
        {
            return $"{EmployeeId} | {FullName} | {Level} | {HourlyRate:C}/hr x {HoursPerWeek:F1}h = ${WeeklyPay:F2}";
        }
    }
}