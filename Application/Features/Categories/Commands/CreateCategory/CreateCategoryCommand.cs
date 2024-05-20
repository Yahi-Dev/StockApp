using AutoMapper;
using MediatR;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Wrappers;
using StockApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace StockApp.Core.Application.Features.Categories.Commands.CreateCategory
{
    /// <summary>
    /// Parámetros para la creacion de una categoria
    /// </summary>  
    public class CreateCategoryCommand : IRequest<Response<int>>
    {
        /// <example>Consolas</example>
        [SwaggerParameter(Description = "El nombre de la categoria")]
        public string Name { get; set; }

        /// <example>Dispositivos electronicos de entretenimiento</example>
        [SwaggerParameter(Description = "La descripcion de la categoria")]
        public string? Description { get; set; }
    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _categoryRepository.AddAsync(category);
            return new Response<int>(category.Id);
        }
    }
}
