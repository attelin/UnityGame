using UnityEngine;

public class GameStartMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; 

    void Start()
    {
        AudioManager.Instance.PlayAudio(backgroundMusic, AudioManager.SoundType.Music, 0.5f, true);
    }
}
