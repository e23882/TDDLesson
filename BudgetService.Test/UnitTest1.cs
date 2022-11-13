using System;
using BudgetService.Interface;
using BudgetService.Model;
using NSubstitute;
using NUnit.Framework;

namespace BudgetService.Test
{
    public class Tests
    {
        #region Declarations
        Service.BudgetService _service = null;
        private IBudgetRepo _repo = null;
        #endregion

        #region MemberFunction
        [SetUp]
        public void Setup()
        {
            _repo = Substitute.For<IBudgetRepo>();
            _service = new Service.BudgetService(_repo);
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
            decimal amount = _service.Query(
                new DateTime(2022, 5, 1), 
                new DateTime(2022, 1, 1));
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
            decimal amount = _service.Query(
                new DateTime(2022, 3, 1), 
                new DateTime(2022, 3, 18));
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
            decimal amount = _service.Query(
                new DateTime(2022, 1, 1), 
                new DateTime(2022, 1, 31));
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
            decimal amount = _service.Query(
                new DateTime(2022, 1, 1), 
                new DateTime(2022, 2, 28));
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
            decimal amount = _service.Query(
                new DateTime(2022, 2, 1), 
                new DateTime(2022, 4, 30));
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
            decimal amount = _service.Query(
                new DateTime(2022, 2, 28), 
                new DateTime(2022, 4, 10));
            Assert.AreEqual(amount, 1+31+10);
        }
        #endregion
    }
}