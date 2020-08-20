namespace BitXBit.Rss.Test
{
	using Xunit;
	using FluentAssertions;

	public class UnitTest1
    {
        [Fact]
        public void FeedReaderReadsFeed()
        {
	        var feedReader = new FeedReader();

	        var result = feedReader.Read();

	        result.Should().NotBeEmpty(because: "We got a feed back");
        }
    }
}
