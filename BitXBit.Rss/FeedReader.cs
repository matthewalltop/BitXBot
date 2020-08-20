namespace BitXBit.Rss
{
	using System.Text;
	using System.Xml;

	public class FeedReader
	{
		private const string FeedUri = "http://bitxbitpodcast.podbean.com/feed/";

		public string Read()
		{
			var xmlDoc = new XmlDocument();
			xmlDoc.Load(FeedUri);

			var nodes = xmlDoc.SelectNodes("rss/channel/item");
			var rssContent = new StringBuilder();


			var feedItem = nodes?[0];

			var rssSubNode = feedItem?.SelectSingleNode("title");
			var title = rssSubNode != null ? rssSubNode.InnerText : "";

			rssSubNode = feedItem?.SelectSingleNode("link");
			var link = rssSubNode != null ? rssSubNode.InnerText : "";

			rssContent.Append("@everyone\n\n Ignore this for now. Testing the rss-bot");
			rssContent.Append($"{title}\n\n");
			rssContent.Append(link);

			return rssContent.ToString();
		}
	}
}
