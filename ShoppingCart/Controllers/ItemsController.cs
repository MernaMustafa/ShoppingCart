using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Filters;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interfaces;


namespace ShoppingCart.Controllers

{
    [EnableCors("ApiCorsPolicy")]
    //[AllowCrossSiteJson]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<Item> _itemRepository;
        public ItemsController(IUnitOfWork unitOfWork, IBaseRepository<Item> itemRepository)
        {
            _unitOfWork = unitOfWork;
            _itemRepository = itemRepository;
        }
        // GET: api/Items
        //[AllowCrossSiteJson]
        [Authorize(Roles = "admin, customer")]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _itemRepository.ListAllAsync();

                if (items == null)
                {
                    return NotFound();
                }

                return Ok(items);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: api/Items/itemId
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {

            try
            {
                var item = await _itemRepository.GetByIdAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {

            try
            {
                var addedItem = await _itemRepository.AddAsync(item);
                int result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    return Ok(addedItem);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] Item item)
        {
            try
            {
                 _itemRepository.UpdateAsync(item);
                int result = await _unitOfWork.Complete();
                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var item = await _itemRepository.GetByIdAsync(id);
                _itemRepository.DeleteAsync(item);
                int result = await _unitOfWork.Complete();
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}