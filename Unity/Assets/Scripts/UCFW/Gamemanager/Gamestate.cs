namespace UCFW
{
    public abstract class Gamestate
    {
        public Gamestate(GamestateDesc desc)
        {
            _sceneName = desc.sceneName;
            _sceneRequired = desc.sceneName != string.Empty;
            _preload = desc.preload;
            _reloadRequired = desc.reloadRequired;
            _buildIndex = desc.buildIndex;
        }

        private string _sceneName = string.Empty;
        private bool _sceneRequired = false;
        private bool _preload = false;
        private bool _reloadRequired = false;
        private int _buildIndex = -1;

        public string sceneName
        {
            get { return _sceneName; }
        }

        public bool sceneRequired
        {
            get { return _sceneRequired; }
        }

        public bool preload
        {
            get { return _preload; }
        }

        public bool reloadRequired
        {
            get { return _reloadRequired; }
        }

        [System.Obsolete("Does not work correctly. Use sceneName instead")]
        public int buildIndex
        {
            get { return _buildIndex; }
        }

        public virtual bool Init() { return true; }
        public virtual void OnUpdate() { }
        public virtual void Loop() { }
        public virtual void DeInit() { }
    }
}
