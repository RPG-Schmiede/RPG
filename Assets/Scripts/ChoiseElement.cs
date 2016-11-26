using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class ChoiseElement : MonoBehaviour
{
    private Button buttonComponent;
    private Text textComponent;

	// Use this for initialization
	protected void Awake()
    {
        buttonComponent = GetComponent<Button>();
        textComponent = GetComponentInChildren<Text>();
    }

    public void SetChoiseElement(string content, int choiseIndex, UnityAction<int> onClickCallback)
    {
        textComponent.text = content;
        buttonComponent.onClick.AddListener(() => onClickCallback(choiseIndex));
    }
}
