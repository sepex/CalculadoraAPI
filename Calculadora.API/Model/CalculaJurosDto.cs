using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.API.Model
{
  public class CalculaJurosDto
  {
    [Required]
    [FromQuery(Name = "valorinicial")]
    [Range(0, double.MaxValue, ErrorMessage = "Favor inserir um valor inicial maior ou igual a zero.")]
    public double ValorInicial { get; set; }

    [Required]
    [FromQuery(Name = "meses")]
    [Range(0, int.MaxValue, ErrorMessage = "Favor inserir um número de períodos maior ou igual a zero.")]
    public int Meses { get; set; }
  }
}
