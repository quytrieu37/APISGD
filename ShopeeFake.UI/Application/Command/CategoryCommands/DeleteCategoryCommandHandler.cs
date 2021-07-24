using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.CategoryCommands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<ResponsDefault>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Product product = _productRepository.Products.FirstOrDefault(x => x.CategoryId == request.CategoryId);
            if(product !=null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.BadRequest,
                    @object = new ResponsDefault()
                    {
                        Data = "this category have product"
                    }
                };
            }
            Category category = _categoryRepository.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
            if (category == null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    @object = new ResponsDefault()
                    {
                        Data = "category not found"
                    }
                };
            }
            _categoryRepository.RemoveCategory(category);
            int update = await _categoryRepository.unitOfWork.SaveAsync();
            if(update >0)
            {
                return new Response<ResponsDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    @object = new ResponsDefault()
                    {
                        Data = "delete category success"
                    }
                };
            }
            return new Response<ResponsDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                @object = new ResponsDefault()
                {
                    Data = "Error database while try to delete Category"
                }
            };
        }
    }
}
