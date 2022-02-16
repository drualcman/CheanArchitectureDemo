using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities.ValueObject
{
    public record struct ProblemDetails(
            int Status,
            string Type,
            string Title,
            string Detail,
            Dictionary<string, List<string>> InvalidParams);
}
