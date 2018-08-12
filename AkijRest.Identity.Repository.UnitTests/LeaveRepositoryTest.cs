using System;
using System.Collections.Generic;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Repositories;
using NUnit.Framework;

namespace AkijRest.Identity.Repository.UnitTests
{
    [TestFixture]
    public class LeaveRepositoryTest
    {
        private LeaveRepository _obj;

        [Test]
        public void Get_GetAllLeave_ReturnListOfLeave()
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
        public void Get_GetLeaveById_ReturnLeave(int id)
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
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(4)]
        public void GetLeaveByUserId_GetLeaveByUserId_ReturnListOfLeave(int id)
        {
            try
            {
                _obj.GetLeaveByUserId(id);
                Assert.That(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestCase("yeasir")]
        [TestCase("debkanti")]
        [TestCase("konock")]
        public void GetLeaveByUserName_GetLeaveByUserName_ReturnListOfLeave(string userName)
        {
            try
            {
                _obj.GetLeaveByUserName(userName);
                Assert.That(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [Test]
        public void Create_CreateNewLeave_ReturnId()
        {
            //Arrange
            LeaveDto leaveDto = new LeaveDto
            {
                Id = 100,
                LeaveTypeId = 1,
                UserId = 1,
                DateStart = "01/01/2018",
                DateEnd = "01/01/2018",
                LeaveAddress = "Unit Test",
                LeaveCause = "Unit Test"
            };

            //Act
            List<int> createResults = _obj.Create(leaveDto);
            foreach (int result in createResults)
            {
                leaveDto.Id = result;
                leaveDto.LeaveCause = "Update Test";
                int updateResult = _obj.Update(leaveDto);
                int deleteResult = _obj.Delete(result);
                Assert.That(result, Is.GreaterThan(0));
                Assert.That(updateResult, Is.GreaterThan(0));
                Assert.That(deleteResult, Is.GreaterThan(0));
            }
            
            //Asert
            

        }
        [SetUp]
        public void Setup()
        {
            _obj = new LeaveRepository();
        }
        [TearDown]
        public void TearDownTest()
        {
            _obj = null;
        }
    }
}
