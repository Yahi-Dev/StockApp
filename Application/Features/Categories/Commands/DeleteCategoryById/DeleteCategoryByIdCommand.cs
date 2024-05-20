using MediatR;
using StockApp.Core.Application.Exceptions;
using StockApp.Core.Application.Interfaces.Repositories;
using StockApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Commands.DeleteCategoryById
{
    /// <summary>
    /// Parámetros para la eliminacion de una categoria
    /// </summary> 
    public class DeleteCategoryByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id de la categoria que se desea eliminar")]
        public int Id { get; set; }
    }
    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand, Response<int>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public DeleteCategoryByIdCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Response<int>> Handle(DeleteCategoryByIdCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(command.Id);
            if (category == null) throw new ApiException("Category not found", (int)HttpStatusCode.NotFound);
            await _categoryRepository.DeleteAsync(category);
            return new Response<int>(category.Id);
        }
    }
}
