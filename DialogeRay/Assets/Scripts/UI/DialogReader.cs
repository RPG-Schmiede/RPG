using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ink.Runtime;

public class DialogReader : MonoBehaviour
{
    public TextAsset textFile;
    private Story _inkStory;

    public Text content;
    public Button[] choiseButtons;

    public UITextAnimator uiTextAnimator;

    // Use this for initialization
    void Start ()
    {
        _inkStory = new Story(textFile.text);

        while(_inkStory.canContinue)
        {
            if(uiTextAnimator != null)
            {
                uiTextAnimator.content = _inkStory.Continue();
            }
            else
            {
                content.text = _inkStory.Continue();
            }
        }

        uiTextAnimator.StartAnimation();

        if(_inkStory.currentChoices.Count > 0)
        {
            for(int i = 0; i < _inkStory.currentChoices.Count; ++i)
            {
                Choice choice = _inkStory.currentChoices[i];
                choiseButtons[i].GetComponentInChildren<Text>().text = choice.text;
            }
        }
    }
	
}
