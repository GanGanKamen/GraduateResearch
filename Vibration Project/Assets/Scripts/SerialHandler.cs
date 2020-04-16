using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

namespace Interface
{
    public class SerialHandler : MonoBehaviour
    {
        public delegate void SerialDataReceivedEventHandler(string message);
        public event SerialDataReceivedEventHandler OnDataReceived;
        public string portName = "";
        public int baudRate = 115200;


        private SerialPort serialPort;
        private Thread thread;
        private bool isRunning = false;
        private string message;
        private bool isMessageReceived  = false;

        private void Awake()
        {
            Open();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Open()
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            isRunning = true;
            thread = new Thread(Read);
            thread.Start();
        }

        private void Close()
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

        private void Read()
        {
            while (isRunning && serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    message = serialPort.ReadLine();
                    isMessageReceived = true;
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
    }
}

