﻿#pragma warning disable 0414
using UnityEngine;
using System.Collections;

namespace Coloring
{
	public class LevelManagerColoring : MonoBehaviour
	{
		public Texture brush;
		public BackGUI backGUI;
		public SetColor brushColor;

		internal bool painting = false;
		internal bool hiddenGUIwhilePainting = false;
		internal float timestamp;

		private int x = (Screen.width / 10)/3;
		private int y = (Screen.height / 10)/2;
		private int w = Screen.width / 16;
		private int h = Screen.height / 9;

#if UNITY_ANDROID
		internal Material neuronMaterial;
		internal Color neuronOriginalColor;
#endif


		void Start()
		{
			timestamp = -2;

#if UNITY_ANDROID
			neuronMaterial = GameObject.Find("Neuron_body").GetComponent<Renderer>().material;
			neuronOriginalColor = neuronMaterial.color;
#endif
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.I))
			{
				hiddenGUIwhilePainting = !hiddenGUIwhilePainting;

				if(painting && hiddenGUIwhilePainting)
					MGC.Instance.ShowCustomCursor(true);
				if(painting && !hiddenGUIwhilePainting)
					MGC.Instance.ShowCustomCursor(false);
			}

			if(MGC.Instance.minigamesGUIObject.activeSelf && MGC.Instance.minigamesGUI.clicked)
			{
#if UNITY_ANDROID
				neuronMaterial.color = neuronOriginalColor;
#endif
				MGC.Instance.minigamesGUI.clicked = false;
			}
		}


		public void ShowColoringGUI(bool isVisible)
		{
            //TODO vypnuto pro Android pro verzi 2.0 z nedostatku casu na odladeni
            #if UNITY_STANDALONE
		    if (isVisible)
		    {
		        MGC.Instance.minigamesGUI.screenshotIcon.show();
		    }
		    else
		    {
                MGC.Instance.minigamesGUI.screenshotIcon.hide();
		    }
            #endif
        }


#if UNITY_STANDALONE
		void OnGUI()
		{
            // drawing a brush cursor
            if (painting && !hiddenGUIwhilePainting)
            {
                GUI.DrawTexture(new Rect(Input.mousePosition.x - x * 2, Screen.height - Input.mousePosition.y - y * 2, w * 2, h * 2), brush);
            }
		}
#endif
	}
}