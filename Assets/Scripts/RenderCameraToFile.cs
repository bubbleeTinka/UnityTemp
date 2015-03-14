﻿using UnityEngine;
using System.Collections;
using System.IO;

public class RenderCameraToFile : MonoBehaviour 
{
#if UNITY_STANDALONE
    public Camera cam;
    public int width = 3840;
    public int height = 2160;
    public string defaultFileName = "savedCameraFrame.png";

    public void RenderToFile(string fileName)
	{
#if UNITY_EDITOR
        if (!UnityEditorInternal.InternalEditorUtility.HasPro())
        {
            Debug.LogWarning("You are now working with FREE version of Unity, but this feature works only with PRO version - feature is therefore disabled, no file will be saved");
            return;
        }
#endif
        // create new render texture and set it        
        RenderTexture tempRT = new RenderTexture(width, height, 24);
        cam.targetTexture = tempRT;

        //render a frame
        cam.Render();

        //read out pixels from renderTexture to Texture2D
        RenderTexture.active = cam.targetTexture;
        Texture2D virtualPhoto = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false); // false, meaning no need for mipmaps
        virtualPhoto.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);

        // can help avoid errors
        RenderTexture.active = null; 
        cam.targetTexture = null;
        
        //save Texture2D to file
        byte[] bytes;
        bytes = virtualPhoto.EncodeToPNG();
        print("Saving picture to file: " + Application.dataPath + "/" + fileName);
        System.IO.File.WriteAllBytes(Application.dataPath + "/" + fileName, bytes);

        Destroy(tempRT);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            
            RenderToFile(defaultFileName);
        }        
	}
#else
	public void RenderToFile(string fileName)
	{
		Debug.LogWarning ("Fucntion not supported on platforms different than standalone.");
	}
#endif
}