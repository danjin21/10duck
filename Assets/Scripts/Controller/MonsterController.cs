using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : BaseController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        base.Init();

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);


        if(position<10)
        {
            if (position == 0)
            {

                Color color;

                Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];
                ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                Tile_Unit.gameObject.GetComponent<Image>().color = color;


            }
            else
            {
                // 갈 수 있는지 없는지 체크

                position -= 1;
                tileMove();
                StartCoroutine(Move());

            }
        }
        else
        {
            if (position %10 == 0)
            {

                Color color;

                Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];
                ColorUtility.TryParseHtmlString("#FFFFFF", out color);
                Tile_Unit.gameObject.GetComponent<Image>().color = color;


            }
            else
            {

                // 갈 수 있는지 없는지 체크


                position -= 1;
                tileMove();
                StartCoroutine(Move());

            }
        }



    }
}
