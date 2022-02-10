using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class SequentialClickableObject : MonoBehaviour
{
    public GameObject[] objectsToClick;
    public float upDuration = 0.5f;
    public float upHeight = 0.1f;
    public UnityEvent OnSequenceEnd;
    public float TimeBeforeEvent = 0.5f;
    public float TimeBeforeDrop = 0.5f;
    private int index;
    private bool active;

    void Start()
    {
        index = 0;
        active = true;
    }

    public IEnumerator CheckClicked(GameObject clicked)
    {
        if (active)
        {
            if (clicked == objectsToClick[index])
            {
                clicked.transform.DOLocalMoveY(clicked.transform.localPosition.y + upHeight, upDuration);
                index++;
                if (index == objectsToClick.Length)
                {
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
            else
            {
                for (int i = 0; i < index; i++)
                {
                    objectsToClick[i].transform.DOLocalMoveY(objectsToClick[i].transform.localPosition.y - upHeight, upDuration);
                }
                index = 0;
            }
        }
    }
}
