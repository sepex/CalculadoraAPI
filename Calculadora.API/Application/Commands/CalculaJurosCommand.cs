using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Calculadora.API.Application.Commands
{
  public class CalculaJurosCommand : IRequest<double>
  {
    [DataMember]
    public double ValorInicial { get; set; }

    [DataMember]
    public double TaxaJuros { get; set; }

    [DataMember]
    public int Meses { get; set; }

    [DataMember]
    public int CasasDecimais { get; set; } = 2;
  }
}
