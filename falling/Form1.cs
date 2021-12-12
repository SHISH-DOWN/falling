using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using myUCLib;
using static System.Math;
using System.Threading;


namespace falling
{
    
    public partial class Form1 : Form
    {
        public double x,y,xprev,yprev,x0,y0,v,v0,u,vx,vy,vxprev,vyprev,vx0,vy0,R,dt,n,Fx,Fy,m,dt2,xmax,ymax,tmax,angle,ax,ay,t,t1;
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            Fx = -R * Pow(v, n) * vxprev;
            Fy = -R * Pow(v, n) * vyprev - m * G;
            ax = Fx / m;
            ay = Fy / m;
            vx = vxprev + ax * dt;
            vy = vyprev + ay * dt;
            x = xprev + vx * dt + ax * Pow(dt, 2) / 2;
            y = yprev + vy * dt + ay * Pow(dt, 2) / 2;
            if (x >= xmax)
            {
                xmax = x;
            }

            if (y >= ymax)
            {
                ymax = y;
            }

            xprev = x;
            yprev = y;
            t = t + dt2;
            vxprev = vx;
            vyprev = vy;
            axis1.StatToDin();
            axis1.Pix_Size = 0.05f;
            axis1.PixDraw((float)x, (float)y, Color.Red, 2);
            axis1.Pix_Size = 0.005f;
            axis1.PixDraw((float)x, (float)y, Color.Blue, 1);
            axis1.DinToPic();

            if (yprev<=0)
            {
                timer1.Enabled = false;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            xprev = x0;
            yprev = y0;
            v = v0;
            t = 0;
            vxprev = vx0;
            vyprev = vy0;
            xmax = Double.MinValue;
            ymax = Double.MinValue;
            timer1.Enabled = true;
           


        }

        public const double G = 9.81;
        private void buttonPreStart_Click(object sender, EventArgs e)
        {
            xprev = x0;
            yprev = y0;
            v = v0;
            t = 0;
            vxprev = vx0;
            vyprev = vy0;
            xmax=Double.MinValue;
            ymax=Double.MinValue;
            dt2=5*dt;

            while (yprev > 0)
            {
                Fx = -R * Pow(v, n) * vxprev;
                Fy= -R*Pow(v,n)*vyprev - m*G;
                ax = Fx / m;
                ay = Fy / m;
                vx = vxprev + ax * dt2;
                vy = vyprev + ay * dt2;
                x = xprev + vx * dt2 + ax * Pow(dt2, 2) / 2;
                y = yprev + vy * dt2 + ay * Pow(dt2, 2) / 2;
                if (x >= xmax)
                {
                    xmax = x;
                }

                if (y >= ymax)
                {
                    ymax = y;
                }

                xprev = x;
                yprev = y;
                t = t + dt2;
                vxprev = vx;
                vyprev = vy;


            }

            MessageBox.Show($"xmax={xmax} ymax={ymax}");
            buttonStart.Enabled = true;
            buttonStop.Enabled = true;
            axis1.Axis_Type = 3;
            axis1.x_Base = (float)(xmax * 1.5);
            axis1.y_Base = (float)(ymax * 1.5);
            axis1.Pix_type = 5;
            axis1.Pix_Size = (float)0.05;
            axis1.AxisDraw();
            axis1.StatToDin();
            axis1.PixDraw(10, 10, Color.Navy, 2);
            axis1.DinToPic();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            x0 = 0; 
            Double.TryParse(textBoxY.Text.Replace('.',','), out y0);
            Double.TryParse(textBoxV.Text.Replace('.',','), out v0);
            Double.TryParse(textBoxAngle.Text.Replace('.',','), out angle);
            Double.TryParse(textBoxMass.Text.Replace('.',','), out m);
            Double.TryParse(textBoxResistanceMultiplier.Text.Replace('.',','), out R);
            Double.TryParse(textBoxResistPow.Text.Replace('.',','), out n);
            Double.TryParse(textBoxDelta.Text.Replace('.',','), out dt);
            vx0 = v0 * Cos(angle*PI/180);
            vy0 = v0 * Sin(angle*PI/180);
            buttonPreStart.Enabled = true;
            

        }
    }
}
