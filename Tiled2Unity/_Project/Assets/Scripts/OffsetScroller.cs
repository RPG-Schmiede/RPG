using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour
{
    public Vector2 scrollSpeed = Vector2.zero;
    private Vector2 savedOffset;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed.x, 1);
        float y = Mathf.Repeat(Time.time * scrollSpeed.y, 1);

        Vector2 offset = new Vector2(x, y);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable()
    {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}