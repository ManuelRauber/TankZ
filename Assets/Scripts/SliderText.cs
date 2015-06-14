using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {
	public string DefaultText;
	public Text TextObject;

	public void Awake () {
		TextObject.text = DefaultText;
	}

	public void SliderValueChanged(float value) {
		var textValue = value.ToString ();

		TextObject.text = textValue;
	}
}
