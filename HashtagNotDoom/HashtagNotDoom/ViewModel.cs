using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCityIN1CTT
{
    /*
        A megjelenítés itt történik. Minden itt van, ami megjelenik a játékban(kivétel a lövedékek).
        Itt valósítjuk meg az életet és a pontozást.
    */
    class ViewModel : Bindable
    {
        const int karakter = 50;

        int life, score;

        public MyShape PlayerS { get; private set; }

        public MyShape MinionS { get; private set; }


        public ViewModel(int palyaW, int palyaH)
        {
            life = 3;
            score = 0;
            PlayerS = new MyShape(palyaW / 2 - karakter / 2, palyaH - karakter, karakter, karakter);
            MinionS = new MyShape(0, 0, karakter, karakter);
        }

        public int Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
                OnPropertyChanged();
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                OnPropertyChanged();
            }
        }
    }
}
