using Calculadora.API.Application.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Calculadora.API.Test
{
  [TestClass]
  public class CalculaJurosCommandHandlerTest
  {
    [TestMethod]
    public async Task Calculo_Juros_2Casas_Trunca_Menos_Metade()
    {
      var command = new CalculaJurosCommand()
      {
        CasasDecimais = 2,
        Meses = 5,
        ValorInicial = 100,
        TaxaJuros = 0.01
      };

      double result = await ExecuteCommand(command);

      Assert.AreEqual(result,105.10);
    }

    [TestMethod]
    public async Task Calculo_Juros_2Casas_Trunca_Mais_Metade()
    {
      var command = new CalculaJurosCommand()
      {
        CasasDecimais = 2,
        Meses = 9,
        ValorInicial = 100,
        TaxaJuros = 0.01
      };

      double result = await ExecuteCommand(command);

      Assert.AreEqual(result,109.36);
    }

    [TestMethod]
    public async Task Calculo_Juros_ValorInicial_Zero()
    {
      var command = new CalculaJurosCommand()
      {
        CasasDecimais = 2,
        Meses = 9,
        ValorInicial = 0,
        TaxaJuros = 0.01
      };

      double result = await ExecuteCommand(command);

      Assert.AreEqual(result, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public async Task Calculo_Juros_ValorInicial_Negativo()
    {
      var command = new CalculaJurosCommand()
      {
        CasasDecimais = 2,
        Meses = 9,
        ValorInicial = -100,
        TaxaJuros = 0.01
      };

      await ExecuteCommand(command);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public async Task Calculo_Juros_Meses_Negativo()
    {
      var command = new CalculaJurosCommand()
      {
        CasasDecimais = 2,
        Meses = -9,
        ValorInicial = 100,
        TaxaJuros = 0.01
      };

      await ExecuteCommand(command);
    }
    private async Task<double> ExecuteCommand(CalculaJurosCommand command)
    {
      var handler = new CalculaJurosCommandHandler();
      return await handler.Handle(command, new CancellationToken());
    }
  }
}
