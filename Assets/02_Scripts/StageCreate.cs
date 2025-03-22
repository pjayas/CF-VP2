using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    const int stageSize = 30;

    public Transform player; // 플레이어 위치 정보
    public GameObject[] stagePrefabs; // 생성할 스테이지들의 프리팹
    public List<GameObject> stageList = new List<GameObject>();// 생성된 프리팹 목록
    public int stageCapacity; //원하는 스테이지 용량 갯수

    int lastindex = -1; // 가장최근에 생성한 스테이지 번호
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
        //스테이지 개수가 많아지면 오래된 스테이지 삭제하는
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
        GameObject oldstage = stageList[0]; // 리스트의 첫번째 오브젝트 저장
        stageList.RemoveAt(0); // 리스트의 첫 번째 노드 제거
        Destroy(oldstage); // 오브젝트 삭제
    }
}
