using System;
using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public static Settings GetSettings { get; private set; }

    public static Resolution[] AvailableResolutions;

    public static event Action<Settings> OnSettingsChanged;

    [SerializeField]
    private string settingsFilePath = "settings.json";

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings(); // ���� ���� �� ���� �ε�
    }

    private void LoadSettings() {
        AvailableResolutions = Screen.resolutions;
        System.Array.Reverse(AvailableResolutions);

        if (File.Exists(settingsFilePath)) {
            Debug.Log("���� �ҷ����� ����");
            string json = File.ReadAllText(settingsFilePath);
            GetSettings = JsonUtility.FromJson<Settings>(json);
        }
        else {
            Debug.Log("�ʱ� ���� ����");
            GetSettings = InitSetting();
            SaveSettings();
        }
        ScreenSetting();
    }

    private void ScreenSetting() {
        ScreenManager.ChangeResolution(GetSettings.graphicsSettings.Resolution);
        
    }

    private Settings InitSetting() {
        Settings settings = new();

        GraphicsSettings currentGraphicsSettings = new();
        currentGraphicsSettings.Resolution = AvailableResolutions[0];
        currentGraphicsSettings.fullScreenMode = Screen.fullScreenMode;
        currentGraphicsSettings.qualityLevel = QualitySettings.GetQualityLevel();
        settings.graphicsSettings = currentGraphicsSettings;

        SoundSettings currentSoundSetting = new();
        currentSoundSetting.masterVolume = 1.0f;
        currentSoundSetting.musicVolume = 1.0f;
        currentSoundSetting.sfxVolume = 1.0f;
        currentSoundSetting.UIVolume = 1.0f;
        currentSoundSetting.dialogVolume = 1.0f;
        settings.soundSettings = currentSoundSetting;

        LanguageSettings languageSettings = new();
        languageSettings.selectedLanguage = Application.systemLanguage.ToString();

        DialogueSettings dialogueSettings = new();
        dialogueSettings.typingSpeed = 0.05f;
        dialogueSettings.dialogueDelay = 2.0f;
        dialogueSettings.dialogueBoxTransparency = 100f;

        ControlSettings controlSettings = new();
        controlSettings.AutoDialogKeyCode = KeyCode.A;
        controlSettings.HideUIKeyCode = KeyCode.Tab;
        controlSettings.LoadKeyCode = KeyCode.L;
        controlSettings.NextDialogKeyCode = KeyCode.Space;
        controlSettings.SaveKeyCode = KeyCode.S;
        controlSettings.ShowLogKeyCode = KeyCode.T;
        controlSettings.SkipKeyCode = KeyCode.LeftControl;
        
        return settings;
    }

    private void SaveSettings() {
        string json = JsonUtility.ToJson(GetSettings);
        File.WriteAllText(settingsFilePath, json);
    }

    public void ApplySetting(Settings gameSettings) {
        if (gameSettings == null) {
            Debug.LogError("Setting Error: gameSettings is null");
            return;
        }
        GetSettings = gameSettings;
        OnSettingsChanged?.Invoke(gameSettings);

        SaveSettings(); // ���� ���� �� ����
    }
}
