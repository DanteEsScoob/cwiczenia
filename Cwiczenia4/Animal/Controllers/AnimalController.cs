using Microsoft.AspNetCore.Mvc;

namespace Animal
{
[Route("api/[controller]")]
[ApiController]
    public class AnimalController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var animals = Database.Animals;
            if (animals == null) return NotFound();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var animal = Database.Animals.FirstOrDefault(x => x.Id == id);
            if (animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpGet("search")]
        public IActionResult Search(string name)
        {
            var animal = Database.Animals.Where(n => n.Name.Contains(name)).ToList();
            if(animal == null) return NotFound();
            return Ok(animal);
        }

        [HttpPost]
        public IActionResult Post(Models.Animal animal)
        {
            Database.Animals.Add(animal);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Modify(int id, Models.Animal animal)
        {
            var oldAnimal = Database.Animals.FirstOrDefault(x => x.Id == id);
            if (oldAnimal == null) return NotFound();
            oldAnimal.Name = animal.Name;
            oldAnimal.Species = animal.Species;
            oldAnimal.Weight = animal.Weight;
            oldAnimal.Color = animal.Color;
            return Ok(oldAnimal);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedAnimal = Database.Animals.FirstOrDefault(x => x.Id == id);
            try
            {
                if (deletedAnimal == null)
                {
                    return NotFound();
                }
                else
                {
                    Database.Animals.Remove(deletedAnimal);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
            
    }
   
}