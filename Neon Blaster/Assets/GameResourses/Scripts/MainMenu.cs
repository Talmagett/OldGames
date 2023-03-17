using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField]

    private BackgroundScript BGScript;
    [Space]
    [Space]
    [Tooltip("Shop")]
    public RectTransform BG;
    private HeroScript heroScript;

    private ShopSave shopSV = new ShopSave();
    private string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "ShopSave.json");
        if (File.Exists(path))
        {
            shopSV = JsonUtility.FromJson<ShopSave>(File.ReadAllText(path));
            LoadShopSaves();
        }
        GameControllScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        BGScript = GameObject.Find("Background").GetComponent<BackgroundScript>();
        if (GameObject.Find("Hero") != null) heroScript = GameObject.Find("Hero").GetComponent<HeroScript>();
    }
    
    private void LoadShopSaves() {

        LvlDamage = shopSV.DamageLVLSave;
        LvlShotDelay = shopSV.AttackSpeedLVLSave;
        LvlAddGun = shopSV.ExtraGunLVLSave;
        LvlShipSpeed = shopSV.ShipSpeedLVLSave;
        LvlLaserGun = shopSV.LaserGunLVLSave;
        LvlShield = shopSV.ShieldLVLSave;
        LvlBonuses = shopSV.BonusesSave;
        LaserBeamBuy = shopSV.LaserBeamShop;
        WindBlowBuy = shopSV.WindBlowShop;
        SlowTimeBuy = shopSV.SlowTimeShop;
        ReviveBuy = shopSV.RevivalShop;


        DamageText.text = LvlDamage.ToString();
        ShotDelayText.text = LvlShotDelay.ToString();
        AddGunText.text = LvlAddGun.ToString();
        ShipSpeedText.text = LvlShipSpeed.ToString();
        LaserGunText.text = LvlLaserGun.ToString();
        ShieldText.text = LvlShield.ToString();
        BonusesText.text = LvlBonuses.ToString();

        DamageCost = (LvlDamage * 100) + 100;
        ShotDelayCost = (LvlShotDelay * 80) + 180;
        AddGunCost = 100 * (int)Mathf.Pow(2,LvlAddGun);
        ShipSpeedCost = LvlShipSpeed * 50 + 200;
        LaserGunCost = (LvlLaserGun * 60) + 140;
        BonusesCost = (LvlBonuses * 400) + 1200;
        ShieldCost = (LvlShield * 500) + 1500;
    }

    [Tooltip("Damage")]
    public int LvlDamage = 1;
    public int MaxLvlDamage;
    public Button DamageButton;
    public int DamageCost;
    public Text DamageText;
    public Text DamageTextCost;
    public void UpgradeDamage()
    {
        LvlDamage++;
        BGScript.MoneyCount -= DamageCost;
        DamageCost = (LvlDamage * 100) + 100;
    }

    [Space]
    [Tooltip("ShotDelay")]
    public int LvlShotDelay = 1;
    public int MaxLvlShotDelay;
    public Button ShotDelayButton;
    public int ShotDelayCost;
    public Text ShotDelayText;
    public Text ShotDelayTextCost;
    public void UpgradeShotDelay()
    {
        LvlShotDelay++;
        BGScript.MoneyCount -= ShotDelayCost;
        ShotDelayCost = (LvlShotDelay * 80) + 180;
    }

    [Space]
    [Tooltip("AddGun")]
    public int LvlAddGun = 1;
    public int MaxLvlAddGun;
    public Button AddGunButton;
    public int AddGunCost;
    public Text AddGunText;
    public Text AddGunTextCost;
    public void UpgradeAddGun()
    {
        LvlAddGun++;
        BGScript.MoneyCount -= AddGunCost;
        AddGunCost = 100 * (int)Mathf.Pow(2, LvlAddGun);
    }


    [Space]
    [Tooltip("ShipSpeed")]
    public int LvlShipSpeed = 0;
    public int MaxLvlShipSpeed;
    public Button ShipSpeedButton;
    public int ShipSpeedCost;
    public Text ShipSpeedText;
    public Text ShipSpeedTextCost;
    public void UpgradeShipSpeed()
    {
        LvlShipSpeed++;
        BGScript.MoneyCount -= ShipSpeedCost;
        ShipSpeedCost = LvlShipSpeed * 50 + 200;
    }

    [Space]
    [Tooltip("LaserGun")]
    public int LvlLaserGun = 0;
    public int MaxLvlLaserGun;
    public Button LaserGunButton;
    public int LaserGunCost;
    public Text LaserGunText;
    public Text LaserGunTextCost;
    
    public void UpgradeLaserGun()
    {
        LvlLaserGun++;
        BGScript.MoneyCount -= LaserGunCost;
        LaserGunCost = (LvlLaserGun * 60) + 140;
    }

    [Space]
    [Tooltip("Bonuses")]
    public int LvlBonuses = 0;
    public int MaxLvlBonuses;
    public Button BonusesButton;
    public int BonusesCost;
    public Text BonusesText;
    public Text BonusesTextCost;

    public void UpgradeBonuses()
    {
        LvlBonuses++;
        BGScript.MoneyCount -= BonusesCost;
        BonusesCost = (LvlBonuses * 400) + 1200;
    }

    [Space]
    [Tooltip("SlowTime")]
    public bool SlowTimeBuy;
    public Button SlowTimeButton;
    public int SlowTimeCost;
    public Text SlowTimeTextCost;
    public void BuySlowTime()
    {
        SlowTimeBuy = true;
        BGScript.MoneyCount -= SlowTimeCost;
        shopSV.SlowTimeShop = SlowTimeBuy;
    }

    [Space]
    [Tooltip("WindBlow")]
    public bool WindBlowBuy;
    public Button WindBlowButton;
    public int WindBlowCost;
    public Text WindBlowTextCost;
    public void BuyWindBlow()
    {
        WindBlowBuy = true;
        BGScript.MoneyCount -= WindBlowCost;
        shopSV.WindBlowShop = WindBlowBuy;
    }

    [Space]
    [Tooltip("LaserBeam")]
    public bool LaserBeamBuy;
    public Button LaserBeamButton;
    public int LaserBeamCost;
    public Text LaserBeamTextCost;
    public void BuyLaserBeam()
    {
        LaserBeamBuy = true;
        BGScript.MoneyCount -= LaserBeamCost;
        shopSV.LaserBeamShop = LaserBeamBuy;
    }

    [Space]
    [Tooltip("Shields")]
    public int LvlShield = 0;
    public int MaxLvlShield;
    public Button ShieldButton;
    public int ShieldCost;
    public Text ShieldText;
    public Text ShieldTextCost;

    public void UpgradeShield()
    {
        LvlShield++;
        BGScript.MoneyCount -= ShieldCost;
        ShieldCost = (LvlShield * 500) + 1500;
    }

    [Space]
    [Tooltip("Revive")]
    public bool ReviveBuy;
    public Button ReviveButton;
    public int ReviveCost;
    public Text ReviveTextCost;
    public void BuyRevive()
    {
        ReviveBuy = true;
        BGScript.MoneyCount -= ReviveCost;
        shopSV.RevivalShop = ReviveBuy;
    }
    public GameObject WindSpell;
    public GameObject TimeSpell;
    public GameObject BeamSpell;

    public void ToShop() {
        BG.localPosition = new Vector3(0,900,0);
    }
    public void ToMenuBack() {
        BG.localPosition = new Vector3(0, 0, 0);
    }

    public void ExitGame() {
        Application.Quit();
    }

    [Space]
    [Tooltip("Pause/Play")]
    public GameObject ContinueButton;
    public GameObject PauseButton;
    public GameObject ToMenu;
    private GameControllerScript GameControllScript;
    private bool isPaused = false;

    public void PauseGame() {
        ContinueButton.SetActive(true);
        ToMenu.SetActive(true);
        PauseButton.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void PlayGame() {
        ContinueButton.SetActive(false);
        ToMenu.SetActive(false);
        PauseButton.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void ToMenuPause()
    {
        ContinueButton.SetActive(false);
        PauseButton.SetActive(false);
        isPaused = false;
        GameControllScript.Restart();
        ToMenu.SetActive(false);
    }

    [Space]
    public GameObject Spells;
    [Space]
    [Tooltip("Reset")]
    public GameObject ToResetButton;
    public GameObject ResetButton;
    private bool isOpen = false;

    public void ToReset() {
        if (!isOpen)
        {
            isOpen = true;
            ResetButton.SetActive(true);
        }
        else {isOpen = false;
        ResetButton.SetActive(false); }
    }

    public void ResetGame (){
        BGScript.Reset();
        LvlDamage=1;
        LvlShotDelay=1;
        LvlAddGun=1;
        LvlShipSpeed=0;
        LvlLaserGun=1;
        LvlShield=0;
        LvlBonuses=0;

        WindBlowBuy = false;
        LaserBeamBuy = false;
        SlowTimeBuy = false;
        ReviveBuy = false;

        DamageCost = (LvlDamage * 100) + 100;
        ShotDelayCost = (LvlShotDelay * 80) + 180;
        AddGunCost = 100 * (int)Mathf.Pow(2, LvlAddGun);
        ShipSpeedCost = LvlShipSpeed * 50 + 200;
        LaserGunCost = (LvlLaserGun * 60) + 140;
        BonusesCost = (LvlBonuses * 400) + 1200;
        ShieldCost = (LvlShield * 500) + 1500;
    }


    void Update()
    {
        if (GameControllScript.isPlaying && !isPaused) PauseButton.SetActive(true);
        else PauseButton.SetActive(false);
        if (GameControllScript.isPlaying) Spells.SetActive(true);
        else Spells.SetActive(false);
        DamageText.text = LvlDamage.ToString();
        ShotDelayText.text = LvlShotDelay.ToString();
        AddGunText.text = LvlAddGun.ToString();
        ShipSpeedText.text = LvlShipSpeed.ToString();
        LaserGunText.text = LvlLaserGun.ToString();
        ShieldText.text = LvlShield.ToString();
        BonusesText.text = LvlBonuses.ToString();

        shopSV.DamageLVLSave = LvlDamage;
        shopSV.AttackSpeedLVLSave = LvlShotDelay;
        shopSV.ExtraGunLVLSave = LvlAddGun;
        shopSV.ShipSpeedLVLSave = LvlShipSpeed;
        shopSV.LaserGunLVLSave = LvlLaserGun;
        shopSV.ShieldLVLSave = LvlShield;
        shopSV.BonusesSave = LvlBonuses;
        shopSV.LaserBeamShop = LaserBeamBuy;
        shopSV.WindBlowShop = WindBlowBuy;
        shopSV.SlowTimeShop = SlowTimeBuy;
        shopSV.RevivalShop = ReviveBuy;
        if (BGScript.MoneyCount < DamageCost) DamageButton.interactable = false;
        else DamageButton.interactable = true;

        if (BGScript.MoneyCount < ShotDelayCost) ShotDelayButton.interactable = false;
        else ShotDelayButton.interactable = true;

        if (BGScript.MoneyCount < AddGunCost) AddGunButton.interactable = false;
        else AddGunButton.interactable = true;

        if (BGScript.MoneyCount < ShipSpeedCost) ShipSpeedButton.interactable = false;
        else ShipSpeedButton.interactable = true;

        if (BGScript.MoneyCount < LaserGunCost) LaserGunButton.interactable = false;
        else LaserGunButton.interactable = true;

        if (BGScript.MoneyCount < ShieldCost) ShieldButton.interactable = false;
        else ShieldButton.interactable = true;

        if (BGScript.MoneyCount < BonusesCost) BonusesButton.interactable = false;
        else BonusesButton.interactable = true;

        if (BGScript.MoneyCount < WindBlowCost) WindBlowButton.interactable = false;
        else WindBlowButton.interactable = true;

        if (BGScript.MoneyCount < SlowTimeCost) SlowTimeButton.interactable = false;
        else SlowTimeButton.interactable = true;

        if (BGScript.MoneyCount < LaserBeamCost) LaserBeamButton.interactable = false;
        else LaserBeamButton.interactable = true;

        if (BGScript.MoneyCount < ReviveCost ) ReviveButton.interactable = false;
        else ReviveButton.interactable = true;

        if (LvlDamage >= MaxLvlDamage) { DamageButton.interactable = false; DamageTextCost.text = "Max"; }
        else DamageTextCost.text = DamageCost.ToString();
        if (LvlShotDelay >= MaxLvlShotDelay) { ShotDelayButton.interactable = false; ShotDelayTextCost.text = "Max"; }
        else ShotDelayTextCost.text = ShotDelayCost.ToString();
        if (LvlAddGun >= MaxLvlAddGun) { AddGunButton.interactable = false; AddGunTextCost.text = "Max"; }
        else AddGunTextCost.text = AddGunCost.ToString();
        if (LvlShipSpeed >= MaxLvlShipSpeed) { ShipSpeedButton.interactable = false; ShipSpeedTextCost.text = "Max"; }
        else ShipSpeedTextCost.text = ShipSpeedCost.ToString();
        if (LvlLaserGun >= MaxLvlLaserGun) { LaserGunButton.interactable = false; LaserGunTextCost.text = "Max"; }
        else LaserGunTextCost.text = LaserGunCost.ToString();
        if (LvlShield >= MaxLvlShield) { ShieldButton.interactable = false; ShieldTextCost.text = "Max"; }
        else ShieldTextCost.text = ShieldCost.ToString();
        if (LvlBonuses >= MaxLvlBonuses) { BonusesButton.interactable = false; BonusesTextCost.text = "Max"; }
        else BonusesTextCost.text = BonusesCost.ToString();

        if (WindBlowBuy) { WindBlowButton.interactable = false; WindBlowTextCost.text = "Bought"; WindSpell.SetActive(true); }
        else { WindBlowTextCost.text = WindBlowCost.ToString(); WindSpell.SetActive(false); }
        if (SlowTimeBuy) { SlowTimeButton.interactable = false; SlowTimeTextCost.text = "Bought"; TimeSpell.SetActive(true); }
        else {SlowTimeTextCost.text = SlowTimeCost.ToString(); TimeSpell.SetActive(false); }
        if (LaserBeamBuy) { LaserBeamButton.interactable = false; LaserBeamTextCost.text = "Bought"; BeamSpell.SetActive(true); }
        else {LaserBeamTextCost.text = LaserBeamCost.ToString(); BeamSpell.SetActive(false); }
        if (ReviveBuy) { ReviveButton.interactable = false; ReviveTextCost.text = "Bought"; }
        else ReviveTextCost.text = ReviveCost.ToString();
    }
    private void OnApplicationQuit()
    {
        File.WriteAllText(path, JsonUtility.ToJson(shopSV));
    }

}

[Serializable]
public class ShopSave
{
    public int DamageLVLSave;
    public int ExtraGunLVLSave;
    public int AttackSpeedLVLSave;
    public int ShipSpeedLVLSave;
    public int LaserGunLVLSave;
    public int ShieldLVLSave;
    public int BonusesSave;
    public bool SlowTimeShop;
    public bool WindBlowShop;
    public bool LaserBeamShop;
    public bool RevivalShop;
}