using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForecastScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public static ForecastScript current;

    private void Start()
    {
        current = this;
    }

    public void UpdateForecast()
    {
        if (RoundScript.instance.roundNumber<10)
        text.text = RoundScript.RoundStringList[(short)RoundScript.instance.RoundList[RoundScript.instance.roundNumber]];
    }
}
