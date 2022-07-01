using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Background : UI_Base
{

    [SerializeField]
    Image _icon = null;

    public MainSystem MainSystem;

    // Start is called before the first frame update
    public override void Init()
    {
        _icon = GetComponent<Image>();

        _icon.gameObject.BindEvent((e) =>
        {
            // 왼쪽 클릭이 아니면 리턴
            if (e.button == PointerEventData.InputButton.Left)
            {
                GameObject Unit_Popup;
                Unit_Popup = MainSystem.GameManager.Unit_Popup;

                Unit_Popup.SetActive(false);

                MainSystem.GameManager.CreateUnit.CurrentTile = -1;


            }
            else if (e.button == PointerEventData.InputButton.Right)
            {



            }
        }, Define.UIEvent.Click);
    }

    // Start is called before the first frame update
    void Start()
    {
        MainSystem = GameObject.Find("MainSystem").gameObject.GetComponent<MainSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
