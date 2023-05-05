using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public List<GameObject> moneyList = new List<GameObject>();

    public Transform OnDeskPoint, MoneyDropPoint;
    public GameObject paperPrefab, MoneyPrefab;

    private void Start()
    {
        StartCoroutine(GenerateMoney());
    }
    IEnumerator GenerateMoney()
    {
        while (true)
        {
            if (paperList.Count > 0)
            {
                GameObject temp = Instantiate(MoneyPrefab);
                temp.transform.position = new Vector3(MoneyDropPoint.position.x, ((float)moneyList.Count / 10), MoneyDropPoint.transform.position.z);
                moneyList.Add(temp);
                RemoveLast();

            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GivePaperOnDesk()
    {
        GameObject temp = Instantiate(paperPrefab);
        temp.transform.position = new Vector3(OnDeskPoint.position.x, (0.8f + (float)paperList.Count / 20), OnDeskPoint.transform.position.z);
        paperList.Add(temp);
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
