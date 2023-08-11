using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string Name;
    public int XP =0;
    public int Level =1;
    public int Health = 100;
    public int Damage = 15;
    public int Armor = 0;

    public List<PlayerAttributes> Attributes = new List<PlayerAttributes>();
}
