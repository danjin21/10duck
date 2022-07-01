using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject Unit_Popup;

    public CreateUnit CreateUnit;


    public List<BaseController> Units = new List<BaseController>();

    public List<PlayerController> Players = new List<PlayerController>();
    public List<MonsterController> Monsters = new List<MonsterController>();

    // Start is called before the first frame update
    void Start()
    {


        Unit_Popup = GameObject.Find("Canvas/Unit_Popup").gameObject;
        Unit_Popup.SetActive(false);

        CreateUnit = GetComponent<CreateUnit>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {

        for (int i = 0; i < 5; i++)
        {


            GameObject Monster = Instantiate(Resources.Load<GameObject>("Prefab/Monster"));


            MonsterController M = Monster.GetComponent<MonsterController>();

            M.templateId = 9000;
            M.speed = 1;
            M.id = Units.Count;
            M.position = 9+i*10;

            Units.Add(M);
            Monsters.Add(M);

            M.Init();
        }
    }
}
