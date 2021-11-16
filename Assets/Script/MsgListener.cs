using System.Collections;
using System.Collections.Generic;
using UnityEngine;

delegate int bitwise(int x, int y);

public class MsgListener : MonoBehaviour
{
    int oper;
    SerialController serialController;

    bitwise addBitwise;
    bitwise subBitwise;

    int add_oper(int x, int y) { return x | y; }
    int sub_oper(int x, int y) { return x & (127 - y); }
    int result(int x, int y, bitwise calc) { return calc(x, y); }

    void Start()
    {
        oper = 0;
        serialController = GetComponent<SerialController>();

        addBitwise = add_oper;
        subBitwise = sub_oper;
    }

    void Update()
    {
        
    }

    public void send_message(int code)
    {
        // code:    -6, -5, ... , -1, 1, 2, ... , 6
        // > 0:     add moving(bit)
        // < 0:     subtract moving(bit)
        // 1st bit: strong vibration at the same time
        // 2nd bit: weak vibration at the same time
        // 3rd bit: vibrate randomly
        // 4th bit: turn on the fans
        // 5th bit: activate cooler module
        // 6th bit: activate heater module

        if (code > 0) {

            oper = result(oper, (int)Mathf.Pow(2.0f, code), addBitwise);
        }
        else if (code < 0) {

            code *= -1;
            oper = result(oper, (int)Mathf.Pow(2.0f, code), subBitwise);
        }

        serialController.SendSerialMessage(oper.ToString());
    }

    void OnMessageArrived(string msg)
    {
        Debug.Log("Arrived: " + msg);
    }

    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "device connected" : "connection failed");
    }
}
