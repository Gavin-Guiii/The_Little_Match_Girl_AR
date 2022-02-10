using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClickableObject : MonoBehaviour
{
    private Camera cam;
    private RaycastHit hit;
    private Ray ray;
    public SequentialClickableObject[] sequentialClickableObjects;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                ClickableObject clickableObject = hit.collider.gameObject.GetComponent<ClickableObject>();

                if (clickableObject != null)
                {
                    clickableObject.OnClick();
                }

                if (sequentialClickableObjects != null)
                {
                    for (int i = 0; i < sequentialClickableObjects.Length; i++)
                    {
                        StartCoroutine(sequentialClickableObjects[i].CheckClicked(hit.collider.gameObject));
                    }
                }
            }
        }
    }
}
