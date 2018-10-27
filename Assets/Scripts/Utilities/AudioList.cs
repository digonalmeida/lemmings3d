using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioList", menuName ="", order =0)]
public class AudioList : ScriptableObject {

    [SerializeField] private List<AudioClip> audios = new List<AudioClip>();
    private AudioClip[] uniqueAudios = new AudioClip[0];

    public AudioClip GetRandom() {
        if (audios.Count <= 0) return null;
        return audios[Random.Range(0, audios.Count)];
    }

    public AudioClip GetUniqueRandom() {
        if (audios.Count <= 0) return null;
        if (uniqueAudios.Length <= 0) uniqueAudios = audios.ToArray();
        uniqueAudios.Pop
    }

}
