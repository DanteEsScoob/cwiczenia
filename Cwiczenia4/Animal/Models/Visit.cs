namespace Animal.Models;

public class Visit
{
    private static int _visitId = 0;

    public int VisitId { get; private set; }
    public DateTime VisitDate { get; set;}
    public int AnimalId { get; set;}
    public string Description { get; set;}
    public decimal PriceForVisit { get; set;}

    public Visit(DateTime visitDate, int animalId, string description, decimal priceForVisit)
    {
        VisitId = ++_visitId;
        this.VisitDate = visitDate;
        this.AnimalId = animalId;
        this.Description = description;
        this.PriceForVisit = priceForVisit;
    }

    public Visit()
    {
        VisitId = ++_visitId;
    }
}