using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Modules.Windows.Common.DimSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXConnect.Desktop.Modules.InstGenesys
{
    interface IInstGenesysView : IView,IMin
    {
        IInstGenesysModel Model { get; set; }
    }
}
