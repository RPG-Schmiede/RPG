using UnityEngine;

namespace UCFW
{
    public struct PlayerPrefString
    {
        public PlayerPrefString(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public PlayerPrefString(string key)
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
            PlayerPrefs.SetString(_key, _value);
            PlayerPrefs.Save();
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
            if (PlayerPrefs.HasKey(_key))
            {
                _value = PlayerPrefs.GetString(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator string(PlayerPrefString s)
        {
            return s._value;
        }
    }

    public struct PlayerPrefInt
    {
        public PlayerPrefInt(string key, int value)
        {
            _key = key;
            _value = value;
        }

        public PlayerPrefInt(string key)
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
            PlayerPrefs.SetInt(_key, _value);
            PlayerPrefs.Save();
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
            if (PlayerPrefs.HasKey(_key))
            {
                _value = PlayerPrefs.GetInt(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator int(PlayerPrefInt i)
        {
            return i._value;
        }
    }

    public struct PlayerPrefBool
    {
        public PlayerPrefBool(string key, bool value)
        {
            _key = key;
            _value = value?1:0;
        }

        public PlayerPrefBool(string key)
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

        public bool value
        {
            get { return _value == 1; }
            set { _value = value?1:0; }
        }

        public void Save()
        {
            PlayerPrefs.SetInt(_key, _value);
            PlayerPrefs.Save();
        }

        public void Save(bool value)
        {
            this.value = value;
            Save();
        }

        public bool Load()
        {
            return Load(value);
        }

        public bool Load(bool defaultValue)
        {
            if (PlayerPrefs.HasKey(_key))
            {
                _value = PlayerPrefs.GetInt(_key);
                return true;
            }
            else
            {
                value = defaultValue;
                return false;
            }
        }

        public static implicit operator bool(PlayerPrefBool b)
        {
            return b.value;
        }
    }

    public struct PlayerPrefFloat
    {
        public PlayerPrefFloat(string key, float value)
        {
            _key = key;
            _value = value;
        }

        public PlayerPrefFloat(string key)
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
            PlayerPrefs.SetFloat(_key, _value);
            PlayerPrefs.Save();
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
            if (PlayerPrefs.HasKey(_key))
            {
                _value = PlayerPrefs.GetFloat(_key);
                return true;
            }
            else
            {
                _value = defaultValue;
                return false;
            }
        }

        public static implicit operator float(PlayerPrefFloat f)
        {
            return f._value;
        }
    }
}