using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventoryElement : MonoBehaviour
{
    public Text Label;
    public Image Icon;

    public void UpdateSlot(int amount, Sprite icon)
    {
        Label.text = string.Format("{0}x", amount);

        if (amount > 0)
        {
            Icon.enabled = true;
            Label.enabled = true;
        }
        else
        {
            Icon.enabled = false;
            Label.enabled = false;
        }
    }
}
