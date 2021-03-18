using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    private int gradeA;
    private int gradeB;
    private int gradeC;
    private int gradeD;
    private int gradeE;

    [SerializeField]
    private GameObject cube;

    void Start()
    {
        gradeA = Random.Range(0, 100);
        gradeB = Random.Range(0, 100);
        gradeC = Random.Range(0, 100);
        gradeD = Random.Range(0, 100);
        gradeE = Random.Range(0, 100);

        float gradeAverage = (gradeA + gradeB + gradeC + gradeD + gradeE) / 5;
        Debug.Log("The gradeAverage is " + gradeAverage);

        if (gradeAverage > 90)
        {
            Debug.Log("The average is higher than 90");
        }
        else if (gradeAverage >= 80 && gradeAverage < 90)
        {
            Debug.Log("The average is between 80 and 90");
        }
        else if (gradeAverage < 80 && gradeAverage > 70)
        {
            Debug.Log("The average is between 70 and 80");
        }
        else if (gradeAverage < 70)
        {
            Debug.Log("The average is below 70");
        }

        var changeColor = cube.GetComponent<Renderer>();
        changeColor.material.SetColor("_Color", Color.red);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var setColor = cube.GetComponent<Renderer>();
            setColor.material.SetColor("_Color", Color.green);
        }
    }
    


    

    

}
