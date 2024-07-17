namespace PontoAPonto.Domain.Interfaces.WebScrapper
{
    public interface IGasPriceScrapper
    {
        Task<string> GetGasolinePriceAsync();
    }
}