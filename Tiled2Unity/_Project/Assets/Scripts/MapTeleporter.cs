using UnityEngine;
using System.Collections;

public class MapTeleporter : MonoBehaviour
{
    private GridMovmentController charController;

    public string map;
    public int map_x;
    public int map_y;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            charController = col.GetComponent<GridMovmentController>();
            Teleport(map);
        }
    }

    public void Teleport(string level)
    {
        Application.LoadLevel(level);
        charController.Teleport(new Vector2(map_x, map_y));
    }
}
