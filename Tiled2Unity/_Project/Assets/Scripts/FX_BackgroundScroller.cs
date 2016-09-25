using UnityEngine;
using System.Collections;

public class FX_BackgroundScroller : MonoBehaviour
{
    public Vector2 scrollSpeed = Vector2.right; //= Vector3.zero;
    private Vector2 position = Vector2.zero;

    private Renderer renderer = null;

	// Use this for initialization
	void Start ()
    {
        renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        position += (scrollSpeed * 0.01f) * Time.deltaTime;

        if (position.x > 1.0f)
            position.x -= 1.0f;

        if (position.y > 1.0f)
            position.y -= 1.0f;

        renderer.material.mainTextureOffset = position;
	}
}
