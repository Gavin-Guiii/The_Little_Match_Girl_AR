using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainGameManager : MonoBehaviour
{
    public Image upperBorder;
    public float upperBorderTargetY;
    public Image lowerBorder;
    public float lowerBorderTargetY;
    public float movementDuration = 0.5f;
    private bool hasInitalized;


    public void Initalize()
    {
        if (!hasInitalized)
        {
            hasInitalized = true;
            upperBorder.transform.DOLocalMoveY(upperBorderTargetY, movementDuration);
            lowerBorder.transform.DOLocalMoveY(lowerBorderTargetY, movementDuration);
            Destroy(GameObject.Find("Emulator Ground Plane").GetComponent<BoxCollider>());
        }
    }
}
