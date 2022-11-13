using System.Collections.Generic;
using BudgetService.Model;

namespace BudgetService.Interface
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}