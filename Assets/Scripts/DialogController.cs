using UnityEngine;
using System.Collections;
using Ink.Runtime;
using UCFW;
using System;
using System.Collections.Generic;

public delegate void SetChoiseCallback(int index);

public class DialogController : Singleton<DialogController>
{
    #region Variables
    public MessageBoxController messageBoxController;
    private bool dialogDrawen = true;
    private bool conversationFinished = false;

    public TextAsset inkAsset; // Set this file to your compiled json asset
    private Story _inkStory; // The ink story that we're wrapping

    private Color[] actorNormalColor;
    public Color actorHighlightColor = Color.red;
    public GameObject[] actors;

    public SetChoiseCallback OnSetChoise;
    public FinishedCallback OnDialogFinished;

    #endregion

    /// <summary>
    /// Initialize
    /// </summary>
    void Awake()
    {
        OnSetChoise += SetChoise;
        OnDialogFinished += SetDialogDrawen;
    }

    public void StartDialog()
    {
        if(actors != null && actors.Length > 0)
        {
            actorNormalColor = new Color[actors.Length];

            for (int i = 0; i < actors.Length; i++)
            {
                actorNormalColor[i] = actors[i].GetComponentInChildren<MeshRenderer>().material.color;
            }  
        }
        LoadStory(inkAsset);
    }

    /// <summary>
    /// DeInitialize
    /// </summary>
    void OnDestroy()
    {
        OnSetChoise -= SetChoise;
        OnDialogFinished -= SetDialogDrawen;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(_inkStory != null && _inkStory.canContinue && dialogDrawen)
        {
            // Draw dialogs and choises
            string dialog = _inkStory.Continue();
            HighlightActors(dialog);
            messageBoxController.ContinueConversation(dialog, _inkStory.currentChoices);
            dialogDrawen = false;
        }
        else if(_inkStory != null && !conversationFinished && dialogDrawen && _inkStory.currentChoices.Count == 0)
        {
            // Draw end dialog
            Debug.Log("Draw end dialog");
            messageBoxController.FinishConversation();
            conversationFinished = true;
        }
    }

    void FixedUpdate()
    {
        if(!dialogDrawen)
        {
            messageBoxController.DialogContainer.GetComponentInParent<UnityEngine.UI.ScrollRect>().normalizedPosition = Vector2.zero;
        }
    }

    #region Load Methods

    /// <summary>
    /// Parse TextAsset into inkStory
    /// </summary>
    /// <param name="inkAsset"></param>
    public void LoadStory(TextAsset inkAsset)
    {
        LoadStory(inkAsset.text);
    }

    /// <summary>
    /// Parse TextAsset into inkStory
    /// </summary>
    /// <param name="inkAsset"></param>
    public void LoadStory(string text)
    {
        conversationFinished = false;
        _inkStory = new Story(text);

        _inkStory.ObserveVariable("fractions_points_pel", OnDialogVariableChange);
        _inkStory.ObserveVariable("fractions_points_vf", OnDialogVariableChange);
        _inkStory.ObserveVariable("fractions_points_eg", OnDialogVariableChange);
        _inkStory.ObserveVariable("fractions_points_spc", OnDialogVariableChange);   
    }

    private void OnDialogVariableChange(string variableName, object newValue)
    {
        Debug.Log(variableName + ": " + newValue);
    }

    #endregion

    public void SetChoise(int index)
    {
        if(conversationFinished)
        {
            QuitConversation();
        }
        else
        {
            if (index < _inkStory.currentChoices.Count)
            {
                _inkStory.ChooseChoiceIndex(index);
            }
        }
    }

    public void QuitConversation()
    {
        HighlightActors(""); // unhighlight
        _inkStory = null;
        messageBoxController.QuitConversation();
    }

    public void SetDialogDrawen()
    {
        dialogDrawen = true;
    }

    public void HighlightActors(string dialog)
    {
        MeshRenderer curActorRenderer = null;

        if(actors != null && actors.Length > 0)
        {
            for (int i = 0; i < actors.Length; i++)
            {
                curActorRenderer = actors[i].GetComponentInChildren<MeshRenderer>();
                if(dialog.IndexOf(actors[i].name) != -1)
                {
                    curActorRenderer.material.color = actorHighlightColor;
                }
                else
                {
                    curActorRenderer.material.color = actorNormalColor[i];
                }
            }
        }
    }
}