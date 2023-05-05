using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectArea();
    public static event OnCollectArea OnPaperCollect;
    public static PrinternManager printerManager;

    public delegate void OnDeskArea();
    public static event OnDeskArea OnPaperGive;
    public static DeskManager deskManager;


    public delegate void OnBuyArea();
    public static event OnBuyArea BuyDesk;

    public static BuyArea areaToBuy;



    public delegate void OnMoneyArea();
    public static event OnMoneyArea OnMoneyCollected;

    bool isCollecting, isGiving;
    void Start()
    {
        StartCoroutine(CollectEmnum());
    }

    IEnumerator CollectEmnum()//
    {
        while (true)
        {
            if (isCollecting == true)
            {
                OnPaperCollect();
            }
            if (isGiving == true)
            {
                OnPaperGive();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            OnMoneyCollected();
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("BuyArea"))
        {
            BuyDesk();
            areaToBuy = other.gameObject.GetComponent<BuyArea>();
        }
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = true;
            printerManager = other.gameObject.GetComponent<PrinternManager>();
        }
        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = true;
            deskManager = other.gameObject.GetComponent<DeskManager>();

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = false;
            printerManager = null;
        }
        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = false;
            deskManager = null;

        }
        if (other.gameObject.CompareTag("BuyArea"))
        {
            areaToBuy = null;
        }
    }
}

