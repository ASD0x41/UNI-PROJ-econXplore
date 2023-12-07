using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI sliderText = null;
    // [SerializeField] private float maxValue = 100.0f;
    // Start is called before the first frame update

    public void SliderChange(float value)
    {
        float localValue = value;
        sliderText.text = localValue.ToString("0");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
