using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSystem : MonoBehaviour
{


    public sceneManager SceneManager;
    public GameManager GameManager;
   

    public Image BackGroundImage;



    // Start is called before the first frame update
    void Start()
    {
        SceneManager = GetComponent<sceneManager>();
        GameManager = GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
