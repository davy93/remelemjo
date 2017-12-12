using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCityIN1CTT
{
    /*
        A terület és sebesség mellett létrehozáskor beállítunk egy előző irány, mely a lövésben segít.
        Kezdőpozícióban a játékos felfelé néz, ezért Direction.Up a kezdeti irány.
        Az előző irány a mozgás metódustól kapottra módosul. Ha a pálya területén van a következő pozíció ahova mozdulni
        szeretnénk, akkor megtörténik az elmozdulás a megfelelő irányba.
        Lövéskor a lövedék az előző irány pozíciójától függően jön létre a Tank megfelelő oldalához viszonyítva és
        hozzáadódik a gyűjteményhez.
    */
    class Player
    {
        MyShape playerS;
        Direction elozoIrany;
        int speed;
        ObservableCollection<BulletBL> bullets;

        public MyShape PlayerS
        {
            get
            {
                return playerS;
            }
        }

        public ObservableCollection<BulletBL> Bullets
        {
            get
            {
                return bullets;
            }
        }

        public Player(MyShape playerS, int speed)
        {
            this.playerS = playerS;
            this.speed = speed;
            elozoIrany = Direction.Up;
            bullets = new ObservableCollection<BulletBL>();
        }

        public void Move(int palyaW, int palyaH, Direction irany)
        {
            elozoIrany = irany;
            if (irany == Direction.Up && PlayerS.Area.Top - speed >= 0)
            {
                PlayerS.ChangeY(-speed);
            }
            else if (irany == Direction.Down && PlayerS.Area.Bottom + speed <= palyaH)
            {
                PlayerS.ChangeY(speed);
            }
            else if (irany == Direction.Left && PlayerS.Area.Left - speed >= 0)
            {
                PlayerS.ChangeX(-speed);
            }
            else if (irany == Direction.Right && PlayerS.Area.Right + speed <= palyaW)
            {
                PlayerS.ChangeX(speed);
            }
            else if (irany == Direction.UpRight && PlayerS.Area.Right + speed <= palyaW && PlayerS.Area.Top <= palyaH)
            {
                PlayerS.ChangeX(speed/2);
                PlayerS.ChangeY(-speed/2);
            }
            else if (irany == Direction.Upleft && PlayerS.Area.Left - speed >= 0 && PlayerS.Area.Top <= palyaH)
            {
                PlayerS.ChangeX(-speed/2);
                PlayerS.ChangeY(-speed/2);
            }
            else if (irany == Direction.Downleft && PlayerS.Area.Left - speed >= 0 && PlayerS.Area.Bottom >= 0)
            {
                PlayerS.ChangeX(-speed/2);
                PlayerS.ChangeY(speed/2);
            }
            else if (irany == Direction.DownRight && PlayerS.Area.Right + speed <= palyaW && PlayerS.Area.Bottom >= 0)
            {
                PlayerS.ChangeX(speed/2);
                PlayerS.ChangeY(speed/2);
            }
        }

        public void Shoot()
        {
            MyShape bullet = null;
            switch (elozoIrany)
            {
                case Direction.Up:
                    {
                        bullet = new MyShape((int)(PlayerS.Area.X + PlayerS.Area.Width / 2 - 2), (int)PlayerS.Area.Y, 5, 5);
                        break;
                    }
                case Direction.Down:
                    {
                        bullet = new MyShape((int)(PlayerS.Area.X + PlayerS.Area.Width / 2 - 2), (int)(PlayerS.Area.Y + PlayerS.Area.Height), 5, 5);
                        break;
                    }
                case Direction.Left:
                    {
                        bullet = new MyShape((int)(PlayerS.Area.X), (int)(PlayerS.Area.Y + PlayerS.Area.Height / 2 - 2), 5, 5);
                        break;
                    }
                case Direction.Right:
                    {
                        bullet = new MyShape((int)PlayerS.Area.X + (int)PlayerS.Area.Width, (int)PlayerS.Area.Y + ((int)PlayerS.Area.Height / 2 - 2), 5, 5);
                        break;
                    }
                case Direction.UpRight:
                    {
                        bullet = new MyShape((int)PlayerS.Area.X + (int)PlayerS.Area.Width, (int)PlayerS.Area.Y + ((int)PlayerS.Area.Height / 2 - 2), 5, 5);
                        break;
                    }
                case Direction.Upleft:
                    {
                        bullet = new MyShape((int)PlayerS.Area.X + (int)PlayerS.Area.Width, (int)PlayerS.Area.Y + ((int)PlayerS.Area.Height / 2 - 2), 5, 5);
                        break;
                    }
                case Direction.DownRight:
                    {
                        bullet = new MyShape((int)PlayerS.Area.X + (int)PlayerS.Area.Width, (int)PlayerS.Area.Y + ((int)PlayerS.Area.Height / 2 - 2), 5, 5);
                        break;
                    }
                case Direction.Downleft:
                    {
                        bullet = new MyShape((int)PlayerS.Area.X + (int)PlayerS.Area.Width, (int)PlayerS.Area.Y + ((int)PlayerS.Area.Height / 2 - 2), 5, 5);
                        break;
                    }
            }
            bullets.Add(new BulletBL(bullet, elozoIrany, speed));
        }
    }
}
