using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projob.Media
{
    public interface Iterator
    {
        public string GetCurrent();

        public void MoveToNext();

        public bool HasNext();
    }
}
