using UnityEngine;
using System.Collections.Generic;

public class InGameConsole : MonoBehaviour
{
    private Queue<string> myLogQueue = new Queue<string>();
    private string myLog = "";

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Chỉ lưu lại Log thường, Warning và Error lỗi đỏ
        myLogQueue.Enqueue("[" + type + "] " + logString);
        if (type == LogType.Exception || type == LogType.Error)
        {
            myLogQueue.Enqueue(stackTrace);
        }
        while (myLogQueue.Count > 15)
        { // Giới hạn hiển thị 15 dòng cho đỡ chật màn hình
            myLogQueue.Dequeue();
        }
        myLog = string.Join("\n", myLogQueue.ToArray());
    }

    // Vẽ một ô chữ nhỏ đè lên góc màn hình để đọc log trực tiếp trên điện thoại
    void OnGUI()
    {
        // Thiết kế khung hiển thị ở góc trên bên trái màn hình
        GUI.Box(new Rect(10, 10, Screen.width * 0.4f, Screen.height * 0.4f), "Console Log");
        GUI.Label(new Rect(20, 30, Screen.width * 0.38f, Screen.height * 0.36f), myLog);
    }
}