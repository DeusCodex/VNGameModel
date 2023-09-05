using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettingUI : SettingOption
{
    [SerializeField] private TMP_Dropdown resolutionDropdown; // �ػ� ������ ���� TMP Dropdown �޴�
    [SerializeField] private TMP_Dropdown fullscreenModeDropdown; // ��ü ȭ�� ��� ������ ���� TMP Dropdown �޴�
    [SerializeField] private TMP_Dropdown qualityDropdown; // �׷��� ǰ�� ������ ���� TMP Dropdown �޴�

    private Settings settings; // ���� ���� ����

    public enum FullScreenModeEnum
    {
        FullScreen,
        Windowed,
    }

    private void Start() {
        settings = SettingsManager.GetSettings;
        InitializeResolutionDropdown();
        InitializeFullScreenModeDropdown();
        InitializeQualityDropdown();
    }

    // �ػ� ��Ӵٿ��� �ʱ�ȭ
    private void InitializeResolutionDropdown() {
        resolutionDropdown.ClearOptions();
        List<string> resolutionOptions = new List<string>();
        Resolution[] availableResolutions = SettingsManager.AvailableResolutions;
        foreach (Resolution resolution in availableResolutions) {
            resolutionOptions.Add(resolution.width + "x" + resolution.height);
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.onValueChanged.AddListener(ResolutionDropdownValueChanged);
    }

    // ��ü ȭ�� ��� Dropdown�� �ʱ�ȭ�ϴ� �޼���
    private void InitializeFullScreenModeDropdown() {
        string[] fullScreenModeOptions = System.Enum.GetNames(typeof(FullScreenModeEnum));

        fullscreenModeDropdown.ClearOptions();
        fullscreenModeDropdown.AddOptions(new List<string>(fullScreenModeOptions));
        fullscreenModeDropdown.onValueChanged.AddListener(OnFullScreenModeDropdownValueChanged);
    }

    private void InitializeQualityDropdown() {
        qualityDropdown.ClearOptions();

        // Unity���� �����ϴ� �׷��� ����Ƽ ������ �����ͼ� ��Ӵٿ� �޴��� �߰�
        List<string> qualityOptions = new List<string>(QualitySettings.names);
        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.onValueChanged.AddListener(QualityDropdownValueChanged);
    }

    public int GetResolutionIndex(Resolution resolution) {
        Resolution currentResolution = resolution;
        Resolution[] availableResolutions = SettingsManager.AvailableResolutions;
        for (int i = 0; i < availableResolutions.Length; i++) {
            Resolution availableResolution = availableResolutions[i];
            if (availableResolution.width == currentResolution.width &&
                availableResolution.height == currentResolution.height) {
                return i;
            }
        }
        return 0;
    }

    public override void LoadSettingsToUI(Settings loadSettings) {
        if (loadSettings == null || loadSettings.graphicsSettings == null) {
            Debug.LogWarning("No SerchDatas");
            return;
        }
        if (loadSettings == null || loadSettings.graphicsSettings == null) {
            Debug.LogWarning("No SearchDatas");
            return;
        }
        // �ػ� ���� �ε�
        resolutionDropdown.value = GetResolutionIndex(loadSettings.graphicsSettings.Resolution);
        // �׷��� ����Ƽ ���� �ε�
        qualityDropdown.value = loadSettings.graphicsSettings.qualityLevel;
        // �׷��� ǰ�� ���� �ε�
        qualityDropdown.value = settings.graphicsSettings.qualityLevel;
    }

    public override void ApplyUIToSettings(Settings settings) {
        // �ػ� ���� ����

        // ��ü ȭ�� ���� ����

        // �׷��� ����Ƽ ���� ����
        settings.graphicsSettings.qualityLevel = qualityDropdown.value;
        QualitySettings.SetQualityLevel((int)qualityDropdown.value);

        // �׷��� ǰ�� ���� ����
        settings.graphicsSettings.qualityLevel = qualityDropdown.value;
    }

    // ����ڰ� �ػ󵵸� �������� �� ȣ��Ǵ� �޼���
    private void ResolutionDropdownValueChanged(int value) {
        Resolution[] availableResolutions = SettingsManager.AvailableResolutions;
        Resolution selectedResolution = availableResolutions[value];
        
        settings.graphicsSettings.Resolution = selectedResolution;
        ScreenManager.ChangeResolution(selectedResolution);

        Debug.Log("Resolution Dropdown Value Changed: " + selectedResolution);
    }

    // ����ڰ� ��ü ȭ�� ��带 �������� �� ȣ��Ǵ� �޼���
    public void OnFullScreenModeDropdownValueChanged(int value) {
        string selectedModeString = fullscreenModeDropdown.options[value].text;
        FullScreenModeEnum selectedMode = (FullScreenModeEnum)System.Enum.Parse(typeof(FullScreenModeEnum), selectedModeString);

        Screen.fullScreenMode = ConvertFullScreenMode(selectedMode);
        Debug.Log("Resolution Dropdown Value Changed: " + value);
    }

    // FullScreenModeEnum�� Unity�� FullScreenMode�� ��ȯ
    private FullScreenMode ConvertFullScreenMode(FullScreenModeEnum mode) {
        switch (mode) {
            case FullScreenModeEnum.FullScreen:
                return FullScreenMode.FullScreenWindow;
            case FullScreenModeEnum.Windowed:
                return FullScreenMode.Windowed;
            default:
                return FullScreenMode.FullScreenWindow;
        }
    }

    // ����ڰ� �׷��� ����Ƽ�� �������� �� ȣ��Ǵ� �޼���
    private void QualityDropdownValueChanged(int value) {
        // ������ �����ϰ� ����
        settings.graphicsSettings.qualityLevel = value;

        // Unity�� �׷��� ����Ƽ �������� ����
        QualitySettings.SetQualityLevel(value);

        Debug.Log("Quality Dropdown Value Changed: " + value);
    }
}

