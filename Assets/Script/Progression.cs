using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progression : MonoBehaviour
{

    public int max;
    public Text currentText;
    public Image mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float.TryParse(currentText.text, out float current);
        float fillAmount = (float)current / (float)max;
        mask.fillAmount = fillAmount;
    }


}
