using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vJoyInterfaceWrap;

public class tec : MonoBehaviour
{
    static public vJoy joystick;
    static public vJoy.JoystickState iReport;
    static public uint id = 1;
    uint count;

    // Start is called before the first frame update
    void Start()
    {
        joystick = new vJoy();
        iReport = new vJoy.JoystickState();

 

        Debug.Log("log output");
        if (id <= 0 || id > 16)
        {
            Debug.Log("Illegal device ID Exit!");
            return;
        }

        // Get the driver attributes (Vendor ID, Product ID, Version Number)
        if (!joystick.vJoyEnabled())
        {
            Debug.Log("vJoy driver not enabled: Failed Getting vJoy attributes.");
            return;
        }
        else
            Debug.Log("OK Vendor:"+joystick.GetvJoyManufacturerString()+
                "Product :"+joystick.GetvJoyProductString()+
                "Version Number:"+ joystick.GetvJoySerialNumberString());

        VjdStat status = joystick.GetVJDStatus(id);
        switch (status)
        {
            case VjdStat.VJD_STAT_OWN:
                //Debug.Log("vJoy Device {0} is already owned by this feeder\n", id);
                Debug.Log("own");
                break;
            case VjdStat.VJD_STAT_FREE:
                //Debug.Log("vJoy Device {0} is free\n", id);
                Debug.Log("free");
                break;
            case VjdStat.VJD_STAT_BUSY:
                //Debug.Log("vJoy Device {0} is already owned by another feeder\nCannot continue\n", id);
                Debug.Log("busy");
                return;
            case VjdStat.VJD_STAT_MISS:
                //Debug.Log("vJoy Device {0} is not installed or disabled\nCannot continue\n", id);
                Debug.Log("miss");
                return;
            default:
                //Debug.Log("vJoy Device {0} general error\nCannot continue\n", id);
                Debug.Log("def");
                return;
        };


    }

    // Update is called once per frame
    void Update()
    {

        VjdStat status = joystick.GetVJDStatus(id);

        //Debug.Log(":"+count);
        bool AxisX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_X);
        bool AxisY = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Y);
        bool AxisZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_Z);
        bool AxisRX = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RX);
        bool AxisRZ = joystick.GetVJDAxisExist(id, HID_USAGES.HID_USAGE_RZ);
        // Get the number of buttons and POV Hat switchessupported by this vJoy device
        int nButtons = joystick.GetVJDButtonNumber(id);
        int ContPovNumber = joystick.GetVJDContPovNumber(id);
        int DiscPovNumber = joystick.GetVJDDiscPovNumber(id);

        //Debug.Log(":" + nButtons+""+ ContPovNumber);
        // Test if DLL matches the driver
        /*
        uint DllVer = 0, DrvVer = 0;
        bool match = joystick.DriverMatch(ref DllVer, ref DrvVer);
        if (match)
            Debug.Log("Version of Driver Matches DLL Version ({)"+ DllVer);
        else
            Debug.Log("Version of Driver () does NOT match DLL Version )"+ DrvVer+","+ DllVer);
        */

        // Acquire the target
        /*
        if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!joystick.AcquireVJD(id))))
        {
            Debug.Log("Failed to acquire vJoy device number "+ id);
            return;
        }
        else
            Debug.Log("Acquired: vJoy device number"+id);
        */

        int X, Y, Z, ZR, XR;;
        long maxval = 0;

        X = 20;
        Y = 30;
        Z = 40;
        XR = 60;
        ZR = 80;
        bool res;

        for (uint i = 0; i < nButtons; i++)
        {
            res = joystick.SetBtn(true, id, 1 + i);
            if (!res)
            {
               // Debug.Log((i+1) + " B  error");
            }
        }

        X += 150; if (X > maxval) X = 0;
        Y += 250; if (Y > maxval) Y = 0;
        Z += 350; if (Z > maxval) Z = 0;
        XR += 220; if (XR > maxval) XR = 0;
        ZR += 200; if (ZR > maxval) ZR = 0;
        count++;

        if (count > 1000)
            count = 0;

        //
    }


}
