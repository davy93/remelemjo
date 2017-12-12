using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCityIN1CTT
{
    /*
        Az osztály nagymértékben hasonlít a Játékos tank osztályához. Az irány véletlenszerűen beállítódik létrehozáskor.
        Mozgáskor elmozdulás előtt elmentjük tartózkodási pontját. Ha történik elmozdulás a pályán a megfelelő irányba,
        felvesszük az új helyzet pontját. ha megegyezik az előző és az újpont, nem volt elmozdulás, ilyenkor iránytváltoztatunk.
        A lövés, mint a PlayerSnál.
    */
    class Minion
    {
        MyShape minionS;
        Direction irany;
        int speed;

        public MyShape MinionS
        {
            get
            {
                return minionS;
            }
        }

        public Direction Irany { get => irany; set => irany = value; }

        Random random;
        Array veletlenIrany;

        public Minion(MyShape minions, int speed)
        {
            this.minionS = minions;
            this.speed = speed;
            random = new Random();
            
        }
        public void spawn()
        {

        }
        public void Move(int palyaW, int palyaH)
        {
            if (Irany == Direction.Up && minionS.Area.Top - speed >= 0)
            {
                minionS.ChangeY(-speed);
            }
            else if (Irany == Direction.Down && minionS.Area.Bottom + speed <= palyaH)
            {

                minionS.ChangeY(speed);
            }
            else if (Irany == Direction.Left && minionS.Area.Left - speed >= 0)
            {

                minionS.ChangeX(-speed);
            }
            else if (Irany == Direction.Right && minionS.Area.Right + speed <= palyaW)
            {

                minionS.ChangeX(speed);
            }
            else if (Irany == Direction.Downleft && minionS.Area.Bottom + speed <= palyaH)
            {
                minionS.ChangeY(speed);
                minionS.ChangeX(-speed);
            }
            else if (Irany == Direction.DownRight && minionS.Area.Bottom + speed <= palyaH)
            {
                minionS.ChangeY(speed);
                minionS.ChangeX(speed);
            }
            else if (Irany == Direction.UpRight && minionS.Area.Bottom + speed <= palyaH)
            {
                minionS.ChangeY(-speed);
                minionS.ChangeX(speed);
            }
            else if (Irany == Direction.Upleft && minionS.Area.Bottom + speed <= palyaH)
            {
                minionS.ChangeY(-speed);
                minionS.ChangeX(-speed);
            }

        }    
    }
}
