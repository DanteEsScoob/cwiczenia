namespace Animal.Models;

public class Animal
{
    private static int _id = 0;

    public int Id { get; private set; }
    public string Name{get;set;}
    public string Species{get;set;}
    public int Weight{get;set;}
    public string Color{get;set;}


    public Animal(string name, string species, int weight, string color)
    {
        Id = ++_id;
        this.Name = name;
        this.Species = species;
        this.Weight = weight;

        this.Color = color;
    }

    public Animal()
    {
        Id = ++_id;
    }

}