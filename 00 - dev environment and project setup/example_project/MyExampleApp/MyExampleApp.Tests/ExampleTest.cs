using FluentAssertions;

namespace MyExampleApp.Tests
{
    public class ExampleTest
    {
        [Fact]
        public void ExampleTest1()
        {
            var example = new ExampleClass();

            example.ToString().Should().Be("MyExampleApp.ExampleClass");
        }
    }
}
