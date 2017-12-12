using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCityIN1CTT
{
    /*
        A terület mellé megjegyzünk egy sebességet és egy irányt. Példányosításkor kötelező megadni.
        A sebesség kétszerese lesz, mivel egy közös sebességgel fognak mozogni a tankok, a lövedék gyorsabb lesz.
        A megadott irányba fog mozogni, amíg nem ütközik falba vagy az ellenséges tankba.
    */
    class BulletBL
    {
        MyShape bullet;
        Direction irany;
        int speed;

        public BulletBL(MyShape bullet, Direction irany, int speed)
        {
            this.bullet = bullet;
            this.irany = irany;
            this.speed = speed*2;
        }

        public MyShape Bullet
        {
            get
            {
                return bullet;
            }
        }

        public bool Move(int palyaW, int palyaH)
        {
            switch (irany)
            {
                case Direction.Up:
                    bullet.ChangeY(-speed); break;
                case Direction.Down:
                    bullet.ChangeY(speed); break;
                case Direction.Left:
                    bullet.ChangeX(-speed); break;
                case Direction.Right:
                    bullet.ChangeX(speed); break;
                case Direction.Downleft:
                    bullet.ChangeX(-speed / 2);
                    bullet.ChangeY(speed / 2);
                    break;
                case Direction.DownRight:
                    bullet.ChangeX(speed / 2);
                    bullet.ChangeY(speed / 2);
                    break;
                case Direction.Upleft:
                    bullet.ChangeX(-speed / 2);
                    bullet.ChangeY(-speed / 2); break;
                case Direction.UpRight:
                    bullet.ChangeX(speed / 2);
                    bullet.ChangeY(-speed / 2);
                    break;
            }
            if (bullet.Area.Top<= 0 || bullet.Area.Left<= 0 || bullet.Area.Bottom >= palyaH || bullet.Area.Right >= palyaW)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
