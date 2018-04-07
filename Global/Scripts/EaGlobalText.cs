using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EaGlobalText : MonoBehaviour {
    private Text _text;
    public Text text { get{ return _text ?? (_text = GetComponent<Text>());}}
    public string textKey;

    private void Start()
    {
        text.text = EaString.instance.GetValue(textKey);
    }

}
