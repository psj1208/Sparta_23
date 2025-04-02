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
    }

    public void PlayOnce(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlayBGM(EBGMType bgm)
    {
        if (BGMSource.clip == BGMClips[(int)bgm]) return; // 이미 재생중이면 무시
        BGMSource.clip = BGMClips[(int)bgm];
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

public enum EBGMType
{

}

public enum ESFXType
{

}

