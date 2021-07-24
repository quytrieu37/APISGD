using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Application.Command.CategoryCommands;
using ShopeeFake.UI.Infrastructure;
using ShopeeFake.UI.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStoreQueries _storeQueries;
        private readonly IMediator _mediator;
        public CategoryController(ICategoryRepository categoryRepository,
            IStoreQueries storeQueries,
            IMediator mediator)
        {
            _categoryRepository = categoryRepository;
            _storeQueries = storeQueries;
            _mediator = mediator;
        }
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-category")]
        public async Task<IActionResult> ListCategory()
        {
            List<Category> categories = await _storeQueries.GetAllCategories();
            return Ok(categories);
        }
        [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-category-byId")]
        public async Task<IActionResult> GetCategoryById([FromQuery]int categoryId)
        {
            Category category = await _storeQueries.GetCategoryById(categoryId);
            if(category!=null)
            {
                return Ok(category);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [HttpPost]
        [Route("add-category")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddCategory(AddCategoryCommand command)
        {
            if(command == null)
            {
                return BadRequest();
            }
            Response<ResponsDefault> result =await _mediator.Send(command);
            if(result.State == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status409Conflict)]
        [HttpPut]
        [Route("edit-category")]
        public async Task<IActionResult> EditCategory(EditCategoryCommand command)
        {
            if(command==null)
            {
                return BadRequest();
            }
            Response<ResponsDefault> result = await _mediator.Send(command);
            if(result.State==true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete]
        [Route("delete-category")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory([FromQuery]DeleteCategoryCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            Response<ResponsDefault> result = await _mediator.Send(command);
            if (result.State == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
