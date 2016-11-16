using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieShopDll.ServiceGateway;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDll.ServiceGateway.Tests
{
    [TestClass()]
    public class CurrencyServiceGatewayTests
    {
        private ICurrencyConverter _cm = new DllFacade().GetCurrencyConverter();

       [TestMethod()]
        public void GetCurrencyRateTest()
        {
            var actual = _cm.GetCurrencyRate("DKK").Rates.Count;
            var expected = 31;
            
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void GetCurrencyUsdValueTest()
        {
            double actual = _cm.GetCurrencyRate("DKK").Rates.FirstOrDefault(x => x.Name == "USD").Value;
          
            double expected = 0.14462;



            Assert.AreEqual(actual, expected);
        }

    }
}