using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static System.Action OnGameStart;
    public static System.Action OnGameEnd;
    public static System.Action OnRoundStart;
    public static System.Action OnRoundEnd;

    public static int RoundNumber;

    public static int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            OnLevelChanged?.Invoke();
        }
    }
    static int level;
    public static System.Action OnLevelChanged;
    [SerializeField] TextMeshProUGUI levelText;

    public static int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            OnGoldChanged?.Invoke();
        }
    }
    static int gold;
    public static System.Action OnGoldChanged;
    [SerializeField] TextMeshProUGUI goldText;

    public static int Gems
    {
        get
        {
            return gems;
        }
        set
        {
            gems = value;
            OnGemsChanged?.Invoke();
        }
    }
    static int gems;
    public static System.Action OnGemsChanged;
    [SerializeField] TextMeshProUGUI gemsText;

    public static int Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;
            OnExperienceChanged?.Invoke();
        }
    }
    static int experience;
    public static System.Action OnExperienceChanged;
    [SerializeField] TextMeshProUGUI experienceText;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        OnGoldChanged += UpdateGoldUI;
        OnGemsChanged += UpdateGemsUI;
        OnLevelChanged += UpdateLevelUI;
        OnExperienceChanged += UpdateExperienceUI;
    }

    private void OnDisable()
    {
        OnGoldChanged -= UpdateGoldUI;
        OnGemsChanged -= UpdateGemsUI;
        OnLevelChanged -= UpdateLevelUI;
        OnExperienceChanged -= UpdateExperienceUI;
    }

    private void Start()
    {
        StartGame();
    }

    [Button]
    public void StartGame()
    {
        OnGameStart?.Invoke();
        RoundNumber = 1;
        Experience = 0;
        Level = 3;
        Gold = 10;
        Gems = 0;
    }

    [Button]
    public void EndGame()
    {
        OnGameEnd?.Invoke();
    }

    [Button]
    public void StartRound()
    {
        OnRoundStart?.Invoke();
    }

    [Button]
    public void EndRound()
    {
        RoundNumber++;
        OnRoundEnd?.Invoke();
    }

    public static void AddGold(int num)
    {
        Gold += num;
    }

    public static void RemoveGold(int num)
    {
        Gold -= num;
    }

    public static void AddGems(int num)
    {
        Gems += num;
    }

    public static void RemoveGems(int num)
    {
        Gems -= num;
    }

    public static void AddExperience(int num)
    {
        Experience += num;
    }

    public static void ResetExperience()
    {
        Experience = 0;
    }

    public static void AddLevel()
    {
        Level++;
    }

    void UpdateLevelUI()
    {
        levelText.SetText("Level: " + level);
    }

    void UpdateExperienceUI()
    {
        experienceText.SetText("Experience: " + experience);
    }

    void UpdateGoldUI()
    {
        goldText.SetText("Gold: " + gold);
    }

    void UpdateGemsUI()
    {
        gemsText.SetText("Gems: " + gems);
    }

    private void Update()
    {
#if UNITY_EDITOR
        SpeedUp();
#endif
    }

    void SpeedUp()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Time.timeScale = 5;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            Time.timeScale = 1;
    }
}
