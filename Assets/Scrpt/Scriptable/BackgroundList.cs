using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundList", menuName = "ScriptableObject/Background List", order = 1)]
public class BackgroundList : ScriptableObject
{
    [Header("Backgrounds")]
    public List<Sprite> backgroundSprites = new List<Sprite>(); // ���ȭ�� ��������Ʈ ����Ʈ

    // ��Ÿ ��� �����̳� ����� �߰��� �� �ֽ��ϴ�.
}
