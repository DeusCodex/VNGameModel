using System;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public GraphicsSettings graphicsSettings;
    public SoundSettings soundSettings;
    public DialogueSettings dialogueSettings;
    public LanguageSettings languageSettings;
    public ControlSettings controlSettings;

    private void Save() {
        SettingsManager.Instance.ApplySetting(this);
    }

}

[System.Serializable]
public class GraphicsSettings
{
    public Resolution Resolution 
    {
        set {
            resolution = new();
            resolution.width = value.width;
            resolution.height = value.height;
        }
        get {
            return new Resolution {
                width = resolution.width,
                height = resolution.height,
                refreshRate = Screen.currentResolution.refreshRate // ���� �������� �ӵ� ���
            };
        }
    }

    public ScreenResolution resolution;
    public FullScreenMode fullScreenMode; //���� fullScreen��� FullScreenMode���
    public int qualityLevel;
}

[System.Serializable]
public class ScreenResolution
{
    public int width;
    public int height;
}

[System.Serializable]
public class SoundSettings
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
    public float dialogVolume;
    public float UIVolume;
}

[System.Serializable]
public class DialogueSettings
{
    public float typingSpeed;
    public float dialogueDelay;
    public float dialogueBoxTransparency;
}

[System.Serializable]
public class LanguageSettings
{
    public string selectedLanguage;
}

[System.Serializable]
public class ControlSettings
{
    public KeyCode SkipKeyCode;
    public KeyCode NextDialogKeyCode;
    public KeyCode AutoDialogKeyCode;
    public KeyCode HideUIKeyCode;
    public KeyCode QuickSaveKeyCode;
    public KeyCode SaveKeyCode;
    public KeyCode LoadKeyCode;
    public KeyCode ShowLogKeyCode;
}
