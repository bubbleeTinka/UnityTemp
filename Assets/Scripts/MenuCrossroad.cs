﻿using UnityEngine;
using System.Collections;

public class MenuCrossroad : MonoBehaviour {
	public string kinectMenuScene;
	public string androidMenuScene;

	// Use this for initialization
	void Start ()
	{
#if UNITY_ANDROID
		Application.LoadLevel(androidMenuScene);
#elif UNITY_STANDALONE
		Application.LoadLevel(kinectMenuScene);
#endif
	}
}
