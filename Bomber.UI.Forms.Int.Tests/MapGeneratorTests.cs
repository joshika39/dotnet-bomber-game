using Bomber.UI.Forms.MapGenerator;
using Xunit;

namespace Bomber.UI.Forms.Int.Tests
{
    public class MapGeneratorTests
    {
        [Fact]
        public void MGT_0001_Given_NullParameter_When_ConstructorCalled_Then_ThrowsException()
        {
            var exception = Record.Exception(() =>
            {
                _ = new MapGeneratorWindowPresenter(null!);
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentNullException>(exception);
        }
    }
}
