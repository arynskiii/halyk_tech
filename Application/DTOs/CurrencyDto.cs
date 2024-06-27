namespace Application.DTOs;

public class RCurrencyDto
{
    // Наименование валюты.
    public string Title { get; set; }
    
    // Код валюты.
    public string Code { get; set; }

    // Значение.
    public decimal Value { get; set; }

    // Дата выгрузки.
    public DateTime A_Date { get; set; }
}