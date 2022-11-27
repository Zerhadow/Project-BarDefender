using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float lerpSpd = 2;
    bool lerpHP = false;
    float time = 0;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health; 
    }

    public void SetHealth(float health, float orginalHP){
        time = 0;
        Debug.Log("Health is " + health);
        slider.value = health;
        Debug.Log("Slider Value is " + slider.value);
        
        if(!lerpHP) {
            StartCoroutine(LerpHP(health, orginalHP));
        }
    }

    IEnumerator LerpHP(float health, float originalHP) {
        lerpHP = true;

        while(time < 1) {
            time += (lerpSpd * Time.deltaTime);
            slider.value = Mathf.Lerp(originalHP, health, time);
            yield return null;
        }

        lerpHP = false;
    }
}
