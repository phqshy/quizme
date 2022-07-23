using HtmlAgilityPack;

namespace QuizMe.Containers{
    class QuizletData{
        public Dictionary<string, string> Data
        { get; }
        private string url;


        public QuizletData(string url){
            this.url = url;
            this.Data = parseHTML(fetchWebpage(url));
        }

        private static async Task<string> fetchURL(string url){
            var baseAddress = new Uri(url);
            using (var handler = new HttpClientHandler { UseCookies = true })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (X11; CrOS x86_64 14816.99.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
                var message = new HttpRequestMessage();
                var result = await client.SendAsync(message);
                result.EnsureSuccessStatusCode();
                var resp = await result.Content.ReadAsStringAsync();
                return resp;
            }
        }

        public static string fetchWebpage(string url){
            string resp = fetchURL(url).Result;
            return resp;
        }

        private static Dictionary<string, string> parseHTML(string data){
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);

            var terms = doc.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("class", "").Equals("SetPageTerms-term"))
            .ToList();
            
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (var element in terms){
                var termlist = element.Descendants("a")
                .Where(node => node.GetAttributeValue("class", "").Equals("SetPageTerm-wordText"))
                .ToList();

                string term = termlist[0].Descendants("span").First().InnerText;

                var deflist = element.Descendants("a")
                .Where(node => node.GetAttributeValue("class", "").Equals("SetPageTerm-definitionText"))
                .ToList();

                string def = deflist[0].Descendants("span").First().InnerText;

                dict.Add(term, def);
            }

            return dict;
        }
    }
}