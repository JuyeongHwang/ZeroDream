using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{



    public Slider bgmSlider;
    public Slider effectSlider;

    public AudioSource playerAudio;
    public AudioSource textAudio;
    public AudioSource bgmAudio;
    

    public void SetEffectSlider()
    {
        playerAudio.volume = effectSlider.value;
        textAudio.volume = effectSlider.value;
    }

    public void SetBGMSlider()
    {
        bgmAudio.volume = bgmSlider.value;
    }

}
