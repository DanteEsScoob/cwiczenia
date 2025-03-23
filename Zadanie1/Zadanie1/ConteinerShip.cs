namespace Zadanie1;

using System.Linq;

public class ConteinerShip
{
    public string Name{get;set;}
    public double MaxVelocity{get;set;}
    public int MaxConteiners{get;set;}
    public double MaxWeight{get;set;}

    private readonly List<Conteiner> _conteiners = new();
    public List<Conteiner> Conteiners => _conteiners;

    public ConteinerShip(string name, double maxVelocity, int maxConteiners, double maxWeight)
    {
        Name = name;
        MaxVelocity = maxVelocity;
        MaxConteiners = maxConteiners;
        MaxWeight = maxWeight;
        
    }


    public void AddConteiner(Conteiner conteiner)
    {
        if (_conteiners.Count >= MaxConteiners)
        {
            throw new OverFillException($"Statek {Name} jest pełny! ");
        }

        double actualWeight = _conteiners.Sum(k => k.CargoWeight + k.OwnMass);
        double newWeight = actualWeight + conteiner.CargoWeight + conteiner.OwnMass;

        if (newWeight > MaxWeight)
        {
            throw new OverFillException("Masa maksymalna statku została przekroczona");
        }
        _conteiners.Add(conteiner);
        
    }

    public void RemoveConteiner(string serialNumber)
    {
        var conteiner = _conteiners.FirstOrDefault(k => k.SerialNumber == serialNumber);
        if (conteiner != null)
        {
            _conteiners.Remove(conteiner);
        }
        
    }

    public void ExtractConteiners(string serialNumber)
    {
        var conteiner = _conteiners.FirstOrDefault(k => k.SerialNumber == serialNumber);
        conteiner?.RemoveCargo();
    }

    public void ChangeConteiner(string serialNumber, Conteiner newConteiner)
    {
        RemoveConteiner(serialNumber);
        AddConteiner(newConteiner);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Statek: {Name} (max speed: {MaxVelocity} węzłów, kontenery: {_conteiners.Count}/{MaxConteiners})");
        foreach (var k in _conteiners)
        {
            Console.WriteLine($"  - {k.SerialNumber} | masa ładunku: {k.CargoWeight} kg, waga własna: {k.OwnMass} kg");
        }
    }
}