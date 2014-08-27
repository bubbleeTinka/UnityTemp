﻿using UnityEngine;
using System.Collections;


namespace FindIt
{
    public class ClickImageScript : MonoBehaviour
    {
        void OnMouseDown()
        {
            //restartIdleTimer();
            //double actualTime = gameStopwatch.ElapsedMilliseconds;

            if (this.gameObject.GetComponent<SpriteRenderer>().sprite.Equals(GameObject.Find("SearchedImage").GetComponent<SpriteRenderer>().sprite))
            {
                if (this.gameObject.tag == "Left")
                {
                    FindItStatistics.RecordLeftGoodClick();
                }
                else if (this.gameObject.tag == "Right")
                {
                    FindItStatistics.RecordLeftGoodClick();
                }
                // else unexpected error
                Camera.main.GetComponent<GameScript>().newTargetImage();
            }
            else
            {
                if (this.gameObject.tag == "Left")
                {
                    FindItStatistics.RecordLeftWrongClick();
                }
                else if (this.gameObject.tag == "Right")
                {
                    FindItStatistics.RecordLeftWrongClick();
                }
            }
        }
    }
}
