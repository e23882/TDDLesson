using System;
using System.Linq;
using BudgetService.Interface;

namespace BudgetService.Service
{
    public class BudgetService
    {
        #region Declarations

        private readonly IBudgetRepo _repo;

        #endregion

        #region MemberFunction

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="repo"></param>
        public BudgetService(IBudgetRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 查詢符合日期的預算
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <returns></returns>
        public decimal Query(DateTime startDateTime, DateTime endDateTime)
        {
            if (endDateTime < startDateTime)
            {
                return 0;
            }

            if (startDateTime.ToString("yyyyMM") == endDateTime.ToString("yyyyMM"))
            {
                int days = (endDateTime - startDateTime).Days + 1;
                int monthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
                int amount = _repo.GetAll()
                    .Where(x => x.YearMonth == startDateTime.ToString("yyyyMM"))
                    .Sum(x => x.Amount);

                return amount / monthDays * days;
            }

            int startMonthDays = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
            int startDays = startMonthDays - startDateTime.Day + 1;
            int startAmount = _repo.GetAll()
                .Where(x => x.YearMonth == startDateTime.ToString("yyyyMM"))
                .Sum(x => x.Amount) / startMonthDays * startDays;

            int endMonthDays = DateTime.DaysInMonth(endDateTime.Year, endDateTime.Month);
            int endAmount = _repo.GetAll()
                .Where(x => x.YearMonth == endDateTime.ToString("yyyyMM"))
                .Sum(x => x.Amount) / endMonthDays * endDateTime.Day;

            if (startDateTime.AddMonths(1).ToString("yyyyMM") == endDateTime.ToString("yyyyMM"))
            {
                return startAmount + endAmount;
            }

            int multiAmount = _repo.GetAll()
                .Where(x => int.Parse(x.YearMonth) > int.Parse(startDateTime.ToString("yyyyMM"))
                            && int.Parse(x.YearMonth) < int.Parse(endDateTime.ToString("yyyyMM")))
                .Sum(x => x.Amount);
            return startAmount + endAmount + multiAmount;
        }

        #endregion
    }
}