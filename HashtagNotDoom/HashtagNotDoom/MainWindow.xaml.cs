using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BattleCityIN1CTT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int speed = 5;

        int palyaW, palyaH; //Canvas=Pálya. Szélessége és magassága
        ViewModel VM; //Megjelenítés
        Player PL; //Működés PlayerS
        Minion EL; //Működés Minion

        DispatcherTimer TGame;
        DispatcherTimer TShoot;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Pálya szélesség és magasság
        //Relatív úton képpel töltjük ki a téglalapot. Összekapcsoljuk a megjelenítést a működéssel
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            palyaW = (int)MyCanvas.ActualWidth;
            palyaH = (int)MyCanvas.ActualHeight;

            Playr.Fill = new ImageBrush(new BitmapImage(new Uri("groot.png", UriKind.Relative)));
            VM = new ViewModel(palyaW, palyaH);
            DataContext = VM;

            PL = new Player(VM.PlayerS, speed);
            EL = new Minion(VM.MinionS, speed);

            //Játék számláló
            TGame = new DispatcherTimer();
            TGame.Interval = new TimeSpan(0, 0, 0, 0, 100);
            TGame.Tick += TGame_Tick;
            TGame.Start();

         

            //Lövés 2 másodpercenként
            TShoot = new DispatcherTimer();
            TShoot.Interval = new TimeSpan(0, 0, 0, 2);
            
            TShoot.Start();
        }

        private void TGame_Tick(object sender, EventArgs e)
        {
            //Mozog az ellenséges tank
            Point playerirany = new Point(PL.PlayerS.Area.X, PL.PlayerS.Area.Y);
            Point minionpont = new Point(EL.MinionS.Area.X, EL.MinionS.Area.Y);
            if (playerirany.X > minionpont.X && playerirany.Y > minionpont.Y)
            {
                EL.Irany = Direction.DownRight;
            }
            else if (playerirany.X < minionpont.X && playerirany.Y > minionpont.Y)
            {
                EL.Irany = Direction.Downleft;
            }
            else if (playerirany.X > minionpont.X && playerirany.Y < minionpont.Y)
            {
                EL.Irany = Direction.UpRight;
            }
            else if (playerirany.X < minionpont.X && playerirany.Y < minionpont.Y)
            {
                EL.Irany = Direction.Upleft;
            }
            else if (playerirany.X > minionpont.X && playerirany.Y == minionpont.Y)
            {
                EL.Irany = Direction.Right;
            }
            else if (playerirany.X < minionpont.X && playerirany.Y == minionpont.Y)
            {
                EL.Irany = Direction.Left;
            }
            else if (playerirany.X == minionpont.X && playerirany.Y < minionpont.Y)
            {
                EL.Irany = Direction.Up;
            }
            else if (playerirany.X == minionpont.X && playerirany.Y > minionpont.Y)
            {
                EL.Irany = Direction.Down;
            }
            EL.Move(palyaW, palyaH);

            //Játékos által leadott lövedékek mozognak, ha találnak, emelkedik a Score
            ObservableCollection<BulletBL> torlendo = new ObservableCollection<BulletBL>();
            foreach (BulletBL item in PL.Bullets)
            {
                bool v = item.Move(palyaW, palyaH);
                if (item.Bullet.Area.IntersectsWith(EL.MinionS.Area))
                {
                    VM.Score++;
                    EL.MinionS.SetXY(0, 0);
                    torlendo.Add(item);
                }
                if (v == true)
                {
                    torlendo.Add(item);
                }
            }
            foreach (BulletBL item in torlendo)
            {
                item.Bullet.SetXY(1000, 1000);
                PL.Bullets.Remove(item);
            }

            //Ellenfelek által leadott lövedékek mozognak, ha találnak, csökken az élet. Ha elfogy, Game Over.
        }
        /*
            A nyilakkal tudunk mozogni, Space-el lőni. Mozgáskor a karakter képe a megfelelő irányba fordul.
            Lövéskor létrehozzuk Bindingolva az elemet egy Ellipseként és hozzáadjuk a Pályánkhoz.
        */
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (Keyboard.IsKeyDown(Key.Up) && !Keyboard.IsKeyDown(Key.Right) && !Keyboard.IsKeyDown(Key.Left))
            {
                PL.Move(palyaW, palyaH, Direction.Up);
                Playr.RenderTransform = new RotateTransform(0, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            else if (Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Right) && !Keyboard.IsKeyDown(Key.Left))
            {
                PL.Move(palyaW, palyaH, Direction.Down);
                Playr.RenderTransform = new RotateTransform(180, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            else if (Keyboard.IsKeyDown(Key.Left) && !Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Up))
            {
                PL.Move(palyaW, palyaH, Direction.Left);
                Playr.RenderTransform = new RotateTransform(270, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            else if (Keyboard.IsKeyDown(Key.Right) && !Keyboard.IsKeyDown(Key.Down) && !Keyboard.IsKeyDown(Key.Up))
            {
                PL.Move(palyaW, palyaH, Direction.Right);
                Playr.RenderTransform = new RotateTransform(90, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            if ((Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Right)) || (Keyboard.IsKeyDown(Key.Right) && Keyboard.IsKeyDown(Key.Up)))
            {
                PL.Move(palyaW, palyaH, Direction.UpRight);
                Playr.RenderTransform = new RotateTransform(45, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            else if ((Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Left)) || (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Down)))
            {
                PL.Move(palyaW, palyaH, Direction.Downleft);
                Playr.RenderTransform = new RotateTransform(225, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            else if ((Keyboard.IsKeyDown(Key.Up) && Keyboard.IsKeyDown(Key.Left)) || (Keyboard.IsKeyDown(Key.Left) && Keyboard.IsKeyDown(Key.Up)))
            {
                PL.Move(palyaW, palyaH, Direction.Upleft);
                Playr.RenderTransform = new RotateTransform(315, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }
            else if ((Keyboard.IsKeyDown(Key.Down) && Keyboard.IsKeyDown(Key.Right)) || (Keyboard.IsKeyDown(Key.Right) && Keyboard.IsKeyDown(Key.Down)))
            {
                PL.Move(palyaW, palyaH, Direction.DownRight);
                Playr.RenderTransform = new RotateTransform(135, VM.PlayerS.Area.Width / 2, VM.PlayerS.Area.Height / 2);
            }

            else if (Keyboard.IsKeyDown(Key.Space))
            {
                //Egyszerre max 10 lövést adhat le
                if (PL.Bullets.Count < 10)
                {
                    PL.Shoot();
                    foreach (BulletBL item in PL.Bullets)
                    {
                        MyShape p = item.Bullet;
                        Ellipse r = new Ellipse();
                        r.DataContext = item.Bullet;
                        r.Fill = new ImageBrush(new BitmapImage(new Uri("bullet.png",UriKind.Relative)));
                        r.SetBinding(Canvas.LeftProperty, new Binding("Area.X"));
                        r.SetBinding(Canvas.TopProperty, new Binding("Area.Y"));
                        r.SetBinding(WidthProperty, new Binding("Area.Width"));
                        r.SetBinding(HeightProperty, new Binding("Area.Height"));
                        MyCanvas.Children.Add(r);
                    }
                }
            }
        }
        //Program bezárása
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
