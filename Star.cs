using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Star : MonoBehaviour
{
    public Image[] Stars;
    public Image Rainbow;
    public CharacterAction action;
    private int counter;

    public void ShowStarsAndRainbow()
    {
        foreach(Image star in Stars)
        {
            float delay = Random.Range(0.5f, 6f);
            float duration = Random.Range(1f, 2f);
            float fadeDuration = Random.Range(0.3f, 0.8f);
            Sequence mySequence = DOTween.Sequence();
            mySequence.AppendInterval(delay);
            mySequence.Append(star.DOFade(1.0f, fadeDuration));
            mySequence.AppendInterval(duration);
            mySequence.Append(star.DOFade(0f, fadeDuration));
            mySequence.AppendCallback(()=>
            {
                counter ++;
                if (Rainbow != null)
                {
                    if (counter == Stars.Length)
                    {
                        Sequence ranbowSequence = DOTween.Sequence();
                        ranbowSequence.Append(Rainbow.DOFade(1f, fadeDuration));
                        ranbowSequence.AppendInterval(3f);
                        ranbowSequence.Append(Rainbow.DOFade(0f, fadeDuration));
                        ranbowSequence.AppendInterval(1f);
                        ranbowSequence.AppendCallback(() =>
                        {
                            action.NextAction();
                        });
                    }
                }
            });
        }
    }

    public void ShowTwoStars()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(Stars[0].DOFade(1f, 0.5f));
        mySequence.Join(Stars[1].DOFade(1f, 0.5f));
        mySequence.AppendInterval(1f);
        mySequence.Append(Stars[0].DOFade(0f, 0.5f));
        mySequence.Join(Stars[1].DOFade(0f, 0.5f));

    }

}
