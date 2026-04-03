using TakeHome.Models;
using TakeHome.Service;

namespace TakeHome.Tests
{
    [TestClass]
    public sealed class PatientTest
    {
        private readonly PatientService _service = new PatientService();

        [TestMethod]
        public void AddPatientTest()
        {
            var dto = new PatientModel
            {
                FirstName = "Dave",
                LastName = "Test",
                DateOfBirth = new DateOnly(2000, 1, 1),
                Email = "davetest@foo.com"
            };

            var result = _service.Create(dto);

            Assert.IsNotNull(result);
            Assert.AreEqual("Dave", result.FirstName);
        }

        [TestMethod]
        public void GuardianRequiredTest()
        {
            var dto = new PatientModel
            {
                FirstName = "Little Dave",
                LastName = "Test",
                DateOfBirth = new DateOnly(2015, 1, 1),
                Email = "littledave@foo.com"
            };

            Assert.ThrowsException<ArgumentException>(() => _service.Create(dto));
        }
    }
}
