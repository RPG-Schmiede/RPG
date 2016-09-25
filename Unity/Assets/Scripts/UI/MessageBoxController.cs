using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

/// <summary>
/// This class represent a messagebox system, that is based on the ink-middleware.
/// </summary>
public class MessageBoxController : MonoBehaviour
{
    [Header("Prefabs")]
    public ChoiseElement ChoisePrefab;
    public DialogElement DialogPrefab;

    [Header("HUD References")]
    public CanvasGroup canvasGroup;
    public RectTransform ChoiseContainer;
    public RectTransform DialogContainer;

    private List<ChoiseElement> choises;
    private List<DialogElement> dialogs;

    #region public methods

    private void OnEnable()
    {
        ResetConversation();
    }

    /// <summary>
    /// Show the MessageBox and draw the first content
    /// </summary>
    public void StartConversation(string dialog, List<Choice> choises = null)
    {
        ResetConversation();
        ContinueConversation(dialog);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// Show the MessageBox and draw the first content
    /// </summary>
    public void ContinueConversation(string dialog, List<Choice> choises = null)
    {
        ResetChoises();
        AddDialog(dialog);

        if (choises != null && choises.Count > 0)
        {
            for (int i = 0; i < choises.Count; ++i)
            {
                AddChoise(choises[i].text, choises[i].index);
            }
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    /// <summary>
    /// Hides the MessageBox and clear the content
    /// </summary>
    public void QuitConversation()
    {
        ResetConversation();

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    /// <summary>
    /// Hides the MessageBox and clear the content
    /// </summary>
    public void ResetConversation()
    {
        ResetDialogView();
        ResetDialogView();
    }

    #endregion

    #region internal methods

    /// <summary>
    /// Remove all choises from the choises view
    /// </summary>
    private void ResetChoises()
    {
        if (choises == null)
        {
            choises = new List<ChoiseElement>();
            return;
        }

        if (choises.Count == 0)
            return;

        for (int i = 0; i < choises.Count; i++)
        {
            RemoveChoise(i);
        }

        choises = null;
        choises = new List<ChoiseElement>();
    }

    /// <summary>
    /// Remove all dialogs from the DialogView
    /// </summary>
    private void ResetDialogView()
    {
        if (dialogs == null)
        {
            dialogs = new List<DialogElement>();
            return;
        }

        if(dialogs.Count == 0)
            return;

        for (int i = 0; i < dialogs.Count; i++)
        {
            RemoveDialog(i);
        }

        dialogs = null;
        dialogs = new List<DialogElement>();
    }

    /// <summary>
    /// Add choise at the end of the ChoiseView
    /// </summary>
    private void AddChoise(string content, int index)
    {
        ChoiseElement choiseElement = ChoiseElement.Instantiate(ChoisePrefab);
        choiseElement.transform.parent = ChoiseContainer.transform;
        choiseElement.SetChoiseElement(content, index, SetChoise);
        choiseElement.gameObject.SetActive(false);

        choises.Add(choiseElement);
    }

    public void EnableChoises()
    {
        for(int i = 0; i < choises.Count; i++)
        {
            choises[i].gameObject.SetActive(true);
        }
    }

    public void DisableChoises()
    {
        for (int i = 0; i < choises.Count; i++)
        {
            choises[i].gameObject.SetActive(false);
        }
    }

    public bool AreChoisesEnabled()
    {
        return choises != null && choises.Count > 0 ? (choises[choises.Count - 1].gameObject.activeSelf) : false;
    }

    /// <summary>
    /// Set the choise of the player and continue the conversation
    /// </summary>
    /// <param name="choiseID"></param>
    public void SetChoise(int choiseID)
    {
        DialogController.instance.OnSetChoise(choiseID);
        DisableChoises();
    }

    /// <summary>
    /// Add dialog at the end of the DialogView
    /// </summary>
    private void AddDialog(string content)
    {
        DialogElement textElement = DialogElement.Instantiate(DialogPrefab);
        textElement.transform.parent = DialogContainer.transform;
        int protagonistIndex = content.IndexOf(":")+1;
        textElement.SetDialogElement(content.Substring(0, protagonistIndex), content.Substring(protagonistIndex), protagonistIndex, DialogIsVisible);

        dialogs.Add(textElement);
    }

    private void DialogIsVisible()
    {
        DialogController.instance.OnDialogFinished();
        EnableChoises();
    }

    /// <summary>
    /// Remove a choise from the ChoiseView
    /// </summary>
    /// <param name="index">Index of the Choise</param>
    private void RemoveChoise(int index)
    {
        Destroy(choises[index].gameObject);
    }

    /// <summary>
    /// Remove a dialog from the DialogView
    /// </summary>
    /// <param name="index">Index of the Dialog</param>
    private void RemoveDialog(int index)
    {
        Destroy(dialogs[index].gameObject);
    }

    public void FinishConversation()
    {
        ResetChoises();
        AddChoise("Ende", -10);
        EnableChoises();
    }

    #endregion
}