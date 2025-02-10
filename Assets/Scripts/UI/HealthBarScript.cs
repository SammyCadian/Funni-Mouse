using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slide;
    private void Start() {
        slide = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    public void Setup(float hp)
    {
        slide.maxValue = hp;
        slide.value = hp;
    }
    public void UpdateBar(float hp)
    {
        slide.value = hp;
        if(hp == 0){}
    }
}
