using EnergydrinkAPI.Models;
using EnergydrinkAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EnergydrinkAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EnergydrinkController : ControllerBase
{
    private readonly EnergydrinkRepository _repository;

    public EnergydrinkController()
    {
        _repository = new EnergydrinkRepository();
    }

    // GET: /energydrink
    [HttpGet]
    public ActionResult<IEnumerable<Energydrink>> Get()
    {
        return Ok(_repository.GetAll());
    }

    // GET: /energydrink/{id}
    [HttpGet("{id}")]
    public ActionResult<Energydrink> Get(int id)
    {
        var drink = _repository.GetById(id);
        if (drink == null)
            return NotFound();
        return Ok(drink);
    }

    // POST: /energydrink
    [HttpPost]
    public ActionResult<Energydrink> Post([FromBody] Energydrink newDrink)
    {
        try
        {
            newDrink.Validate();
            if (_repository.GetById(newDrink.Id) != null)
                return Conflict("An Energydrink with this Id already exists.");

            _repository.Add(newDrink);
            return CreatedAtAction(nameof(Get), new { id = newDrink.Id }, newDrink);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: /energydrink/{id}
    [HttpPut("{id}")]
    public ActionResult Put([FromBody] Energydrink updatedDrink)
    {
        try
        {
            updatedDrink.Validate();
            if (!_repository.Update(updatedDrink))
                return NotFound();
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: /energydrink/{id}
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (!_repository.Delete(id))
            return NotFound();
        return NoContent();
    }
}
