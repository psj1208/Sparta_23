using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;

    public List<AudioClip> BGMClips;
    public List<AudioClip> SFXClips;

    protected override void Awake()
    {
        base.Awake();
        BGMSource.volume = 0.2f;
        PlayRandomBGM();
    }

    public void PlayOnce(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlayRandomBGM()
    {
        int rBgm = Random.Range(0, BGMClips.Count);
        if (BGMSource.clip == BGMClips[rBgm]) return; // 이미 재생중이면 무시
        BGMSource.clip = BGMClips[rBgm];
        BGMSource.loop = true;
        BGMSource.Play();
    }

    public void PlayBGM(int bgm)
    {
        if (BGMSource.clip == BGMClips[bgm]) return; // 이미 재생중이면 무시
        BGMSource.clip = BGMClips[bgm];
        BGMSource.loop = true;
        BGMSource.Play();
    }
    public void PlaySFX(ESFXType sfxType)
    {
        SFXSource.PlayOneShot(SFXClips[(int)sfxType]);
    }

    public void PlaySFX(int typeNum)
    {
        SFXSource.PlayOneShot(SFXClips[typeNum]);
    }
}