using System;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using NUnit.Framework;
using UnitsNet;
using UnitsNet.Units;

namespace UnitTests
{
    public class Tests
    {
        private UomHelper _uomHelper;
        
        [SetUp]
        public void Setup()
        {
            _uomHelper = new UomHelper();
        }

        [TestCase("0")]
        [TestCase("16.5")]
        [TestCase("148974.464664")]
        [TestCase("-1")]
        public void PoundsToOzTest(string weight)
        {
            var tw1 = new TotalWeight { Unit = "lbs", Weight = Convert.ToDecimal(weight) };
            var tw2 = new TotalWeight { Unit = "pounds", Weight = Convert.ToDecimal(weight) };
            var tw3 = new TotalWeight { Unit = "pound", Weight = Convert.ToDecimal(weight) };
            const string endUom1 = "oz";
            const string endUom2 = "ounce";
            const string endUom3 = "ounces";

            var convertedWeight1 = _uomHelper.ConvertTotalWeight(tw1, endUom1);
            var convertedWeight2 = _uomHelper.ConvertTotalWeight(tw2, endUom2);
            var convertedWeight3 = _uomHelper.ConvertTotalWeight(tw3, endUom3);

            var expected = UnitConverter.Convert(tw1.Weight, MassUnit.Pound, MassUnit.Ounce);
            
            
            Assert.AreEqual(convertedWeight1, expected);
            Assert.AreEqual(convertedWeight2, expected);
            Assert.AreEqual(convertedWeight3, expected);
        }
        
        [TestCase("0")]
        [TestCase("16.5")]
        [TestCase("148974.464664")]
        [TestCase("-1")]
        public void PoundsToKgTest(string weight)
        {
            var tw1 = new TotalWeight { Unit = "lbs", Weight = Convert.ToDecimal(weight) };
            var tw2 = new TotalWeight { Unit = "pounds", Weight = Convert.ToDecimal(weight) };
            var tw3 = new TotalWeight { Unit = "pound", Weight = Convert.ToDecimal(weight) };
            const string endUom1 = "kg";
            const string endUom2 = "kilogram";
            const string endUom3 = "kilograms";

            var convertedWeight1 = _uomHelper.ConvertTotalWeight(tw1, endUom1);
            var convertedWeight2 = _uomHelper.ConvertTotalWeight(tw2, endUom2);
            var convertedWeight3 = _uomHelper.ConvertTotalWeight(tw3, endUom3);

            var expected = UnitConverter.Convert(tw1.Weight, MassUnit.Pound, MassUnit.Kilogram);
            
            
            Assert.AreEqual(convertedWeight1, expected);
            Assert.AreEqual(convertedWeight2, expected);
            Assert.AreEqual(convertedWeight3, expected);
        }
        
        [TestCase("0")]
        [TestCase("16.5")]
        [TestCase("148974.464664")]
        [TestCase("-1")]
        public void PoundsToPoundsTest(string weight)
        {
            var tw1 = new TotalWeight { Unit = "lbs", Weight = Convert.ToDecimal(weight) };
            var tw2 = new TotalWeight { Unit = "pounds", Weight = Convert.ToDecimal(weight) };
            var tw3 = new TotalWeight { Unit = "pound", Weight = Convert.ToDecimal(weight) };
            const string endUom1 = "lbs";
            const string endUom2 = "pounds";
            const string endUom3 = "pound";

            var convertedWeight1 = _uomHelper.ConvertTotalWeight(tw1, endUom1);
            var convertedWeight2 = _uomHelper.ConvertTotalWeight(tw2, endUom2);
            var convertedWeight3 = _uomHelper.ConvertTotalWeight(tw3, endUom3);

            var expected = UnitConverter.Convert(tw1.Weight, MassUnit.Pound, MassUnit.Pound);
            
            
            Assert.AreEqual(convertedWeight1, expected);
            Assert.AreEqual(convertedWeight2, expected);
            Assert.AreEqual(convertedWeight3, expected);
        }
        
        [TestCase("0")]
        [TestCase("16.5")]
        [TestCase("148974.464664")]
        [TestCase("-1")]
        public void KgToOzTest(string weight)
        {
            var tw1 = new TotalWeight { Unit = "kg", Weight = Convert.ToDecimal(weight) };
            var tw2 = new TotalWeight { Unit = "kilograms", Weight = Convert.ToDecimal(weight) };
            var tw3 = new TotalWeight { Unit = "kilogram", Weight = Convert.ToDecimal(weight) };
            const string endUom1 = "oz";
            const string endUom2 = "ounces";
            const string endUom3 = "ounce";

            var convertedWeight1 = _uomHelper.ConvertTotalWeight(tw1, endUom1);
            var convertedWeight2 = _uomHelper.ConvertTotalWeight(tw2, endUom2);
            var convertedWeight3 = _uomHelper.ConvertTotalWeight(tw3, endUom3);

            var expected = UnitConverter.Convert(tw1.Weight, MassUnit.Kilogram, MassUnit.Ounce);
            
            
            Assert.AreEqual(convertedWeight1, expected);
            Assert.AreEqual(convertedWeight2, expected);
            Assert.AreEqual(convertedWeight3, expected);
        }
        
        [TestCase("0")]
        [TestCase("16.5")]
        [TestCase("148974.464664")]
        [TestCase("-1")]
        public void OzToPoundTest(string weight)
        {
            var tw1 = new TotalWeight { Unit = "ounce", Weight = Convert.ToDecimal(weight) };
            var tw2 = new TotalWeight { Unit = "ounces", Weight = Convert.ToDecimal(weight) };
            var tw3 = new TotalWeight { Unit = "oz", Weight = Convert.ToDecimal(weight) };
            const string endUom1 = "pounds";
            const string endUom2 = "lbs";
            const string endUom3 = "pound";

            var convertedWeight1 = _uomHelper.ConvertTotalWeight(tw1, endUom1);
            var convertedWeight2 = _uomHelper.ConvertTotalWeight(tw2, endUom2);
            var convertedWeight3 = _uomHelper.ConvertTotalWeight(tw3, endUom3);

            var expected = UnitConverter.Convert(tw1.Weight, MassUnit.Ounce, MassUnit.Pound);
            
            
            Assert.AreEqual(convertedWeight1, expected);
            Assert.AreEqual(convertedWeight2, expected);
            Assert.AreEqual(convertedWeight3, expected);
        }
    }
}