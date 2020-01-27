using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Store
    {
        private const int NUMBER_START_CLIENT = 20;
        private const int NUMBER_CHECKOUT = 9;
        private List<Client> _clients;
        private List<Checkout> _checkouts;
        private Pen _wall;
        private Pen _arrival;
        static Random rdm;
        private RectangleF _zoneShopping;
        private int _height;
        private int _width;
        public Store(int width, int height)
        {
            _width = width;
            _height = height;
            _wall = new Pen(Color.DarkOrange, 50);
            _arrival = new Pen(Color.Green, 50);
            _checkouts = new List<Checkout>();
            _zoneShopping = new RectangleF(_wall.Width, _wall.Width, _width / 4 * 2, _height - _wall.Width * 2);
            rdm = new Random();
            for (int i = 0; i < NUMBER_CHECKOUT; i++)
            {
                _checkouts.Add(new Checkout(new Rectangle(_width - Convert.ToInt32(_wall.Width) - 100, Convert.ToInt32(_wall.Width) + 56 * i, 100, 50)));
            }
            _checkouts[0].IsActive = true;
            _clients = new List<Client>();
            for (int i = 0; i < NUMBER_START_CLIENT; i++)
            {
                _clients.Add(new Client(new RectangleF(rdm.Next(Convert.ToInt32(_wall.Width), _width / 4 * 2 - 50), rdm.Next(Convert.ToInt32(_wall.Width), _height - Convert.ToInt32(_wall.Width) - 50), 50, 50), _zoneShopping));
            }
        }
        public void Draw(object sender, Graphics graphicsObj)
        {
            //Draw Wall
            graphicsObj.DrawRectangle(_wall, 0 + _wall.Width / 2, 0 + _wall.Width / 2, _width - _wall.Width, _height - _wall.Width);
            //Draw arrival
            graphicsObj.DrawLine(_arrival, _width - _arrival.Width / 2, 0 + _wall.Width, _width - _arrival.Width / 2, _height - _wall.Width);
            foreach (var ck in _checkouts)
            {
                ck.Draw(sender, graphicsObj);
            }
            foreach (var cl in _clients)
            {
                cl.Draw(sender, graphicsObj,_checkouts[0]);
            }
        }
    }
}
