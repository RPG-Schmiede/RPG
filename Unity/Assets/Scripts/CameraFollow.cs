using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float m_speed = 0.1f;

    private ScreenOrientation screenOrientation;
    private float screenWidth = 0.0f;

    [Header("ScaleFactor for Camera - 2.0f means normal, 4.0f double")]
    public float scaleFactor = 4.0f;
    private float dpiNormalScale = 96;
    Camera mycam;

	// Use this for initialization
	void Start ()
    {
        mycam = GetComponent<Camera>();
        UpdateOrthograhicSize();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Screen.orientation != screenOrientation  || Screen.width != screenWidth)
        {
            UpdateOrthograhicSize();
        }

        if(target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, m_speed) + new Vector3(0, 0, -10);
        }
	}

    void UpdateOrthograhicSize()
    {
        mycam.orthographicSize = ((Screen.height / 100f) / scaleFactor) / (Screen.dpi / dpiNormalScale);
        screenOrientation = Screen.orientation;
        screenWidth = Screen.width;
    }
}
