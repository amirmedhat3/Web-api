using Inventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Inventory.Domain.Entities;

namespace Inventory.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }


//GET /api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }

//GET /api/products/{id}
    [HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var product = await _repository.GetByIdAsync(id);

    if (product == null)
        return NotFound();

    return Ok(product);
}

//POST /api/products
[HttpPost]
public async Task<IActionResult> Add(Product product)
{
    await _repository.AddAsync(product);
    return Ok(product);
}

//PUT /api/products/{id}
[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, Product product)
{
    if (id != product.Id)
        return BadRequest();

    await _repository.UpdateAsync(product);
    return Ok(product);
}

//DELETE /api/products/{id}
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    await _repository.DeleteAsync(id);
    return Ok();
}
}