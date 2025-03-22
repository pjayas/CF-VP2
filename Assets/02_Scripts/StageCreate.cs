using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    const int stageSize = 30;

    public Transform player; // �÷��̾� ��ġ ����
    public GameObject[] stagePrefabs; // ������ ������������ ������
    public List<GameObject> stageList = new List<GameObject>();// ������ ������ ���
    public int stageCapacity; //���ϴ� �������� �뷮 ����

    int lastindex = -1; // �����ֱٿ� ������ �������� ��ȣ
    void Start()
    {
        UpdateStage(stageCapacity);
    }

    void Update()
    {
        int currentIndex = (int)(player.position.z / stageSize);
        if(currentIndex + stageCapacity>lastindex)
        {
            UpdateStage(currentIndex + stageCapacity);
        }
    }

    void UpdateStage(int index)
    {
        if (index <= lastindex)
            return;
        for(int i = lastindex+1;i<=index; i++)
        {
            GameObject stage = GenerateStage(i);
            stageList.Add(stage);
        }
        //�������� ������ �������� ������ �������� �����ϴ�
        while (stageList.Count > stageCapacity +2)
        {
            DestroyOldStage();
        }
        lastindex = index;
    }
    GameObject GenerateStage(int index)
    {
        int i = Random.Range(0, stagePrefabs.Length);

        GameObject newstage = Instantiate(
            stagePrefabs[i],
            new Vector3(0, 0, index * stageSize),
            Quaternion.identity);

        return newstage;
    }
    void DestroyOldStage()
    {
        GameObject oldstage = stageList[0]; // ����Ʈ�� ù��° ������Ʈ ����
        stageList.RemoveAt(0); // ����Ʈ�� ù ��° ��� ����
        Destroy(oldstage); // ������Ʈ ����
    }
}
