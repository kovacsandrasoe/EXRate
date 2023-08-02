using EXRate.Backend.Models;
using MNB;
using System.Globalization;
using System.Xml.Linq;

namespace EXRate.Backend.Services
{
    public class MNBService : IMNBService
    {
        IMNBArfolyamServiceSoapClient client;

        public MNBService(IMNBArfolyamServiceSoapClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Rate>> GetRates()
        {
            var result = await client.GetCurrentExchangeRatesAsync(new GetCurrentExchangeRatesRequestBody());
            XDocument xmlData = XDocument.Parse(result.GetCurrentExchangeRatesResponse1.GetCurrentExchangeRatesResult);
            return xmlData.Descendants("Rate").Select(t =>
            {
                return new Rate()
                {
                    Currency = t.Attribute("curr")?.Value?.ToString(),
                    Value = double.Parse(t.Value, CultureInfo.CurrentCulture)
                };
            });
        }

    }
}
