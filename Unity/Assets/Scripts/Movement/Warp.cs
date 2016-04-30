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

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(OnWarp(other.gameObject));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(OnWarp(other.gameObject));
    }

    public IEnumerator OnWarp(GameObject other)
    {
        // Object Placement
        PlayerController pm = other.GetComponent<PlayerController>();
        if (pm)
        {
            pm.Lock = true;
            
            // FadeOut
            yield return StartCoroutine(sf.FadeToBlack());
        }

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

        if (pm)
        {    
            Camera.main.transform.position = warpPosition;
       
            // FadeIn
            yield return StartCoroutine(sf.FadeToClear());
            pm.Lock = false;
        }
    }

    public static void WarpTo(Transform other, Vector3 warpPosition)
    {
        other.transform.position = warpPosition;
        Camera.main.transform.position = warpPosition;
    }
}
