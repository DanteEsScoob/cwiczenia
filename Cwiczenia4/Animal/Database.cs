using Animal.Models;
namespace Animal;

public class Database
{
    public static List<Models.Animal> Animals = new List<Models.Animal>()
    {
        new Models.Animal("Pinky", "Cat", 2, "White"),
        new Models.Animal("Chocolate",  "Dog", 10, "Brown"),
        new Models.Animal("Pineapple", "Horse", 3,  "White"),
        new Models.Animal("Pig", "Cat", 9, "White")
    };

    public static List<Models.Visit> Visits = new List<Models.Visit>()
    {
        new Visit(DateTime.Today, 1, "Fryzjer", 100),
        new Visit(DateTime.Today, 2, "Mycie", 80),
        new Visit(DateTime.Today, 3, "Dietetyk", 250),
        new Visit(DateTime.Today, 4, "Rehabilitacja", 200),
    };
}