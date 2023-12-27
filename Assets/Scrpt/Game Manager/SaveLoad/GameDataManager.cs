using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    private SaveFileManager saveLoadManager;
    private GameData gameData;

    // GameDataManager�� ����Ͽ� ������ ���� 
    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        gameData = new GameData();
        saveLoadManager = new SaveFileManager();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        string saveFileName = "Save/save01";
        // ������ ���� 

        // ���� ���� ������ ������Ʈ

        if (saveLoadManager != null) {
            saveLoadManager.SaveDataToLocal(saveFileName, gameData);
        }

    }

    public void LoadData()
    {
        string saveFileName = "Save/save01";
        // ������ �ҷ����� �õ�

        // �ҷ��� ������ ���ӿ� ����

        if (saveLoadManager != null) {
            gameData = saveLoadManager.LoadDataToLocal(saveFileName);
        }
    }

    public void LoadSaveConfig()
    {

    }

    public void UpdateSaveConfig()
    {

    }
}
