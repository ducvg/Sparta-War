using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSprite : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;

    public List<Sprite> sprites;

    private Slider slider;

    void Awake()
    {
        if (!slider) slider = GetComponent<Slider>(); ;

    }

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateSprite);
        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = sprites.Count;
    }

    private void UpdateSprite(float arg0)
    {
        Mathf.Clamp(arg0, 0, sprites.Count - 1);
        backgroundImage.sprite = sprites[(int)arg0];
    }
}
