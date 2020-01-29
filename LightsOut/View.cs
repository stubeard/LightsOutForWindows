using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class View : Form, IView
    {
        IModel model = new Model();

        bool complete = false;

        public View()
        {
            InitializeComponent();

            model.AddObserver(this);
        }

        private void labelMouseDown(object sender, MouseEventArgs e)
        {
            if (!complete)
            { 
                if (sender is Label)
                {
                    Label label = (Label)sender;
                    TableLayoutPanelCellPosition position = table.GetPositionFromControl(label);

                    if (position != null)
                    {
                        model.Set(position.Column, position.Row);
                    }
                }
            }
        }

        void IView.Update()
        {
            for (int i = 0; i < Model.MaxColumns; ++i)
            {
                for (int j = 0; j < Model.MaxRows; ++j)
                {
                    table.GetControlFromPosition(i, j).BackColor = model.Get(i, j) ? Color.Lime : Color.DarkGreen;
                }
            }
        }

        void IView.SetComplete()
        {
            complete = true;

        }
    }
}
