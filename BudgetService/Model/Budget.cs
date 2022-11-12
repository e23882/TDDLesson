namespace BudgetService
{
    public class Budget
    {
        #region Property
        /// <summary>
        /// 年月 yyyyMM
        /// </summary>
        public string YearMonth { get; set; }
        /// <summary>
        /// 金額(不含負數)
        /// </summary>
        public int Amount { get; set; }
        #endregion
    }
}