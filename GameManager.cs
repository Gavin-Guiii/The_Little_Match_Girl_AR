using System;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class objectAndHeight
    {
        public GameObject targetObject;
        public float desHeight;
    }
    public objectAndHeight[] ObjectsToShow;
    public objectAndHeight[] ObjectsToHide;
    public GameObject SnowParticle;

    public float moveDuration;
    public bool autoStart;

    void Start()
    {
        if (autoStart == true)
        {
            ShowObjects();
        }
    }

    public void ShowObjects()
    {
        foreach(objectAndHeight objectToShow in ObjectsToShow)
        {
            objectToShow.targetObject.transform.DOLocalMoveY(objectToShow.desHeight, moveDuration).SetEase(Ease.OutCubic);
        }
    }

    public void HideObjects()
    {
        foreach (objectAndHeight objectToHide in ObjectsToHide)
        {
            objectToHide.targetObject.transform.DOLocalMoveY(objectToHide.desHeight, moveDuration).SetEase(Ease.OutCubic);
        }
        if (SnowParticle != null)
        {
            SnowParticle.SetActive(false);
        }
    }
}
