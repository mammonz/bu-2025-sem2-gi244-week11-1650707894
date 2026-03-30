using UnityEngine;

public class IndicatorFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, -0.5f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
