using MySpectrum.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpectrum.Interfaces
{
    public interface IDataService
    {
        bool createDatabase();
        bool InsertData();
        List<User> ReadData();
        bool DeletData();

    }
}
