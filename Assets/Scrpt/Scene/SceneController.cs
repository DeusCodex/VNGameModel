using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] VNBackgroundController backgroundController;


    void Start()
    {
        backgroundController = backgroundController.GetComponent<VNBackgroundController>();
    }

    public void PlaySceneTransitionEffect(EventData eventData) {
        // ��� ���̵� ��

        // ��� ��ȯ

        // ��� ���̵� �ƿ�
    }

    public void PlayCharacterTransitionEffect(EventData eventData) {
        


    }
}
