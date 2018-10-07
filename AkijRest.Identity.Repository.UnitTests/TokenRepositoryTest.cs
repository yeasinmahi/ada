using System;
using AkijRest.IdentityServer.Repository.Repositories;
using NUnit.Framework;

namespace AkijRest.Identity.Repository.UnitTests
{
    [TestFixture]
    public class TokenRepositoryTest
    {
        private TokenRepository _obj;
        [TestCase("yeasir")]
        public void InsertToken_InsertToken_ReturnInsertedTokenId(string userName)
        {
            string tokenContent = "test";
            int insertedTokenId = _obj.InsertToken(userName, tokenContent);
            Assert.That(insertedTokenId, Is.GreaterThan(0));
        }
        [TestCase("yeasir")]
        public void TokenExists_TokenExists_ReturnBool(string userName)
        {
            try
            {
                _obj.TokenExists(userName);
                Assert.That(true);
            }
            catch (Exception e)
            {
                Assert.That(false);
            }
        }
        [SetUp]
        public void Setup()
        {
            _obj = new TokenRepository();
        }
        [TearDown]
        public void TearDownTest()
        {
            _obj = null;
        }
    }
}
