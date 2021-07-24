using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.ProductCommands
{
    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, Response<ResponsDefault>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ICategoryRepository _categoryRepository;
        public EditProductCommandHandler(IProductRepository productRepository,
            IStoreRepository storeRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _storeRepository = storeRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _productRepository.Products.FirstOrDefault(x => x.Id == request.Id);
            if(product==null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    @object = new ResponsDefault()
                    {
                        Data = "Product Identify is not exist"
                    }
                };
            }
            if (request.ProductName.Trim() == "")
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotEmpty,
                    @object = new ResponsDefault()
                    {
                        Data = "product name is required"
                    }
                };
            }
            Store store = _storeRepository.Stores.FirstOrDefault(x => x.Id == request.StoreId);
            Category cate = _categoryRepository.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
            if (store == null || cate == null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    @object = new ResponsDefault()
                    {
                        Data = "category or store identify is not Exist"
                    }
                };
            }

            product.CategoryId = request.CategoryId;
            product.Amount = request.Amount;
            product.Description = request.Description;
            product.isHidden = request.isHidden;
            product.Price = request.Price;
            product.ProductName = request.ProductName;
            product.StoreId = request.StoreId;

            _productRepository.EditProduct(product);
            int update = await _productRepository.unitOfWork.SaveAsync();
            if (update > 0)
            {
                return new Response<ResponsDefault>()
                {
                    Message = ErrorCode.Success,
                    State = true,
                    @object = new ResponsDefault()
                    {
                        Data = "Edit product success"
                    }
                };
            }
            return new Response<ResponsDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                @object = new ResponsDefault()
                {
                    Data = "excute database error"
                }
            };
        }
    }
}
