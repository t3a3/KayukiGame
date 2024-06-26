using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider _volumeSlider;

    [SerializeField] AudioSource _bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;
    [SerializeField] List<SESoundData> seSoundDatas;
    void Start()
    {
        _bgmAudioSource.Play();
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.AddListener((value) =>
            {
                // valueは0〜1の値を期待する。それを保証するための処理
                value = Mathf.Clamp01(value);

                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, -80f, 0f);
                audioMixer.SetFloat("Master", decibel);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.PlayOneShot(data.audioClip);
    }
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Aokiseki,
        Down,
        Count,
        CountStart// これがラベルになる
    }

    public SE se;
    public AudioClip audioClip;
}