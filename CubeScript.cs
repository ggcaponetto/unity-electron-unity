using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CubeScript : MonoBehaviour
{
    public Texture dynTexture;
    void Start()
    {
        Debug.Log("started");
        dynTexture = LoadPNG("Assets/Images/input.jpeg");
    }

    // Update is called once per frame
    void Update(){
        // Debug.Log("updated");
        dynTexture = LoadPNG("Assets/Images/input.jpeg");
        GetComponent<Renderer>().material.mainTexture = dynTexture;
    }

    public static Texture2D LoadPNG(string filePath) {

    Texture2D tex = null;
    byte[] fileData;
    if (File.Exists(filePath)){
        // Debug.Log("the file exists");
        fileData = File.ReadAllBytes(filePath);
        tex = new Texture2D(2, 2);
        tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
    } else {
        Debug.Log("the file doesnt exist");
    }
    return tex;
    }
}
