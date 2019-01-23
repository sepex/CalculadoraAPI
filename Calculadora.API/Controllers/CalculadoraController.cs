using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Calculadora.API.Application.Commands;
using Calculadora.API.Model;
using Calculadora.API.Model.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Calculadora.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CalculadoraController : ControllerBase
  {
    private readonly IOptions<DefaultSettings> _defaultSettings;
    private readonly IMediator _mediator;
    public CalculadoraController(IOptions<DefaultSettings> defaultSettings, IMediator mediator)
    {
      _defaultSettings = defaultSettings;
      _mediator = mediator;
    }

    // GET api/values
    [HttpGet("calculajuros")]
    public async Task<ActionResult<double>> CalculaJuros([FromQuery] CalculaJurosDto parameters)
    {
      try
      {
        CalculaJurosCommand command = new CalculaJurosCommand()
        {
          ValorInicial = parameters.ValorInicial,
          Meses = parameters.Meses,
          CasasDecimais = _defaultSettings.Value.DecimalPlaces,
          TaxaJuros = _defaultSettings.Value.Interest
        };

        // Utilização do MediatR como preparação para separação do command em microserviço.
        double commandResult = await _mediator.Send(command);

        // Retorno 200
        return Ok(commandResult);
      }
      catch (ValidationException ex)
      {
        // Retorno do status 400 - Bad Request
        return BadRequest(new { ex.Message, ex.HelpLink });
      }
      catch (Exception ex)
      {
        // Retorno do status 500 - Internal Server Error
        return StatusCode((int)HttpStatusCode.InternalServerError, new { ex.Message, ex.HelpLink });
      }
    }

    [HttpGet("showmethecode")]
    public ActionResult<string> ShowMeTheCode()
    {
      return Ok(_defaultSettings.Value.GitHubUrl);
    }
  }
}
