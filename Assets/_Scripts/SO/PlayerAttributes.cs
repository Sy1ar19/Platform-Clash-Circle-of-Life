using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAttributes
{
    public Attributes Attribute;
    public int Ammount;

    public PlayerAttributes(Attributes attribute, int ammount)
    {
        Attribute = attribute;
        Ammount = ammount;
    }
}
