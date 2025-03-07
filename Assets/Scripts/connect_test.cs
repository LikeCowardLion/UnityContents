﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class connect_test : MonoBehaviour
{

    #region Singleton                                       
    private static connect_test _Instance;    


    //public GameObject cube;
    
    public static connect_test Instance                  
    {
        get                       
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<connect_test>();
            return _Instance;
        }
    }
    #endregion

    ZenClientHandle_t mZenHandle = new ZenClientHandle_t();
    ZenSensorHandle_t mSensorHandle = new ZenSensorHandle_t();

    public Vector3 sensorEulerData; // 데이터 측정값
    public Vector3 offset; // 0, 0, 0으로 만들어주기
    public bool save_offset;

    public enum OpenZenIoTypes { SiUsb, Bluetooth };

    [Tooltip("IO Type which OpenZen should use to connect to the sensor.")]
    public OpenZenIoTypes OpenZenIoType = OpenZenIoTypes.Bluetooth;

    [Tooltip("Idenfier which is used to connect to the sensor. The name depends on the IO type used and the configuration of the sensor.")]
    public string OpenZenIdentifier;

    // Use this for initialization
    void Start()
    {
        OpenZenIdentifier = "00:04:3E:9B:A2:85";
        save_offset = false;
        sensorEulerData = new Vector3(0, 0, 0);
        offset = new Vector3(0, 0, 0);

        // create OpenZen
        OpenZen.ZenInit(mZenHandle);



        print("Trying to connect to OpenZen Sensor on IO " + OpenZenIoType +
            " with sensor name " + OpenZenIdentifier);

        var sensorInitError = OpenZen.ZenObtainSensorByName(mZenHandle,
            OpenZenIoType.ToString(), OpenZenIdentifier, 0, mSensorHandle);

        if (sensorInitError != ZenSensorInitError.ZenSensorInitError_None)
        {
            print("Error while connecting to sensor.");
            print("IMU 센서 연결 대기중");
            System.Threading.Thread.Sleep(500);
            Start();
        }

        else
        {
            ZenComponentHandle_t mComponent = new ZenComponentHandle_t();
            OpenZen.ZenSensorComponentsByNumber(mZenHandle, mSensorHandle, OpenZen.g_zenSensorType_Imu, 0, mComponent);

            OpenZen.ZenSensorComponentSetBoolProperty(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_StreamData, true);

            OpenZen.ZenSensorComponentSetInt32Property(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_SamplingRate, 100);


            OpenZen.ZenSensorComponentSetInt32Property(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_FilterMode, 2);


            OpenZen.ZenSensorComponentSetBoolProperty(mZenHandle, mSensorHandle, mComponent,
               (int)EZenImuProperty.ZenImuProperty_OutputGyr0AlignCalib, true);

            print("Sensor configuration complete");
        }
    }

    void Update()
    {
        //cube.GetComponent<Transform>().position = new Vector3(sensorEulerData.x, sensorEulerData.y, sensorEulerData.z);

        // IMU에서 데이터 받아와서 오일러값으로 변환하는 부분
        // 오프셋은 스페이스, 코드 모듈화는 선택

        while (true)
        {
            ZenEvent zenEvent = new ZenEvent();

            if (!OpenZen.ZenPollNextEvent(mZenHandle, zenEvent))
                break;

            if (zenEvent.component.handle != 0)
            {
                switch (zenEvent.eventType)
                {
                    case ZenEventType.ZenEventType_ImuData:

                        OpenZenFloatArray fq = OpenZenFloatArray.frompointer(zenEvent.data.imuData.r); 
                                                                                               
                        sensorEulerData = new Vector3(fq.getitem(2) * -1f, fq.getitem(0) * -1f, fq.getitem(1));


                        if (!save_offset)
                        {
                            offset = sensorEulerData;
                            save_offset = true;
                        }
                        sensorEulerData -= offset;

                        sensorEulerData.x = (float)dataset(sensorEulerData.x);
                        sensorEulerData.y = (float)dataset(sensorEulerData.y);
                        sensorEulerData.z = (float)dataset(sensorEulerData.z);

                        //print("x : " + sensorEulerData.x.ToString("N0")
                        //  + ", y : " + sensorEulerData.y.ToString("N0")
                        //  + ", z : " + sensorEulerData.z.ToString("N0"));

                        break;
                }
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            offset += sensorEulerData;
        }
    }

    // 0~360범위 내의 값만 나오게 하는 코드
    public double dataset(double data)
    {
        if (data > 180)
            data -= 360;
        else if (data < -180)
            data += 360;

        return data;

    }
    void OnDestroy()
    {
        if (mSensorHandle != null)
        {
            OpenZen.ZenReleaseSensor(mZenHandle, mSensorHandle);
        }

        OpenZen.ZenShutdown(mZenHandle);
    }
}