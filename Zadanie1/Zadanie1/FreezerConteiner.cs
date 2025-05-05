namespace Zadanie1;
using System.Collections.Generic;


public class FreezerConteiner : Conteiner, HazardNotification
{
    public string Product { get; set; }
    public double Temperature { get; set; }

    private static readonly Dictionary<string, double> RequiredTemp = new()
    {
        {"banany", 13.3},
        {"czekolada", 18.0},
        {"mieso", -15.0},
        {"lody", -18.0},
        {"ryby", -20.0},
        {"jaja", 5.0},
        {"mleko", 4.0}
    };
    
    public FreezerConteiner( double maxCargo, string product, double temperature) : base("C", maxCargo)
    {
        Product = product.ToLower();
        Temperature = temperature;
        if (!RequiredTemp.ContainsKey(product))
        {
            throw new ArgumentException($"Produktu {product} nie znaleziono");
        }

        if (temperature > RequiredTemp[product])
        {
            throw new ArgumentException($"Temperatura w kontenerze jest za wysoka dla produktu {product} ");
        }
    }

    public override void AddCargo(double cargo)
    {
        if (cargo + cargoWeight > MaxCargo)
        {
            Notify("Kontener przepełniony!");
        } 
        cargoWeight += cargo;
    }

    public override void RemoveCargo()
    {
        cargoWeight = 0;
    }

    public void Notify(string msg)
    {
        Console.WriteLine($"[Hazard] {msg}");
    }
}