using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] BackgroundController backgroundController;
    [SerializeField] CharacterController characterController;


    void Start()
    {
        backgroundController = backgroundController.GetComponent<BackgroundController>();
    }

    public void PlaySceneTransitionEffect(EventData eventData) {
        // ��� ���̵� ��

        // ��� ��ȯ

        // ��� ���̵� �ƿ�
    }

    public void PlayCharacterTransitionEffect(EventData eventData) {
        


    }
}
