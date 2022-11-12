using System.Collections.Generic;

namespace BudgetService
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}