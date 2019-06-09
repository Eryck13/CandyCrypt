using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudItems : MonoBehaviour
{
    // VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
    // 
    // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    // Make sure to attach sprites to proper areas if needed. (Script attached to BaseUI)
    /*
        sprites
        0 = broken sword
        1 = candy cane sword
        2 = lolipop sword
        3 = gingerbread armor
        4 = marshmellow helmet
        5 = Powerup
        6 = UImask
        (others may not be inlcuded here)

        slots
        Weapon Slot = Weapon_Slot
        Armor Slot = Armor_Slot
        Helmet Slot = Helmet_Slot
        Power Up Slot = PowerUp_Slot
        Player = Player

        (other things may be needed that are not included here)
    */

    public Sprite[] sprites;

    public GameObject WeaponSlot;
    public GameObject ArmorSlot;
    public GameObject HelmetSlot;
    public GameObject PowerUpSlot;
    public GameObject Player;
    public GameObject HitBox;
    public Text UIText;

    public int CurrentScore = 0;
    public int KillCounter = 0;

    float counter = 10f;
    bool bPowerUpActive = false; // for use of if statemement
    bool bArmor = false;
    bool bHelmet = false;
    bool bSword = false;
    public bool bPoweredUp = false; // for use of actual power up

    // Start is called before the first frame update
    void Start()
    {
        // settings all the sprites to default values that the player will start with
        WeaponSlot.GetComponent<Image>().sprite = sprites[0];
        ArmorSlot.GetComponent<Image>().sprite = sprites[6];
        HelmetSlot.GetComponent<Image>().sprite = sprites[6];
        PowerUpSlot.GetComponent<Image>().sprite = sprites[6];
        // finding attackhitbox for counter
        HitBox = GameObject.FindGameObjectWithTag("AttackHitBox");
    }

    // Update is called once per frame
    void Update()
    {
        bArmor = Player.GetComponent<BaseStats>().hasArmor;
        bHelmet = Player.GetComponent<BaseStats>().hasHelmet;
        bSword = Player.GetComponent<PermStatMod>().hasSword;

        if (bArmor == true) // add armor to hud
        {
            ArmorSlot.GetComponent<Image>().sprite = sprites[3];
        }
        if (bArmor == false) // remove armor to hud
        {
            ArmorSlot.GetComponent<Image>().sprite = sprites[6];
        }
        if (bSword == true)
        {
            WeaponSlot.GetComponent<Image>().sprite = sprites[2];
        }
        if (bSword == false)
        {
            WeaponSlot.GetComponent<Image>().sprite = sprites[0];
        }
        if (bHelmet == true) // add helmet to hud
        {
            HelmetSlot.GetComponent<Image>().sprite = sprites[4];
        }
        if (bHelmet == false) // remove helmet to hud    
        {
            HelmetSlot.GetComponent<Image>().sprite = sprites[6];
        }
        if (bPoweredUp == true && !bPowerUpActive) // activate powerup to hud
        {
            bPowerUpActive = true;
            counter = 10f;
        }

        PoweredUp();
        ScoreTracking();
    }
    void PoweredUp() 
    {

        if (bPowerUpActive == true) // making the powerup sprite appear
        {
            PowerUpSlot.GetComponent<Image>().sprite = sprites[5];
            counter -= Time.deltaTime;
        }
        if (counter <= 0) // after a certain amount of time make the powerup sprite dissapear
        {
            bPoweredUp = false;
            bPowerUpActive = false;
            PowerUpSlot.GetComponent<Image>().sprite = sprites[6];
        }
       
    }

    void ScoreTracking()
    {
        // checking for health to reset score
        if (Player.GetComponent<HealthUI>().health <= 0)
        {
            CurrentScore = 0;
        }
        // get score from wherever it is being held.
        //KillCounter = HitBox.GetComponent<AttackHitBox>().LocalKillCount;
        // convert score into our variable.
        CurrentScore = KillCounter * 100;
        // display score into text box for the score object.
        UIText.text = CurrentScore.ToString();
               
    }
}
