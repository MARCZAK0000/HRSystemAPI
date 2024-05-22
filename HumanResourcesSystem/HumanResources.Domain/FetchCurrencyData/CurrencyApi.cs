using HumanResources.Domain.Exceptions;

namespace HumanResources.Domain.CurrencyData
{
    public static class CurrencyApi
    {
        static readonly HttpClient client = new();
        public async static Task<string> CurrencyApi_Data(string url, CancellationToken token)
        {
            var response = await client.GetAsync(url, token);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new BadRequestException("Request has an error");
            }
            return await response.Content.ReadAsStringAsync(token);
        }
    }
}
