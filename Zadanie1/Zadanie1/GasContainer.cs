namespace Zadanie1;

public class GasContainer : Conteiner, HazardNotification

{
    public GasContainer(double maxCargo) : base("G", maxCargo)
    {
    }

    public override void AddCargo(double cargo)
    {
        if (cargo + cargoWeight > maxCargo)
        {
            Notify("Przeładowanie konteneru na gaz! " + SerialNumber);
        }
        cargoWeight += cargo;
        
    }

    public override void RemoveCargo()
    {
        cargoWeight *= 0.05;
    }

    public void Notify(string msg)
    {
        Console.WriteLine($"[Hazard] {msg}");
    }
}