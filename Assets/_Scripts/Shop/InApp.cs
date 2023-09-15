using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InApp : MonoBehaviour
{
    [DllImport("__internal")] private static extern void BuyMoney();
    //[DllImport("__internal")] private static extern void BuyMoney();

    public void BuyMoneyWithButton()
    {
        BuyMoney();
    }
}
