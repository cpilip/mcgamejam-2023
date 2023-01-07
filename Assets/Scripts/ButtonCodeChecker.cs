using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCodeChecker : MonoBehaviour
{
    [SerializeField] GameObject Button1;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Button3;
    [SerializeField] GameObject Button4;

    [SerializeField] GameObject objectToChange;

    private bool isSolved;

    // 214
    private List<bool[]> correctOrderList = new List<bool[]>
        {
        new bool[] { false, true, false, false },
        new bool[] { true, true, false, false },
        new bool[] { true, true, false, true }
    };

    private int index = 0;

    
    void applyButtonPower() {
        if (!isSolved) {
            if (checkSolution()) {
                // do something
            }
        }
    }

    public bool checkSolution() {
        bool isPressed1 = Button1.GetComponent<ButtonScript>().isPressed();
        bool isPressed2 = Button2.GetComponent<ButtonScript>().isPressed();
        bool isPressed3 = Button3.GetComponent<ButtonScript>().isPressed();
        bool isPressed4 = Button4.GetComponent<ButtonScript>().isPressed();
        bool[] combination = { isPressed1, isPressed2, isPressed3, isPressed4};

        Debug.Log("Is Pressed: " + isPressed1 + isPressed2 + isPressed3 + isPressed4);

        if (string.Join("", combination) == string.Join("", correctOrderList[index])) {
            Debug.Log("Combination is correct");

            index++;

            if (index == 3) {
                isSolved = true;
                objectToChange.SendMessage("TurnOffLaser");

                return true;
            }
        } else {
            Debug.Log("Combination is not correct");

            index = 0;

            Button1.GetComponent<ButtonScript>().setPressed(false);
            Button2.GetComponent<ButtonScript>().setPressed(false);
            Button3.GetComponent<ButtonScript>().setPressed(false);
            Button4.GetComponent<ButtonScript>().setPressed(false);
        }
        return false;
    }
}
