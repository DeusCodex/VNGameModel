using UnityEngine;
using UnityEngine.UI;

public class VNScreenController : MonoBehaviour
{
    void Start() {
        // Canvas�� ����� RectTransform ��������
        RectTransform canvasRect = GetComponent<RectTransform>();

        // �ڽ��� RectTransform�� �θ� ũ�⿡ �� �°� ����
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null) {
            rectTransform = gameObject.AddComponent<RectTransform>();
        }
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        // �ڽ��� RectTransform�� Image ������Ʈ �߰� (���� ȭ�� ����)
        Image blackOutImage = gameObject.AddComponent<Image>();
        blackOutImage.color = Color.black;
        blackOutImage.raycastTarget = false; // �̺�Ʈ ����

        // �ڽ� UI ��� �߰� �� ũ�� ���� (��: �ؽ�Ʈ UI ��� �߰�)
        GameObject childObject = new GameObject("ChildUI");
        RectTransform childRectTransform = childObject.AddComponent<RectTransform>();
        childRectTransform.SetParent(rectTransform); // �θ� ����
        childRectTransform.anchorMin = Vector2.zero;
        childRectTransform.anchorMax = Vector2.one;
        childRectTransform.offsetMin = Vector2.zero;
        childRectTransform.offsetMax = Vector2.zero;

        Text textComponent = childObject.AddComponent<Text>();
        textComponent.text = "Hello, World!";
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // ��Ʈ ����
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.color = Color.white;
    }
}
