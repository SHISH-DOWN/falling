using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Math;


namespace falling
{
    
    public partial class Form1 : Form
    {
        public double x,y,xprev,yprev,x0,y0,v,v0,u,vx,vy,vxprev,vyprev,vx0,vy0,R,dt,n,Fx,Fy,m,dt2,xmax,ymax,tmax,angle,ax,ay,t,t1;
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

            while (xprev > 0)
            {
                Fx = -R * Pow(v, n - 1) * vx;
                Fy= -1*R*Pow(v,n - 1)*vy - m*G;
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

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            Double.TryParse(textBoxX.Text, out x0);
            Double.TryParse(textBoxY.Text, out y0);
            Double.TryParse(textBoxV.Text, out v0);
            Double.TryParse(textBoxAngle.Text, out angle);
            Double.TryParse(textBoxMass.Text, out m);
            Double.TryParse(textBoxResistanceMultiplier.Text, out R);
            Double.TryParse(textBoxResistPow.Text, out n);
            Double.TryParse(textBoxDelta.Text, out dt);
            vx0 = v0 * Cos(angle*PI/180);
            vy0 = v0 * Sin(angle*PI/180);
            buttonPreStart.Enabled = true;
            

        }
    }
}
