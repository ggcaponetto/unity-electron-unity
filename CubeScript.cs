using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.Networking;

public class CubeScript : MonoBehaviour
{
    void Start()
    {
        Debug.Log("started");
        Texture dynTexture = LoadPNG("Assets/Images/input.jpeg");
        GetComponent<Renderer>().material.mainTexture = dynTexture;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("updated");
        try
        {
            Texture dynTexture = LoadPNG("Assets/Images/input.jpeg");
            GetComponent<Renderer>().material.mainTexture = dynTexture;

            // Destroy(dynTexture);
            Resources.UnloadUnusedAssets();
        }
        catch (IOException e)
        {
            Debug.Log("the file is busy");
        }
    }

    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;
        if (File.Exists(filePath))
        {
            // Debug.Log("the file exists");
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        else
        {
            Debug.Log("the file doesnt exist");
        }
        return tex;
    }
}
