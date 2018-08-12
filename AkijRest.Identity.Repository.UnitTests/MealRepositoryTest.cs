using System;
using AkijRest.IdentityServer.Repository.Repositories;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace AkijRest.Identity.Repository.UnitTests
{
    [TestFixture]
    public class MealRepositoryTest
    {
        private MealRepository _obj;
        [Test]
        public void Get_GetAllMeal_ReturnListOfMeal()
        {
            try
            {
                _obj.Get();
                Assert.That(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Get_GetMealById_ReturnMeal(int id)
        {
            try
            {
                _obj.Get(id);
                Assert.That(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestCase("Saturday")]
        [TestCase("MonDay")]
        [TestCase("Tuesday")]
        public void GetByDay_GetMealByDay_ReturnMeal(string day)
        {
            try
            {
                _obj.GetByDay(day);
                Assert.That(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        
        [SetUp]
        public void Setup()
        {
            _obj = new MealRepository();
        }
        [TearDown]
        public void TearDownTest()
        {
            _obj = null;
        }
    }
}
