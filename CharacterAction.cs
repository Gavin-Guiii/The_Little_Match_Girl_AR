using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAction : MonoBehaviour
{
    public enum ActionType
    {
        none,
        walk,
        rotate,
        scale
    }

    [Serializable]
    public class Action
    {
        public float duration;
        public ActionType type;
        public Vector3 walkDirection;
        public float walkSpeed;
        public Vector3 targetRotation;
        public float rotateSpeed;
        public Vector3 targetScale;
        public float scaleSpeed;
        public UnityEvent OnCountdownEnd;
    }

    public UnityEvent OnSequenceStartEvent;
    public Animator animator;
    public Transform marker;
    public Action[] ActionSequence;
    private float counter;
    private int ActionIndex;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        ActionIndex = 0;
        if (ActionSequence.Length == 0)
        {
            Destroy(this);
        }
        if (OnSequenceStartEvent != null)
        {
            OnSequenceStartEvent.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (ActionSequence[ActionIndex] != null) // Check whether this action exists
        {
            if (ActionSequence[ActionIndex].type == ActionType.walk) // Walk
            {
                transform.Translate(ActionSequence[ActionIndex].walkDirection * ActionSequence[ActionIndex].walkSpeed * Time.deltaTime, marker.transform); // Translate the character
            }

            else if (ActionSequence[ActionIndex].type == ActionType.rotate) // Rotate
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, ActionSequence[ActionIndex].targetRotation, ActionSequence[ActionIndex].rotateSpeed * Time.deltaTime); // Rotate the character
            }

            else if (ActionSequence[ActionIndex].type == ActionType.scale) // Scale
            {
                transform.localScale = Vector3.Lerp(transform.localScale, ActionSequence[ActionIndex].targetScale, ActionSequence[ActionIndex].scaleSpeed * Time.deltaTime); // Scale the character
            }

            else if (ActionSequence[ActionIndex].type == ActionType.none) // None
            {
                // Do nothing
            }

            if (counter >= ActionSequence[ActionIndex].duration) // Countdown is over
            {
                counter = 0; // Reset the counter


                if (ActionSequence[ActionIndex].OnCountdownEnd != null) // Check whether the event is valid
                {
                    ActionSequence[ActionIndex].OnCountdownEnd.Invoke(); // If so, invoke the event
                }

                if (ActionIndex == ActionSequence.Length - 1) // Check whether this is the last one
                {
                    enabled = false; // If so, disable this script
                }
                else
                {
                    ActionIndex ++;
                }
            }
        }
        else
        {
            enabled = false;
        }
    }

    public void SetAnimatorBoolToTrue(string name)
    {
        animator.SetBool(name, true);
    }

    public void SetAnimatorBoolToFalse(string name)
    {
        animator.SetBool(name, false);
    }

    public void NextAction()
    {
        counter = 1000;
    }
}
