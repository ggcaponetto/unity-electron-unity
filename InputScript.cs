using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using UnityEngine.Networking;

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

                // genrate the json to write
                Dictionary<string, string> postBody = new Dictionary<string, string>();
                postBody.Add("action", "click");
                Dictionary<string, string> hitPoint = new Dictionary<string, string>();
                hitPoint.Add("x", hit.point.x.ToString());
                hitPoint.Add("y", hit.point.y.ToString());
                postBody.Add("payload", JsonConvert.SerializeObject(hitPoint));
                string jsonPostBody = JsonConvert.SerializeObject(postBody);
                Post(jsonPostBody);
            }
        }
    }

    void Post(string postBody) {
        StartCoroutine(browserIO.Post("http://localhost:3000", postBody));        
    }

}