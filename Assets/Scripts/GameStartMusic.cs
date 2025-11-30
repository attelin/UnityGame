using UnityEngine;

public class GameStartMusic : MonoBehaviour
{
    public AudioClip backgroundMusic; // Vedä musiikki tähän Inspectorissa

    void Start()
    {
        AudioManager.Instance.PlayAudio(backgroundMusic, AudioManager.SoundType.Music, 0.5f, true);
    }
}
