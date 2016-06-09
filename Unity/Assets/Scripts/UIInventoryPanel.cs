using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIInventoryPanel : MonoBehaviour
{
    public UIInventoryElement[] Slots;

    [Header("References")]
    public InventoryManager inventoryManager;

    public RectTransform inventoryPanel;
    public GameObject slotPrefab;
    public RectTransform contentPanel;
    public ScrollRect scrollRect;

    private Vector3 swipeStartPosition;
    private Vector3 swipeDirection = Vector3.zero;

    /*
    private void FixedUpdate()
    {
        int index = 0;

        foreach (KeyValuePair<string, int> item in inventoryManager.items)
        {
            if(Slots.Length-1 >= index)
            {
                Slots[index].UpdateSlot(item.Value, null);
            }

            ++index;
        }
    }
    */

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            swipeStartPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 heading = Input.mousePosition - swipeStartPosition;
            float distance = heading.magnitude;

            swipeDirection = heading / distance;

            if(swipeDirection.y >= 0.8f)
            {
                Swipe(false);
            }
            else if(swipeDirection.y <= -0.8f)
            {
                Swipe(true);
            }
        }
    }


    public void SetSlots()
    {
        UIInventoryElement[] slotElements = contentPanel.GetComponentsInChildren<UIInventoryElement>();

        if (slotElements != null && slotElements.Length > 0)
        {
            Slots = new UIInventoryElement[slotElements.Length];
            contentPanel.sizeDelta = new Vector2(0, inventoryPanel.sizeDelta.y * (Slots.Length / 4));

            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = i > slotElements.Length - 1 ? null : slotElements[i];
            }
        }
    }

    /// <summary>
    /// Swipe SideInventory
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public bool Swipe(bool forward)
    {
        if(forward && scrollRect.verticalNormalizedPosition > 0.0f)
        {
            scrollRect.verticalNormalizedPosition -= 0.5f; //(inventoryPanel.sizeDelta.y * 0.5f) / contentPanel.sizeDelta.y; //(contentPanel.anchoredPosition.y / inventoryPanel.sizeDelta.y);
            return true;
        }
        else if(!forward && scrollRect.verticalNormalizedPosition < ((inventoryPanel.sizeDelta.y * 0.5f) / contentPanel.sizeDelta.y) + 0.5f)
        {
            scrollRect.verticalNormalizedPosition += 0.5f; //(inventoryPanel.sizeDelta.y * 0.5f) / contentPanel.sizeDelta.y; //(contentPanel.anchoredPosition.y / inventoryPanel.sizeDelta.y);
            return true;
        }
        
        return false;
    }
}
