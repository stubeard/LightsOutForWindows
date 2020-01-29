using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{
    public interface IModel
    {
        void AddObserver(IView view);
        void RemoveObserver(IView view);
        void Set(int x, int y);
        bool Get(int x, int y);
        void Reset();
        void Randomise();
    }
}
