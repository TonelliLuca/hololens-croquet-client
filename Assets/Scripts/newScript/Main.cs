using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Main : MonoBehaviour
{
    private CounterModel data;
    private CounterView view;
    private GameObject debugGameObj;
    private TextMeshProUGUI debugConsole;
    private GameObject testCube;
    // Start is called before the first frame update
    void Start()
    {
        
        testCube = GameObject.Find("TestCube");
        this.data = new CounterModel(testCube.transform.position.x, testCube.transform.position.y, testCube.transform.position.z);
        this.view = new CounterView(this.data);
        //debugGameObj = GameObject.Find("DebugTxt");
        //debugConsole = debugGameObj.GetComponent<TextMeshProUGUI>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //testCube.transform.position = new Vector3(data.cubeX, data.cubeY, data.cubeZ);
        this.view.SendData(new CounterModel(testCube.transform.position.x, testCube.transform.position.y, testCube.transform.position.z));

    }
}
