using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject Unit_Popup;

    public CreateUnit CreateUnit;


    public List<BaseController> Units = new List<BaseController>();

    public List<PlayerController> Players = new List<PlayerController>();
    public List<MonsterController> Monsters = new List<MonsterController>();

    public NexusController Nexus;

    public GameObject PlayButton;

    public bool checkClear = false;

    public GameObject GameClearPopup;

    public int gold = 0;
    public int count = 0;
    public int Time = 0;
    public int mana = 10;


    public Text GoldText;
    public Text WaveText;
    public Text TimeText;
    public Text ManaText;

    public List<PlayerController> PlayerDecks = new List<PlayerController>();


    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
            RefreshSource();
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            RefreshSource();
        }
    }

    public int Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
            RefreshSource();
        }
    }


    int TotalCount = 3;

    public void CheckClear()
    {


        if(Monsters.Count ==0 && Count == TotalCount)
        {
            checkClear = true;
            GameClearPopup.gameObject.SetActive(true);
            Gold += 100;
        }
        else
        {

        }


    }

    public void CreateDeck()
    {
        int PlayerCount = 3;

        for (int i = 0; i < PlayerCount; i++)
        {
            GameObject PlayerDeck = Instantiate(Resources.Load<GameObject>("Prefab/Deck_Unit"));
            PlayerDeck.transform.parent = Unit_Popup.transform.GetChild(1);
            PlayerDeck.GetComponent<Deck_Unit>().PlayerKind = i + 1;



            PlayerDeck.gameObject.transform.GetChild(0).transform.GetComponent<Text>().text = $"Duck : {i + 1} \n $ {100*(i+1)}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        Unit_Popup = GameObject.Find("Canvas/Unit_Popup").gameObject;
        Unit_Popup.SetActive(false);

        CreateUnit = GetComponent<CreateUnit>();

        Nexus = GameObject.Find("Canvas/Nexus").GetComponent<NexusController>();

        PlayButton = GameObject.Find("Canvas/Bt_Play").gameObject;

        GameClearPopup = GameObject.Find("Canvas/GameClear");
        GameClearPopup.gameObject.SetActive(false);

        GoldText = GameObject.Find("Canvas/Info/Src_Gold").GetComponent<Text>();
        WaveText = GameObject.Find("Canvas/Info/Src_Wave").GetComponent<Text>();
        TimeText = GameObject.Find("Canvas/Info/Src_Time").GetComponent<Text>();
        ManaText = GameObject.Find("Canvas/Info/Src_Mana").GetComponent<Text>();
        RefreshSource();

        Mana = 15;

        CreateDeck();
    }

    public void RefreshSource()
    {
        GoldText.text = "$ "+ Gold.ToString();
        WaveText.text = "Wave : " + Count.ToString() + "/"+TotalCount;
        TimeText.text = Time.ToString();
        ManaText.text = "Mana : " + Mana.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Coroutine A = null;




    public void StartWave()
    {

        if (Count == 0)
        {
            checkClear = false;

            PlayButton.GetComponent<Button>().interactable = false;

            Count += 1;
            StartCoroutine(CoPlayWave());


        }

        
     
    }


    IEnumerator CoPlayWave()
    {
        

        for (int i = 0; i < 5; i++)
        {


            GameObject Monster = Instantiate(Resources.Load<GameObject>("Prefab/Monster"));


            MonsterController M = Monster.GetComponent<MonsterController>();

            M.templateId = 9000;
            M.speed = 5;
            M.id = Units.Count;
            M.position = 9 + i * 10;
            M.Hp = 5;
            M.maxHp = M.Hp;
            M.atk = 1;
            M.atkSpeed = 2;

            Monsters.Add(M);

            M.Init();
        }        

        yield return new WaitForSeconds(3.0f);

        if (Count < TotalCount)
        {
            Count += 1;
            StartCoroutine(CoPlayWave());
        }


    }


}
