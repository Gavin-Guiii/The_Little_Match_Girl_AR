using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool isMuted;

    void Start()
    {
        audioMixer.SetFloat("Volume", 0);
        isMuted = false;
    }

    public void DownloadButton()
    {
        Application.OpenURL("https://388886.ma3you.cn/articles/RMNmvQO");
    }

    public void SoundButton()
    {
        if (isMuted)
        {
            isMuted = false;
            audioMixer.SetFloat("Volume", 0);
        }
        else
        {
            isMuted = true;
            audioMixer.SetFloat("Volume", -80);
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
