using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Data.FakerDataSeedng
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
