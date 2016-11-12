using UnityEditor;

namespace UCFW.Editorscripts
{
    public struct EditorPrefString
    {
        public EditorPrefString(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public EditorPrefString(string key)
        {
            _key = key;
            _value = string.Empty;
        }

        private string _key;
        private string _value;

        public string key
        {
            get { return _key; }
        }

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void Save()
        {
            EditorPrefs.SetString(_key, _value);
        }

        public void Save(string value)
        {
            _value = value;
            Save();
        }

        public bool Load()
        {
            return Load(_value);
        }

        public bool Load(string defaultValue)
        {
            if (EditorPrefs.HasKey(_key))
            {
                _value = EditorPrefs.GetString(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator string(EditorPrefString s)
        {
            return s._value;
        }
    }

    public struct EditorPrefInt
    {
        public EditorPrefInt(string key, int value)
        {
            _key = key;
            _value = value;
        }

        public EditorPrefInt(string key)
        {
            _key = key;
            _value = 0;
        }

        private string _key;
        private int _value;

        public string key
        {
            get { return _key; }
        }

        public int value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void Save()
        {
            EditorPrefs.SetInt(_key, _value);
        }

        public void Save(int value)
        {
            _value = value;
            Save();
        }

        public bool Load()
        {
            return Load(_value);
        }

        public bool Load(int defaultValue)
        {
            if (EditorPrefs.HasKey(_key))
            {
                _value = EditorPrefs.GetInt(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator int(EditorPrefInt i)
        {
            return i._value;
        }
    }

    public struct EditorPrefBool
    {
        public EditorPrefBool(string key, bool value)
        {
            _key = key;
            _value = value;
        }

        public EditorPrefBool(string key)
        {
            _key = key;
            _value = false;
        }

        private string _key;
        private bool _value;

        public string key
        {
            get { return _key; }
        }

        public bool value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void Save()
        {
            EditorPrefs.SetBool(_key, _value);
        }

        public void Save(bool value)
        {
            _value = value;
            Save();
        }

        public bool Load()
        {
            return Load(_value);
        }

        public bool Load(bool defaultValue)
        {
            if (EditorPrefs.HasKey(_key))
            {
                _value = EditorPrefs.GetBool(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator bool(EditorPrefBool b)
        {
            return b._value;
        }
    }

    public struct EditorPrefFloat
    {
        public EditorPrefFloat(string key, float value)
        {
            _key = key;
            _value = value;
        }

        public EditorPrefFloat(string key)
        {
            _key = key;
            _value = 0.0f;
        }

        private string _key;
        private float _value;

        public string key
        {
            get { return _key; }
        }

        public float value
        {
            get { return _value; }
            set { _value = value; }
        }

        public void Save()
        {
            EditorPrefs.SetFloat(_key, _value);
        }

        public void Save(float value)
        {
            _value = value;
            Save();
        }

        public bool Load()
        {
            return Load(_value);
        }

        public bool Load(float defaultValue)
        {
            if (EditorPrefs.HasKey(_key))
            {
                _value = EditorPrefs.GetFloat(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator float(EditorPrefFloat f)
        {
            return f._value;
        }
    }
}