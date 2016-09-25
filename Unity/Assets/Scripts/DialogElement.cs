using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class DialogElement : MonoBehaviour
{
    private Text textComponent;
    private UITextAnimator textAnimator;

    // Use this for initialization
    protected void Awake()
    {
        textComponent = GetComponent<Text>();
        textAnimator = GetComponent<UITextAnimator>();
    }

    public void SetDialogElement(string protagonist, string content, int animationStartIndex = 0, FinishedCallback finishCallback = null)
    {
        if(textAnimator != null)
        {
            textAnimator.StartAnimation(string.Format("<b>{0}</b>\n{1}", protagonist, content.Trim()), animationStartIndex+6, null, finishCallback);
        }
    }
}
