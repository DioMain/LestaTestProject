using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioMixer musicMixer;

    public float AudioVolume
    {
        get
        {
            audioMixer.GetFloat("Volume", out float volume);
            return volume;
        }
        set => audioMixer.SetFloat("Volume", value);
    }
    public float MusicVolume
    {
        get
        {
            musicMixer.GetFloat("Volume", out float volume);
            return volume;
        }
        set => musicMixer.SetFloat("Volume", value);
    }

    public void SetMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}