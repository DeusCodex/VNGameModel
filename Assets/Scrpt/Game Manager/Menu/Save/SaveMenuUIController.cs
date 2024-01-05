using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenuUIController : MenuModal
{
    [SerializeField] private GameObject saveSlotPrefep;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private int slotCount;

    private List<SaveSlotComponent> saveSlots;
    private List<GameData> saveDatas;

    void Awake()
    {
        saveSlots = new List<SaveSlotComponent>();
        for (int i = 0; i < slotCount; i++) {
            GameObject slotObj = Instantiate(saveSlotPrefep, slotTransform.position, Quaternion.identity);
            SaveSlotComponent saveSlotController = slotObj.GetComponent<SaveSlotComponent>();
            slotObj.transform.SetParent(slotTransform);
            saveSlots.Add(saveSlotController);

            saveSlotController.OnClick += DoSaveEventHandler;
        }
    }

    private void OnEnable()
    {
        OpenMenu();
    }

    private void OnDisable()
    {
        CloseMenu();
    }

    public override void OpenMenu()
    {
        saveDatas = new List<GameData> {
            
        };
        

        for (int i = 0; i < slotCount; i++) {
            SaveSlotComponent slot = saveSlots[i];
            if (saveDatas.Count > i) {


            }
            else {
                slot.SetEmptySaveSlot();
            }
        }
    }

    public override void CloseMenu()
    {
        //todo : close event
    }

    // Handle the click event
    private void DoSaveEventHandler(SaveSlotComponent clickedSlot)
    {
        int index = saveSlots.IndexOf(clickedSlot);
        if (index == -1) {
            Debug.Log("Clicked on save slot at index: " + index);
            return;
        }
        Debug.Log(index);
        if (index < slotCount) {
            SaveCurrentGameData(index);
        }
        else {
            ShowOverwriteWarning();
        }
    }


    private void SaveCurrentGameData(int slotIndex)
    {
        // ���� ���� �����͸� �����ϴ� ������ ���⿡ ����
        // ���� ���, GameManager�� �ִ� SaveGame �޼��� ȣ�� ��
        //GameDataManager.Instance.SaveData(slotIndex);
        // ���� �� �޴��� �ٽ� ���� ����
        OpenMenu();
    }

    private void ShowOverwriteWarning()
    {
        // ���̺긦 ����� ���� ��� �޽����� ǥ���ϴ� ������ ���⿡ ����
        // ���� ���, ���̾�α׸� ���ų� UI�� ������Ʈ�ϴ� ���� ����
        Debug.Log("Show Overwrite Warning");
    }


}
