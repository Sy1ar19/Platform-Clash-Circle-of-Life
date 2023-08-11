using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] PlayerMoney playerMoney;

    public void PurchaseItem(int cost)
    {
        if (playerMoney.SpendMoney(cost))
        {
            // Выполните покупку предмета
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
