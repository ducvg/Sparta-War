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

        slider.onValueChanged.AddListener(UpdateSprite);
        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = sprites.Count-1;
    }

    void Start()
    {
        slider.onValueChanged.AddListener(UpdateSprite);
        slider.wholeNumbers = true;
        slider.minValue = 0;
        slider.maxValue = sprites.Count-1;
    }

    private void UpdateSprite(float arg0)
    {
        int index = Mathf.Clamp((int)arg0, 0, sprites.Count - 1);
        backgroundImage.sprite = sprites[index];
    }
}
