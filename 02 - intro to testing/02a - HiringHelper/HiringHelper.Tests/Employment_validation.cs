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
    }
}
