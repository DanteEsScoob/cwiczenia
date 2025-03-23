namespace Zadanie1;

public abstract class Conteiner
{
    private int _counter = 0;
    protected double cargoWeight;

    public string SerialNumber { get; set; }
    public double CargoWeight => cargoWeight;
    public double MaxCargo => maxCargo;
    protected double maxCargo;
    public double Height { get; set; }
    public double Depth { get; set; }
    public double OwnMass { get; set; }

    protected Conteiner(string typ, double maxCargo)
    {
        SerialNumber = $"KON-{typ}-{_counter++}";
        this.maxCargo = maxCargo;
        this.cargoWeight = 0;
    }

    public abstract void AddCargo(double cargo);
    public abstract void RemoveCargo();
}