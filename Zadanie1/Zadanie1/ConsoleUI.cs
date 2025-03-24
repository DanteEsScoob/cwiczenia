using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace Zadanie1;

public class ConsoleUI
{
    private readonly List<ConteinerShip> ships = new();
    private readonly List<Conteiner> conteiners = new();

    public void Start()
    {
        var work = true;
        while (work)
        {
            Console.Clear();
            Console.WriteLine("Witaj użytkoniku aplikacji twój mały port SA");
            Console.WriteLine("Lista kontenerowców w twoim porcie");
            foreach (var s in ships)
            {
                Console.WriteLine("* " + s.Name);
            }

            Console.WriteLine("============================================== \n");
            Console.WriteLine("Kontenery");
            foreach (var c in conteiners)
            {
                Console.WriteLine("\t * " + c.SerialNumber);
            }
            
            Console.WriteLine("========================================== \n");

            Console.WriteLine("Dostępne opjce: ");
            Console.WriteLine("1. Dodaj kontenerowiec");
            Console.WriteLine("2. Dodaj kontener");
            Console.WriteLine("3. Załaduj kontener na statek");
            Console.WriteLine("4. Załaduj listę kontenerów na statek");
            Console.WriteLine("5. Usuń kontener ze statku");
            Console.WriteLine("6. Rozładuj kontener");
            Console.WriteLine("7. Zamień kontener na statku");
            Console.WriteLine("8. Przenieś kontener między statkami");
            Console.WriteLine("9. Informacje o kontenerze");
            Console.WriteLine("10. Informacje o statku");
            Console.WriteLine("11. Opróżnij kontener");
            Console.WriteLine("0. Wyjście");
            Console.WriteLine("Co chcesz zrobić?: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1": AddShip(); break;
                case "2": AddConteiner(); break;
                case "3": AddConteinerToShip(); break;
                case "4": AddListOfContainers(); break;
                case "5": RemoveConteinerFromShip(); break;
                case "6": ExtractContainer(); break;
                case "7": ChangeContainer(); break;
                case "8": MoveContainer(); break;
                case "9": DisplayConteinerInfo();break;
                case "10": DisplayShipInfo();break;
                case "11": RemoveCargoFromConteiner(); break;
                case "0": work = false; break;
                default: Console.WriteLine("Nie znana komenda! "); break;
                
            }
            
                
            
            
        }
    }

    private void RemoveCargoFromConteiner()
    {
        Console.WriteLine("Numer seryjny kontenera: ");
        var serialNumber = Console.ReadLine();
        var k = conteiners.Find(c => c.SerialNumber == serialNumber);
        if (k is null)
        {
            Console.WriteLine("Nie znana komenda! ");
        }
        else
        {
            k.RemoveCargo();
        }
    }

    private void DisplayShipInfo()
    {
        Console.WriteLine("Nazwa statku: ");
        var name = Console.ReadLine();
        var s = ships.Find(x => x.Name == name);
        s?.DisplayInfo();
        Console.ReadKey();
    }

    private void DisplayConteinerInfo()
    {
        Console.WriteLine("Numer seryjny kontenera: ");
        var input = Console.ReadLine();
        
        var k = conteiners.Find(c => c.SerialNumber == input);
        if (k != null)
        {
            Console.WriteLine($"{k.SerialNumber}: \nMasa Ładunku = {k.CargoWeight} kg \nWaga Własna = {k.OwnMass} kg \n Typ: {k.GetType()}");
        }
        Console.ReadKey();
        
    }

    private void MoveContainer()
    {
        Console.WriteLine("Numer seyjny kontenera: ");
        var input = Console.ReadLine();
        Console.WriteLine("Nazwa statku źródłowego: ");
        var name = Console.ReadLine();
        Console.WriteLine("Statek docelowy: ");
        var newName = Console.ReadLine();
        var s1 = ships.Find(s => s.Name == name);
        var s2 = ships.Find(s => s.Name == newName);
        var c = s1?.Conteiners.Find(k => k.SerialNumber == input);
        if (s1 != null && s2 != null && c != null)
        {
            s1.RemoveConteiner(input);
            try{s2.AddConteiner(c);} catch(Exception e){
                Console.WriteLine(e.Message);
            }

            
        }
    }

    private void ChangeContainer()
    {
        Console.WriteLine("Numer seryjny starego kontenera: ");
        var oldConteiner = Console.ReadLine();
        Console.WriteLine("Numer seryjny nowego kontenera: ");
        var newConteiner = Console.ReadLine();
        var newConteinerOnBoard = conteiners.Find(c => c.SerialNumber == newConteiner);
        foreach (var s in ships)
        {
            if (s.Conteiners.Any(k => k.SerialNumber == oldConteiner))
            {
                s.ChangeConteiner(oldConteiner, newConteinerOnBoard);
            }
        }
        
        
        
    }

    private void ExtractContainer()
    {
        Console.WriteLine("Numer seryjny kontenera: ");
        var serialNumber = Console.ReadLine();
        foreach (var s in ships)
        {
            s.ExtractConteiners(serialNumber);
        }
    }

    private void RemoveConteinerFromShip()
    {
        Console.WriteLine("Numer seryjny kontenera: ");
        var serialNumber = Console.ReadLine();
        foreach (var s in ships)
        {
            s.RemoveConteiner(serialNumber);
        }
    }

    private void AddListOfContainers()
    {
        Console.WriteLine("Nazwa statku: ");
        var name = Console.ReadLine();
        var ship = ships.Find(s => s.Name == name);
        if (ship == null)
        {
            Console.WriteLine("Nie znaleziono statku: ");
            Console.ReadKey();
            return;
        }

        foreach (var c in conteiners)
        {
            try
            {
                ship.AddConteiner(c);
            }
            catch
            {
                
            }
        }
    }

    private void AddConteinerToShip()
    {
        Console.WriteLine("Numer seryjny kontenera: ");
        var input = Console.ReadLine();
        Console.WriteLine("Nazwa statku: ");
        var name = Console.ReadLine();
        
        var conteiner = conteiners.Find(k => k.SerialNumber == input);
        var ship = ships.Find(k => k.Name == name);

        if (conteiner == null || ship == null)
        {
            Console.WriteLine("Nie znaleziono statku lub kontenera! ");
            Console.ReadKey();
            return;
        }

        try
        {
            ship.AddConteiner(conteiner);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Błąd: " + ex.Message);
            Console.ReadKey();
        }
    }

    private void AddConteiner()
    {
        Console.Write("Typ (L/G/C): ");
        string type = Console.ReadLine().ToUpper();
        Console.Write("Max ładowność: ");
        double maxCargo = double.Parse(Console.ReadLine());
        Console.WriteLine("Wysokość kontenera: ");
        var height = double.Parse(Console.ReadLine());
        Console.WriteLine("Głębokość kontenera: ");
        var depth = double.Parse(Console.ReadLine());
        Console.WriteLine("Masa własna: ");
        var ownMas = double.Parse(Console.ReadLine());
        Console.WriteLine("Podaj mase w KG: ");
        var cargo = double.Parse(Console.ReadLine());
        
        
        Conteiner newConteiner = null;

        switch (type)
        {
            case "L":
                Console.Write("Czy niebezpieczny (true/false): ");
                bool hazard = bool.Parse(Console.ReadLine());
                newConteiner = new LiquidConteiner(maxCargo, hazard);
                break;
            case "G":
                newConteiner = new GasContainer(maxCargo);
                break;
            case "C":
                Console.Write("Produkt: ");
                string product = Console.ReadLine();
                Console.Write("Temperatura: ");
                double temp = double.Parse(Console.ReadLine());
                newConteiner = new FreezerConteiner(maxCargo, product, temp);
                break;
            default:
                Console.WriteLine("Nie znany typ kontenera! ");
                Console.ReadKey();
                break;
        }
        if (newConteiner != null)
        {
            newConteiner.Height = height;
            newConteiner.Depth = depth;
            newConteiner.OwnMass = ownMas;
            newConteiner.AddCargo(cargo);
            conteiners.Add(newConteiner);
        }
    }
    

    private void AddShip()
    {
        Console.WriteLine("Nazwa: ");
        var name = Console.ReadLine();
        Console.WriteLine("Prędkość maksymalna: ");
        var velocity = double.Parse(Console.ReadLine());
        Console.WriteLine("Maksymalna liczba kontenerów: ");
        var max = int.Parse(Console.ReadLine());
        Console.WriteLine("Masa maksymalna: ");
        var mass = double.Parse(Console.ReadLine());
        
        ships.Add(new ConteinerShip(name, velocity, max, mass));
    }
}