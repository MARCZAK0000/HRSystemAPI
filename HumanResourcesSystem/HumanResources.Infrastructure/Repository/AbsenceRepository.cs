using HumanResources.Application.Authentication;
using HumanResources.Domain.AbsenceModelDto;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Enums;
using HumanResources.Domain.Exceptions;
using HumanResources.Domain.Repository;
using HumanResources.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace HumanResources.Infrastructure.Repository
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly HumanResourcesDatabase _database;

        private readonly UserManager<User> _userManager;

        private readonly IUserContext _userContext;

        public AbsenceRepository(HumanResourcesDatabase database,
            UserManager<User> userManager, IUserContext userContext)
        {
            _database = database;
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<Absence> CreateAbsenceAsync(CreateAbsenceDto createAbsence, CancellationToken token)
        {


            var currentUser = _userContext.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            var findAbsences = await _database.Absences
                .Where(pr => pr.UserId == user.Id)
                .FirstOrDefaultAsync(
                pr => (pr.StartTime.Date <= createAbsence.StartTime.Date && pr.EndTime.Date >= createAbsence.StartTime.Date) &&
                (pr.StartTime.Date <= createAbsence.EndTime.Date && pr.EndTime > createAbsence.EndTime.Date), token);

            if (findAbsences != null)
            {
                throw new BadRequestException("You have absences on this period of time");
            }

            var newAbsences = new Absence()
            {
                UserId = user.Id,
                Name = createAbsence.Name,
                AbsenceId = createAbsence.AbsenceTypeId,
                CreatedTime = DateTime.Now.Date,
                StartTime = createAbsence.StartTime.Date,
                EndTime = createAbsence.EndTime.Date,
            };
            newAbsences.CalculatePeriodOfTime();

            await _database.Absences
                .AddAsync(newAbsences, token);

            await _database.SaveChangesAsync(token);

            return newAbsences;

        }

        public async Task<List<Absence>> ShowAbsencesByYearAsync(string userID, int year, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if(!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Leader)))
            {
                userID = user.Id;
            }

            if (year < 2020 || year > 2050)
            {
                throw new BadRequestException("Invalid year");
            }

            var result = await _database.Absences
                .Include(pr => pr.AbsencesType)
                .Where(pr => pr.StartTime.Year == year && pr.UserId == userID)
                .ToListAsync(cancellationToken: token);

            if (!result.Any())
            {
                throw new NotFoundException("Not found absences in this year");
            }

            return result;
        }

        public async Task<Absence> AbsenceDecisionAsync(AbsenceDecisionInfoDto infoDto, CancellationToken token)
        {
            var currentUser = _userContext.GetCurrentUser();

            var user = await _userManager.FindByIdAsync(currentUser.Id) ??
                throw new InvalidEmailOrPasswordExcepiton("Invalid UserName or Password");

            if (!await _userManager.IsInRoleAsync(user, nameof(RolesEnum.Leader)))
            {
                throw new UnauthorizedExceptions("UnAuthorized");
            }

            var getLeaderDepartmentID = await _database.UserInfo
                .Where(pr => pr.UserId == user.Id)
                .Select(pr => pr.DepartmentID)
                .FirstOrDefaultAsync(cancellationToken: token);

            if (getLeaderDepartmentID <= 0)
            {
                throw new NotFoundException("NotFound leader");
            }

            var baseQuery = _database
                .Absences
                .Include(pr => pr.AbsencesType)
                .Include(pr => pr.User)
                .Where(pr => pr.User.DepartmentID == getLeaderDepartmentID);

            if(!baseQuery.Any()) 
            {
                throw new NotFoundException("User is not in your Department");
            }

            var subordinateAbsence = await baseQuery 
                .FirstOrDefaultAsync(pr => pr.Id == infoDto.AbsenceId && pr.User.UserCode == infoDto.UserCode, cancellationToken: token) ??
                throw new NotFoundException("Not Found Absences");

            if (subordinateAbsence.IsAccepted)
            {
                throw new BadRequestException("Absence was accepted or decision is negative");
            }

            if (!infoDto.Decision)
            {
                subordinateAbsence.Declined = true;
                await _database.SaveChangesAsync(token);
                return subordinateAbsence;
            }

            subordinateAbsence.IsAccepted = infoDto.Decision;
            subordinateAbsence.User.DaysOfAbsencesCurrentYear += subordinateAbsence.PeriodOfTime;
            var daysToUse = subordinateAbsence.CalculateDayToUse(() =>
            {
                int daysToUse = 0;

                switch (subordinateAbsence.AbsenceId)
                {
                    case 1:
                        daysToUse = subordinateAbsence.PeriodOfTime;
                        break;
                    case 2:
                        daysToUse = subordinateAbsence.PeriodOfTime;
                        break;
                    default:
                        break;
                }

                return daysToUse;
            });

            if(subordinateAbsence.User.DaysOfAbsencesToUse -  daysToUse < 0 && daysToUse > 0) 
            {
                subordinateAbsence.Declined = true;

                await _database.SaveChangesAsync(token);
                throw new BadRequestException($"User: {subordinateAbsence.User.UserCode} used all of his absence days");
            }


            subordinateAbsence.User.DaysOfAbsencesToUse-= daysToUse;
            await _database.SaveChangesAsync(token);
            return subordinateAbsence;


        }

        public Task<MemoryStream> GenerateAbsenceReportPDF(List<AbsenceInfoDto> list, (string userID, int year) info, CancellationToken token)
        {
            
            token.ThrowIfCancellationRequested();
            var count = Math.Ceiling((decimal)(list.Count/ 20));
            var iteration = 0;
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var document = Document.Create(container =>
            {
                while (iteration <= count)
                {
                    container.Page(page =>
                    {
                        page.PageColor(Colors.Brown.Lighten4);
                        page.MarginTop(10);
                        page.Header()
                            .Text($"HRSystem\r\n") 
                            .AlignCenter()
                            .SemiBold().FontSize(30);


                        page.Content()
                            .AlignCenter()
                            .Column(x =>
                            {
                                x.Item().AlignCenter().Text($"Absence report for user: {info.userID} in {info.year}").FontSize(16);
                                x.Spacing(10);
                                x.Item().AlignCenter().Table(table =>
                                {   
                                    table.ColumnsDefinition(colums =>
                                    {
                                        colums.ConstantColumn(90);
                                        colums.ConstantColumn(60);
                                        colums.ConstantColumn(90);
                                        colums.ConstantColumn(90);
                                        colums.ConstantColumn(60);
                                        colums.ConstantColumn(60);
                                    });
                                    table.Header(header =>
                                    {
                                        header.Cell().Text("Name");
                                        header.Cell().Text("Type");
                                        header.Cell().Text("Start Time");
                                        header.Cell().Text("End Time");
                                        header.Cell().Text("Accepted");
                                        header.Cell().Text("Declined");
                                    });

                                    while (iteration <= count)
                                    {
                                        var content = list.Skip(iteration * 20)
                                            .Take(20);

                                        foreach (var item in content)
                                        {
                                            table.Cell().Text(item.Name);
                                            table.Cell().Text(item.AbsenceTypeName);
                                            table.Cell().Text(item.StartTime.Date.ToShortDateString().ToString());
                                            table.Cell().Text(item.EndTime.Date.ToShortDateString().ToString());
                                            table.Cell().Text(item.IsAccepted ? "TRUE" : "FALSE");
                                            table.Cell().Text(item.Declined ? "TRUE" : "FALSE");
                                        }
                                        iteration++;
                                    }


                                });
                            });


                        page.MarginBottom(10);
                        page.Footer()
                            .Text($"HRSystem {DateTime.Now.Year}")
                            .AlignCenter()
                            .FontSize(10);
                    });
                }

            }).GeneratePdf();

            var stream = new MemoryStream(document);
            return Task.FromResult(stream);
        }
    }
}
