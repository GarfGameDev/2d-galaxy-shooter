using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCalculator : MonoBehaviour
{
    public float quiz1;
    public float quiz2;
    public float quiz3;
    public float averageMark;
    // Start is called before the first frame update
    void Start()
    {
        quiz1 = Random.Range(1f, 20f);
        quiz2 = Random.Range(1f, 20f);
        quiz3 = Random.Range(1f, 20f);
        averageMark = (quiz1 + quiz2 + quiz3) / 3;
        // Timesing by 10f and dividing by 10f determines the number of decimal points, in this case it's 1 decimal point.
        // If both values were 100f it would be 2 decimal points
        Debug.Log("The average grade was " + Mathf.Round(averageMark * 10f) / 10f);
    }


}
