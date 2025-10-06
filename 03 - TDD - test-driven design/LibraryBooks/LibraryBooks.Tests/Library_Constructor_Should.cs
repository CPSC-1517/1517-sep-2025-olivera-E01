namespace LibraryBooks.Tests
{
    public class Library_Constructor_Should
    {
        [Fact]
        public void Constructor_OnlyRequiredFields_Succeeds()
        {

        }

        [Fact]
        public void Constructor_WithAddressTrim_TrimsAddress()
        {

        }

        [Fact]
        public void Constructor_WithBookList_Succeeds()
        {

        }

        [Fact]
        public void Constructor_DuplicateISBN_ThrowsArgumentException()
        {

        }

        [Theory]
        [InlineData()] // <-- hydrate with test data. Also, consider this a hydration check! :)
        public void Constructor_MissingName_ThrowsArgumentNullException()
        {

        }
    }
}

