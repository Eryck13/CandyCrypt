using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermStatMod : MonoBehaviour
{

    private int armor = 0;
    private int weapon = 0;
    private int helmet = 0;
    private int defense = 0;

    public bool hasSword = false;
     
    public void SetPermAttackMod(int modData)
    {
        weapon = modData;
        hasSword = true;
    }
    
    public int GetPermAttackMod()
    {
        return weapon;
    }

}
