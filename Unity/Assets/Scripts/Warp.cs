using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour
{
    public Transform warpTarget;
    ScreenFader sf;

    void Start()
    {
        sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>(); 
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        // Object Placement
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm)
        {
            pm.Lock = true;
        }

        // FadeIn
        yield return StartCoroutine(sf.FadeToBlack());

        other.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;

        // FadeIn
        yield return StartCoroutine(sf.FadeToClear());

        if (pm)
        {
            pm.Lock = false;
        }
    }
}
