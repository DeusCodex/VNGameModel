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

        LoadSettings(); // 게임 시작 시 설정 로드
    }

    private void LoadSettings() {
        AvailableResolutions = Screen.resolutions;
        System.Array.Reverse(AvailableResolutions);

        bool isFile = File.Exists(settingsFilePath);

        if (false) {
            Debug.Log("세팅 불러오기 성공");
            string json = File.ReadAllText(settingsFilePath);
            GetSettings = JsonUtility.FromJson<Settings>(json);
        }
        else {
            Debug.Log("초기 세팅 저장");
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

        DialogueSettings dialogueSettings = new();
        dialogueSettings.typingSpeed = 0.05f;
        dialogueSettings.dialogueDelay = 2.0f;
        dialogueSettings.dialogueBoxTransparency = 100f;
        settings.dialogueSettings = dialogueSettings;

        LanguageSettings languageSettings = new();
        languageSettings.selectedLanguage = Application.systemLanguage.ToString();
        settings.languageSettings = languageSettings;

        ControlSettings controlSettings = new();
        controlSettings.AutoDialogKeyCode = KeyCode.A;
        controlSettings.HideUIKeyCode = KeyCode.Tab;
        controlSettings.LoadKeyCode = KeyCode.L;
        controlSettings.NextDialogKeyCode = KeyCode.Space;
        controlSettings.SaveKeyCode = KeyCode.S;
        controlSettings.ShowLogKeyCode = KeyCode.T;
        controlSettings.SkipKeyCode = KeyCode.LeftControl;
        settings.controlSettings = controlSettings;

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

        SaveSettings(); 
    }
}
