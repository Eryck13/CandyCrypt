using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemGenerator : MonoBehaviour
{
    //The level variable is just used for testing right now
    int level = 1;
    string[] sItemType = new string[20];
    string[] WeaponName = new string[30];

    [SerializeField]
    Sprite[] WeaponSprite;
    SpriteRenderer WeaponRend;
    GameObject Weapon = new GameObject();

    EnemyHealth enemyHealth;
    Transform enemypos;

    public Rigidbody Heart;
    public Rigidbody Coke;

    const int NUM_ITEM_TYPES = 2;

    const int MIN_BASE_MIN_DMG = 0;
    const int MAX_BASE_MIN_DMG = 2;
    const int MAX_BASE_MAX_DMG = 2;
    const int MIN_BASE_MAX_DMG = 4;
    const int NUM_WEAPON_ICONS = 3;

    const int NUM_PICKUPS = 2;
    const int MAX_ITEM_GEN_RANGE = 10;
    void Start()
    {
        sItemType[0] = "Consumable";
        sItemType[1] = "Weapon";
        sItemType[2] = "Bow";
        sItemType[3] = "Shield";
        sItemType[4] = "Armour";
        sItemType[5] = "Boots";
    }
    private void Update()
    {
        enemypos = enemyHealth.localPos;
    }
    void GenerateWeaponStats()
    {
        //generates the weapon's stats
        int iMinDMGGenerator = Random.Range(MIN_BASE_MIN_DMG + level, MAX_BASE_MIN_DMG + level);
        int iMaxDMGGenerator = Random.Range(MIN_BASE_MAX_DMG + level, MAX_BASE_MAX_DMG + level);

        int iMinDMG = iMinDMGGenerator;
        int iMaxDMG = iMaxDMGGenerator;
        // damageRange = Random.Range(iMinDMGGenerator, iMaxDMGGenerator);
    }

    void GenerateWeaponSprite()
    {
        //generate a random value
        //choose the sprite based on the value

        int spriteVal = Random.Range(1, NUM_WEAPON_ICONS);

        WeaponRend = Weapon.AddComponent<SpriteRenderer>();
        WeaponRend.sprite = WeaponSprite[spriteVal - 1];

    }

    void GenerateWeapon()
    {
        GenerateWeaponStats();
        GenerateWeaponSprite();
        Weapon.SetActive(true);
        Weapon.tag = sItemType[1];
    }

    //finna be used later for generating consumables
    void GenerateConsumable()
    {
        int iWhichConsumable = Random.Range(1, NUM_PICKUPS);

        switch (iWhichConsumable)
        {
            case 1:
                Rigidbody CokePickup = Instantiate(Coke, enemypos);
                CokePickup.tag = sItemType[0];
                break;

            case 2:
                Rigidbody HeartPickup = Instantiate(Heart, enemypos);
                HeartPickup.tag = sItemType[0];
                break;
        }
    }

    public void generateitem()
    {
        int iWhatItem = Random.Range(1, NUM_ITEM_TYPES);
        
        switch (iWhatItem)
        {
            case 1:
                GenerateConsumable();
                break;
            case 2:
                GenerateWeapon();
                break;

            default:
                break;               
        }
       
    }

    
}

