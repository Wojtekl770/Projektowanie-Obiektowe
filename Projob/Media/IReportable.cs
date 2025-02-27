using Projob1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob.Media
{
    public interface IReportable
    {
        string Accept(Visitor vis);
    }

}
