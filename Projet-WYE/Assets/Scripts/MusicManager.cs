using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private AudioSource _audioSource;
    public List<MusicByPhase> musicByPhase = new List<MusicByPhase>();

    // Start is called before the first frame update
    public void CheckMusic()
    {
        foreach(MusicByPhase mbp in musicByPhase)
        {
            if(mbp.phase == MasterManager.Instance.currentPhase)
            {
                _audioSource.clip = mbp.music;
                return;
            }
        }
    }

    public void ChangeMusicbyPhase(int i)
    {
        _audioSource.clip = musicByPhase[i].music;
    }
}
[System.Serializable]
public class MusicByPhase
{
    public AudioClip music;
    public Phases phase;
}
