using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    class HealthBar : MonoBehaviour
    {
        public Slider Slider;

        public void SetHealth(float health)
        {
            Slider.value = health;
        }

        public void SetMaxHealth(float health)
        {
            Slider.maxValue = health;
            Slider.value = health;
        }
    }
