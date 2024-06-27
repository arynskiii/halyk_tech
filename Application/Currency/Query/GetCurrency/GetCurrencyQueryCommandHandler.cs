using Application.Currency.Specification;
using Application.DTOs;
using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Currency.Query.GetCurrency;

internal sealed class GetCurrencyQueryCommandHandler : IRequestHandler<GetCurrencyQueryCommand, List<RCurrencyDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCurrencyQueryCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<RCurrencyDto>> Handle(GetCurrencyQueryCommand request, CancellationToken cancellationToken)
    {
        var currencies = await _context.RCurrencies
            .WithSpecification( RCurrencySpecification.Default() 
                .ByDate(request.Date)
                .ByCode(request.Code))
            .ToListAsync(cancellationToken);
        
        if (!currencies.Any())
            throw new Exception("По твоему запросу не были найдены данные.");
        
        return MapToDto(currencies);
    }

    private List<RCurrencyDto> MapToDto(List<RCurrency> currencies)
    {
        var dtos = new List<RCurrencyDto>();
        foreach (var currency in currencies)
        {
            dtos.Add(new RCurrencyDto
            {
                Title = currency.Title,
                Code = currency.Code,
                Value = currency.Value,
                A_Date  = currency.A_Date
            });
        }
        return dtos;
    }
}