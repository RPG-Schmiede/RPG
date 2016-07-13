using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class represent a messagebox system, that is based on the ink-middleware.
/// </summary>
public class MessageBoxController : MonoBehaviour
{
    [Header("Prefabs")]
    public Button ChoisePrefab;
    public Text DialogPrefab;

    [Header("HUD References")]
    public RectTransform ChoiseContainer;
    public RectTransform DialogContainer;

    private List<Button> choises;
    private List<Text> dialogs;

    #region public methods

    /// <summary>
    /// Show the MessageBox and draw the first content
    /// </summary>
    public void StartConversation()
    {
    
    }

    /// <summary>
    /// Set the choise of the player and continue the conversation
    /// </summary>
    /// <param name="choiseID"></param>
    public void SetChoise(int choiseID)
    {

    }

    /// <summary>
    /// Hides the MessageBox and clear the content
    /// </summary>
    public void QuitConversation()
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
        if (choises == null || choises.Count > 0)
            return;

        for (int i = 0; i < choises.Count; i++)
        {
            RemoveChoise(i);
        }

        choises = null;
        choises = new List<Button>();
    }

    /// <summary>
    /// Remove all dialogs from the DialogView
    /// </summary>
    private void ResetDialogView()
    {
        if (dialogs == null || dialogs.Count > 0)
            return;

        for (int i = 0; i < dialogs.Count; i++)
        {
            RemoveDialog(i);
        }

        dialogs = null;
        dialogs = new List<Text>();
    }

    /// <summary>
    /// Add choise at the end of the ChoiseView
    /// </summary>
    private void AddChoise(string content)
    {
        Button choiseElement = Button.Instantiate(ChoisePrefab);
        choiseElement.transform.parent = ChoiseContainer.transform;

        choiseElement.GetComponentInChildren<Text>().text = content;
        choises.Add(choiseElement);
    }

    /// <summary>
    /// Add dialog at the end of the DialogView
    /// </summary>
    private void AddDialog(string content)
    {
        Text textElement = Text.Instantiate(DialogPrefab);
        textElement.transform.parent = DialogContainer.transform;

        textElement.text = content;
        dialogs.Add(textElement);
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

    #endregion
}