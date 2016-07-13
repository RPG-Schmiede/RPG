using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIShowStates : MonoBehaviour
{
    Text textField;

	// Use this for initialization
	void Start ()
    {
        textField = GetComponent<Text>();
        textField.text = Screen.dpi.ToString();
    }

}
