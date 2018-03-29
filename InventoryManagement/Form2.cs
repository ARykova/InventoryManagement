using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagement
{
    public partial class Form2 : Form
    {
        public Form2(double[] x1, double[] y1, double[] x2, double[] y2, double[] x3, double[] y3, double[] x4, double[] y4)
        {
            InitializeComponent();
            chart1.Series[0].Points.DataBindXY(x1, y1);
            chart2.Series[0].Points.DataBindXY(x2, y2);
            chart3.Series[0].Points.DataBindXY(x3, y3);
            chart4.Series[0].Points.DataBindXY(x4, y4);
        }
    }
}
