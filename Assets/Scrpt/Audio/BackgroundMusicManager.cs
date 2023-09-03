using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;

    private AudioSource audioSource;
    public MusicList musicList;

    private float masterVolume = 1.0f;
    private float musicVolume = 1.0f;
    private float currentVolume = 1.0f;

    private string currentPlayingMusicName; // ���� ��� ���� ���� �̸�

    private Coroutine fadeOutCoroutine;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        SettingsManager.OnSettingsChanged += SetSoundSetting;
    }

    private void OnDestroy() {
        SettingsManager.OnSettingsChanged -= SetSoundSetting;
    }

    public void PlayMusic(string musicName) {
        if (musicName == currentPlayingMusicName) {
            // �̹� �ش� ������ ��� ���̸� �ƹ� �۾��� ���� ����
            return;
        }

        MusicList.Music selectedMusic = musicList.musicClips.Find(music => music.musicName == musicName);

        if (selectedMusic != null) {
            if (fadeOutCoroutine != null) {
                StopCoroutine(fadeOutCoroutine);
            }

            if (currentPlayingMusicName != null) {
                // ���� ������ ������ ���̵� �ƿ�
                fadeOutCoroutine = StartCoroutine(FadeOutMusic());
            }
            else {
                // ���� ������ ���� ���, ��� ���� ���
                audioSource.volume = 0.0f;
                audioSource.clip = selectedMusic.musicClip;
                audioSource.Play();
            }

            currentPlayingMusicName = musicName;

            // ���� ������ ����Ͽ� ����
            float finalVolume = masterVolume * musicVolume * selectedMusic.volume;
            currentVolume = finalVolume;
        }
        else {
            // ������ ã�� �� ���� ���
            Debug.LogWarning("������ ã�� �� �����ϴ�: " + musicName);
        }
    }

    public void PauseMusic() {
        audioSource.Pause();
    }

    public void ResumeMusic() {
        audioSource.UnPause();
    }

    public void SetVolume(float volume) {
        float finalVolume = masterVolume * musicVolume * volume;
        audioSource.volume = Mathf.Clamp01(finalVolume);
    }

    private void SetSoundSetting(Settings settings) {
        masterVolume = settings.soundSettings.masterVolume;
        musicVolume = settings.soundSettings.musicVolume;

        // ������ ����� �� ���� ��� ���� ���ǿ� ���ο� ������ ����
        if (!string.IsNullOrEmpty(currentPlayingMusicName)) {
            float finalVolume = masterVolume * musicVolume * currentVolume;
            audioSource.volume = Mathf.Clamp01(finalVolume);
        }
    }

    private IEnumerator FadeOutMusic() {
        float startVolume = audioSource.volume;
        float fadeDuration = 1.0f; // ���̵� �ƿ��� �ɸ��� �ð�

        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration) {
            float elapsed = Time.time - startTime;
            float t = elapsed / fadeDuration;
            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, t);
            yield return null;
        }

        audioSource.volume = 0.0f;
        audioSource.Stop();
    }
}
