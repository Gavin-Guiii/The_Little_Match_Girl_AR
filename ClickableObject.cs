using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ClickableObject : MonoBehaviour
{
    public enum action
    {
        Jump,
        Hide,
        None
    }
    public action ClickAction;
    public float JumpHeight = 0.1f;
    public float JumpDuration = 0.5f;
    public int JumpTimes = 3;

    public UnityEvent OnClickEvent;

    public void OnClick()
    {
        switch(ClickAction){
            case action.Jump:
            gameObject.transform.DOLocalMoveY(gameObject.transform.localPosition.y + JumpHeight, JumpDuration).SetLoops(2 * JumpTimes, LoopType.Yoyo);
            break;
            case action.Hide:
            gameObject.SetActive(false);
            break;
            case action.None:
            break;
        }

        if (OnClickEvent != null)
        {
            OnClickEvent.Invoke();
        }
    }
}
