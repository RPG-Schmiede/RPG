using UnityEngine;
using UnityEngine.UI;
using UCFW;

public class AudioManager : Singleton<AudioManager>
{
    public enum AudioChannel
    {
        BackgroundTrack = 0,
        BackgroundInterlude,
        WeaponSound,
        Default,
        // Default has to be last element
    }

    [System.Serializable]
    public struct BackgroundTrackState
    {
        public string name;
        public AudioClip clip;
    }

    public int defaultChannelCount = 3;
    public BackgroundTrackState[] backgroundTracks = null;

    private AudioSource[] sources = null;

    private Image[] buttonImages = null;

    private PlayerPrefBool audioOn = new PlayerPrefBool("audioOn", true);

    private bool destroyed = false;

    static bool _isPlaying = false;

    protected override void OnAwake()
    {
        AudioListener.volume = 1.0f;

        if (AudioButton.localInstance != null)
        {
            buttonImages = AudioButton.localInstance.GetComponentsInChildren<Image>();
        }

        audioOn.Load();

        SetAudio();
        
        CreateAudioSourcesIfNecessary();
    }

    protected override void OnDestroyed()
    {
        OnApplicationQuit();
    }

    private void CreateAudioSourcesIfNecessary()
    {
        if (sources == null)
        {
            int numSources = System.Enum.GetValues(typeof(AudioChannel)).Length + defaultChannelCount - 1;
            GameObject go = transform.CreateEmptyChild("AudioSources").gameObject;

            sources = new AudioSource[numSources];

            for (int i = 0; i < numSources; ++i)
            {
                sources[i] = go.AddComponent<AudioSource>();
                sources[i].loop = false;
                sources[i].playOnAwake = false;
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (destroyed) return;

        audioOn.Save();

        destroyed = true;
    }

    private void OnChangeGamestate()
    {
        //SetAudio();
    }

    private void SetAudio()
    {
        if (AudioButton.localInstance != null)
        {
            buttonImages = AudioButton.localInstance.GetComponentsInChildren<Image>();
        }

        if (buttonImages != null)
        {
            float alpha = audioOn ? 1.0f : 0.7f;
            Color color;
            for (int i = 0; i < buttonImages.Length; ++i)
            {
                color = buttonImages[i].color;
                color.a = alpha;
                buttonImages[i].color = color;
            }
        }

        AudioListener.volume = audioOn ? 1.0f : 0.0f;
    }

    public void Toggle()
    {
        audioOn.value = !audioOn;

        SetAudio();
    }

    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, AudioChannel.Default);
    }

    public void PlaySound(AudioClip clip, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Default:
                for (int i = (int)channel; i < sources.Length; ++i)
                {
                    if (!sources[i].isPlaying)
                    {
                        sources[i].clip = clip;
                        sources[i].Play();
                        break;
                    }
                }
                break;
            default:
                sources[(int)channel].clip = clip;
                sources[(int)channel].Play();
                break;
        }
    }

    public void SetBackgroundTrackStateByName(string name)
    {
        for (int i = 0; i < backgroundTracks.Length; ++i)
        {
            if (backgroundTracks[i].name == name)
            {
                SetBackgroundTrackStateByIndex(i);
            }
        }
    }

    public void SetBackgroundTrackStateByIndex(int index)
    {
        if (index >= 0 && index < backgroundTracks.Length)
        {
            CreateAudioSourcesIfNecessary();
            AudioSource s = sources[(int)AudioChannel.BackgroundTrack];
            s.clip = backgroundTracks[index].clip;
            s.loop = true;
            s.Play();
        }
    }
}
