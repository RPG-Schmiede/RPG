using UnityEngine;
using UCFW;

public class AudioButton : BetterBehaviour
{
    static GameObject _localInstance = null;
    /// <summary>
    /// The audiobutton in the current scene
    /// </summary>
    public static GameObject localInstance
    {
        get
        {
            if(_localInstance == null)
            {
                AudioButton b = FindObjectOfType<AudioButton>();
                if(b == null)
                {
                    _localInstance = null;
                }
                else
                {
                    _localInstance = b.gameObject;
                }
            }

            return _localInstance;
        }
    }

    protected override void OnAwake()
    {
        _localInstance = gameObject;

        UnityEngine.UI.Button b;
        if(this.HasComponent(out b))
        {
            b.onClick.AddListener(Toggle);
        }
    }

    void Toggle()
    {
        AudioManager.instance.Toggle();
    }
}