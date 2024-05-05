using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Repository
{
    public interface IPDFReportRepository<T>
    {
        Task<MemoryStream> GetPDFReport(List<T> items);
    }
}
