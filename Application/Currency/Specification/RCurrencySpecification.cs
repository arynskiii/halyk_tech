using Ardalis.Specification;
using Domain;

namespace Application.Currency.Specification;

internal sealed class RCurrencySpecification : Specification<RCurrency>
{
    public static RCurrencySpecification Default() => new();

    // Фильтрация по дате.
    public RCurrencySpecification ByDate(DateTime? date)
    {
        Query.Where(x => x.A_Date == date, date.HasValue);
        return this;
    }

    public RCurrencySpecification ByCode(string? code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            Query.Where(x => x.Code == code);
        }
        return this;
    }
}