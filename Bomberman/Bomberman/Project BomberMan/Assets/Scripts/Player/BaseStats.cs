using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    [SerializeField]
    private int speed = 2;
    [SerializeField]
    private int attack = 1;
    [SerializeField]
    private int defense = 0;
    [SerializeField]
    private int health = 9;

    private int maxHealthArmor = 9;
    private int maxArmor = 6;
    private int maxHelmet = 3;

    public bool hasArmor = false;
    public bool hasHelmet = false;

    private PermStatMod statMods;
    private TempStatMod tempStatMods;

    private void Awake()
    {
        statMods     = this.GetComponent<PermStatMod>();
        tempStatMods = this.GetComponent<TempStatMod>();
    }

    private void Update()
    {
        if (defense <= 0)
        {
            hasHelmet = false;
            hasArmor  = false;
        }
        else if (hasHelmet && defense <= 3)
        {
            hasArmor = false;
        }
    }

    public float GetTotalModdedSpeed()
    {
        if(this.GetComponent<PlayerCollision>().isAreaTransition)
        {
            return 0;
        }
        else if(this.GetComponent<PlayerCollision>().isSlowed)
        {
           return (speed + tempStatMods.GetTempSpeedMod()) / 2.5f;
        }
        else
        {
            return speed + tempStatMods.GetTempSpeedMod();
        }
    }

    public int GetTotalModdedAttack()
    {
        return attack + statMods.GetPermAttackMod() + tempStatMods.GetTempAttackMod();
    }

    public int GetTotalModdedDefense()
    {
        return defense;
    }

    public int GetTotalModdedHealth()
    {
        return health;
    }

    public void IncreaseHealth(int increaseVal)
    {
        health += increaseVal;
        if(health >= maxHealthArmor)
        {
            health = maxHealthArmor;
        }
    }
    public void AddHelmetDefense(int modData)
    {
        defense += modData;
        if (defense > 3 && !hasArmor)
        {
            defense = maxHelmet;
        }
        else if (defense > maxHealthArmor)
        {
            defense = maxHealthArmor;
        }
        hasHelmet = true;
    }

    public void AddArmorDefense(int modData)
    {
        defense += modData;
        if (defense > 6 && !hasHelmet)
        {
            defense = maxArmor;
        }
        else if (defense > maxHealthArmor)
        {
            defense = maxHealthArmor;
        }
        hasArmor = true;
    }

    public bool CheckAtMaxHealth()
    {
        if(maxHealthArmor == health)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TakeDamage(int value)
    {
        if(defense > 0)
        {
            defense -= value;
            if (defense < 0)
                defense = 0;
        }
        else
        {
            health -= value;
            if (health < 0)
                health = 0;
        }
    }
}
