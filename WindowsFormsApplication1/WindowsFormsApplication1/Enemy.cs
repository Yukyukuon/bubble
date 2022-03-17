using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsFormsApplication1
{
     public class Enemy
    {
        public const int RADIUS = 18;

        public int a = 100, b = 100;
        public Bitmap bitmap;
        public int[][] box;
        public Enemy(int x,int y)
        {
            this.a = x;
            this.b = y;
            this.bitmap = new Bitmap(@"1.png");
            this.box = this.getBox();
            SetaTimerParam();
            SetcTimerParam();//构造函数时执行一次Timer触发
        }
        public void display(Graphics g)
        {
            g.DrawImage(this.bitmap, this.a - this.bitmap.Width / 2, this.b - this.bitmap.Height / 2, this.bitmap.Width, this.bitmap.Height);
        }

        System.Timers.Timer aTimer = new System.Timers.Timer();  //实体化Timer类
        private void testa(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.a <= 0)
            {
                aTimer.Enabled = false;
                bTimer.Enabled = true;
                SetbTimerParam();
            }
            else
            {
                aTimer.Enabled = true;
                this.a -= 2;
                //aTimer.Elapsed -= new ElapsedEventHandler(testa);
            }
            
        }
        public void SetaTimerParam()
        {
            aTimer.Elapsed += new ElapsedEventHandler(testa);
            aTimer.Interval = 10;
            aTimer.AutoReset = true;//执行一次Flase ，一直执行true
            aTimer.Enabled = true;
        }


        System.Timers.Timer bTimer = new System.Timers.Timer();
        public void SetbTimerParam()
        {
            bTimer.Elapsed += new ElapsedEventHandler(testb);
            bTimer.Interval = 10;
            bTimer.AutoReset = true;//执行一次Flase ，一直执行true
            bTimer.Enabled = true;
        }
        private void testb(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.a >= 730)
            {
                bTimer.Enabled = false;
                aTimer.Enabled = true;
                SetaTimerParam();
            }
            else
            {
                bTimer.Enabled = true;
                this.a += 2;
                //bTimer.Elapsed -= new ElapsedEventHandler(testb);
            }
             
        }



        System.Timers.Timer cTimer = new System.Timers.Timer();
        public void SetcTimerParam()
        {
            cTimer.Elapsed += new ElapsedEventHandler(testc);
            cTimer.Interval = 10;
            cTimer.AutoReset = true;//执行一次Flase ，一直执行true
            cTimer.Enabled = true;
        }
        private void testc(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.b <= 0)
            {
                cTimer.Enabled = false;
                dTimer.Enabled = true;
                SetdTimerParam();
            }
            else
            {
                cTimer.Enabled = true;
                this.b -= 2;
            }
        }



        System.Timers.Timer dTimer = new System.Timers.Timer();
        public void SetdTimerParam()
        {
            dTimer.Elapsed += new ElapsedEventHandler(testd);
            dTimer.Interval = 10;
            dTimer.AutoReset = true;//执行一次Flase ，一直执行true
            dTimer.Enabled = true;
        }
        private void testd(object source, System.Timers.ElapsedEventArgs e)
        {
            if (this.b >= 587)
            {
                dTimer.Enabled = false;
                cTimer.Enabled = true;
                SetcTimerParam();
            }
            else
            {
                dTimer.Enabled = true;
                this.b += 2;
            }
        }



        public int[][] getBox()
        {
            const int N_POINT = 36;
            int[][] box = new int[N_POINT][];
            for (int i = 0; i < N_POINT; i++)
            {
                double x = RADIUS * Math.Cos(i * (360 / (double)N_POINT) * (Math.PI / 180));  // 测量大致半径为9个像素点
                double y = RADIUS * Math.Sin(i * (360 / (double)N_POINT) * (Math.PI / 180));
                box[i] = new int[2] {Convert.ToInt32(x), Convert.ToInt32(y)}; 
            }
            return box;
        }

        
    }
}

