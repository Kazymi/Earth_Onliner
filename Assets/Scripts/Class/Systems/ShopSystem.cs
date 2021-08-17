    public class ShopSystem
    {
        private int _lastCanvasId;
        private int _currentIdCanvas;

        public ShopSystem(int lastCanvasId)
        {
            _lastCanvasId = lastCanvasId;
        }
        
        public int NextShop()
        {
            _currentIdCanvas++;
            if (_currentIdCanvas > _lastCanvasId)
            {
                _currentIdCanvas = 0;
            }

            return _currentIdCanvas;
        }

        public int LastShop()
        {
            _currentIdCanvas--;
            if (_currentIdCanvas < 0)
            {
                _currentIdCanvas = _lastCanvasId;
            }

            return _currentIdCanvas;
        }
    }