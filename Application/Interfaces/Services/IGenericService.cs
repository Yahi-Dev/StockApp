using StockApp.Core.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Model> 
        where SaveViewModel : class 
        where ViewModel : class
        where Model : class
    {
        Task Update(SaveViewModel viewModel, int id);
        Task<SaveViewModel> Add(SaveViewModel viewModel);
        Task Delete(int id);
        Task<SaveViewModel> GetByIdSaveViewModel(int id);
        Task<List<ViewModel>> GetAllViewModel();
    }
}
