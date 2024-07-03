using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    private int XPNow;
    public int Level;
    private int xpToNext;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject lvlWindowPrefab;
    private Slider slider;
    private TMP_Text xpText;
    private TMP_Text lvlText;
    private Image starImage;
    private static bool initialized;
    private static Dictionary<int, int> xpToNextLevel = new Dictionary<int, int>();
    private static Dictionary<int, int[]> lvlReward = new Dictionary<int, int[]>();
    private void Awake()
    {
        slider = levelPanel.GetComponent<Slider>();
        xpText = levelPanel.transform.Find("XP text").GetComponent<TMP_Text>();
        starImage = levelPanel.transform.Find("Star").GetComponent<Image>();
        lvlText = starImage.transform.GetChild(0).GetComponent<TMP_Text>();

        if (!initialized)
        {
            Initialize();
        }

        xpToNextLevel.TryGetValue(Level, out xpToNext);
    }

    private void Start()
    {
        EventManager.Instance.AddListener<XpAddEvent>(OnXPAdded);
        EventManager.Instance.AddListener<LevelChangeEvent>(OnLevelChanged);
        
        UpdateUI();
    }

    private static void Initialize()
    {
        try
        {
            // path to the csv file
            string path = "LevelStats";
            
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            string[] lines = textAsset.text.Split('\n');
            
            xpToNextLevel = new Dictionary<int, int>(lines.Length - 1);
            
            for(int i = 1; i < lines.Length - 1; i++)
            {
                string[] columns = lines[i].Split(',');
                
                int lvl = -1;
                int xp = -1;
                
                int.TryParse(columns[0], out  lvl);
                int.TryParse(columns[1], out xp);

                if (lvl >= 0 && xp > 0)
                {
                    if (!xpToNextLevel.ContainsKey(lvl))
                    {
                        xpToNextLevel.Add(lvl, xp);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        initialized = true;
    }

    private void UpdateUI()
    {
        float fill = (float) XPNow / xpToNext;
        slider.value = fill;
        xpText.text = XPNow + "/" + xpToNext;
    }

    private void OnXPAdded(XpAddEvent info)
    {
        XPNow += info.amount;
        
        UpdateUI();

        if (XPNow >= xpToNext)
        {
            Level++;
            LevelChangeEvent levelChange = new LevelChangeEvent(Level);
            EventManager.Instance.QueueEvent(levelChange);
        }
    }

    public void OnLevelChanged(LevelChangeEvent info)
    {
        XPNow -= xpToNext;
        xpToNext = xpToNextLevel[info.newLevel];
        lvlText.text = (info.newLevel + 1).ToString();
        UpdateUI();

        GameObject window = Instantiate(lvlWindowPrefab, GameManager.Instance.canvas.transform);
        
        //initialize texts and images here
        
        window.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
        {
            Destroy(window);
        });
    }
}
