using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
   public Slider slider;
   void Start()
   {
    slider=GetComponent<Slider>();
   }

   public void SetMaxScroe(int score)
   {
    slider.minValue=score;
    slider.value=score;
   }
    public void SetScore(int score)
    {
        slider.value=score;
    }
}