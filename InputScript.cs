using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputScript : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;

    // public string commandsFilePath;
    public BrowserIO browserIO = new BrowserIO();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");
            // Reset ray with new mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit " + ray.ToString());
                string clickInfo = string.Format("x: {0}, y: {1}", hit.point.x, hit.point.y);
                Debug.Log(clickInfo);
                Debug.Log("Hit " + hit.ToString());
                // ReadCommand();
                // WriteCommand(clickInfo);
                SendCommand(clickInfo);
            }
        }
    }

    void SendCommand(string commandString) {
        StartCoroutine(browserIO.GetRequest("http://localhost:3000/?command=" + commandString));        
    }

    void ReadCommand() {
        browserIO.ReadString();
    }
    void WriteCommand(string commandString)
    {
        browserIO.WriteString(commandString);
    }

}