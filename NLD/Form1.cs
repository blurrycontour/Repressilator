using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NLD
{
    public partial class Repressilator : Form
    {
        //Initial Conditions
        double m1 = 0.2, m2 = 0.1, m3 = 0.2;
        double p1 = 0.1, p2 = 0.4, p3 = 0.5;
        //Parameters
        double a0 = 0, a = 50, n = 2.0, b = 0.2;
        //Range
        double Ta = 0, Tb = 300, h = 1;
        //...
        double M1 = 0, M2 = 0, M3 = 0, P1 = 0, P2 = 0, P3 = 0;

        double[] km1 = new double[4];
        double[] km2 = new double[4];
        double[] km3 = new double[4];
        double[] kp1 = new double[4];
        double[] kp2 = new double[4];
        double[] kp3 = new double[4];

        double t;
        int graphNo=1;

        public Repressilator()
        {
            InitializeComponent();
            
       //defalut values...     
            textBox3.Text = Convert.ToString(n);
            textbox10.Text = Convert.ToString(a);
            textBox1.Text = Convert.ToString(a0);
            textBox2.Text = Convert.ToString(b);

            textBox5.Text = Convert.ToString(m1);
            textBox4.Text = Convert.ToString(m2);
            textBox6.Text = Convert.ToString(m3);
            textBox7.Text = Convert.ToString(p1);
            textBox8.Text = Convert.ToString(p2);
            textBox9.Text = Convert.ToString(p3);

            textBox11.Text = Convert.ToString(Ta);
            textBox12.Text = Convert.ToString(Tb);
            textBox13.Text = Convert.ToString(h);

            checkBox2.Checked = true;
            checkBox1.Checked = false;
            checkBox3.Checked = false;
        
            chart1.Series[0].Color = Color.Red;
            chart1.Series[1].Color = Color.Blue;
            chart1.Series[2].Color = Color.Green;
           
            chart1.Series[3].Color = Color.Red;
            chart1.Series[4].Color = Color.Blue;
            chart1.Series[5].Color = Color.Green;

            chart1.Series[6].Color = Color.Red;
            chart1.Series[7].Color = Color.Blue;
            chart1.Series[8].Color = Color.Green;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();
            chart1.Series[5].Points.Clear();
            chart1.Series[6].Points.Clear();
            chart1.Series[7].Points.Clear();
            chart1.Series[8].Points.Clear();

             t = Ta;

             n = Convert.ToDouble(textBox3.Text);
             a = Convert.ToDouble(textbox10.Text);
             a0 = Convert.ToDouble(textBox1.Text);
             b = Convert.ToDouble(textBox2.Text);

             m1 = Convert.ToDouble(textBox5.Text);
             m2 = Convert.ToDouble(textBox4.Text);
             m3 = Convert.ToDouble(textBox6.Text);
             p1 = Convert.ToDouble(textBox7.Text);
             p2 = Convert.ToDouble(textBox8.Text);
             p3 = Convert.ToDouble(textBox9.Text);

             Ta = Convert.ToDouble(textBox11.Text);
             Tb = Convert.ToDouble(textBox12.Text);
             h = Convert.ToDouble(textBox13.Text);

            for (int i = 0; i <= (Tb - Ta) / h; i++)
            {


                km1[0] = f(p2, m1);
                km2[0] = f(p3, m2);
                km3[0] = f(p1, m3);
                kp1[0] = g(p1, m1);
                kp2[0] = g(p2, m2);
                kp3[0] = g(p3, m3);

                km1[1] = f(p2 + (h / 2) * kp2[0], m1 + (h / 2) * km1[0]);
                km2[1] = f(p3 + (h / 2) * kp3[0], m2 + (h / 2) * km2[0]);
                km3[1] = f(p1 + (h / 2) * kp1[0], m3 + (h / 2) * km3[0]);
                kp1[1] = g(p1 + (h / 2) * kp1[0], m1 + (h / 2) * km1[0]);
                kp2[1] = g(p2 + (h / 2) * kp2[0], m2 + (h / 2) * km2[0]);
                kp3[1] = g(p3 + (h / 2) * kp3[0], m3 + (h / 2) * km3[0]);

                km1[2] = f(p2 + (h / 2) * kp2[1], m1 + (h / 2) * km1[1]);
                km2[2] = f(p3 + (h / 2) * kp3[1], m2 + (h / 2) * km2[1]);
                km3[2] = f(p1 + (h / 2) * kp1[1], m3 + (h / 2) * km3[1]);
                kp1[2] = g(p1 + (h / 2) * kp1[1], m1 + (h / 2) * km1[1]);
                kp2[2] = g(p2 + (h / 2) * kp2[1], m2 + (h / 2) * km2[1]);
                kp3[2] = g(p3 + (h / 2) * kp3[1], m3 + (h / 2) * km3[1]);

                km1[3] = f(p2 + (h) * kp2[2], m1 + (h) * km1[2]);
                km2[3] = f(p3 + (h) * kp3[2], m2 + (h) * km2[2]);
                km3[3] = f(p1 + (h) * kp1[2], m3 + (h) * km3[2]);
                kp1[3] = g(p1 + (h) * kp1[2], m1 + (h) * km1[2]);
                kp2[3] = g(p2 + (h) * kp2[2], m2 + (h) * km2[2]);
                kp3[3] = g(p3 + (h) * kp3[2], m3 + (h) * km3[2]);


                M1 = m1 + ((km1[0] + 2 * km1[1] + 2 * km1[2] + km1[3]) * h / 6);
                M2 = m2 + ((km2[0] + 2 * km2[1] + 2 * km2[2] + km2[3]) * h / 6);
                M3 = m3 + ((km3[0] + 2 * km3[1] + 2 * km3[2] + km3[3]) * h / 6);
                P1 = p1 + ((kp1[0] + 2 * kp1[1] + 2 * kp1[2] + kp1[3]) * h / 6);
                P2 = p2 + ((kp2[0] + 2 * kp2[1] + 2 * kp2[2] + kp2[3]) * h / 6);
                P3 = p3 + ((kp3[0] + 2 * kp3[1] + 2 * kp3[2] + kp3[3]) * h / 6);

               switch(graphNo)
                {
                    case 1: {chart1.Series[0].Points.AddXY(t + h, P1);
                            chart1.Series[1].Points.AddXY(t + h, P2);
                            chart1.Series[2].Points.AddXY(t + h, P3);
                            break;}
                    case 2: { chart1.Series[3].Points.AddXY(t + h, M1);
                            chart1.Series[4].Points.AddXY(t + h, M2);
                            chart1.Series[5].Points.AddXY(t + h, M3);
                            break;}
                    case 3: {chart1.Series[6].Points.AddXY(P2, P1);
                            chart1.Series[7].Points.AddXY(P3, P2);
                            chart1.Series[8].Points.AddXY(P1, P3); 
                            break;}
                    default: break;
                }
                
             

                t = t + h;
                m1 = M1;
                m2 = M2;
                m3 = M3;
                p1 = P1;
                p2 = P2;
                p3 = P3;

            }
        }
        double f(double p, double m)
        {
            double z1 = -m + (a / (1 + Math.Pow(p, n))) + a0;
            return z1;
        }
        double g(double p, double m)
        {
            double z2 = -b * (p - m);
            return z2;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                graphNo = 3;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
            else
            {
                graphNo = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;     
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                graphNo = 1;
                checkBox3.Checked = false;
                checkBox1.Checked = false;
            }
            else
            {
                graphNo = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                graphNo = 2;
                checkBox2.Checked = false;
                checkBox1.Checked = false;
            }
            else
            {
                graphNo = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }

        }
    }
}