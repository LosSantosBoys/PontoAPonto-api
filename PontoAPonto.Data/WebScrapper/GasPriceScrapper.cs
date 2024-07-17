using HtmlAgilityPack;
using PontoAPonto.Domain.Interfaces.WebScrapper;

namespace PontoAPonto.Data.WebScrapper
{
    public class GasPriceScrapper : IGasPriceScrapper
    {
        public async Task<string> GetGasolinePriceAsync()
        {
            var url = "https://precos.petrobras.com.br/w/gasolina/pa";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);

                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(response);

                var priceNode = document.DocumentNode.SelectSingleNode("//p[@id='telafinal-precofinal']");

                return priceNode?.InnerText.Trim();
            }
        }
    }
}
