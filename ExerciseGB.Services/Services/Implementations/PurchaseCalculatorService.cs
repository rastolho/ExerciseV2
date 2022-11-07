using ExerciseGB.DTO.Models;
using ExerciseGB.Exceptions;
using ExerciseGB.Services.Interfaces;
using System.Linq;
using System.Net;

namespace ExerciseGB.Services.Implementations
{
    public class PurchaseCalculatorService : IPurchaseCalculatorService
    {

        public async Task<Purchase> GetPurchaseValue(double? net, double? gross, double? vat, VatRate? vatRate)
        {
            var purchase = new Purchase();

            Validations(net, gross, vat, vatRate);

            PurchaseCalculator(net, gross, vat, vatRate, purchase);

            return purchase;
        }

        private static void PurchaseCalculator(double? net, double? gross, double? vat, VatRate? vatRate, Purchase purchase)
        {
            if (net != null)
                PurchaseCalculatorBynet(net, vatRate, purchase);

            else if (gross != null)
                PurchaseCalculatorBygross(gross, vatRate, purchase);

            else if (vat != null)
                PurchaseCalculatorByvat(vat, vatRate, purchase);
        }

        private static void Validations(double? net, double? gross, double? vat, VatRate? vatRate)
        {
            if (net == null && gross == null && vat == null)
                throw new AppException("Net or Gross or VAT must have a value.");

            if (vatRate == null)
                throw new AppException("Vat Rate is required.");

            ValidateMoreThanOneInput(net, gross, vat);
            ValidateValidInputValue(net, "Net");
            ValidateValidInputValue(gross, "Gross");
            ValidateValidInputValue(vat, "VAT");
        }

        private static void PurchaseCalculatorByvat(double? vat, VatRate? vatRate, Purchase purchase)
        {
            var gross = vat.Value / ((double)vatRate / 100);
            var net = gross - vat.Value;

            purchase.Vat = Math.Round(vat.Value, 2);
            purchase.Net = Math.Round(net, 2);
            purchase.Gross = Math.Round(gross, 2);
        }

        private static void PurchaseCalculatorBygross(double? gross, VatRate? vatRate, Purchase purchase)
        {
            var vat = gross.Value * ((double)vatRate / 100);
            var net = (double)(gross.Value - vat);

            purchase.Vat = Math.Round(vat, 2);
            purchase.Net = Math.Round(net, 2);
            purchase.Gross = Math.Round(gross.Value, 2);
        }

        private static void PurchaseCalculatorBynet(double? net, VatRate? vatRate, Purchase purchase)
        {
            var gross = net.Value / ((100 - (double)vatRate) / 100);
            var vat = (double)(gross - net);

            purchase.Vat = Math.Round(vat, 2);
            purchase.Net = Math.Round(net.Value, 2);
            purchase.Gross = Math.Round(gross, 2);
        }

        private static void ValidateMoreThanOneInput(double? net, double? gross, double? vat)
        {
            if ((net != null && (gross != null || vat != null)) ||
                            (gross != null && (net != null || vat != null)) ||
                            (vat != null && (net != null || gross != null)))
            {
                throw new AppException("You cannot send more than one input.");
            }
        }
        private static void ValidateValidInputValue(double? inputValue, string input)
        {
            if (inputValue != null && inputValue < 0)
            {
                throw new AppException(string.Format("{0} must be greater than 0.", input));
            }
        }
    }
}
