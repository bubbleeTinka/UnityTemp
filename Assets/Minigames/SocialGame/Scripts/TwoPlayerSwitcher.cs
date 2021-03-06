﻿using UnityEngine;
using System.Collections;
using Kinect;


namespace SocialGame{
	public class TwoPlayerSwitcher : MonoBehaviour {
		#if UNITY_STANDALONE
		KinectManager KManager;
		public bool TwoPlayer;
		public SkinnedMeshRenderer player2;
		public Animator animPlayer2;
		public Check button;

		public Material ghost;
		private Material normal;
		// Use this for initialization
		void Start () {      
            SocialGame.LevelManager.gameSelected = 0;
			normal = player2.material;
			KManager = MGC.Instance.kinectManagerInstance;
		}

		IEnumerator WaitOnStartMGC()
		{
			float timeOfWaiting = Time.time;
			while ((MGC.Instance != null && MGC.Instance.kinectManagerInstance != null)) {

				if ((Time.time - timeOfWaiting) > 10) {
					Debug.LogWarning ("MGC or Kineck is not created");
					break;
				}

				yield return null;
			}
			KManager = MGC.Instance.kinectManagerInstance;
		}
		
		/// <summary>
		/// check second player and show him
		/// </summary>
		void Update () {
			if(KManager && KManager.GetUserIdByIndex(1) != 0)
			{
				if(!TwoPlayer)
				{
					Activate2player();
				}
			}
			else
			{
				if(TwoPlayer)
				{
					Deactivate2player();
				}
			}
		}

		/// <summary>
		/// Activate2player this instance.
		/// </summary>
		 public void Activate2player()
		{
			TwoPlayer = true;
            Debug.Log("Player2 connect");
			if(ghost)
			{
				player2.material = normal;
			}
			else
			{
				player2.enabled = true;
			}
			if(animPlayer2)
				animPlayer2.SetBool("TwoPlayers",true);
			if(LevelManager.gameSelected == 0 && button)
			{
				button.activate();
			}
		}

		/// <summary>
		/// Deactivate2player this instance.
		/// </summary>
		public void Deactivate2player()
		{
            if(KManager)
                KManager.ClearKinectUsers();
			TwoPlayer = false;
			if(ghost)
			{
				player2.material = ghost;
			}
			else
			{
				player2.enabled = false;
			}

		}
		#endif
	}
}