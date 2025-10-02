
namespace ComplexEmployment.Tests
{
    using ComplexEmployment.ClassLibrary;
    using FluentAssertions;

    // made a new project for your tests? make sure you've...
    //    1. added project references from the class library (other project)
    //    2. install FluentAssertions to this project

    public class Person_Should
    {

        // in a test: Arrange, Act, Assert
        //
        // Arrange: setting up the necessary givens
        // Act:     firing behavioural logic necessary to produce test result
        // Assert:  declaring that/whether (e.g.) test result == expected result

        [Fact]
        public void Create_Default_Instance()
        {
            // arrange? + act
            var sut = new Person();  // sut -> "Subject Under Test". just a convention; you do you

            // assert
            sut.FirstName.Should().Be("Unknown");
            sut.LastName.Should().Be("Unknown");
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Should().BeEmpty();
        }

        [Fact]
        public void Create_With_Trimmed_Name_Inputs()
        {
            var sut = new Person("  Oliver  ", "  Antoniu  ", null, null);
            sut.FirstName.Should().Be("Oliver");
            sut.LastName.Should().Be("Antoniu");
        }

        [Fact]
        public void FullName_ValidValues_ReturnsCorrectFormat()
        {
            // arrange? + act
            var sut = new Person("Oliver", "Antoniu", null, null);

            // assert
            sut.FullName.Should().Be("Antoniu, Oliver");
        }

        [Theory]
        // [InlineData(null)] --> Throws NullReferenceException, not ArgumentNullException
        [InlineData("")]
        [InlineData(" ")]
        public void Throw_Exception_For_Null_Or_Blank_FirstName(string firstname)
        {
            Action act = () => new Person(firstname, "Antoniu", null, null);
            
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("FirstName*cannot be blank*");  // gross but gets the job done?
        }
    }
}

