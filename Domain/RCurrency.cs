using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class RCurrency
{
    // Id.
    public int Id { get; set; }
    
    // Наименование валюты.
    [Required(ErrorMessage = "Наименование валюты обязательно для заполнения.")]
    public string Title { get; set; }
        
        // Код валюты.
    [Required(ErrorMessage = "Код валюты обязателен для заполнения.")]
    public string Code { get; set; }
        
    // Значение.
    [Required(ErrorMessage = "Значение обязательно для заполнения.")]
    public decimal Value { get; set; }
        
    // Дата выгрузки.
    [Required(ErrorMessage = "Дата выгрузки обязательна для заполнения.")]
    public DateTime A_Date { get; set; }
}