using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;

public class BrowserIO
{
    [MenuItem("Tools/Write file")]
    public void WriteString(string commandString)
    {
        string path = "Assets/Resources/unity-electron/test.txt";

        // genrate the json to write
        Dictionary<string, string> commandObject = new Dictionary<string, string>();
        commandObject.Add("action", "click");
        commandObject.Add("payload", commandString);
        string jsonCommand = JsonConvert.SerializeObject(commandObject);

        // Object deserializedProduct = JsonConvert.DeserializeObject<Object>(jsonCommand);

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(jsonCommand);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("Assets/Resources/unity-electron/test.txt");
    }

    [MenuItem("Tools/Read file")]
    public void ReadString()
    {
        string path = "Assets/Resources/unity-electron/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string readText = reader.ReadToEnd();
        Debug.Log(readText);
        reader.Close();
    }


    public IEnumerator GetRequest(string uri)
    {
        Debug.Log("GetRequest: " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}