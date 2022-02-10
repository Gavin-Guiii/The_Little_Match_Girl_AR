using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class LightMatch : MonoBehaviour
{
    public float animDuration = 1f;
    public float matchDuration = 2f;
    public Image MatchThoughts;
    public Image ThoughtsBackground;
    public GameObject LightMatchCanvas;
    public GameObject Match;
    public Vector3 startMatchRotation;
    public Vector3 endMatchRotation;
    public UnityEvent OnMatchLighted;
    public Image YellowEffect;
    public AudioSource MatchAudio;

    public void ThinkingAboutMatch()
    {
        MatchThoughts.gameObject.SetActive(true);
        ThoughtsBackground.gameObject.SetActive(true);
        MatchThoughts.DOFade(1f, animDuration);
        ThoughtsBackground.DOFade(1f, animDuration);
    }

    public void LightingMatchUI()
    {
        MatchThoughts.DOFade(0f, animDuration).OnComplete(()=>
        {
            MatchThoughts.gameObject.SetActive(false);
        });
        ThoughtsBackground.DOFade(0f, animDuration).OnComplete(() =>
        {
            ThoughtsBackground.gameObject.SetActive(false);
        });
        LightMatchCanvas.SetActive(true);
        Match.transform.localEulerAngles = startMatchRotation;
        Match.transform.DOLocalRotate(endMatchRotation, matchDuration).SetLoops(-1, LoopType.Yoyo);
    }

    public void CheckMatch()
    {
        if (Match.transform.localEulerAngles.z >= 240f && Match.transform.localEulerAngles.z <= 300f)
        {
            Match.transform.DOPause();
            MatchAudio.Play();
            Transition();
        }
    }

    public void Transition()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(YellowEffect.DOFade(1f, 2f));
        mySequence.AppendCallback(()=>
        {
            if (OnMatchLighted != null)
            {
                OnMatchLighted.Invoke();
            }
        });
        mySequence.AppendInterval(2f);
        mySequence.Append(YellowEffect.DOFade(0f, 2f));
        mySequence.AppendCallback(()=>
        {
            Destroy(LightMatchCanvas);
        });
    }
}
