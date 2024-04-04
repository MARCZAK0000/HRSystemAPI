using HumanResources.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.UserModelDto
{
    public class GetArrivalsDto
    {
        public int Id { get; set; }

        public string UserCode { get; set; }

        public string UserId { get; set; }

        public DateTime? Arrival { get; set; }

        public DateTime? Departure { get; set; }

        public DateTime CreateDay { get; set; }

        public bool IsCompleted { get; set; }
    }
}
