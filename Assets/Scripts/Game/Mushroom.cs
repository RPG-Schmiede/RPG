using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour, ICollectable
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    private InventoryManager inventoryManager;

    private void Start()
    {
        if (!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if(!boxCollider)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision(collision.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnCollision(GameObject gameObject)
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        inventoryManager.CollectItem(GetType().ToString(), 1);
    }
}
