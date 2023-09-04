using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class LanguageSettingUI : SettingOption
{
    public TMP_Dropdown languageDropdown; // UI Dropdown ������Ʈ

    private void Start() {
        InitializeDropdown(); // ���� Dropdown�� �ʱ�ȭ�մϴ�.
        LoadSettingsToUI(SettingsManager.GetSettings); // �� ���� ������ �ҷ��ɴϴ�.
    }

    // Dropdown�� �ʱ�ȭ�ϰ� �����Ǵ� ��� ����� ����
    private void InitializeDropdown() {
        if (languageDropdown == null) {
            Debug.LogError("Language Dropdown is not assigned.");
            return;
        }

        // �����Ǵ� ��� ��� ��������
        List<string> supportedLanguages = LocalizationSettings.AvailableLocales.Locales.ConvertAll(locale => locale.name);

        Debug.Log(supportedLanguages);

        // Dropdown�� ��� ��� �߰�
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(supportedLanguages);

        // Dropdown�� ���� ���� �׸��� ���� ���� ����
        int currentLanguageIndex = supportedLanguages.IndexOf(LocalizationSettings.SelectedLocale.name);
        languageDropdown.value = currentLanguageIndex;

        // Dropdown�� ���� �̺�Ʈ�� ��� ���� �Լ� ����
        languageDropdown.onValueChanged.AddListener(OnLanguageDropdownValueChanged);
    }

    // ����ڰ� �� �������� �� ȣ��Ǵ� �Լ�
    private void OnLanguageDropdownValueChanged(int index) {
        string selectedLanguage = languageDropdown.options[index].text;

        // ������ ���� ��� ����
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(selectedLanguage);

        // ��� ������ ���� (SettingsManager�� ����Ͽ� ������ ����)
        Settings settings = SettingsManager.GetSettings;
        settings.languageSettings.selectedLanguage = selectedLanguage;
        //SettingsManager.SaveSettings(settings);
    }

    public override void LoadSettingsToUI(Settings settings) {
        // ����� ��� ������ UI�� ����
        int selectedLanguageIndex = languageDropdown.options.FindIndex(option => option.text == settings.languageSettings.selectedLanguage);
        languageDropdown.value = selectedLanguageIndex;
    }

    public override void ApplyUIToSettings(Settings settings) {
        // ����ڰ� ������ ���� ��� ������ ����
        int selectedLanguageIndex = languageDropdown.value;
        string selectedLanguage = languageDropdown.options[selectedLanguageIndex].text;
        settings.languageSettings.selectedLanguage = selectedLanguage;

        // ����� ��� ������ ���� (SettingsManager�� ����Ͽ� ������ ����)
        //SettingsManager.SaveSettings(settings);
    }
}
