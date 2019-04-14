using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Newtonsoft.Json.Converters;
using System.Dynamic;

public class InputScript : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;

    // public string commandsFilePath;
    public BrowserIO browserIO = new BrowserIO();
    JsonSerializer serializer = new JsonSerializer();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

        serializer.Converters.Add(new JavaScriptDateTimeConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;
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
                string clickInfo = string.Format("x: {0}, y: {1}", hit.point.x, hit.point.y);
                Debug.Log(clickInfo);
                Debug.Log("Hit " + hit.ToString());


                string json = JsonConvert.SerializeObject(hit.point, Formatting.None, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                Debug.Log(json);
                Post(json);
            }
        }
    }

    void Post(string postBody) {
        StartCoroutine(browserIO.Post("http://localhost:3000", postBody));        
    }

}