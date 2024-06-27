using Application.DTOs;
using MediatR;

namespace Application.Currency.Query.GetCurrency;

public sealed class GetCurrencyQueryCommand :  IRequest<List<RCurrencyDto>>
{
    public DateTime? Date { get; set; }
    public string? Code { get; set; }
}