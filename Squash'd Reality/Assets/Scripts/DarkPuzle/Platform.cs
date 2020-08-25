using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    RaycastHit hit;
    bool hitDetect;
    int layerMask = 1 << 31;
    float maxDist = 0.5f;
   // [HideInInspector]
    public bool isPressed = false;

    private GameObject PressedBox;

    // Start is called before the first frame update
    void Start()
    {
        PressedBox = transform.GetChild(0).gameObject;
        PressedBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        hitDetect = Physics.Raycast(transform.position, transform.up, out hit, maxDist, layerMask);
        if (hitDetect)
        {
            isPressed = true;
            PressedBox.SetActive(true);
        }
        else
        {
            isPressed = false;
            PressedBox.SetActive(false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * maxDist);

    }
}
