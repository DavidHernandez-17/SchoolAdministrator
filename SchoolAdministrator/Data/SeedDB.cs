using SchoolAdministrator.Data.Entities;

namespace SchoolAdministrator.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckInstitutionsAsync();
            await CheckSubjectsAsync();
        }

        private async Task CheckSubjectsAsync()
        {
            if(!_context.Subjects.Any())
            {
                _context.Subjects.Add(new Subject
                {
                    Name = "Física",
                    CategorySubject = "Ciencias exactas"
                });
                _context.Subjects.Add(new Subject
                {
                    Name = "Sociales",
                    CategorySubject = "Humanidades"
                });
                _context.Subjects.Add(new Subject
                {
                    Name = "Español",
                    CategorySubject = "Humanidades"
                });
                _context.Subjects.Add(new Subject
                {
                    Name = "Matemáticas",
                    CategorySubject = "Ciencias exactas"
                });
                _context.Subjects.Add(new Subject
                {
                    Name = "Inglés",
                    CategorySubject = "Humanidades"
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckInstitutionsAsync()
        {
            if(!_context.Institutions.Any())
            {
                _context.Institutions.Add(new Institution
                { 
                    Name = "Salazar y herrera",
                    InstitutionType = "Privada"
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "Samuel Barrientos",
                    InstitutionType = "Pública"
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "La divisa",
                    InstitutionType = "Pública"
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "Lola Gonzales",
                    InstitutionType = "Pública"
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "Stela Vélez",
                    InstitutionType = "Privada"
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
