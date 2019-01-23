using Calculadora.API.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Calculadora.API.Application.Commands
{
  public class CalculaJurosCommandHandler : IRequestHandler<CalculaJurosCommand, double>
  {
    public Task<double> Handle(CalculaJurosCommand request, CancellationToken cancellationToken)
    {
      // O valor inicial não deve ser menor do que zero.
      if (request.ValorInicial < 0)
        throw new ValidationException("O valor inicial não deve ser menor que zero.");

      // Não é possível calcular juros para período menor que zero.
      if (request.Meses < 0)
        throw new ValidationException("Número de meses não pode ser menor que zero.");

      // Código adicionado para economia de performance, uma vez que qualquer multiplicação por zero resulta no mesmo resultado.
      if (request.ValorInicial == 0)
        return Task.FromResult(request.ValorInicial);

      //Realiza o cálculo.
      return Task.FromResult(MathHelper.Truncate(request.ValorInicial * Math.Pow(1 + request.TaxaJuros, request.Meses),request.CasasDecimais));
    }
  }
}
