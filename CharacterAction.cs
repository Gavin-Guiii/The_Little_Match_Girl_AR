using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAction : MonoBehaviour
{
    // Four common characters' action types
    public enum ActionType
    {
        none,
        walk,
        rotate,
        scale
    }

    // Action is represented by a serializable class
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
        // Implement the timeline
        counter += Time.deltaTime;

        // Check whether this action exists
        if (ActionSequence[ActionIndex] != null) 
        {
            // Walk
            if (ActionSequence[ActionIndex].type == ActionType.walk) 
            {
                transform.Translate(ActionSequence[ActionIndex].walkDirection * ActionSequence[ActionIndex].walkSpeed * Time.deltaTime, marker.transform); // Translate the character
            }

            // Rotate
            else if (ActionSequence[ActionIndex].type == ActionType.rotate) 
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, ActionSequence[ActionIndex].targetRotation, ActionSequence[ActionIndex].rotateSpeed * Time.deltaTime); // Rotate the character
            }

            // Scale
            else if (ActionSequence[ActionIndex].type == ActionType.scale)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, ActionSequence[ActionIndex].targetScale, ActionSequence[ActionIndex].scaleSpeed * Time.deltaTime); // Scale the character
            }

            // None
            else if (ActionSequence[ActionIndex].type == ActionType.none) 
            {
                // Do nothing
            }

            // If the counter is over, reset the counter
            if (counter >= ActionSequence[ActionIndex].duration) 
            {
                counter = 0;

                // Check whether the OnCountdownEnd event is valid. If so, invoke the event.
                if (ActionSequence[ActionIndex].OnCountdownEnd != null) 
                {
                    ActionSequence[ActionIndex].OnCountdownEnd.Invoke();
                }

                // Check whether this is the last one. If so, disable this script. Otherwise, goes to the next one.
                if (ActionIndex == ActionSequence.Length - 1) 
                {
                    enabled = false;
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

    // These functions are provided to make animator configration easier.
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
