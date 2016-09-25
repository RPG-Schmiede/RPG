using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_GameSaveController : MonoBehaviour
{
    public Text textArea;
    public float displayTime = 5.0f;
    public AnimationCurve fadeTime;

    private float processStartTime = 0.0f;
    private float displayOutTime = 0.0f;

    public void Start()
    {
        if(textArea == null)
        {
            textArea = GetComponent<Text>();
        }

        textArea.enabled = false;
    }

    public void Show()
    {
        textArea.enabled = true;
        processStartTime = Time.time;
        displayOutTime = Time.time + displayTime;
    }

    public void Hide()
    {
        textArea.enabled = false;
        displayOutTime = 0.0f;
    }

    private void Process()
    {
        Debug.Log(string.Format("{0}/{1}: {2}", Time.time, displayOutTime, (Time.time - processStartTime) / (Time.time - displayOutTime)));
    }

    private void Update()
    {
        if (displayOutTime < 0.1f)
            return;

        if(Time.time <= displayOutTime)
        {
            Process();
        }
        else
        {
            Hide();
        }
    }
}
