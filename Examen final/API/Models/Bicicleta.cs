using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Bicicleta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public int Tamano { get; set; }

    public int CantidadPlatos { get; set; }

    public int CantidadPinones { get; set; }
}
