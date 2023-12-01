using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawPatientManagerWebAPI.DBContextFiles;
using PawPatientManagerWebAPI.DTOs;

namespace PawPatientManagerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public MedicationController(MyDbContext dContext)
        {
            _dbContext = dContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicationDTO>>> GetMedications()
        {
            if(_dbContext.Medications == null)
            {
                return NotFound();
            }
            return await _dbContext.Medications.ToListAsync();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<MedicationDTO>> GetMedication(Guid ID)
        {
            if (_dbContext.Medications == null)
            {
                return NotFound();
            }

            MedicationDTO? med = await _dbContext.Medications.FindAsync(ID);
            if (med == null)
            {
                return NotFound();
            }

            return med;
        }

        [HttpPost]
        public async Task<ActionResult<MedicationDTO>> PostMedication(MedicationDTO med)
        {
            _dbContext.Medications.Add(med);

            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedication), new { ID = med.ID }, med);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult> UpdateMedication(Guid ID, MedicationDTO med)
        {
            if(ID != med.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(med).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            } 
            catch(DbUpdateConcurrencyException ex) 
            {
                if (MedicationExists(ID))
                {
                    return NotFound();
                }
            }

            return Ok();

        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteMedication(Guid ID)
        {
            if(_dbContext.Medications == null) return NotFound();

            var med = await _dbContext.Medications.FindAsync(ID);

            if(med == null)
            {
                return NotFound();
            }

            _dbContext.Medications.Remove(med);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool MedicationExists(Guid ID)
        {
            return _dbContext.Medications.Any(d => d.ID == ID);
        }
    }
}
