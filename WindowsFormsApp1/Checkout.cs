using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Checkout
    {
        private bool _isActive;
        private List<Client> _clients;
        private Rectangle _rectangle;
        private Brush _inactive;
        private Brush _active;
        private PointF _lastPosition;
        private int _time;

        internal List<Client> Clients { get => _clients; set => _clients = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }
        public Rectangle Rectangle { get => _rectangle; set => _rectangle = value; }
        public PointF LastPosition { get => _lastPosition; set => _lastPosition = value; }

        public Checkout(Rectangle rectangle)
        {
            _inactive = new SolidBrush(Color.FromArgb(0,0,51));
            _active = new SolidBrush(Color.FromArgb(0,0,255));
            Rectangle = rectangle;
            LastPosition = new PointF(Rectangle.X, Rectangle.Y);
            IsActive = false;
            Clients = new List<Client>();
            _time = 0;
        }
        public bool AddClient(Client client)
        {
            if(Clients.Count() >= 5)
            {
                return false;
            }
            else
            {
                LastPosition = new PointF(LastPosition.X - client.Rectangle.Width * 1.1F, LastPosition.Y);
                Clients.Add(client);
                return true;
            }
        }
        public void RemoveClient()
        {
            Clients[0].Status = Status.isFinished;
            LastPosition = new PointF(LastPosition.X + Clients[0].Rectangle.Width * 1.1F, LastPosition.Y);
            Clients.RemoveAt(0);
            foreach (Client cl in Clients)
            {
                cl.Destination = new PointF(cl.Destination.X + cl.Rectangle.Width * 1.1F, cl.Destination.Y);
            }
        }
        public bool Scan()
        {
            if(Clients[0] != null)
            { 
                if (_time >= Clients[0].TimeSecond)
                {
                    RemoveClient();
                    return true;
                }
                else
                {
                    _time++;
                }
            }
            return false;

        }
        public void Draw(object sender, Graphics graphicsObj)
        {
            graphicsObj.FillRectangle((IsActive?_active:_inactive), Rectangle);
        }
    }
}
