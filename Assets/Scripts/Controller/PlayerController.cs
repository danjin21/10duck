using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    public BaseController CounterPlayer;


    // Start is called before the first frame update
    void Start()
    {
   
    }

    public override void Damaged(int atk)
    {
        base.Damaged(atk);

        if (Hp <= 0)
        {
            MainSystem.GameManager.Units.Remove(this);
            MainSystem.GameManager.Players.Remove((PlayerController)this);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void ColorChange(int colorCode)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#FF2323", out color);

        switch (colorCode)
        {
            case 0:
                ColorUtility.TryParseHtmlString("#235BFF", out color);
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#FE23FF", out color);
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#235BFF", out color);
                break;
        }

        gameObject.GetComponent<SpriteRenderer>().color = color;
    }


    IEnumerator Battle()
    {
        if (CounterPlayer != null)
        {

            CounterPlayer.Damaged(atk);
            ColorChange(1);


            yield return new WaitForSeconds(1.0f / atkSpeed);

            if (CounterPlayer.Hp > 0)
            {
                StartCoroutine(Battle());
            }
            
            if(CounterPlayer == null)
            {
                // ColorChange(0);
                StartCoroutine(Search());
       
            }
        }
        else
        {
            StartCoroutine(Search());
            ColorChange(0);
        }

      

    }

    IEnumerator Search()
    {

        // 갈 수 있는지 없는지 체크
        BaseController monster = currentCheck(2);

        // 몬스터인지 확인
        if (monster != null && monster.GetType() == typeof(MonsterController))
        {
            ColorChange(1);
            CounterPlayer = monster;

            StartCoroutine(Battle());

        }
        else
        {
            ColorChange(0);

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(Search());

        }




    }

    public override void Init()
    {
        base.Init();

        StartCoroutine(Search());
    }


}
