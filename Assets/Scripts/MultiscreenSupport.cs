using UnityEngine;
using System.Collections;

public class MultiscreenSupport : MonoBehaviour
{
    private Camera secondScreenCamera;

	// Use this for initialization
	void Start ()
    {
        secondScreenCamera = GetComponent<Camera>();

        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            secondScreenCamera.enabled = true;
        }
    }

    // TODO: Show some cool stuff like Inventory, Map, aso.
}
