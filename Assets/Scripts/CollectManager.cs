using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{

    public List<GameObject> paperList = new List<GameObject>();
    public GameObject paperPrefab;
    public Transform collectPoint;
    int paperLimit = 10;


    void OnEnable()
    {
        TriggerManager.OnPaperCollect += GetPaper;
        TriggerManager.OnPaperGive += GivePaper;
    }

    void OnDisable()
    {
        TriggerManager.OnPaperCollect -= GetPaper;
        TriggerManager.OnPaperGive -= GivePaper;
    }

    void GetPaper()
    {
        if (paperList.Count <= paperLimit)
        {
            GameObject temp = Instantiate(paperPrefab, collectPoint);
            temp.transform.position = new Vector3(collectPoint.position.x,
           (0.5f + (float)paperList.Count / 40),//Player'in elinde kağıtları dizdik
            collectPoint.transform.position.z);
            paperList.Add(temp);

            if (TriggerManager.printerManager != null)
            {
                TriggerManager.printerManager.RemoveLast();
            }
        }
    }
    void GivePaper()
    {
        if (paperList.Count > 0)
        {
            TriggerManager.deskManager.GivePaperOnDesk();
            RemoveLast();
        }
    }

    public void RemoveLast()
    {
        if (paperList.Count > 0)
        {
            Destroy(paperList[paperList.Count - 1]);
            paperList.RemoveAt(paperList.Count - 1);//Son paper'i kaldır


        }
    }
}
