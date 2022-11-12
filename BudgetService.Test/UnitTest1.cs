using NSubstitute;
using NUnit.Framework;

namespace BudgetService
{
    public class Tests
    {
        #region Declarations
        BudgetService _service = null;
        private IBudgetRepo _repo = null;
        #endregion

        #region MemberFunction
        [SetUp]
        public void Setup()
        {
            _repo = Substitute.For<IBudgetRepo>();
            _service = new BudgetService(_repo);
        }

        [TestCase]
        public void TestGetBudgetError()
        {
            _repo.GetAll().Returns(new System.Collections.Generic.List<Budget>() {
                new Budget(){YearMonth = "202205", Amount = 31},
                new Budget(){YearMonth = "202204", Amount = 30},
                new Budget(){YearMonth = "202202", Amount = 28},
                new Budget(){YearMonth = "202201", Amount = 31},
            });
            decimal amount = _service.Query("20220501", "20220101");
            Assert.Zero(amount);
        }

        [TestCase]
        public void TestGetBudgetSignalMonth()
        {
            _repo.GetAll().Returns(new System.Collections.Generic.List<Budget>() {
                new Budget(){YearMonth = "202205", Amount = 31},
                new Budget(){YearMonth = "202204", Amount = 30},
                new Budget(){YearMonth = "202202", Amount = 28},
                new Budget(){YearMonth = "202201", Amount = 31},
            });
            decimal amount = _service.Query("20220301", "20220318");
            Assert.Zero(amount);
        }

        [TestCase]
        public void TestGetBudgetSameMonth() {
            _repo.GetAll().Returns(new System.Collections.Generic.List<Budget>() {
                new Budget(){YearMonth = "202205", Amount = 31},
                new Budget(){YearMonth = "202204", Amount = 30},
                new Budget(){YearMonth = "202202", Amount = 28},
                new Budget(){YearMonth = "202201", Amount = 31},
            });
            decimal amount = _service.Query("20220101", "20220131");
            Assert.AreEqual(amount, 31);

        }
        [TestCase]
        public void TestGetBudgetCrossMonth() {
            _repo.GetAll().Returns(new System.Collections.Generic.List<Budget>() {
                new Budget(){YearMonth = "202205", Amount = 31},
                new Budget(){YearMonth = "202204", Amount = 30},
                new Budget(){YearMonth = "202202", Amount = 28},
                new Budget(){YearMonth = "202201", Amount = 31},
            });
            decimal amount = _service.Query("20220101", "20220228");
            Assert.AreEqual(amount, 59);
        }

        [TestCase]
        public void TestGetBudgetCrossMultiMonth()
        {
            _repo.GetAll().Returns(new System.Collections.Generic.List<Budget>() {
                new Budget(){YearMonth = "202205", Amount = 31},
                new Budget(){YearMonth = "202204", Amount = 30},
                new Budget(){YearMonth = "202202", Amount = 28},
                new Budget(){YearMonth = "202201", Amount = 31},
            });
            decimal amount = _service.Query("20220201", "20220430");
            Assert.AreEqual(amount, 58);
        }

        [Test]
        public void TestGetBudgetCrossMultiMonth_test()
        {
            _repo.GetAll().Returns(new System.Collections.Generic.List<Budget>() {
                new Budget(){YearMonth = "202203", Amount = 31},
                new Budget(){YearMonth = "202204", Amount = 30},
                new Budget(){YearMonth = "202202", Amount = 28},
                new Budget(){YearMonth = "202201", Amount = 31},
            });
            decimal amount = _service.Query("20220228", "20220410");
            Assert.AreEqual(amount, 1+31+10);
        }
        #endregion
    }
}