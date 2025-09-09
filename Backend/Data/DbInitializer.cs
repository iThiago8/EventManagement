using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            //context.Database.EnsureDeleted(); //Usado somente quando quero resetar o banco de dados

            context.Database.Migrate();

            // A ordem de inicialização é crucial para respeitar as chaves estrangeiras.
            // 1. Entidades sem dependências externas (Address, Subject, Person)
            // 2. Entidades que dependem do primeiro grupo (Symposium, Workshop, ScientificCommittee, Article)
            // 3. Tabelas de Junção e Relações (Muitos-para-Muitos)

            // ========================================================================
            // 1. ADDRESS (Nível 0 - Sem dependências)
            // ========================================================================
            if (!context.Address.Any())
            {
                var addresses = new Address[]
                {
                    new Address{Street = "Rua Araçatuba", Number = "202", Neighborhood = "SESI", City = "Videira", State = "SC", PostalCode = "89564346", Country = "Brasil"},
                    new Address{Street = "Rua Saul Brandalise", Number = "1120", Neighborhood = "Centro", City = "Videira", State = "SC", PostalCode = "89560150", Country = "Brasil"},
                    new Address{Street = "Avenida Dom Pedro II", Number = "580", Neighborhood = "Universitário", City = "Videira", State = "SC", PostalCode = "89566252", Country = "Brasil"},
                    new Address{Street = "Rua Brasil", Number = "850", Neighborhood = "Centro", City = "Videira", State = "SC", PostalCode = "89560126", Country = "Brasil"},
                    new Address{Street = "Rua XV de Novembro", Number = "1987", Neighborhood = "Matriz", City = "Videira", State = "SC", PostalCode = "89560410", Country = "Brasil"}
                };
                context.Address.AddRange(addresses);
                context.SaveChanges();
            }

            // ========================================================================
            // 2. SUBJECT (Nível 0 - Sem dependências)
            // ========================================================================
            if (!context.Subject.Any())
            {
                var subjects = new Subject[]
                {
                    new Subject{Name = "Inteligência Artificial"},
                    new Subject{Name = "Biotecnologia"},
                    new Subject{Name = "Desenvolvimento Sustentável"},
                    new Subject{Name = "História Contemporânea"},
                    new Subject{Name = "Computação Quântica"},
                    new Subject{Name = "Finanças Digitais"}
                };
                context.Subject.AddRange(subjects);
                context.SaveChanges();
            }

            // ========================================================================
            // 3. PERSON (Nível 0 - Sem dependências)
            // ========================================================================
            if (!context.Person.Any())
            {
                var people = new Person[]
                {
                    new Person{Cpf = "111.111.111-11", Name = "Dr. Carlos Andrade", Email = "carlos.andrade@email.com", PhoneNumber = "49911111111", BirthDate = new DateTime(1975, 5, 20)},
                    new Person{Cpf = "222.222.222-22", Name = "Dra. Beatriz Lima", Email = "beatriz.lima@email.com", PhoneNumber = "49922222222", BirthDate = new DateTime(1982, 9, 15)},
                    new Person{Cpf = "333.333.333-33", Name = "Pedro Mendes", Email = "pedro.mendes@email.com", PhoneNumber = "49933333333", BirthDate = new DateTime(1998, 2, 10)},
                    new Person{Cpf = "444.444.444-44", Name = "Juliana Costa", Email = "juliana.costa@email.com", PhoneNumber = "49944444444", BirthDate = new DateTime(2001, 11, 30)},
                    new Person{Cpf = "555.555.555-55", Name = "Dr. Ricardo Nunes", Email = "ricardo.nunes@email.com", PhoneNumber = "49955555555", BirthDate = new DateTime(1968, 7, 25)},
                    new Person{Cpf = "666.666.666-66", Name = "Mariana Fernandes", Email = "mariana.f@email.com", PhoneNumber = "49966666666", BirthDate = new DateTime(1995, 4, 18)},
                    new Person{Cpf = "777.777.777-77", Name = "Dra. Lúcia Pereira", Email = "lucia.pereira@email.com", PhoneNumber = "49977777777", BirthDate = new DateTime(1980, 1, 5)},
                    new Person{Cpf = "888.888.888-88", Name = "Fernando Gonçalves", Email = "fernando.g@email.com", PhoneNumber = "49988888888", BirthDate = new DateTime(1999, 8, 22)},
                };
                context.Person.AddRange(people);
                context.SaveChanges();
            }

            // ========================================================================
            // 4. SYMPOSIUM (Nível 1 - Depende de Address)
            // ========================================================================
            if (!context.Symposium.Any())
            {
                var addresses = context.Address.ToArray();
                var symposiums = new Symposium[]
                {
                    new Symposium{Name = "Simpósio de Tecnologia e Inovação", StartDate = new DateTime(2025, 10, 20), EndDate = new DateTime(2025, 10, 24), LocationAddressId = addresses[1].Id, Description = "O futuro da tecnologia em debate."},
                    new Symposium{Name = "Conferência de Biociências", StartDate = new DateTime(2025, 11, 5), EndDate = new DateTime(2025, 11, 7), LocationAddressId = addresses[2].Id, Description = "Avanços e desafios na área de biotecnologia."},
                    new Symposium{Name = "Encontro de Humanidades Digitais", StartDate = new DateTime(2026, 3, 15), EndDate = new DateTime(2026, 3, 18), LocationAddressId = addresses[0].Id, Description = "A interseção entre a história e a tecnologia."}
                };
                context.Symposium.AddRange(symposiums);
                context.SaveChanges();
            }

            // ========================================================================
            // 5. WORKSHOP (Nível 1 - Depende de Subject)
            // ========================================================================
            if (!context.Workshop.Any())
            {
                var subjects = context.Subject.ToArray();
                var workshops = new Workshop[]
                {
                    new Workshop{Name = "Introdução ao Machine Learning", Hours = 8, SubjectId = subjects.First(s => s.Name == "Inteligência Artificial").Id},
                    new Workshop{Name = "CRISPR-Cas9: Edição Genética na Prática", Hours = 12, SubjectId = subjects.First(s => s.Name == "Biotecnologia").Id},
                    new Workshop{Name = "Análise de Fontes Históricas com Python", Hours = 6, SubjectId = subjects.First(s => s.Name == "História Contemporânea").Id},
                    new Workshop{Name = "Blockchain e o Futuro das Finanças", Hours = 4, SubjectId = subjects.First(s => s.Name == "Finanças Digitais").Id}
                };
                context.Workshop.AddRange(workshops);
                context.SaveChanges();
            }

            // ========================================================================
            // 6. SCIENTIFIC COMMITTEE (Nível 1 - Depende de Subject)
            // ========================================================================
            if (!context.ScientificCommittee.Any())
            {
                var subjects = context.Subject.ToArray();
                var people = context.Person.ToArray();

                var committees = new ScientificCommittee[]
                {
                    new ScientificCommittee{Name = "Comitê de Avaliação de IA", SubjectId = subjects.First(s => s.Name == "Inteligência Artificial").Id},
                    new ScientificCommittee{Name = "Comitê de Ética em Biotecnologia", SubjectId = subjects.First(s => s.Name == "Biotecnologia").Id},
                    new ScientificCommittee{Name = "Comitê de Ciências Humanas", SubjectId = subjects.First(s => s.Name == "História Contemporânea").Id}
                };

                // Atribuindo pessoas aos comitês (Relação Muitos-para-Muitos)
                committees[0].Person.Add(people[0]); // Dr. Carlos
                committees[0].Person.Add(people[4]); // Dr. Ricardo
                committees[1].Person.Add(people[1]); // Dra. Beatriz
                committees[1].Person.Add(people[6]); // Dra. Lúcia
                committees[2].Person.Add(people[0]); // Dr. Carlos
                committees[2].Person.Add(people[6]); // Dra. Lúcia

                context.ScientificCommittee.AddRange(committees);
                context.SaveChanges();
            }

            // ========================================================================
            // 7. ARTICLE (Nível 1 - Depende de Subject)
            // ========================================================================
            if (!context.Article.Any())
            {
                var subjects = context.Subject.ToArray();
                var people = context.Person.ToArray();

                var articles = new Article[]
                {
                    new Article{Name = "Redes Neurais Convolucionais para Análise de Imagens Médicas", PublicationDate = new DateTime(2024, 5, 10), Abstract = "Este artigo explora...", SubjectId = subjects.First(s => s.Name == "Inteligência Artificial").Id},
                    new Article{Name = "Impacto da Terapia Gênica em Doenças Hereditárias", PublicationDate = new DateTime(2024, 8, 22), Abstract = "Uma revisão sobre os avanços...", SubjectId = subjects.First(s => s.Name == "Biotecnologia").Id},
                    new Article{Name = "A Revolução Digital e seus Efeitos na Sociedade do Século XXI", PublicationDate = new DateTime(2023, 11, 1), Abstract = "Análise histórica das transformações...", SubjectId = subjects.First(s => s.Name == "História Contemporânea").Id}
                };

                // Atribuindo autores aos artigos (Relação Muitos-para-Muitos)
                articles[0].Author.Add(people[0]); // Dr. Carlos
                articles[0].Author.Add(people[1]); // Dra. Beatriz
                articles[1].Author.Add(people[1]); // Dra. Beatriz
                articles[2].Author.Add(people[4]); // Dr. Ricardo
                articles[2].Author.Add(people[6]); // Dra. Lúcia

                context.Article.AddRange(articles);
                context.SaveChanges();
            }

            // ========================================================================
            // 8. ARTICLE REVIEW (Nível 2 - Depende de Article e ScientificCommittee)
            // ========================================================================
            if (!context.ArticleReview.Any())
            {
                var articles = context.Article.Include(a => a.Subject).ToArray();
                var committees = context.ScientificCommittee.Include(c => c.Subject).ToArray();

                var reviews = new ArticleReview[]
                {
                    new ArticleReview{ArticleId = articles[0].Id, ScientificCommitteeId = committees.First(c => c.SubjectId == articles[0].SubjectId).Id, Grade = 4.8f, Review = "Trabalho sólido com excelente metodologia.", ReviewDate = new DateTime(2024, 6, 15)},
                    new ArticleReview{ArticleId = articles[1].Id, ScientificCommitteeId = committees.First(c => c.SubjectId == articles[1].SubjectId).Id, Grade = 4.5f, Review = "Revisão abrangente e bem fundamentada.", ReviewDate = new DateTime(2024, 9, 30)},
                    new ArticleReview{ArticleId = articles[2].Id, ScientificCommitteeId = committees.First(c => c.SubjectId == articles[2].SubjectId).Id, Grade = 4.2f, Review = "Análise pertinente, mas poderia aprofundar a discussão sobre os impactos sociais.", ReviewDate = new DateTime(2023, 12, 10)}
                };
                context.ArticleReview.AddRange(reviews);
                context.SaveChanges();
            }

            // ========================================================================
            // 9. WORKSHOP SYMPOSIUM (Nível 2 - Tabela de Junção)
            // ========================================================================
            if (!context.WorkshopSymposium.Any())
            {
                var workshops = context.Workshop.ToArray();
                var symposiums = context.Symposium.ToArray();

                var workshopSymposiums = new WorkshopSymposium[]
                {
                    // Workshops do Simpósio de Tecnologia
                    new WorkshopSymposium { WorkshopId = workshops[0].Id, SymposiumId = symposiums[0].Id, StartDate = new DateTime(2025, 10, 21, 9, 0, 0), EndDate = new DateTime(2025, 10, 21, 17, 0, 0), MaxParticipants = 30 },
                    new WorkshopSymposium { WorkshopId = workshops[3].Id, SymposiumId = symposiums[0].Id, StartDate = new DateTime(2025, 10, 22, 14, 0, 0), EndDate = new DateTime(2025, 10, 22, 18, 0, 0), MaxParticipants = 50 },
                    // Workshop da Conferência de Biociências
                    new WorkshopSymposium { WorkshopId = workshops[1].Id, SymposiumId = symposiums[1].Id, StartDate = new DateTime(2025, 11, 6, 8, 0, 0), EndDate = new DateTime(2025, 11, 6, 20, 0, 0), MaxParticipants = 20 },
                    // Workshop do Encontro de Humanidades
                    new WorkshopSymposium { WorkshopId = workshops[2].Id, SymposiumId = symposiums[2].Id, StartDate = new DateTime(2026, 3, 16, 9, 0, 0), EndDate = new DateTime(2026, 3, 16, 15, 0, 0), MaxParticipants = 25 },
                };
                context.WorkshopSymposium.AddRange(workshopSymposiums);
                context.SaveChanges();
            }


            // ========================================================================
            // 10. PERSON SYMPOSIUM (Nível 2 - Tabela de Junção)
            // ========================================================================
            if (!context.PersonSymposium.Any())
            {
                var people = context.Person.ToArray();
                var symposiums = context.Symposium.ToArray();

                var personSymposiums = new PersonSymposium[]
                {
                    // Organizadores
                    new PersonSymposium { PersonId = people[0].Id, SymposiumId = symposiums[0].Id, Role = "Organizador" },
                    new PersonSymposium { PersonId = people[1].Id, SymposiumId = symposiums[1].Id, Role = "Organizador" },
                    // Participantes
                    new PersonSymposium { PersonId = people[2].Id, SymposiumId = symposiums[0].Id, Role = "Participante" },
                    new PersonSymposium { PersonId = people[3].Id, SymposiumId = symposiums[0].Id, Role = "Participante" },
                    new PersonSymposium { PersonId = people[5].Id, SymposiumId = symposiums[1].Id, Role = "Participante" },
                    new PersonSymposium { PersonId = people[7].Id, SymposiumId = symposiums[2].Id, Role = "Participante" },
                };
                context.PersonSymposium.AddRange(personSymposiums);
                context.SaveChanges();
            }

            // ========================================================================
            // 11. SYMPOSIUM WORKSHOP ENROLLMENT (Nível 3 - Tabela de Junção Complexa)
            // ========================================================================
            if (!context.SymposiumWorkshopEnrollment.Any())
            {
                var people = context.Person.ToArray();
                var workshops = context.Workshop.ToArray();
                var symposiums = context.Symposium.ToArray();

                var enrollments = new SymposiumWorkshopEnrollment[]
                {
                    // Palestrantes dos workshops
                    new SymposiumWorkshopEnrollment { PersonId = people[4].Id, WorkshopId = workshops[0].Id, SymposiumId = symposiums[0].Id, IsLecturer = true, EnrollmentDate = new DateTime(2025, 8, 1) },
                    new SymposiumWorkshopEnrollment { PersonId = people[1].Id, WorkshopId = workshops[1].Id, SymposiumId = symposiums[1].Id, IsLecturer = true, EnrollmentDate = new DateTime(2025, 8, 5) },
                    // Inscrições de participantes nos workshops
                    new SymposiumWorkshopEnrollment { PersonId = people[2].Id, WorkshopId = workshops[0].Id, SymposiumId = symposiums[0].Id, IsLecturer = false, EnrollmentDate = new DateTime(2025, 9, 10) },
                    new SymposiumWorkshopEnrollment { PersonId = people[3].Id, WorkshopId = workshops[0].Id, SymposiumId = symposiums[0].Id, IsLecturer = false, EnrollmentDate = new DateTime(2025, 9, 12) },
                    new SymposiumWorkshopEnrollment { PersonId = people[3].Id, WorkshopId = workshops[3].Id, SymposiumId = symposiums[0].Id, IsLecturer = false, EnrollmentDate = new DateTime(2025, 9, 15) },
                    new SymposiumWorkshopEnrollment { PersonId = people[5].Id, WorkshopId = workshops[1].Id, SymposiumId = symposiums[1].Id, IsLecturer = false, EnrollmentDate = new DateTime(2025, 10, 2) },
                };
                context.SymposiumWorkshopEnrollment.AddRange(enrollments);
                context.SaveChanges();
            }
        }
    }
}