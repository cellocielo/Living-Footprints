using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneInitializer : MonoBehaviour
{
    public GameObject fridge;
    public GameObject fridgeX;
    public GameObject openGreenhouse;
    public GameObject openIrrigation;
    public GameObject openLand;
    public GameObject laundry;
    public Button laundryX;
    public Button dynamicPricing;
    public TextMeshProUGUI timeMessageText;
    public TimeBasedInteraction timeBasedInteraction;
    public Button openEnergyUsage;
    public TextMeshProUGUI clockText;
    public GameObject PS4;
    public Button PS4X;
    public Button openEnergy;
    public Button openStandby;
    public GameObject plants;
    public Button plantsX;
    public Button pollutants;
    public Button temperature;
    public GameObject pill;
    public Button pillX;
    public Button openContaminants;
    public GameObject tree;
    public Button treeX;
    public GameObject rainBarrel;
    public Button rainBarrelX;
    public Button openDomesticWaterUsage;
    public GameObject solarPanel;
    public Button solarPanelX;
    public Button openSolarEnergy;
    public GameObject pizzaBox;
    public Button pizzaBoxX;
    public GameObject eWaste;
    public Button eWasteX;
    public GameObject pan;
    public GameObject panX;
    public GameObject car;
    public GameObject carX;
    public Button openCO2;
    public Button openNO2;
    public GameObject jam;
    public GameObject jamX;
    public GameObject nativePlant;
    public GameObject nativePlantX;

    private void Start()
    {
        if (SceneStateManager.showFridgeUI)
        {
            fridge.SetActive(true);
            fridgeX.SetActive(true);
            openGreenhouse.gameObject.SetActive(true);
            openIrrigation.gameObject.SetActive(true);
            openLand.gameObject.SetActive(true);
            SceneStateManager.showFridgeUI = false;
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showLaundryUI) {
            laundry.SetActive(true);
            laundryX.gameObject.SetActive(true);
            dynamicPricing.gameObject.SetActive(true);
            openEnergyUsage.gameObject.SetActive(true);
            timeMessageText.text = timeBasedInteraction.GetTimeBasedMessage();
            SceneStateManager.showLaundryUI = false;
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showPlantsUI) {
            plants.SetActive(true);
            plantsX.gameObject.SetActive(true);
            pollutants.gameObject.SetActive(true);
            temperature.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }
        
        else if (SceneStateManager.showPS4UI) {
            PS4.SetActive(true);
            PS4X.gameObject.SetActive(true);
            openEnergy.gameObject.SetActive(true);
            openStandby.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showPillUI) {
            pill.SetActive(true);
            pillX.gameObject.SetActive(true);
            openContaminants.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showTreeUI) {
            tree.SetActive(true);
            treeX.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showRainBarrelUI) {
            rainBarrel.SetActive(true);
            rainBarrelX.gameObject.SetActive(true);
            openDomesticWaterUsage.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showSolarPanelUI) {
            solarPanel.SetActive(true);
            solarPanelX.gameObject.SetActive(true);
            openSolarEnergy.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showPizzaBoxUI) {
            pizzaBox.SetActive(true);
            pizzaBoxX.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showEWasteUI) {
            eWaste.SetActive(true);
            eWasteX.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showPanUI) {
            pan.SetActive(true);
            panX.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }

        else if (SceneStateManager.showCarUI) {
            car.SetActive(true);
            carX.gameObject.SetActive(true);
            openCO2.gameObject.SetActive(true);
            openNO2.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }
        
        else if (SceneStateManager.showJamUI) {
            jam.SetActive(true);
            jamX.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }
        
        else if (SceneStateManager.showNativePlantUI) {
            nativePlant.SetActive(true);
            nativePlantX.gameObject.SetActive(true);
            if (DigitalClock.Instance != null) {
                DigitalClock.Instance.TogglePause();
                DigitalClock.Instance.SetClockText(clockText);
            }
        }
    }
}