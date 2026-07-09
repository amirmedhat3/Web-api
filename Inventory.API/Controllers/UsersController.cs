using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Entities;

namespace Inventory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    // GET /api/users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }

    // GET /api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // POST /api/users
    [HttpPost]
    public async Task<IActionResult> Add(User user)
    {
        await _repository.AddAsync(user);
        return Ok(user);
    }

    // PUT /api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        if (id != user.Id)
            return BadRequest();

        await _repository.UpdateAsync(user);
        return Ok(user);
    }

    // DELETE /api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return Ok();
    }
}