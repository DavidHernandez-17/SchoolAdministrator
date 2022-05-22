using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data.Entities;
using SchoolAdministrator.Enums;
using SchoolAdministrator.Helpers;

namespace SchoolAdministrator.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDB(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckInstitutionsAsync();
            await CheckSubjectsAsync();
            await CheckInscriptionsAsync();
            await CheckRolesAsync();
            await CheckProductsAsync();
            await CheckUserAsync("CC", "2020", "David", "Hernandez", "21", "davidh@yopmail.com", "301 796 6824", "DavidHernandez.jpg", UserType.Admin);
            await CheckUserAsync("CC", "1231", "Daye", "Ruiz", "22", "daye22@yopmail.com", "301 796 3421", "Daye.jpg", UserType.User);
            await CheckUserAsync("CC", "1231", "Ana", "Perez", "20", "anaperez20@yopmail.com", "320 435 3904", "Ana.jpg", UserType.User);

        }

        private async Task CheckInscriptionsAsync()
        {
            if(!_context.Inscriptions.Any())
            {
                _context.Inscriptions.Add(new Inscription { PeriodAcedemic = "2022" });
                _context.Inscriptions.Add(new Inscription { PeriodAcedemic = "2023" });
                _context.Inscriptions.Add(new Inscription { PeriodAcedemic = "2024" });
                _context.Inscriptions.Add(new Inscription { PeriodAcedemic = "2025" });
            }
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Borrador", 500M, 25F, new List<string>() { "Física", "Sociales" }, new List<string>() { "Borrador.jpg" });
                await AddProductAsync("Colbon", 1300M, 10F, new List<string>() { "Matemáticas", "Español" }, new List<string>() { "colbon.jpg" });
                await AddProductAsync("Cuaderno", 2100M, 15F, new List<string>() { "Inglés", "Español" }, new List<string>() { "Cuaderno.jpg" });
                await AddProductAsync("Lapiz", 800M, 40F, new List<string>() { "Matemáticas", "Español" }, new List<string>() { "Lapiz.jpg" });
                await AddProductAsync("Uniforme Mujer", 43000M, 50F, new List<string>() { "Religión", "Español" }, new List<string>() { "Uniforme.jpg" });
                await AddProductAsync("Bolso escolar", 65000M, 12F, new List<string>() { "Sociales", "Español", "Inglés", "Matemáticas", "Religión", "Física" }, new List<string>() { "bolsoescolar.jpg" });
                await AddProductAsync("Caja 12 lapiceros", 13000M, 20F, new List<string>() { "Matemáticas", "Español" }, new List<string>() { "Caja12lapicerospilot.jpg" });
                await AddProductAsync("Cartuchera hombre", 30000M, 12F, new List<string>() { "Inglés", "Español" }, new List<string>() { "cartucherahombre.jpg" });
                await AddProductAsync("Cartuchera mujer", 34000M, 12F, new List<string>() { "Física", "Español" }, new List<string>() { "cartucheramuejr.png" });
                await AddProductAsync("Tijeras", 2500M, 32F, new List<string>() { "Religión", "Español" }, new List<string>() { "tijeras.jpg" });
                await AddProductAsync("Sacapuntas", 600M, 60F, new List<string>() { "Matemáticas", "Física", "Español" }, new List<string>() { "sacapuntas.jpg" });
                await AddProductAsync("Regla metálica", 1000M, 25F, new List<string>() { "Matemáticas", "Física" }, new List<string>() { "reglametalica.jpg" });

            }
        }

        private async Task AddProductAsync(string name, decimal price, float stock, List<string> subjects, List<string> images)
        {
            Product product = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            foreach (string? subject in subjects)
            {
                product.ProductCategories.Add(new ProductCategory { Subject = await _context.Subjects.FirstOrDefaultAsync(c => c.Name == subject) });
            }


            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\products\\{image}", "products");
                product.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(product);
        }


        private async Task<User> CheckUserAsync(
            string documenttype,
            string document,
            string firstName,
            string lastName,
            string age,
            string email,
            string phone,
            string image,
            UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");
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
                    ImageId = imageId,
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

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
                _context.Subjects.Add(new Subject
                {
                    Name = "Religión",
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
                        },
                        new Level()
                        {
                            Name = "9",
                            Type = "B"
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
                _context.Institutions.Add(new Institution
                {
                    Name = "Colombo Americano",
                    InstitutionType = "Privada",
                    Levels = new List<Level>()
                    {
                        new Level()
                        {
                            Name = "10",
                            Type = "A"
                        },
                        new Level()
                        {
                            Name = "9",
                            Type = "B"
                        },
                        new Level()
                        {
                            Name = "11",
                            Type = "A"
                        }
                    }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
