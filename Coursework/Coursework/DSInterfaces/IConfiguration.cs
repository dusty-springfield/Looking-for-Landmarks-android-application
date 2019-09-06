using Coursework.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.DSInterfaces
{
    public interface IConfiguration
    {
        Task Save(Configuration configuration);
        Configuration Load();
    }
}
