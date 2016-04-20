using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour
{
    [SerializeField]
    public Transform warpTarget;
    //public Vector2 pivot;
    public string warpTargetString;
    ScreenFader sf;

    void Start()
    {
        sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        
        if(!warpTarget)
        {
            warpTarget = GameObject.Find(warpTargetString).transform;
        } 
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        // Object Placement
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm)
        {
            pm.Lock = true;
        }

        // FadeOut
        yield return StartCoroutine(sf.FadeToBlack());

        Vector3 warpPosition = Vector3.zero;
        BoxCollider2D boxCol = warpTarget.GetComponent<BoxCollider2D>();

        if (boxCol)
        {
            warpPosition = new Vector3(warpTarget.position.x + boxCol.offset.x, warpTarget.position.y + boxCol.offset.y, warpTarget.position.z);
        }
        else
        {
            warpPosition = warpTarget.position;
        }

        other.transform.position = warpPosition;
        Camera.main.transform.position = warpPosition;

        // FadeIn
        yield return StartCoroutine(sf.FadeToClear());

        if (pm)
        {
            pm.Lock = false;
        }
    }

    public static void WarpTo(Transform other, Vector3 warpPosition)
    {
        other.transform.position = warpPosition;
        Camera.main.transform.position = warpPosition;
    }
}
