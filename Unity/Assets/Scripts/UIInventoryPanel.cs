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

    public RectTransform normalContentPanel;
    public RectTransform battleContentPanel;

    public Button arrowUp;
    public Button arrowDown;

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

    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public void SetSlots()
    {
        UIInventoryElement[] slotElements = scrollRect.content.GetComponentsInChildren<UIInventoryElement>();

        if (slotElements != null && slotElements.Length > 0)
        {
            Slots = new UIInventoryElement[slotElements.Length];
            scrollRect.content.sizeDelta = new Vector2(0, inventoryPanel.sizeDelta.y * (Slots.Length / 4));

            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = i > slotElements.Length - 1 ? null : slotElements[i];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void SwitchToNormal()
    {
        SwitchContent(normalContentPanel);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SwitchToBattle()
    {
        SwitchContent(battleContentPanel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    private void SwitchContent(RectTransform content)
    {
        scrollRect.content.gameObject.SetActive(false);
        scrollRect.content = content;
        scrollRect.content.gameObject.SetActive(true);

        scrollRect.verticalNormalizedPosition = 1.0f;
        UpdateArrowButtonStatus();
    }

    public void SwipeUp()
    {
        Swipe(false);
    }

    public void SwipeDown()
    {
        Swipe(true);
    }

    /// <summary>
    /// Swipe SideInventory
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public bool Swipe(bool forward)
    {
        bool status = false;

        if(forward && scrollRect.verticalNormalizedPosition > 0.0f)
        {
            scrollRect.verticalNormalizedPosition -= (inventoryPanel.sizeDelta.y / scrollRect.content.sizeDelta.y) * 2.0f; //(inventoryPanel.sizeDelta.y * 0.5f) / contentPanel.sizeDelta.y; //(contentPanel.anchoredPosition.y / inventoryPanel.sizeDelta.y);
            status = true;
        }
        else if(!forward && scrollRect.verticalNormalizedPosition < ((inventoryPanel.sizeDelta.y * 0.5f) / scrollRect.content.sizeDelta.y) + 0.5f)
        {
            scrollRect.verticalNormalizedPosition += (inventoryPanel.sizeDelta.y / scrollRect.content.sizeDelta.y) * 2.0f; //(inventoryPanel.sizeDelta.y * 0.5f) / contentPanel.sizeDelta.y; //(contentPanel.anchoredPosition.y / inventoryPanel.sizeDelta.y);
            status = true;
        }
        
        UpdateArrowButtonStatus();

        return status;
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateArrowButtonStatus()
    {
        if (arrowUp != null)
        {
            arrowUp.interactable = IsSwipePossible(false);
        }

        if (arrowDown != null)
        {
            arrowDown.interactable = IsSwipePossible(true);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="forward"></param>
    /// <returns></returns>
    public bool IsSwipePossible(bool forward)
    {
        if (forward)
        {
            if(scrollRect.verticalNormalizedPosition > 0.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(scrollRect.content.sizeDelta.y <= inventoryPanel.sizeDelta.y)
            {
                return false;
            }
            else if (scrollRect.verticalNormalizedPosition < ((inventoryPanel.sizeDelta.y * 0.5f) / scrollRect.content.sizeDelta.y) + 0.5f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
