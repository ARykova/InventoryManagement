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
    public partial class Form1 : Form
    {
        public static List<Component> Components = new List<Component>
        {
            new Component
            {
                Name = "Подпятник",
                Count = 1,
                Price = 150,
                IntervalTime = 30,
                SupplyTime = 5,
                DelayTime = 5,
                SupplyCount = 2000,
                Supplier = "Куса"
            },
            new Component
            {
                Name = "Корпус",
                Count = 1,
                Price = 800,
                IntervalTime = 7,
                SupplyTime = 1,
                DelayTime = 2,
                SupplyCount = 500,
                Supplier = "Москва"
            },
            new Component
            {
                Name = "Кольцо головки",
                Count = 1,
                Price = 215,
                IntervalTime = 30,
                SupplyTime = 1,
                DelayTime = 5,
                SupplyCount = 2000,
                Supplier = "Москва"
            },
            new Component
            {
                Name = "Гайка",
                Count = 1,
                Price = 50,
                IntervalTime = 7,
                SupplyTime = 1,
                DelayTime = 5,
                SupplyCount = 2000,
                Supplier = "Москва"
            }

        };
        public List<FixedSizeSystemParameters> FixedSizeSystemParameters = new List<FixedSizeSystemParameters> {
            new FixedSizeSystemParameters(Components[0], 24500),
            new FixedSizeSystemParameters(Components[1], 24500),
            new FixedSizeSystemParameters(Components[2], 24500),
            new FixedSizeSystemParameters(Components[3], 24500),
        };
        public List<FixedTimeSystemParameters> FixedTimeSystemParameters = new List<FixedTimeSystemParameters> {
            new FixedTimeSystemParameters(Components[0], 24500),
            new FixedTimeSystemParameters(Components[1], 24500),
            new FixedTimeSystemParameters(Components[2], 24500),
            new FixedTimeSystemParameters(Components[3], 24500),
        };

        public double[] MakeYMas(double[] days, double maxReserve, double orderSize, double dailyCons)
        {
            double[] YMas = new double[days.Length];
            YMas[0] = maxReserve;
            for (int i = 1; i < days.Length; i+=2)
            {
                YMas[i] = YMas[i - 1] - (dailyCons * (days[i] - days[i - 1]));
                YMas[i + 1] = YMas[i] + orderSize;
            }
            return YMas;
        }

        public double[] MakeXMas(int orderTime)
        {
            double[] XMas = new double[(480/orderTime)+1];
            int j = 1;
            XMas[0] = 0;
            for(int i = orderTime; j < XMas.Length; i += orderTime)
            {
                XMas[j] = i;
                XMas[j + 1] = i;
                j+=2;
            }
            return XMas;
        }

        public double[] MakeXMas(int orderTime, double delay)
        {
            double[] XMas = new double[(480 / orderTime) + 1];
            int j = 3;
            XMas[0] = 0;
            XMas[1] = orderTime + delay;
            XMas[2] = XMas[1];
            for (int i = Convert.ToInt32(XMas[2])+orderTime; j < XMas.Length; i += orderTime)
            {
                XMas[j] = i;
                XMas[j + 1] = i;
                j += 2;
            }
            return XMas;
        }

        public double[] MakeXMas(int orderTime, double[] delay)
        {
            double[] XMas = new double[(480 / orderTime) + 1];
            int j = 1;
            XMas[0] = 0;
            for (int k = 0; k < delay.Length; k++)
            {
                XMas[j] = XMas[j - 1] + orderTime + delay[k];
                XMas[j + 1] = XMas[j];
                j += 2;
            }

            for (int i = Convert.ToInt32(XMas[j-1]) + orderTime; j <XMas.Length; i += orderTime)
            {
                XMas[j] = i;
                XMas[j + 1] = i;
                j += 2;
            }
            return XMas;
        }

        public Form1()
        {            
            InitializeComponent();
            dataGridView1.DataSource = Components;
            dataGridView2.DataSource = FixedSizeSystemParameters;
            dataGridView3.DataSource = FixedTimeSystemParameters;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] x1 = MakeXMas(FixedSizeSystemParameters[0].ConsumptionTime);
            double[] y1 = MakeYMas(x1, FixedSizeSystemParameters[0].MaxReserve, FixedSizeSystemParameters[0].OptimalOrderSize, FixedSizeSystemParameters[0].DailyConsumption);
            double[] x2 = MakeXMas(FixedSizeSystemParameters[1].ConsumptionTime);
            double[] y2 = MakeYMas(x2, FixedSizeSystemParameters[1].MaxReserve, FixedSizeSystemParameters[1].OptimalOrderSize, FixedSizeSystemParameters[1].DailyConsumption);
            double[] x3 = MakeXMas(FixedSizeSystemParameters[2].ConsumptionTime);
            double[] y3 = MakeYMas(x3, FixedSizeSystemParameters[2].MaxReserve, FixedSizeSystemParameters[2].OptimalOrderSize, FixedSizeSystemParameters[2].DailyConsumption);
            double[] x4 = MakeXMas(FixedSizeSystemParameters[3].ConsumptionTime);
            double[] y4 = MakeYMas(x4, FixedSizeSystemParameters[3].MaxReserve, FixedSizeSystemParameters[3].OptimalOrderSize, FixedSizeSystemParameters[3].DailyConsumption);
            
            Form2 form = new Form2(x1, y1, x2, y2, x3, y3, x4, y4);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[] x1 = MakeXMas(FixedTimeSystemParameters[0].IntervalTime);
            double[] y1 = MakeYMas(x1, FixedTimeSystemParameters[0].MaxReserve, FixedTimeSystemParameters[0].OrderSize, FixedTimeSystemParameters[0].DailyConsumption);
            double[] x2 = MakeXMas(FixedTimeSystemParameters[1].IntervalTime);
            double[] y2 = MakeYMas(x2, FixedTimeSystemParameters[1].MaxReserve, FixedTimeSystemParameters[1].OrderSize, FixedTimeSystemParameters[1].DailyConsumption);
            double[] x3 = MakeXMas(FixedTimeSystemParameters[2].IntervalTime);
            double[] y3 = MakeYMas(x3, FixedTimeSystemParameters[2].MaxReserve, FixedTimeSystemParameters[2].OrderSize, FixedTimeSystemParameters[2].DailyConsumption);
            double[] x4 = MakeXMas(FixedTimeSystemParameters[3].IntervalTime);
            double[] y4 = MakeYMas(x4, FixedTimeSystemParameters[3].MaxReserve, FixedTimeSystemParameters[3].OrderSize, FixedTimeSystemParameters[3].DailyConsumption);

            Form2 form = new Form2(x1, y1, x2, y2, x3, y3, x4, y4);
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] x1 = MakeXMas(FixedSizeSystemParameters[0].ConsumptionTime, FixedSizeSystemParameters[0].DelayTime);
            double[] y1 = MakeYMas(x1, FixedSizeSystemParameters[0].MaxReserve, FixedSizeSystemParameters[0].OptimalOrderSize, FixedSizeSystemParameters[0].DailyConsumption);
            double[] x2 = MakeXMas(FixedSizeSystemParameters[1].ConsumptionTime, FixedSizeSystemParameters[1].DelayTime);
            double[] y2 = MakeYMas(x2, FixedSizeSystemParameters[1].MaxReserve, FixedSizeSystemParameters[1].OptimalOrderSize, FixedSizeSystemParameters[1].DailyConsumption);
            double[] x3 = MakeXMas(FixedSizeSystemParameters[2].ConsumptionTime, FixedSizeSystemParameters[2].DelayTime);
            double[] y3 = MakeYMas(x3, FixedSizeSystemParameters[2].MaxReserve, FixedSizeSystemParameters[2].OptimalOrderSize, FixedSizeSystemParameters[2].DailyConsumption);
            double[] x4 = MakeXMas(FixedSizeSystemParameters[3].ConsumptionTime, FixedSizeSystemParameters[3].DelayTime);
            double[] y4 = MakeYMas(x4, FixedSizeSystemParameters[3].MaxReserve, FixedSizeSystemParameters[3].OptimalOrderSize, FixedSizeSystemParameters[3].DailyConsumption);

            Form2 form = new Form2(x1, y1, x2, y2, x3, y3, x4, y4);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[] delay1 = new double[] { 3, 4, 5 };
            double[] delay2 = new double[] { 1, 2 };
            double[] delay3 = new double[] { 2, 4 };
            double[] delay4 = new double[] { 2, 3, 4 };
            double[] x1 = MakeXMas(FixedSizeSystemParameters[0].ConsumptionTime, delay1);
            double[] y1 = MakeYMas(x1, FixedSizeSystemParameters[0].MaxReserve, FixedSizeSystemParameters[0].OptimalOrderSize, FixedSizeSystemParameters[0].DailyConsumption);
            double[] x2 = MakeXMas(FixedSizeSystemParameters[1].ConsumptionTime, delay2);
            double[] y2 = MakeYMas(x2, FixedSizeSystemParameters[1].MaxReserve, FixedSizeSystemParameters[1].OptimalOrderSize, FixedSizeSystemParameters[1].DailyConsumption);
            double[] x3 = MakeXMas(FixedSizeSystemParameters[2].ConsumptionTime, delay3);
            double[] y3 = MakeYMas(x3, FixedSizeSystemParameters[2].MaxReserve, FixedSizeSystemParameters[2].OptimalOrderSize, FixedSizeSystemParameters[2].DailyConsumption);
            double[] x4 = MakeXMas(FixedSizeSystemParameters[3].ConsumptionTime, delay4);
            double[] y4 = MakeYMas(x4, FixedSizeSystemParameters[3].MaxReserve, FixedSizeSystemParameters[3].OptimalOrderSize, FixedSizeSystemParameters[3].DailyConsumption);

            Form2 form = new Form2(x1, y1, x2, y2, x3, y3, x4, y4);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double[] x1 = MakeXMas(FixedTimeSystemParameters[0].IntervalTime, FixedTimeSystemParameters[0].DelayTime);
            double[] y1 = MakeYMas(x1, FixedTimeSystemParameters[0].MaxReserve, FixedTimeSystemParameters[0].OrderSize, FixedTimeSystemParameters[0].DailyConsumption);
            double[] x2 = MakeXMas(FixedTimeSystemParameters[1].IntervalTime, FixedTimeSystemParameters[1].DelayTime);
            double[] y2 = MakeYMas(x2, FixedTimeSystemParameters[1].MaxReserve, FixedTimeSystemParameters[1].OrderSize, FixedTimeSystemParameters[1].DailyConsumption);
            double[] x3 = MakeXMas(FixedTimeSystemParameters[2].IntervalTime, FixedTimeSystemParameters[2].DelayTime);
            double[] y3 = MakeYMas(x3, FixedTimeSystemParameters[2].MaxReserve, FixedTimeSystemParameters[2].OrderSize, FixedTimeSystemParameters[2].DailyConsumption);
            double[] x4 = MakeXMas(FixedTimeSystemParameters[3].IntervalTime, FixedTimeSystemParameters[3].DelayTime);
            double[] y4 = MakeYMas(x4, FixedTimeSystemParameters[3].MaxReserve, FixedTimeSystemParameters[3].OrderSize, FixedTimeSystemParameters[3].DailyConsumption);

            Form2 form = new Form2(x1, y1, x2, y2, x3, y3, x4, y4);
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double[] delay1 = new double[] { 3, 4, 5 };
            double[] delay2 = new double[] { 1, 2 };
            double[] delay3 = new double[] { 2, 4 };
            double[] delay4 = new double[] { 2, 3, 4 };

            double[] x1 = MakeXMas(FixedTimeSystemParameters[0].IntervalTime, delay1);
            double[] y1 = MakeYMas(x1, FixedTimeSystemParameters[0].MaxReserve, FixedTimeSystemParameters[0].OrderSize, FixedTimeSystemParameters[0].DailyConsumption);
            double[] x2 = MakeXMas(FixedTimeSystemParameters[1].IntervalTime, delay2);
            double[] y2 = MakeYMas(x2, FixedTimeSystemParameters[1].MaxReserve, FixedTimeSystemParameters[1].OrderSize, FixedTimeSystemParameters[1].DailyConsumption);
            double[] x3 = MakeXMas(FixedTimeSystemParameters[2].IntervalTime, delay3);
            double[] y3 = MakeYMas(x3, FixedTimeSystemParameters[2].MaxReserve, FixedTimeSystemParameters[2].OrderSize, FixedTimeSystemParameters[2].DailyConsumption);
            double[] x4 = MakeXMas(FixedTimeSystemParameters[3].IntervalTime, delay4);
            double[] y4 = MakeYMas(x4, FixedTimeSystemParameters[3].MaxReserve, FixedTimeSystemParameters[3].OrderSize, FixedTimeSystemParameters[3].DailyConsumption);

            Form2 form = new Form2(x1, y1, x2, y2, x3, y3, x4, y4);
            form.Show();
        }
    }
}
