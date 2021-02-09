using DotNetCore_OpeningHoursQuestion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_OpeningHoursQuestion.Domain.Services
{
    public interface IOpeningHoursService
    {
        string FormatOpeningHours(OpeningHours openingHours);
    }
}
