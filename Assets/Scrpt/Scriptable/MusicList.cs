using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music List", menuName = "ScriptableObject/Music List", order = 0)]
public class MusicList : ScriptableObject
{
    [System.Serializable]
    public class Music
    {
        public AudioClip musicClip;
        public string musicName;
        public float volume = 1.0f;
    }

    [Header("Music Clips")]
    public List<Music> musicClips = new List<Music>(); //��ϵ� ������ǵ�
}