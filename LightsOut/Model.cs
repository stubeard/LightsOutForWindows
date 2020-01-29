using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{
    public class Model : IModel
    {
        public static readonly int MaxColumns = 5;
        public static readonly int MaxRows = 5;

        const int OffsetCount = 5;
        const int OffsetSize  = 2;

        public static readonly int[][] Offsets = new int[OffsetCount][] { new int[OffsetSize]{  0,  0 },
                                                                          new int[OffsetSize]{ -1,  0 },
                                                                          new int[OffsetSize]{  0, -1 },
                                                                          new int[OffsetSize]{  1,  0 },
                                                                          new int[OffsetSize]{  0,  1 } };

        private bool[,] lights = new bool[MaxColumns,MaxRows];

        private List<IView> observers = new List<IView>();

        public Model()
        {
            ((IModel) this).Randomise();
        }

        void IModel.Set( int x, int y)
        {
            if ((x >= 0) &&
                (y >= 0) &&
                (x < MaxColumns) &&
                (y < MaxRows))
            {
                foreach(int[] offset in Offsets)
                {
                    int xx = x + offset[0];
                    int yy = y + offset[1];

                    if ((xx >= 0) &&
                        (yy >= 0) &&
                        (xx < MaxColumns) &&
                        (yy < MaxRows))
                    {
                        lights[xx, yy] = !lights[xx, yy];
                    }
                }

                FireModelChanged();

                //check if complete
                bool finished = true;

                for (int i = 0; i < Model.MaxRows; ++i)
                {
                    for (int j = 0; j < Model.MaxColumns; ++j)
                    { 
                        finished &= !lights[i, j];
                    }
                }

                if (finished)
                {
                    FireModelComplete();
                }
            }
        }

        bool IModel.Get(int x, int y)
        {
            if ((x >= 0) &&
                (y >= 0) &&
                (x < MaxColumns) &&
                (y < MaxRows))
            {
                return lights[x, y];
            }
            return false;
        }

        void IModel.AddObserver(IView view)
        {
            if( ! observers.Contains(view))
            {
                observers.Add(view);
                FireModelChanged();
            }
        }

        void IModel.RemoveObserver(IView view)
        {
            if (observers.Contains(view))
            {
                observers.Remove(view);
            }
        }

        private void FireModelChanged()
        {
            foreach( IView view in observers)
            {
                view.Update();
            }
        }

        private void FireModelComplete()
        {
            foreach (IView view in observers)
            {
                view.SetComplete();
            }
        }

        void IModel.Randomise()
        {
            Random random = new Random();

            for (int i = 0; i < Model.MaxRows; ++i)
            {
                for (int j = 0; j < Model.MaxColumns; ++j)
                {
                    lights[i, j] = (random.NextDouble() > 0.5);
                }
            }
        }

        void IModel.Reset()
        {
            Random random = new Random();

            for (int i = 0; i < Model.MaxRows; ++i)
            {
                for (int j = 0; j < Model.MaxColumns; ++j)
                {
                    lights[i, j] = false;
                }
            }
        }
    }
}
