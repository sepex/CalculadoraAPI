using Calculadora.API.Application.Commands;
using Calculadora.API.Controllers;
using Calculadora.API.Model;
using Calculadora.API.Model.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Calculadora.API.Test
{
  [TestClass]
  public class CalculadoraControllerTest
  {
    #region Initialize & Properties

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    { 
      _calculaJurosDto = new CalculaJurosDto()
      {
        ValorInicial = 100,
        Meses = 5
      };

      _defaultSettings = GetIOptionsMock();
    }

    #region Properties
    private static CalculaJurosDto _calculaJurosDto;
    private static Mock<IOptions<DefaultSettings>> _defaultSettings;
    #endregion

    private static Mock<IOptions<DefaultSettings>> GetIOptionsMock()
    {
      var options = new Mock<IOptions<DefaultSettings>>();

      options.Setup(x => x.Value).Returns(new DefaultSettings()
      {
        DecimalPlaces = 2,
        GitHubUrl = "GitHubUrl",
        Interest = 0.01
      });

      return options;
    }

    private static Mock<IMediator> GetIMediator()
    {
      var result = new Mock<IMediator>();

      result.Setup(m => m.Send(It.IsAny<CalculaJurosCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromException<double>(new NotImplementedException()));

      return result;
    }
    #endregion

    [TestMethod]
    public async Task Testa_Tipo_Retorno_CalculaJuros_OK()
    {
      var value = 105.1;

      var mediator = GetIMediator();
      mediator.Setup(m => m.Send(It.IsAny<CalculaJurosCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<double>(value));
      
      var controller = new CalculadoraController(_defaultSettings.Object, mediator.Object);

      ActionResult<double> result = await controller.CalculaJuros(_calculaJurosDto);

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Result);
      Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
      Assert.AreEqual((result.Result as OkObjectResult).Value, value);
    }

    [TestMethod]
    public async Task Testa_Tipo_Retorno_CalculaJuros_Erro_Validacao()
    {
      var mediator = GetIMediator();
      mediator.Setup(m => m.Send(It.IsAny<CalculaJurosCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromException<double>(new ValidationException("Validate Exception!")));
      
      var controller = new CalculadoraController(_defaultSettings.Object, mediator.Object);

      ActionResult<double> result = await controller.CalculaJuros(_calculaJurosDto);

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Result);
      Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task Testa_Tipo_Retorno_CalculaJuros_Erro_Interno()
    {
      var mediator = GetIMediator();
      mediator.Setup(m => m.Send(It.IsAny<CalculaJurosCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromException<double>(new Exception("Unhandled Exception!")));
      
      var controller = new CalculadoraController(_defaultSettings.Object, mediator.Object);

      ActionResult<double> result = await controller.CalculaJuros(_calculaJurosDto);

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Result);
      Assert.IsInstanceOfType(result.Result, typeof(ObjectResult));
      Assert.AreEqual((result.Result as ObjectResult).StatusCode, (int)HttpStatusCode.InternalServerError);
    }

    [TestMethod]
    public void Testa_ShowMeTheCode()
    {
      var controller = new CalculadoraController(_defaultSettings.Object, It.IsAny<IMediator>());

      ActionResult<string> result = controller.ShowMeTheCode();

      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Result);
      Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
      Assert.AreEqual((result.Result as ObjectResult).Value, _defaultSettings.Object.Value.GitHubUrl);
    }

  }
}
