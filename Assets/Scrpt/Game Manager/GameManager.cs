using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private SaveLoadManager saveLoadManager;
    public static UserData userData;

    public event Action<UserData> SaveGame;
    public event Action<UserData> LoadGame;

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
        userData = saveLoadManager.LoadUserData("PlayerSave");

        if (userData == null) {
            userData = CreateDefaultUserData();
            saveLoadManager.SaveUserData(userData, "PlayerSave");
        }

        PrintUserData();
    }

    private UserData CreateDefaultUserData() {
        UserData defaultData = new UserData();
        defaultData.uid = "";

        // ���÷� �� ���� ���� �����͸� ����
        SaveData save1 = new SaveData();
        save1.playerName = "";
        save1.chapter = "";
        save1.dialogId = "";

        defaultData.saveDatas.Add(save1);

        return defaultData;
    }

    private void PrintUserData() {
        // ���� ������ ��� (���÷� ���, ���� ���ӿ����� �ʿ信 ���� ���)
        Debug.Log("Loaded UID: " + userData.uid);
        foreach (SaveData save in userData.saveDatas) {
            Debug.Log("Player Name: " + save.playerName);
            Debug.Log("Chapter: " + save.chapter);
            Debug.Log("Dialog ID: " + save.dialogId);
        }
    }

    // ���� ��ư Ŭ�� �� ȣ���� �޼��� (����)
    public void OnSaveUserData() {
        SaveGame?.Invoke(userData);
        saveLoadManager.SaveUserData(userData, "PlayerSave");
    }

    // �ε� ��ư Ŭ�� �� ȣ���� �޼��� (����)
    public void OnLoadUserData() {
        userData = saveLoadManager.LoadUserData("PlayerSave");
        LoadGame?.Invoke(userData);
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
