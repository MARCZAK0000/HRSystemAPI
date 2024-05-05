using HumanResources.Domain.Repository;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure.Repository
{
    public class PDFReportRepository<T>: IPDFReportRepository<T>
    {
        public Task<MemoryStream> GetPDFReport(List<T> items)
        {
            var count = Math.Ceiling((decimal)(items.Count/5));
            var iteration = 0;
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var document = Document.Create(container =>
            {
                while (iteration <= count)
                {
                    container.Page(page =>
                    {
                        page.Header()
                            .Text("Report")
                            .AlignCenter()
                            .SemiBold().FontSize(30);

                        var content = items.Skip(iteration * 5)
                            .Take(5);

                        foreach (var item in content)
                        {
                            page.Content()
                                .PaddingVertical(1, QuestPDF.Infrastructure.Unit.Centimetre)
                                .Column(x =>
                                {
                                    x.Spacing(20);
                                    x.Item().Text(item!.ToString());
                                });
                        }
                        page.Footer()
                            .Text("HRSystem")
                            .AlignCenter()
                            .FontSize(10);
                    });
                    iteration++;
                }
                
            }).GeneratePdf();

            var stream = new MemoryStream(document);
            return Task.FromResult(stream);
        }
    }
}
