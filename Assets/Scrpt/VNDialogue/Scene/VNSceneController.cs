using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNSceneController : MonoBehaviour
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

    public void PlayEventScene(EventData eventData) {

    }

    public void PlayCharacterTransitionEffect(EventData eventData) {
        //


    }
}
