using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

public class Player
{
    public int x = 400;
    public int y = 500;
    private const int OFFSET_X = 13;
    private const int OFFSET_Y = 20;
    public Bitmap bitmap;
    public Player()
    {
        bitmap = new Bitmap(@"2.png");
    }
    public void key_ctrl(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Up) { y = y - 5; }
        if (e.KeyCode == Keys.Down) { y =y +5; }
        if (e.KeyCode == Keys.Left) { x = x - 5; }
        if (e.KeyCode == Keys.Right) { x = x + 5; }
    }
    public void draw(Graphics g)
    {
        g.DrawImage(this.bitmap, x - OFFSET_X, y - OFFSET_Y, this.bitmap.Width, this.bitmap.Height);
    }

    public int collision_ray = 50;

    public int[][] box = new int[5][] // 以Player角色中心为坐标，设置模型外部5个碰撞检测点的相对坐标
    {
        new int[] { -6, -19 },
        new int[] {  6, -21 },
        new int[] {  9,   8 },
        new int[] {  1,  21 },
        new int[] { -8,   7 }
    };

    public bool isCollided(int[][] bBox, int bx, int by)
    {
        return Player.isCollided(this.box, this.x, this.y, bBox, bx, by); // 将调用静态函数，
    }

    public static bool isCollided(int[][] aBox, int ax, int ay, int[][] bBox, int bx, int by)
    {
        foreach(int[] p in aBox)
        {
            if (isInBox(bBox, bx, by, absolutePosition(p, ax, ay))) return true;
        }
        foreach (int[] p in bBox)
        {
            if (isInBox(aBox, ax, ay, absolutePosition(p, bx, by))) return true;
        }
        return false;
    }

    private static bool isInBox(int[][] box, int x, int y, int[] p)
    {
        int n = box.GetLength(0);
       for(int i=0; i<n; i++)
        {
            
            if (!isInAngle(p, absolutePosition(box[i % n], x, y),
                absolutePosition(box[(i+1) % n], x, y),
                absolutePosition(box[(i+2) % n], x, y))) return false; 
        }
        return true;
    }
    private static bool isInAngle(int[] p, int[] a, int[] b, int[] c)
    {
        int[] BA = makeVector(b, a);
        int[] BP = makeVector(b, p);
        int[] BC = makeVector(b, c);

        int BAP = outerProduct(BA, BP);
        int BCP = outerProduct(BC, BP);

        return BAP * BCP <= 0; // 异号（夹在内部）或为 0（恰好在边上） 如果相碰返回true,没有返回false
    }

    private static int outerProduct(int[] a, int[] b)
    {
        return a[0] * b[1] - b[0] * a[1]; // 向量叉乘
    }

    private static int[] makeVector(int[] a,int[] b)
    {
        return new int[] { b[0] - a[0], b[1] - a[1] }; // 生成向量
    }

    private static int[] absolutePosition(int[] p, int x, int y)
    {
        return new int[] { p[0] + x, p[1] + y }; // 将相对坐标转换成绝对坐标
    }
}