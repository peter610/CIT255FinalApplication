using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IFirearmRepository : IDisposable
    {
        Firearm SelectById(int Id);
        List<Firearm> SelectAll();
        void Update(Firearm firearm);
        void Insert(Firearm firearm);
    }
}
