using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketTrisServer
{
    internal class Controller
    {
        private Model _model;
        private List<VirtualView> _views;

        public Controller(Model model)
        {
            _model = model;
            _views = new List<VirtualView>();
        }

        public void AddView(VirtualView view)
        {
            _views.Add(view);
            _model.AddView(view);

            // todo add listeners
        }

        public void Start()
        {

            foreach (var v in _views)
            {
                v.SendMessage(new Message { MessageCode = Message.Code.StartGame });
            }
        }
    }
}
