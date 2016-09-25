using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public delegate void SpecialCharFoundCallback(AudioClip audioClip);
public delegate void FinishedCallback();

public class UITextAnimator : MonoBehaviour 
{
	#region Variables
	private Text component;

	[Header("Setup")]
	public string content;

	[Header("Additional Settings")]
	public bool playOnEnable = true;
	public bool loop = true;
	public bool pingpong = false;
	public float duration = 0.1f;
    public bool useComponentText = true;

	public bool blinkingCursor = false;
	public string blinkingCursorChar = "";

	public string[] specialChars;
	public AudioClip[] specialCharsAudio;
	public bool adjustOnSoundLength = false;

	public Dictionary<char,AudioClip> audioMapping = new Dictionary<char, AudioClip>();

	// Runtime variables
	private bool directionForward = true;
	private int charIndex = 0;
	private int maxCharIndex = 0;
	private bool blinkingCursorCharToogle = true;

	private float nextCharPlacementTime = 0.0f;
	public event SpecialCharFoundCallback OnSpecialCharFoundCallback;
    public event FinishedCallback OnFinishedCallback;

    private bool animation = true;
	private AudioClip curAudioClip = null;

	#endregion

	#region Functions

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	private void OnEnable()
	{
		component = GetComponent<Text>();

        if (useComponentText)
        {
            content = component.text;
        }

        MapAudioToContent();

        //if(AudioManager.instance != null)
		//OnSpecialCharFoundCallback += AudioManager.instance.PlaySound;

		if (playOnEnable) 
		{
			StartAnimation();
		}
	}

	/// <summary>
	/// Starts the animation.
	/// </summary>
	public void StartAnimation(int startIndex = 0, SpecialCharFoundCallback callback = null, FinishedCallback finishedCallback = null)
	{
		if(callback != null) 
		{
			OnSpecialCharFoundCallback += callback;
		}

        if (finishedCallback != null)
        {
            OnFinishedCallback += finishedCallback;
        }

        charIndex = startIndex;
		maxCharIndex = content.Length-1;

		nextCharPlacementTime = 0.0f;
		animation = directionForward = true;
	}

    /// <summary>
	/// Starts the animation.
	/// </summary>
	public void StartAnimation(string content, int startIndex = 0, SpecialCharFoundCallback callback = null, FinishedCallback finishedCallback = null)
    {
        this.content = content;
        MapAudioToContent();

        StartAnimation(startIndex, callback, finishedCallback);
    }

    /// <summary>
    /// Stops the animation.
    /// </summary>
    public void StopAnimation()
	{
		OnSpecialCharFoundCallback = null;
        OnFinishedCallback = null;
        animation = false;
	}

	/// <summary>
	/// Run the Animation-cylce if its started
	/// </summary>
	public void FixedUpdate()
	{
		if(!animation)
			return;

		if(Time.time > nextCharPlacementTime) 
		{
			if (charIndex > maxCharIndex) 
			{
                if(OnFinishedCallback != null)
                    OnFinishedCallback();

                if(pingpong) 
				{
					directionForward = !directionForward;
					charIndex = maxCharIndex;
				} 
				else if (loop) 
				{
					charIndex = 0;
				} 
				else 
				{
					StopAnimation();
					return;
				}
			} 
			else if(pingpong && charIndex <= -1) 
			{
				directionForward = !directionForward;
				charIndex = 0;
			}

            string curCharString = content[charIndex].ToString();
            /*
            char curChar = curCharString[0];

            if(!string.IsNullOrEmpty(curCharString))
            {
                curChar = curCharString.ToLower() != null && curCharString.Length > 0 ? curCharString.ToLower()[0] : curCharString[0];
            }
            */

            if (OnSpecialCharFoundCallback != null && audioMapping.TryGetValue(curCharString.ToLower()[0], out curAudioClip)) 
			{
				OnSpecialCharFoundCallback(curAudioClip);
			}

            component.text = content.Substring(0, directionForward ? ++charIndex : charIndex--);

            if(blinkingCursor) 
			{
				component.text += blinkingCursorCharToogle ? blinkingCursorChar : "";
				blinkingCursorCharToogle = !blinkingCursorCharToogle;
			}

			nextCharPlacementTime = adjustOnSoundLength && curAudioClip != null ? Time.time + curAudioClip.length : Time.time + duration;
		}
	}

	/// <summary>
	/// Maps the content to the audio.
	/// </summary>
	public void MapAudioToContent()
	{
		for (int i = 0; i < content.Length; i++) 
		{
			for (int y = 0; y < specialChars.Length; y++) 
			{
				if(specialChars[y].Contains(content[i].ToString().ToLower()) && !audioMapping.ContainsKey(content[i].ToString().ToLower()[0])) 
				{
					audioMapping.Add(content[i].ToString().ToLower()[0], specialCharsAudio[y]);
					break;
				}
			}
		}
	}

	#endregion
}
