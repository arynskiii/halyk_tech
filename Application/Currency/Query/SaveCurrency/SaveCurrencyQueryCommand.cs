using MediatR;

namespace Application.Currency.Query;

public sealed class SaveCurrencyQueryCommand : IRequest<int>
{
    public string Date { get; set; }
}