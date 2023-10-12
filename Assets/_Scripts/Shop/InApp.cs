using System.Runtime.InteropServices;
using UnityEngine;

public class InApp : MonoBehaviour
{
    [DllImport("__internal")] private static extern void BuyMoney();

    public void BuyMoneyWithButton()
    {
        BuyMoney();
    }
}
