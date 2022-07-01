using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseController : MonoBehaviour
{

    public int id;
    public int speed;
    public int atk;
    public int templateId;

    public MainSystem MainSystem;

    public int position;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Init()
    {
        MainSystem = GameObject.Find("MainSystem").gameObject.GetComponent<MainSystem>();

        tileMove();


    }

    public void tileMove()
    {

        Color color;

        Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];

        switch (templateId)
        {
            case 10:

                ColorUtility.TryParseHtmlString("#6679F7", out color);
                Tile_Unit.gameObject.GetComponent<Image>().color = color;
                break;

            case 9000:

                ColorUtility.TryParseHtmlString("#F76666", out color);
                Tile_Unit.gameObject.GetComponent<Image>().color = color;
                break;


        }

        if(position%10 != 9)
        {
            Tile_Unit Tile_Unit_past = MainSystem.GameManager.CreateUnit.Tile_Units[position + 1];

            ColorUtility.TryParseHtmlString("#FFFFFF", out color);
            Tile_Unit_past.gameObject.GetComponent<Image>().color = color;
        }


    }


}
