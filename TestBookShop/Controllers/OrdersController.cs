using Microsoft.AspNetCore.Mvc;
using TestBookShop.Abstractions;
using TestBookShop.Entities;
using TestBookShop.Exceptions;

namespace TestBookShop.Controllers;

[ApiController] [Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IRepositoryService _repository;

    public OrdersController(IRepositoryService repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetListOfOrdersByFilter(int numberOfOrder, DateTime dateOfOrder)
    {
        var orderList = await _repository.GetListOfOrderAsync(numberOfOrder, dateOfOrder);

        if (orderList is null)
            throw new NotFoundException("No orders found with these values.");
        
        return Ok(orderList);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(int[] booksId)
    {
        var order = await _repository.PostOrderAsync(booksId);

        if (order is null)
            throw new Exception("Something went wrong.");
        
        return Ok(order);
    }
}