using AutoMapper;
using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Wrappers;
using StockApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Commands.UpdateCategory
{
    /// <summary>
    /// Parámetros para la actualizacion de una categoria
    /// </summary> 
    public class UpdateCategoryCommand : IRequest<Response<CategoryUpdateResponse>>
    {
        [SwaggerParameter(Description = "El id de la categoria que se quiere actualizar")]
        public int Id { get; set; }

        /// <example>Consolas</example>
        [SwaggerParameter(Description = "El nuevo nombre de la categoria")]
        public string Name { get; set; }

        /// <example>Dispositivos electronicos de entretenimiento</example>
        [SwaggerParameter(Description = "La nueva descripcion de la categoria")]
        public string? Description { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<CategoryUpdateResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<CategoryUpdateResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new ApiException("Category not found", (int)HttpStatusCode.NotFound);
            }
            else
            {
                category = _mapper.Map<Category>(command);
                await _categoryRepository.UpdateAsync(category, category.Id);
                var categoryVm = _mapper.Map<CategoryUpdateResponse>(category);
                
                return new Response<CategoryUpdateResponse>(categoryVm);
            }
        }
    }
}
