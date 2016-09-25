using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_GameCharacterCoordiates : MonoBehaviour
{
    public GridMovmentController character;
    public Text textArea;

    public void Start()
    {
        if (textArea == null)
        {
            textArea = GetComponent<Text>();
        }
    }

    public void ShowCoordinates()
    {
        // CALLBACK
        textArea.text = string.Format("x: {0} y: {1}", character.GridPosition.x.ToString(), character.GridPosition.y.ToString());
    }

    private void FixedUpdate()
    {
        ShowCoordinates();
    }
}
