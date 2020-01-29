using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{
    public interface IView
    {
        void Update();
        void SetComplete();
    }
}
