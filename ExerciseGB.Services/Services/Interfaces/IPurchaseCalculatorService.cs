using ExerciseGB.DTO.Models;

namespace ExerciseGB.Services.Interfaces
{
    public interface IPurchaseCalculatorService
    {
        Task<Purchase> GetPurchaseValue(double? netValue, double? grossValue, double? vatValue, VatRate? vatRate);
    }
}
