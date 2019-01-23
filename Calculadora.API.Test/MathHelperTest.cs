using Calculadora.API.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculadora.API.Test
{
  [TestClass]
  public class MathHelperTest
  {
    [TestMethod]
    public void Testa_Correta_Execucao()
    {
      double result = MathHelper.Truncate(1.001, 2);
      Assert.AreEqual(result, 1);

      result = MathHelper.Truncate(1.009, 2);
      Assert.AreEqual(result, 1);

      result = MathHelper.Truncate(1.011, 2);
      Assert.AreEqual(result, 1.01);

      result = MathHelper.Truncate(1.019, 2);
      Assert.AreEqual(result, 1.01);

      result = MathHelper.Truncate(1.9, 0);
      Assert.AreEqual(result, 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArithmeticException))]
    public void Testa_Erro_Casas_Menor_Zero()
    {
      MathHelper.Truncate(1.001, -1);
    }
  }
}
