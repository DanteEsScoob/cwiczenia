namespace Zadanie1;
public class LiquidConteiner : Conteiner, HazardNotification
{
    private bool Hazard{get;set;}
    public LiquidConteiner(double maxCargo, bool hazard) : base("L", maxCargo)
    {
        Hazard = hazard;
    }

    public override void AddCargo(double cargo)
    {
       var limit = Hazard ? maxCargo * 1.5 : maxCargo * 0.9;
       if (cargo + cargoWeight > limit)
       {
           Notify("Kontener przepełniony!" + SerialNumber);
           throw new OverFillException("Przepełniono kontener");
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