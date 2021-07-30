using UnityEngine;

namespace FruitsVSJunks.Scripts.Services
{
    public enum SoundFX
    {
        GatesCrash,
        GlassShatter,
        Cheerful,
        Jump
    }

    /// <summary>
    /// A very simple audio service example that can play specific sounds.
    /// Instead of using the Unity API, it can also be implemented with a 3rd party Audio plugin
    /// </summary>
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<SoundFX, AudioClip> clips;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySoundFX(SoundFX soundFx)
        {
            if (!clips.Keys.Contains(soundFx))
            {
                Debug.LogWarning("No sound clip was added for: " + soundFx);

                return;
            }

            audioSource.PlayOneShot(clips[soundFx], 1f);
        }
    }
}
