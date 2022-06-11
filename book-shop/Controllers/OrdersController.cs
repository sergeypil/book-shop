using AutoMapper;
using book_shop.Data;
using book_shop.Data.Entities;
using book_shop.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace book_shop.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IBookRepository bookRepository,
          ILogger<OrdersController> logger,
          IMapper mapper,
          UserManager<StoreUser> userManager)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(bool includeItem = true)
        {
            try
            {
                var userName = User.Identity.Name;
                var results = _bookRepository.GetAllOrdersByUser(userName, includeItem);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _bookRepository.GetOrderById(User.Identity.Name, id);
                if (order != null) return Ok(_mapper.Map<Order, OrderViewModel>(order));
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get order: {ex}");
                return BadRequest("Failed to get order");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    _bookRepository.AddOrder(newOrder);

                    if (_bookRepository.SaveAll())
                    {
                        var vm = _mapper.Map<Order, OrderViewModel>(newOrder);

                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (System.Exception ex)
            {

                _logger.LogError($"Failed to add a new order: {ex}");
            }
            return BadRequest("Failed to add a new order");
        }
    }
}
