using System;
using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class SubtitleManager : MonoBehaviour
{
    [Serializable]
    public class Subtitle
    {
        public string Text;
        public AudioClip Voice;
        public float duration;
        public bool autoNext;
    }

    public TMP_Text SubtitleText;
    public Image SubtitleBackground;
    public AudioSource SubtitleAudio;
    public float fadeDuration = 0.5f;
    public bool autoStart;
    public UnityEvent OnSubtitleFinished;

    public Subtitle[] subtitles;
    private float counter;
    private int index;
    private bool isActive;

    void Start()
    {

        if (autoStart)
        {
            PlaySubtitle();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            counter += Time.deltaTime;
            if (counter >= subtitles[index].duration)
            {
                index++;

                if (index == subtitles.Length)
                {
                    if (OnSubtitleFinished != null)
                    {
                        OnSubtitleFinished.Invoke();
                    }
                    enabled = false;
                }

                if (subtitles[index - 1].autoNext)
                {
                    PlaySubtitle();
                }

                else
                {
                    SubtitleBackground.DOFade(0f, fadeDuration);
                    SubtitleText.DOFade(0f, fadeDuration);
                    isActive = false;
                }
            }
        }
    }

    public void PlaySubtitle()
    {
        isActive = true;
        counter = 0;
        if (index == 0 || subtitles[index - 1].autoNext == false)
        {
            SubtitleBackground.DOFade(1f, fadeDuration);
            SubtitleText.DOFade(1f, fadeDuration);
        }
        if(subtitles[index].Voice != null)
        {
            SubtitleAudio.PlayOneShot(subtitles[index].Voice);
        }
        SubtitleText.text = subtitles[index].Text;
    }
}
