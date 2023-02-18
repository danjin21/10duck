﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : BaseController
{

    public BaseController CounterPlayer;
    public NexusController CounterNexus;

    bool IsMoving;

    private Vector3 m_Position_Default;
    [SerializeField] protected Vector3 m_Position_End;
    [SerializeField] protected float m_RunTime = 1.0f;




    // Start is called before the first frame update
    void Start()
    {
   
        CounterPlayer = null;

    }


    public override void Damaged(int atk)
    {
        base.Damaged(atk);

        if (Hp <= 0)
        {
            MainSystem.GameManager.Units.Remove(this);
            MainSystem.GameManager.Monsters.Remove((MonsterController)this);
        }

        MainSystem.GameManager.CheckClear();

    }


    // Update is called once per frame
    void Update()
    {

    }

    public override void Init()
    {
        base.Init();

        // CounterNexus = MainSystem.GameManager.Nexus.GetComponent<NexusController>();

        IsMoving = false;
        m_RunTime = 1.0f / speed;


        StartCoroutine(Move());
    }

    public void ColorChange(int colorCode)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#FF2323", out color);

        switch (colorCode)
        {
            case 0:
                ColorUtility.TryParseHtmlString("#FF2323", out color);
                break;
            case 1:
                ColorUtility.TryParseHtmlString("#7723FF", out color);
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#FF2323", out color);
                break;
        }

        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator Battle()
    {
 

        if (CounterPlayer != null)
        {
            ColorChange(1);
            CounterPlayer.Damaged(atk);

        }




        yield return new WaitForSeconds(1f / atkSpeed);

        if (CounterPlayer.Hp <= 0 || CounterPlayer == null)
        {
            StartCoroutine(Move());
        }

        if (CounterPlayer.Hp > 0)
        {
            StartCoroutine(Battle());
        }

    }


    IEnumerator Battle_Nexus()
    {
        CounterNexus = MainSystem.GameManager.Nexus.GetComponent<NexusController>();

        if (CounterNexus != null)
        {
            ColorChange(1);
            CounterNexus.Damaged(atk);

        }

        if (CounterNexus.Hp <= 0)
        {
            // 게임종료
        }

        yield return new WaitForSeconds(1f / atkSpeed);

        if (CounterNexus.Hp > 0)
        {
            StartCoroutine(Battle_Nexus());
        }

    }

    IEnumerator Run(float duration)
    {
        var runTime = 0.0f;

        while (runTime < duration)
        {
            runTime += Time.deltaTime;

            transform.position = Vector3.Lerp(m_Position_Default, m_Position_End, runTime / duration);

            yield return null;
        }
    }


    public void UpdateMoving(int direction)
    {        

        m_Position_Default = transform.position;
        m_Position_End = transform.position -= new Vector3(1.409692f* direction, 0, 0);
        StartCoroutine(Run(m_RunTime));
    }



    IEnumerator Move()
    {   
        // 갈 수 있는지 없는지 체크
        BaseController Currentplayer = currentCheck(1);

        if (Currentplayer == null)
        {
            UpdateMoving(1);
            ColorChange(2);
        }
        else
        {
            ColorChange(1);
        }

        yield return new WaitForSeconds(1f/ speed);



        // 맨 윗줄
        if (position<10)
        {
            if (position == 0)
            {

                // 갈 수 있는지 없는지 체크
                BaseController player = currentCheck(1);

                // 플레이어인지 확인
                if (player != null && player.GetType() == typeof(PlayerController))
                {
                    CounterPlayer = player;
                    StartCoroutine(Battle());
                }
                else
                {
                    StartCoroutine(Battle_Nexus());

                    // 위치를 넥서스 앞으로

                    Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];
                    transform.position = Tile_Unit.transform.position + new Vector3(-margin, 0, 0);


                }


            }
            else
            {
                // 갈 수 있는지 없는지 체크
                BaseController player = currentCheck(1);

                // 플레이어인지 확인
                if (player !=null && player.GetType() == typeof(PlayerController))
                {
                    CounterPlayer = player;
                    StartCoroutine(Battle());
                }
                else
                {
                    // 앞으로 나아가준다.

                    position -= 1;
                    tileMove();
                    StartCoroutine(Move());
                }




            }
        }
        // 맨 아래줄
        else
        {
            if (position %10 == 0)
            {

                // 갈 수 있는지 없는지 체크
                BaseController player = currentCheck(1);
                if (player != null)
                {
                    CounterPlayer = player;
                    StartCoroutine(Battle());
                }
                else
                {
                    StartCoroutine(Battle_Nexus());

                    // 위치를 넥서스 앞으로

                    Tile_Unit Tile_Unit = MainSystem.GameManager.CreateUnit.Tile_Units[position];
                    transform.position = Tile_Unit.transform.position + new Vector3(-margin, 0, 0);
                }


            }
            else
            {

                // 갈 수 있는지 없는지 체크
                BaseController player = currentCheck(1);
                if (player != null)
                {
                    CounterPlayer = player;
                    StartCoroutine(Battle());
                }
                else
                {
                    // 앞으로 나아가준다.

                    position -= 1;
                    tileMove();
                    StartCoroutine(Move());
                }

            }
        }



    }
}
