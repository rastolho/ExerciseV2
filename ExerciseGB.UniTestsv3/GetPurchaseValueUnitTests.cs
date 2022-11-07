using ExerciseGB.Exceptions;
using ExerciseGB.Services.Implementations;
using ExerciseGB.Services.Interfaces;

namespace ExerciseGB.UniTestsv3
{
    [TestClass]
    public class GetPurchaseValueUnitTests
    {
        private PurchaseCalculatorService PurchaseCalculatorService;

        public GetPurchaseValueUnitTests()
        {
            this.PurchaseCalculatorService = new PurchaseCalculatorService();
        }

        [TestInitialize]
        public void Initialize()
        {
        }


        [TestMethod]    
        public void GetPurchaseValue_Invalid_Net_Gross_Vat_MustHaveAValue()
        {
            Assert.ThrowsExceptionAsync<AppException>(() => this.PurchaseCalculatorService.GetPurchaseValue(null, null, null, DTO.Models.VatRate.VatRate10), 
                "Net or Gross or VAT must have a value.");
        }


        [TestMethod]
        public void GetPurchaseValue_Invalid_VatRate_IsRequired()
        {
            Assert.ThrowsExceptionAsync<AppException>(() => this.PurchaseCalculatorService.GetPurchaseValue(null, null, 10, null),
                "Vat Rate is required.");
        }


        [TestMethod]
        public void GetPurchaseValue_Invalid_SendMoreThanOneInput()
        {
            Assert.ThrowsExceptionAsync<AppException>(() => this.PurchaseCalculatorService.GetPurchaseValue(null, 20, 10, DTO.Models.VatRate.VatRate10),
                "You cannot send more than one input.");
        }

        [TestMethod]
        public void GetPurchaseValue_Invalid_NetValueMustBeGreaterThan0()
        {
            Assert.ThrowsExceptionAsync<AppException>(() => this.PurchaseCalculatorService.GetPurchaseValue(-10, null, null, DTO.Models.VatRate.VatRate10),
                "Net must be greater than 0");
        }

        [TestMethod]
        public void GetPurchaseValue_Invalid_GrossValueMustBeGreaterThan0()
        {
            Assert.ThrowsExceptionAsync<AppException>(() => this.PurchaseCalculatorService.GetPurchaseValue(null, -20, null, DTO.Models.VatRate.VatRate10),
                "Gross must be greater than 0");
        }

        [TestMethod]
        public void GetPurchaseValue_Invalid_VATValueMustBeGreaterThan0()
        {
            Assert.ThrowsExceptionAsync<AppException>(() => this.PurchaseCalculatorService.GetPurchaseValue(null, null, 0, DTO.Models.VatRate.VatRate10),
                "VAT must be greater than 0.");
        }

        [TestMethod]
        public void GetPurchaseValue_Valid_WithNetValue()
        {
            var result = this.PurchaseCalculatorService.GetPurchaseValue(9000, null, null, DTO.Models.VatRate.VatRate10).GetAwaiter().GetResult();

            Assert.AreEqual(result.Net, 9000);
            Assert.AreEqual(result.Gross, 10000);
            Assert.AreEqual(result.Vat, 1000);
        }



        [TestMethod]
        public void GetPurchaseValue_Valid_WithGrossValue()
        {
            var result = this.PurchaseCalculatorService.GetPurchaseValue(null, 432, null, DTO.Models.VatRate.VatRate20).GetAwaiter().GetResult();

            Assert.AreEqual(result.Net, 345.60);
            Assert.AreEqual(result.Gross, 432);
            Assert.AreEqual(result.Vat, 86.40);
        }


        [TestMethod]
        public void GetPurchaseValue_Valid_WithVatValue()
        {
            var result = this.PurchaseCalculatorService.GetPurchaseValue(null, null, 722.15, DTO.Models.VatRate.VatRate13).GetAwaiter().GetResult();

            Assert.AreEqual(result.Net, 4832.85);
            Assert.AreEqual(result.Gross, 5555);
            Assert.AreEqual(result.Vat, 722.15);
        }


    }
}