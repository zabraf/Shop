using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    enum Status
    {
        isShopping,
        wantToBeInLine,
        WantToCheckout,
        isFinished
    }
    class Client
    {
        private const int MIN_SECOND = 30;
        private const int MAX_SECOND = 300;
        private int _timeSecond;
        private int speed;
        private RectangleF _rectangle;
        private PointF _destination;
        private Brush _brush;
        private float _tolerance = 1F;
        private RectangleF _zoneShopping;
        private Status _status;

        private int _NumberOfMove;
        static Random rdm = new Random();

        public PointF Destination { get => _destination; set => _destination = value; }
        public RectangleF Rectangle { get => _rectangle; set => _rectangle = value; }
        public int TimeSecond { get => _timeSecond; set => _timeSecond = value; }
        internal Status Status { get => _status; set => _status = value; }

        public Client(RectangleF rectangle, RectangleF zoneShopping)
        {
            Status = Status.isShopping;
            _zoneShopping = zoneShopping;
            Destination = new PointF(rectangle.X, rectangle.Y);
            Rectangle = rectangle;
            TimeSecond = rdm.Next(MIN_SECOND, MAX_SECOND + 1);
            _NumberOfMove = TimeSecond / 10;
            speed = 1;
            int colorRgbValue = (int)((double)(TimeSecond - 30) / (MAX_SECOND - 30) * 255);
            _brush = new SolidBrush(Color.FromArgb(colorRgbValue, colorRgbValue, colorRgbValue));
        }
        private bool Move()
        {
            float diffx = Destination.X - Rectangle.X;
            float diffy = Destination.Y - Rectangle.Y;
            float angle = (float)(Math.Atan2(diffy, diffx));
            if (Math.Sqrt(Math.Pow(diffx, 2) + Math.Pow(diffx, 2)) > _tolerance)
            {
                _rectangle.X += speed * (float)Math.Cos(angle);
                _rectangle.Y += speed * (float)Math.Sin(angle);
                return false;
            }
            return true;
        }
        public void Draw(object sender, Graphics graphicsObj, Checkout lastOpenCheckout)
        {
            switch (Status)
            {
                case Status.isShopping:
                    if (Move())
                    {
                        if (_NumberOfMove <= 0)
                        {
                            Status = Status.wantToBeInLine;
                        }
                        _NumberOfMove--;
                        Destination = new PointF(rdm.Next(Convert.ToInt32(_zoneShopping.X), Convert.ToInt32(_zoneShopping.Width + _zoneShopping.X - Rectangle.Width)), rdm.Next(Convert.ToInt32(_zoneShopping.Y), Convert.ToInt32(_zoneShopping.Height + _zoneShopping.Y - Rectangle.Height)));
                    }
                    break;

                case Status.wantToBeInLine:
                    Destination = lastOpenCheckout.LastPosition;
                    if (Move())
                    {
                        if (lastOpenCheckout.AddClient(this))
                        {
                            Status = Status.WantToCheckout;
                        }
                        else
                        {
                            Destination = new PointF(rdm.Next(Convert.ToInt32(_zoneShopping.X), Convert.ToInt32(_zoneShopping.Width + _zoneShopping.X - Rectangle.Width)), rdm.Next(Convert.ToInt32(_zoneShopping.Y), Convert.ToInt32(_zoneShopping.Height + _zoneShopping.Y - Rectangle.Height)));
                        }
                    }
                    break;

                case Status.WantToCheckout:
                    Move();
                    break;

                case Status.isFinished:
                    Destination = new PointF(0, 0);
                    Move();
                    break;

                default:
                    throw new System.ArgumentException("_status cannot be anything else");
            }
            graphicsObj.FillEllipse(_brush, Rectangle);
        }
    }
}
