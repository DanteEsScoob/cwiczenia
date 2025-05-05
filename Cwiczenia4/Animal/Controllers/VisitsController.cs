using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Animal.Models;

namespace Animal.Controllers
{
    [Route("api/animal/{animalId}/visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Visit>> GetAllVisits(int animalId)
        {
            var visits = Database.Visits
                .Where(v => v.AnimalId == animalId)
                .ToList();
            return Ok(visits);
        }

        [HttpGet("{visitId}")]
        public ActionResult<Visit> Get(int animalId,int visitId)
        {
            var visit = Database.Visits
                .FirstOrDefault(v => v.AnimalId == animalId && v.VisitId == visitId);
            
            if (visit == null) return NotFound();
            return Ok(visit);
        }

        [HttpPost]
        public ActionResult<Visit> Add(int animalId, [FromBody] Visit visit)
        {
            visit.AnimalId = animalId;
            visit.VisitDate = DateTime.Now;
            
            Database.Visits.Add(visit);
            return CreatedAtAction(nameof(Get), new { animalId, visitId = visit.VisitId }, visit);
        }
        

    }
}