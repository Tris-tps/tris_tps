namespace WebSocketTrisServer
{
    public class Model
    {
        private List<VirtualView> _views;
        private TrisGame _trisGame;
        public int Turn { get; set; }

        public Model()
        {
            _views = new List<VirtualView>();
            _trisGame = new TrisGame();
            Turn = 0;
        }

        public void AddView(VirtualView view)
        {
            _views.Add(view);
        }

        public void OnModelUpdate()
        {
            //TODO
        }
    }
}
