using System;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using NUnit.Framework;

namespace AkijRest.Identity.Repository.UnitTests
{
    [TestFixture]
    public class LeaveTypeRepositoryTest
    {

        [Test]
        public void Get_GetAllLeaveType_ReturnListOfLeaveType()
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
        public void Get_GetLeaveTypeById_ReturnListOfLeaveType(int id)
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
        [Test]
        public void Create_CreateNewLeaveType_ReturnId()
        {
            //Arrange
            LeaveTypeDto leaveTypeDto = new LeaveTypeDto
            {
                Id = 100,
                Name = "Test",
                ApplicableFor = "B",
                CompanyPolicy = 20,
                MaxApplicationAtAMonth = 3,
                MaximumAllowedAtATime = 3,
                IsBalanceChecked = true,
                IsHalfDayAllowed = false,
                IsOnlyOneTime = true,
                IsRestricted = false
            };

            //Act
            int result = _obj.Create(leaveTypeDto);
            //Asert
            Assert.That(result,Is.GreaterThan(0));

        }


        private LeaveTypeRepository _obj;
        [SetUp]
        public void Setup()
        {
            _obj = new LeaveTypeRepository();
        }
        [TearDown]
        public void TearDownTest()
        {
            _obj = null;
        }

    }
}
