using UnityEngine;
using System.Collections;
using System;

public class ClockAnimator : MonoBehaviour {

	public Transform hours, minutes, seconds;
	public bool analog = false;
	
	private const float hoursToDegrees = 360f / 12f;
	private const float minutesToDegrees = 360f / 60f;
	private const float secondsToDegrees = 360f / 60f;
	
	
	// Update is called once per frame
	void Update () {
		if (analog) {
			TimeSpan span = DateTime.Now.TimeOfDay;
			hours.localRotation = Quaternion.Euler (0f, 0f, (float) span.TotalHours * -hoursToDegrees);
			minutes.localRotation = Quaternion.Euler (0f, 0f, (float) span.TotalMinutes * -minutesToDegrees);
			seconds.localRotation = Quaternion.Euler (0f, 0f, (float) span.TotalSeconds * -secondsToDegrees);
		} else {
			DateTime now = DateTime.Now;
			hours.localRotation = Quaternion.Euler(0f, 0f, now.Hour * -hoursToDegrees);
			minutes.localRotation = Quaternion.Euler (0f, 0f, now.Minute * -minutesToDegrees);
			seconds.localRotation = Quaternion.Euler (0f, 0f, now.Second * -secondsToDegrees);	
		}
	}
}
