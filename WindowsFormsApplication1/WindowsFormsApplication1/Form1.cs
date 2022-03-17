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
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class Form1 : Form
    {
        public static Form1 form1;
        public Form1()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
            form1 = this;
        }
        Player player = new Player();
        Enemy[] enemy = new Enemy[5];

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) nowGeme = 1;
            else player.key_ctrl(e);
        }
        System.Timers.Timer bTimer = new System.Timers.Timer();
        private void test2(object source, System.Timers.ElapsedEventArgs e)
        {
            Draw();
            if (player.isCollided(enemy[0].box, enemy[0].a, enemy[0].b)) nowGeme = 2;
            if (player.isCollided(enemy[1].box, enemy[1].a, enemy[1].b)) nowGeme = 2;
            if (player.isCollided(enemy[2].box, enemy[2].a, enemy[2].b)) nowGeme = 2;
            if (player.isCollided(enemy[3].box, enemy[3].a, enemy[3].b)) nowGeme = 2;
            if (player.isCollided(enemy[4].box, enemy[4].a, enemy[4].b)) nowGeme = 2;
            else form1.Text = player.x.ToString();
        }
        public void SetTimerParam()
        {
            bTimer.Elapsed += new ElapsedEventHandler(test2);
            bTimer.Interval = 10;
            bTimer.AutoReset = true;
            bTimer.Enabled = true;
        }
        private void Draw()
        {             //双缓存解决图片闪烁
                      //创建在PictueBox 1上的图形g1
            Graphics g1 = pictureBox1.CreateGraphics();
            //图形画在内存上
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            //自定义绘图
            player.draw(g);
            enemy[0].display(g);
            enemy[1].display(g);
            enemy[2].display(g);
            enemy[3].display(g);
            enemy[4].display(g);
            //enemy[1].display(g);
            //显示图像并释放内存
            myBuffer.Render();
            myBuffer.Dispose();
            //旧方法GDI+绘图
            /*Bitmap bitmap = new Bitmap(@"2.png");
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawImage(bitmap, x, y);*/
        }
 



        private void gameOver()
        {
            System.Environment.Exit(0);
        }
        System.Timers.Timer mainTimer = new System.Timers.Timer();
        private void Maintest(object source, System.Timers.ElapsedEventArgs e)
        {
            switch (nowGeme)
            {
                case 0:
                    gameLoad();
                    break;
                case 1:
                    SetTimerParam();
                    break;
                case 2:
                    gameOver();
                    break;
            }
        }
        public void SetMainTimerParam()
        {
            mainTimer.Elapsed += new ElapsedEventHandler(Maintest);
            mainTimer.Interval = 10;
            mainTimer.AutoReset = true;
            mainTimer.Enabled = true;
        }
        private static void gameLoad()
        {
                form1.Text = " v1.0";

                Bitmap bit = new Bitmap(@"main.jpg");
                Graphics g = form1.pictureBox1.CreateGraphics();
                g.DrawImage(bit, 0, 0,2*(bit.Width)/3,2*(bit.Height)/3);
        }
        public int nowGeme = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.CenterToScreen();
            SetMainTimerParam();

            enemy[0] = new Enemy(0, 0);
            enemy[0].bitmap = new Bitmap(@"1.png");

            enemy[1] = new Enemy(100, 200);
            enemy[1].bitmap = new Bitmap(@"1.png");

            enemy[2] = new Enemy(700,500);
            enemy[2].bitmap = new Bitmap(@"1.png");

            enemy[3] = new Enemy(300, 0);
            enemy[2].bitmap = new Bitmap(@"1.png");

            enemy[4] = new Enemy(0, 500);
            enemy[4].bitmap = new Bitmap(@"1.png");
        }
    }
}
