using TakeHome.Models;

namespace TakeHome.Service
{
    public interface IPatientService
    {
        PatientModel Create(PatientModel patientModel);
        PatientModel GetById(int id);
        IEnumerable<PatientModel> Search(string? firstName, string? lastName, DateTime? dob);
        PatientModel Update(int id, PatientModel dto);
    }
}
