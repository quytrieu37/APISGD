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
    public class EditCategoryCommandHandle : IRequestHandler<EditCategoryCommand, Response<ResponsDefault>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public EditCategoryCommandHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = _categoryRepository.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
            if(category== null)
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
            Category cateExist = _categoryRepository.Categories.FirstOrDefault(x => x.CategoryName == request.CategoryName);
            if (cateExist != null)
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
            category.CategoryName = request.CategoryName;
            _categoryRepository.EditCategory(category);
            int update = await _categoryRepository.unitOfWork.SaveAsync();
            if(update>0)
            {
                return new Response<ResponsDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    @object = new ResponsDefault()
                    {
                        Data = "edit category success"
                    }
                };
            }
            return new Response<ResponsDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                @object = new ResponsDefault()
                {
                    Data = "Error Excute DB"
                }
            };
        }
    }
}
