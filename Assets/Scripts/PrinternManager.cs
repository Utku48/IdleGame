using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinternManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();//oluşturulan kağıtlar bu listede tutulacak.
    public GameObject paperPrefab;//Yazıcının oluşturacağı paper prefab'ı
    public Transform exitPoint; //Kağıtların printer'dan çıkıp istiflendiği yer.
    bool isWorking;
    int stackCount = 10;
    void Start()
    {
        StartCoroutine(PrintPaper());
    }
    public void RemoveLast()
    {
        if (paperList.Count > 0)
        {
            Destroy(paperList[paperList.Count - 1]);
            paperList.RemoveAt(paperList.Count - 1);//Son paper'i kaldır
        }
    }
    IEnumerator PrintPaper()
    {
        while (true)
        {
            int rowCount = (int)paperList.Count / stackCount;//Bir stack'de kaç tane istediğimizi yazdık
            if (isWorking == true)
            {
                GameObject temp = Instantiate(paperPrefab); //Paper prefab oluştur

                temp.transform.position = new Vector3(exitPoint.position.x + ((float)rowCount / 2), 0.05f + (float)(paperList.Count % stackCount) / 40, exitPoint.position.z);//Oluşturulan paper'ın konumu 
                //Y ekseninde üst üste istiflenen paper'lerın arasındaki mesafe çok olmaması için 40'a böldük.
                //X ekseninde yan yana 10'ar tane istiflenen paperlar'ın arasındaki mesafe çok olmaması için 3 e böldük
                paperList.Add(temp);

                if (paperList.Count >= 30)//Printer üst üste 30 tane paper üretebilir.

                {
                    isWorking = false;
                }
            }
            else if (paperList.Count < 30)
            {
                isWorking = true;
            }

            yield return new WaitForSeconds(1f);//1 Saniye bekle çalış


        }
    }
}
