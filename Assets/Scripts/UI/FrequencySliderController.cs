using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencySliderController : MonoBehaviour {

	public AudioClip bassSound;
	public AudioSource source;

	private Dictionary<float, float> notes = new Dictionary<float, float>();

    public Slider frequencySlider;
	//private float pitch;
    private float sliderSpeed = 1.0f;

    public void Awake() {
        frequencySlider = gameObject.GetComponent<Slider>();
    }

	public void Update () {
		if (Input.GetButton("Fire1"))
		{
			//lower note
			frequencySlider.value -= sliderSpeed * Time.deltaTime;
			source.pitch = frequencySlider.value;
		}
		else if (Input.GetButton("Fire2"))
		{
			//raise note
			frequencySlider.value += sliderSpeed * Time.deltaTime;
			source.pitch = frequencySlider.value;
		}
    }
}
