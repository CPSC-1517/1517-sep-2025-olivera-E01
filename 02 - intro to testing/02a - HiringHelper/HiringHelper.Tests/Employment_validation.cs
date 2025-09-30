namespace HiringHelper
{

    using FluentAssertions;
    using Xunit;

    public class Employment_validation
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")] // fixme: does not throw exception
        public void FullName_Blank_Throws(string? bad)
        {
            var act = () => new Employment { FullName = bad };
            act.Should().Throw<ArgumentException>()
                .WithMessage("*FullName*");
        }

        [Theory]
        // pattern: e.g. AA1234
        [InlineData("A1234")]
        [InlineData("AB12X4")]
        [InlineData("ab1234")]
        // [InlineData("AA1234")] // -> I'm testing for *bad* inputs, so this test would fail
                                  //     because it wouldn't trigger an exception
        public void EmployeeId_InvalidPattern_Throws(string badId)
        {
            var act = () => new Employment { EmployeeId = badId };
            act.Should().Throw < ArgumentException >()
                .WithMessage("*EmployeeId*");
        }

    }
}
