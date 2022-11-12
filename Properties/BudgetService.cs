using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetService
{
    public class BudgetService
    {
        private readonly IBudgetRepo repo;

        public BudgetService(IBudgetRepo repo)
        {
            this.repo = repo;
        }

        public decimal Query(string startDate, string endDate)
        {
            decimal result = 0;
            DateTime startDateTime = new DateTime(
                int.Parse(startDate.Substring(0, 4)),
                int.Parse(startDate.Substring(4, 2)),
                int.Parse(startDate.Substring(6, 2)));
            DateTime endDateTime = new DateTime(
                int.Parse(endDate.Substring(0, 4)),
                int.Parse(endDate.Substring(4, 2)),
                int.Parse(endDate.Substring(6, 2)));
            if(endDateTime > startDateTime) 
            {
                return 0;
            }


            return result;
            //repo.GetAll().Where(x=>x.YearMonth )
        }
    }
}
