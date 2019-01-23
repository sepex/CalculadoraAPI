using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.API.Helpers
{
  public static class MathHelper
  {
    public static double Truncate(double value, int decimalPlaces)
    {
      if (decimalPlaces < 0)
        throw new ArithmeticException($"Não é possível arredondar para menos que zero casas decimais. Número de casas solicitadas: {decimalPlaces}");

      // Caso o número de casas decimais seja igual a zero somente trunca com a função original, economizando as operações de multiplicação e divisão por 1.
      if (decimalPlaces == 0)
        return Math.Truncate(value);
      
      // Cálculo do fator de multiplicação.
      int factor = (int) Math.Truncate(Math.Pow(10, (double)decimalPlaces));
      
      // Multiplica pelo número de casas decimais antes de truncar.
      // Após truncado divide para que os valores retornem para as respectivas casas decimais.
      return Math.Truncate(value * factor) / factor;
    }
  }
}
