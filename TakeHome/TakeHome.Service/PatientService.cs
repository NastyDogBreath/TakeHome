using TakeHome.Models;

namespace TakeHome.Service
{
    public class PatientService : IPatientService
    {
        private IList<PatientModel> _patients = new List<PatientModel>();

        public PatientModel Create(PatientModel dto)
        {
            var age = CalculateAge(dto.DateOfBirth);

            if (age < 18 && dto.Guardian == null)
                throw new ArgumentException("Guardian is required for minors.");

            var patient = new PatientModel
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Guardian = dto.Guardian
            };

            _patients.Add(patient);
            return patient;
        }

        public PatientModel? GetById(int id)
        {   var patient = _patients.FirstOrDefault(p => p.Id == id);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");

            return patient;
        }

        public IEnumerable<PatientModel> Search(string? firstName, string? lastName, DateTime? dob)
        {
            var query = _patients.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(p =>
                    p.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(p =>
                    p.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));

            if (dob.HasValue)
            {
                var dobDateOnly = DateOnly.FromDateTime(dob.Value);
                query = query.Where(p => p.DateOfBirth == dobDateOnly);
            }

            return query.ToList();
        }

        public PatientModel Update(int id, PatientModel dto)
        {
            var patient = GetById(id);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found.");
            patient.FirstName = dto.FirstName ?? patient.FirstName;
            patient.LastName = dto.LastName ?? patient.LastName;
            patient.Email = dto.Email ?? patient.Email;

            return patient;
        }

        private int CalculateAge(DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }
}
