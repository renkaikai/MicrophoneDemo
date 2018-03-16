using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.UI;
using WebSocketSharp;
  
public class MicrophoneManager : MonoBehaviour  
{  
    public static float volume;  
    private AudioClip micRecord;  
    private string device;  
    public Slider slider;
	//private WebSocket socket; 
	private float speed=0;
    void Start()  
    {           
        device = Microphone.devices[0];//获取设备麦克风  
        micRecord = Microphone.Start(device, true, 999, 44100);//44100音频采样率   固定格式  
		
		//socket =new WebSocket("ws://192.168.1.1:7681");
		//socket.Connect();
    }  	

    void Update()  
    {  
        volume = GetMaxVolume();   
		slider.value=volume; 
		
		//speed=volume*1000;		   

		//socket.Send("motor "+speed.ToString()+" "+speed.ToString()+"\n");          
    }  

    //每一帧实时处理那一帧接收的音频文件的大小  
    private float GetMaxVolume()  
    {  
        float maxVolume = 0f;  
        //剪切音频  
        float[] volumeData = new float[128];  
        int offset = Microphone.GetPosition(device) - 128 + 1;  
        if (offset < 0)  
        {  
            return 0;  
        }  
        micRecord.GetData(volumeData, offset);  
  
        for (int i = 0; i < 128; i++)  
        {  
            float tempMax = volumeData[i];//修改音量的敏感值  
            if (maxVolume < tempMax)  
            {  
                maxVolume = tempMax;  
            }  
        }  
        return maxVolume;  
    }  
}  
