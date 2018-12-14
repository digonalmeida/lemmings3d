using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioList", menuName ="", order =0)]
public class AudioList : ScriptableObject {

    [SerializeField] private List<AudioClip> audios = new List<AudioClip>();
    private List<int> uniqueAudiosIndex = new List<int>();

    public AudioClip GetRandom() {
        if (audios.Count <= 0) return null;
        return audios[Random.Range(0, audios.Count)];
    }

    private void OnEnable()
    {
        uniqueAudiosIndex.Clear();
    }

    public AudioClip GetUniqueRandom() {
        if (audios.Count <= 0) return null;
        if (uniqueAudiosIndex.Count <= 0)
        {
            for (int i = 0; i < audios.Count; i++)
            {
                uniqueAudiosIndex.Add(i);
            }
        }
        int index = Random.Range(0, uniqueAudiosIndex.Count);
        int audioIndex = uniqueAudiosIndex[index];
        uniqueAudiosIndex.RemoveAt(index);
        return audios[audioIndex];
    }

}
