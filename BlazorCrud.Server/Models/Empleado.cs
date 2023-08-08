using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null !;

    public int Telefono { get; set; }

    public string Email { get; set; } = null!;

    public int Sueldo { get; set; }

    public DateTime FechaContrato { get; set; }

    public int IdDepartamento { get; set; }

    public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
}
