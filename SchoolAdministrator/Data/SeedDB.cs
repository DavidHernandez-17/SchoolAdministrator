using SchoolAdministrator.Data.Entities;
using SchoolAdministrator.Enums;
using SchoolAdministrator.Helpers;

namespace SchoolAdministrator.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckInstitutionsAsync();
            await CheckSubjectsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Cédula", "2020", "David", "Hernandez", "21", "davidh@yopmail.com", "301 796 6824", UserType.Admin);
            await CheckUserAsync("Cédula", "1231", "Javier", "Goméz", "34", "javierg@yopmail.com", "301 796 3421", UserType.User);

        }

        private async Task<User> CheckUserAsync(
            string documenttype,
            string document,
            string firstName,
            string lastName,
            string age,
            string email,
            string phone,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    DocumentType = documenttype,
                    Document = document,
                    Age = age,
                    Institution = _context.Institutions.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckSubjectsAsync()
        {
            if (!_context.Subjects.Any())
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
            if (!_context.Institutions.Any())
            {
                _context.Institutions.Add(new Institution
                {
                    Name = "Salazar y herrera",
                    InstitutionType = "Privada",
                    Levels = new List<Level>()
                    {
                        new Level()
                        {
                            Name = "6",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "7",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "8",
                            Type = "A"
                        }
                    }
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "Samuel Barrientos",
                    InstitutionType = "Pública",
                    Levels = new List<Level>()
                    {
                        new Level()
                        {
                            Name = "6",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "7",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "8",
                            Type = "A"
                        }
                    }
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "La divisa",
                    InstitutionType = "Pública",
                    Levels = new List<Level>()
                    {
                        new Level()
                        {
                            Name = "6",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "7",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "8",
                            Type = "A"
                        }
                    }
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "Lola Gonzales",
                    InstitutionType = "Pública",
                    Levels = new List<Level>()
                    {
                        new Level()
                        {
                            Name = "6",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "7",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "8",
                            Type = "A"
                        }
                    }
                });
                _context.Institutions.Add(new Institution
                {
                    Name = "Stela Vélez",
                    InstitutionType = "Privada",
                    Levels = new List<Level>()
                    {
                        new Level()
                        {
                            Name = "6",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "7",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "8",
                            Type = "A"
                        }
                    }
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
