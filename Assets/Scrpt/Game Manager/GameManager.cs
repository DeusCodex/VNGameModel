using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SaveLoadManager saveLoadManager;
    public static GameData gameData;

    public event Action<GameData> SaveGame;
    public event Action<GameData> LoadGame;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        // SaveLoadManager �ν��Ͻ� ����
        saveLoadManager = new SaveLoadManager();

        // ���� ������ �ҷ����� (����� �����Ͱ� ���� ��� �⺻ ������ ����)
        gameData = saveLoadManager.LoadGameData("PlayerSave");

        if (gameData == null) {
            gameData = CreateDefaultGameData();
            saveLoadManager.SaveGameData(gameData, "PlayerSave");
        }

        // ���� ������ ��� (���÷� ���, ���� ���ӿ����� �ʿ信 ���� ���)
        Debug.Log("Loaded UID: " + gameData.uid);
        foreach (SaveData save in gameData.saveDatas) {
            Debug.Log("Player Name: " + save.playerName);
            Debug.Log("Chapter: " + save.chapter);
            Debug.Log("Dialog ID: " + save.dialogId);
        }
    }

    private GameData CreateDefaultGameData() {
        GameData defaultData = new GameData();
        defaultData.uid = "default";

        // ���÷� �� ���� ���� �����͸� ����
        SaveData save1 = new SaveData();
        save1.playerName = "Player1";
        save1.chapter = "Chapter1";
        save1.dialogId = "Dialog1";

        SaveData save2 = new SaveData();
        save2.playerName = "Player2";
        save2.chapter = "Chapter2";
        save2.dialogId = "Dialog2";

        defaultData.saveDatas.Add(save1);
        defaultData.saveDatas.Add(save2);

        return defaultData;
    }

    // ���� ��ư Ŭ�� �� ȣ���� �޼��� (����)
    public void OnSaveGameData() {
        SaveGame.Invoke(gameData);
        saveLoadManager.SaveGameData(gameData, "PlayerSave");
    }

    // �ε� ��ư Ŭ�� �� ȣ���� �޼��� (����)
    public void OnLoadGameData() {
        gameData = saveLoadManager.LoadGameData("PlayerSave");
        LoadGame?.Invoke(gameData);
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
