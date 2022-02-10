using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

// Sequential Clickable Object: Users should click some objects in order to trigger an event.
public class SequentialClickableObject : MonoBehaviour
{
    public GameObject[] objectsToClick;
    public float upDuration = 0.5f;
    public float upHeight = 0.1f;
    public UnityEvent OnSequenceEnd;
    public float TimeBeforeEvent = 0.5f;
    public float TimeBeforeDrop = 0.5f;
    private bool active;

    // The index of current object in the sequence
    private int index;

    void Start()
    {
        index = 0;
        active = true;
    }

    public IEnumerator CheckClicked(GameObject clicked)
    {
        if (active)
        {
            // If the clicked object is the one should be clicked
            if (clicked == objectsToClick[index])
            {
                // Make it float
                clicked.transform.DOLocalMoveY(clicked.transform.localPosition.y + upHeight, upDuration);
                index++;

                // If this is the last object in the sequence
                if (index == objectsToClick.Length)
                {
                    // Invoke the OnSequenceEnd event
                    if (OnSequenceEnd != null)
                    {
                        yield return new WaitForSeconds(TimeBeforeEvent);
                        OnSequenceEnd.Invoke();
                        yield return new WaitForSeconds(TimeBeforeDrop);
                        for (int i = 0; i < index; i++)
                        {
                            objectsToClick[i].transform.DOLocalMoveY(objectsToClick[i].transform.localPosition.y - upHeight, upDuration);
                        }
                        active = false;
                    }
                }
            }
            // Otherwise, if the clicked object is NOT the one should be clicked (wrong object clicked or wrong order)
            else
            {
                for (int i = 0; i < index; i++)
                {
                    // All the floating objects will go back to original places
                    objectsToClick[i].transform.DOLocalMoveY(objectsToClick[i].transform.localPosition.y - upHeight, upDuration);
                }
                index = 0;
            }
        }
    }
}
