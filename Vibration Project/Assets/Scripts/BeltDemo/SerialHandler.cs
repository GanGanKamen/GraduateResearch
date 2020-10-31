using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

namespace Interface
{
    public class SerialHandler : MonoBehaviour
    {
        public string portName = "";
        public int baudRate = 115200;


        public SerialPort serialPort;
        private Thread thread;
        private bool isRunning = false;
        [SerializeField]private string message;
        private bool isMessageReceived  = false;

        private void Start()
        {
            Close();
            Open();
        }

        // Update is called once per frame
        void Update()
        {
            if (isMessageReceived)
            {
                OnDataReceived(message);
            }
            isMessageReceived = false;
        }

        public void Open()
        {
            serialPort = new SerialPort(portName, baudRate);
            var time0 = Time.deltaTime;
            serialPort.Open();
            thread = new Thread(Read);
            thread.Start();
        }

        public void Close()
        {
            isMessageReceived = false;
            isRunning = false;

            if (thread != null && thread.IsAlive)
            {
                thread.Join();
            }

            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort.Dispose();
            }
        }

        public void Read()
        {
            while (isRunning && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    message = serialPort.ReadLine();
                    isMessageReceived = true;
                    Debug.Log(message);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
        }

        public void Write(string msg)
        {
            try
            {
                serialPort.Write(msg);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        void OnDestroy()
        {
            Debug.LogWarning("OnDestroy");
            Close();
        }

        void OnDataReceived(string message)
        {
            Debug.LogWarning("OnDataReceived1");
            var data = message.Split(
                    new string[] { "\t" }, System.StringSplitOptions.None);
            if (data.Length < 2) return;

            try
            {
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
}

