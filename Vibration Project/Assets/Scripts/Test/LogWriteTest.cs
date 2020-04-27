using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LogWriteTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextSave(string msg)
    {
        string txt = msg + "\n" + msg;
        FileInfo fileInfo = new FileInfo(Application.dataPath + "testLog.txt");
        StreamWriter writer = fileInfo.AppendText();
        writer.WriteLine(txt);
        writer.Flush();
        writer.Close();
    }
}
