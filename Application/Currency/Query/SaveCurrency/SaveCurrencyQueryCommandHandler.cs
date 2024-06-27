using System.Globalization;
using System.Xml.Linq;
using Application.Interfaces;
using Domain;
using MediatR;

namespace Application.Currency.Query
{
    internal sealed class SaveCurrencyQueryCommandHandler : IRequestHandler<SaveCurrencyQueryCommand, int>
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IApplicationDbContext _context;
        
        private const string BaseUrl = "https://nationalbank.kz/rss/get_rates.cfm";

        public SaveCurrencyQueryCommandHandler(IHttpClientFactory clientFactory, IApplicationDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }
     
        public async Task<int> Handle(SaveCurrencyQueryCommand request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.CreateClient();
            string formattedUrl = $"{BaseUrl}?fdate={request.Date}";
    
            var response = await client.GetAsync(formattedUrl, cancellationToken);
            response.EnsureSuccessStatusCode();
    
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
    
            DateTime parsedDate = ParseDate(request.Date);
    
            var doc = XDocument.Parse(content);
            var rates = doc.Descendants("item").Select(item => new RCurrency
            {
                Title = item.Element("fullname")?.Value,
                Code = item.Element("title")?.Value,
                Value = decimal.Parse(item.Element("description")?.Value, CultureInfo.InvariantCulture),
                A_Date = parsedDate
            }).ToList();

            if (!rates.Any())
                throw new InvalidOperationException("Нет данных для парсинга.");

            _context.RCurrencies.AddRange(rates);
           
            await _context.SaveChangesAsync(cancellationToken);

            return rates.Count;
        }

        private static DateTime ParseDate(string date)
        {
            if (DateTime.TryParseExact(date, new[] { "dd.MM.yyyy", "dd/MM/yyyy" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out DateTime parsedDate))
            {
                return DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc); 
            }
            throw new ApplicationException("Неправильный формат времени был отправлен.");
        }

    }
}
