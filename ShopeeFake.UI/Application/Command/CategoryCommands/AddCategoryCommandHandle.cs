using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;

namespace ShopeeFake.UI.Application.Command.CategoryCommands
{
    public class AddCategoryCommandHandle : IRequestHandler<AddCategoryCommand, Response<ResponsDefault>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public AddCategoryCommandHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            Category cateExist = _categoryRepository.Categories.FirstOrDefault(x => x.CategoryName == request.CategoryName);
            if(cateExist!=null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExistStore,
                    @object = new ResponsDefault()
                    {
                        Data = "category name is already exist"
                    }
                };
            }    
            Category category = new Category()
            {
                CategoryName = request.CategoryName
            };
            _categoryRepository.AddCategory(category);
            int update = await _categoryRepository.unitOfWork.SaveAsync();
            if(update>0)
            {
                return new Response<ResponsDefault>()
                {
                    Message = ErrorCode.Success,
                    State = true,
                    @object = new ResponsDefault()
                    {
                        Data = "Add Category success"
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
