using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartArea1.AxisY.Maximum = 100;
            chartArea1.AxisY.Minimum = -100;
            //chartArea1.Name = "Default";

            //timerRealTimeData.Enabled = true;
            this.timerRealTimeData.Interval = 50;
            this.timerRealTimeData.Tick += new System.EventHandler(this.timerRealTimeData_Tick);
        }

        Timer timerRealTimeData = new System.Windows.Forms.Timer();



        private void chart1_Click(object sender, EventArgs e)
        {
            if (this.timerRealTimeData.Enabled)
            {
                this.timerRealTimeData.Enabled = false;
                //buttonStart.Text = "&Start Real Time Data";
            }
            else
            {
                this.timerRealTimeData.Enabled = true;
                //buttonStart.Text = "&Stop Real Time Data";
            }
        }

        private Random random = new Random();
        private int pointIndex = 0;
        private float counter;
        private float increment = 0.1f;
        private void timerRealTimeData_Tick(object sender, System.EventArgs e)
        {
            // |Counter increment
            counter += increment;
            // Define some variables
            int numberOfPointsInChart = 200;// int.Parse(comboBoxVisiblePoints.Text);
            int numberOfPointsAfterRemoval = 200; // numberOfPointsInChart - int.Parse(comboBoxPointsRemoved.Text);

            // Simulate adding new data points
            int numberOfPointsAddedMin = 1;
            int numberOfPointsAddedMax = 100;
            //for (int pointNumber = 0; pointNumber <
                //random.Next(numberOfPointsAddedMin, numberOfPointsAddedMax); pointNumber++)
            {
                //chart1.Series[0].Points.AddXY(pointIndex + 1, random.Next(-90, 90));
                chart1.Series[0].Points.AddXY(pointIndex + 1, Math.Sin(counter));
                //chart1.Series[1].Points.AddXY(pointIndex + 1, random.Next(1000, 5000));
                ++pointIndex;
            }

            // Adjust Y & X axis scale
            chart1.ResetAutoValues();
            /*if (chart1.ChartAreas["Default"].AxisX.Maximum < pointIndex)
            {
                chart1.ChartAreas["Default"].AxisX.Maximum = pointIndex;
            }*/

            // Keep a constant number of points by removing them from the left
            while (chart1.Series[0].Points.Count > numberOfPointsInChart)
            {
                // Remove data points on the left side
                while (chart1.Series[0].Points.Count > numberOfPointsAfterRemoval)
                {
                    chart1.Series[0].Points.RemoveAt(0);
                }

                // Adjust X axis scale
                //chart1.ChartAreas["Default"].AxisX.Minimum = pointIndex - numberOfPointsAfterRemoval;
                //chart1.ChartAreas["Default"].AxisX.Maximum = chart1.ChartAreas["Default"].AxisX.Minimum + numberOfPointsInChart;
            }

            // Redraw chart
            chart1.Invalidate();
        }
    }
}
