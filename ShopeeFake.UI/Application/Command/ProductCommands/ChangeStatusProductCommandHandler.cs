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
    public class ChangeStatusProductCommandHandler : IRequestHandler<ChangeStatusProductCommand, Response<ResponsDefault>>
    {
        private readonly IProductRepository _productRepository;
        public ChangeStatusProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(ChangeStatusProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _productRepository.Products.FirstOrDefault(x => x.Id == request.ProductId);
            if (product == null)
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
            product.isHidden =!product.isHidden;
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
                        Data = "Change status product success"
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
