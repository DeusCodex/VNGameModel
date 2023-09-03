using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public ScreenManager Instance {get; private set; }

    void Awake() {
        if(Instance != null) {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    // �ػ� ���� ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    public static void ChangeResolution(int width, int height) {
        // ������ �ػ� ����
        Resolution newResolution = new Resolution {
            width = width,
            height = height,
            refreshRate = Screen.currentResolution.refreshRate // ���� �������� �ӵ� ���
        };

        // ������ �ػ� ����
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    // �ػ� ���� ��ư�� Ŭ���� �� ȣ��Ǵ� �Լ�
    public static void ChangeResolution(Resolution resolution) {
        // ������ �ػ� ����
        Resolution newResolution = new Resolution {
            width = resolution.width,
            height = resolution.height,
            refreshRate = Screen.currentResolution.refreshRate // ���� �������� �ӵ� ���
        };

        // ������ �ػ� ����
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen);
    }

    // ��ü ȭ�� ���� ����
    public static void SetFullScreen(bool setMode) {
        Screen.fullScreen = setMode;
    }

    public static void SetFullScreenMode(FullScreenMode screenMode) {
        Screen.fullScreenMode = screenMode;
    }

    // ������ �ӵ� ���� ����
    public static void SetFrameRateLimit(int frameRate) {
        // ���� �Ǵ� 0�� ��쿡�� ������ ������ �ǹ��մϴ�.
        if (frameRate <= 0) {
            Application.targetFrameRate = -1;
        }
        else {
            Application.targetFrameRate = frameRate;
        }
    }

    // �׷��� ǰ�� ����
    public static void SetGraphicsQuality(int qualityLevel) {
        // ��ȿ�� ǰ�� ���� (0���� QualitySettings.names.Length - 1����)���� ����
        qualityLevel = Mathf.Clamp(qualityLevel, 0, QualitySettings.names.Length - 1);

        // �׷��� ǰ�� ���� ����
        QualitySettings.SetQualityLevel(qualityLevel);
    }

    // ��Ƽ�ٸ���� ����
    public static void SetAntiAliasingLevel(int antiAliasingLevel) {
        // ��ȿ�� ��Ƽ�ٸ���� �������� ����
        antiAliasingLevel = Mathf.Clamp(antiAliasingLevel, 0, 8); // �ִ� ������ 8�� ����
        QualitySettings.antiAliasing = antiAliasingLevel; // ���� Unity�� QualitySettings�� ���� ����


        // ��Ƽ�ٸ���� ���� ����
        QualitySettings.antiAliasing = antiAliasingLevel;
    }

    // V-Sync ����
    public static void SetVSync(int vSyncLevel) {
        // ��ȿ�� V-Sync ������ ����
        vSyncLevel = Mathf.Clamp(vSyncLevel, 0, 2);

        // V-Sync ���� ����
        QualitySettings.vSyncCount = vSyncLevel;
    }
}
