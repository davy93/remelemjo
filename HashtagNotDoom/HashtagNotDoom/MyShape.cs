using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BattleCityIN1CTT
{
    class MyShape : Bindable
    {
        /*
            Rect alapú terület, példányosításkor hozzuk létre. A terület csak olvasható tulajdonsággal rendelkezik.
            Metódusokkal tudjuk változtatni a helyzetét és ütközést vizsgálhatunk egy másik MyShape objektummal.
        */

        Rect area;

        public Rect Area
        {
            get
            {
                return area;
            }
        }

        public MyShape(int x, int y, int w, int h)
        {
            area = new Rect(x, y, w, h);
        }

        public void ChangeX(int speed)
        {
            area.X += speed;
            OnPropertyChanged("Area");
        }

        public void ChangeY(int speed)
        {
            area.Y += speed;
            OnPropertyChanged("Area");
        }

        public void SetXY(int newX, int newY)
        {
            area.X = newX;
            area.Y = newY;
            OnPropertyChanged("Area");
        }
    }
}
