using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
/// <summary>
/// 简单的摄像头单例类，挂载在场景物体上
/// </summary>
public class WebCamera : MonoBehaviour
{    
    public static WebCamera Instance;
    /// <summary>
    /// 当前摄像头下标，存在多个摄像头设备时用于切换功能
    /// </summary>
    private int curCamIndex = 0;
    /// <summary>
    /// 所有摄像头设备列表
    /// </summary>
    private WebCamDevice[] devices;
    /// <summary>
    /// 摄像头渲染纹理
    /// </summary>
    private WebCamTexture webCamTex;
    /// <summary>
    /// 当前设备的名称
    /// </summary>
    public string deviceName { get; private set; }
    /// <summary>
    /// 摄像头是否打开
    /// </summary>
    public bool CameraIsOpen { get; private set; }
    /// <summary>
    /// 最终渲染画面
    /// </summary>
    public Texture renderTex { get; private set; }

    /// <summary>
    /// 最新的截图
    /// </summary>
    public Texture2D lastShotText { get; private set; }
    /// <summary>
    /// 画面的宽高
    /// </summary>
    private int width,height;
    /// <summary>
    /// 帧率
    /// </summary>
    private int fps;

    void Awake()
    {
        Instance = this;
    }

    public void InitCamera(int width,int height,int fps=30)
    {        
        this.width = width;
        this.height = height;
        this.fps = fps;
    }

    /// <summary>
    /// 打开摄像头
    /// </summary>
    public void OpenCamera()
    {
        //用户授权
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //显示画面的设备就是要打开的摄像头
            devices = WebCamTexture.devices;
            if (devices.Length <= 0)
            {
                Debug.LogError("没有检测到摄像头，检查设备是否正常"); return;
            }
            deviceName = devices[curCamIndex].name;
            webCamTex = new WebCamTexture(deviceName, width,height,fps);

            renderTex = webCamTex;
            //开启摄像头
            webCamTex.Play();
            CameraIsOpen = true;
        }
    }

    /// <summary>
    /// 关闭摄像头
    /// </summary>
    public void CloseCamera()
    {
        if (CameraIsOpen && webCamTex != null)
        {
            webCamTex.Stop();
            CameraIsOpen=false;
        }
    }
    /// <summary>
    /// 切换摄像头
    /// </summary>
    public void SwapCamera()
    {
        if (devices.Length > 0)
        {
            curCamIndex = (curCamIndex + 1) % devices.Length;
            if (webCamTex!= null)
            {
                CloseCamera();
                OpenCamera();
            }
        }
    }

    public void SaveScreenShot(string path)
    {
        Texture2D shotTex = TextureToTexture2D(webCamTex);
        lastShotText = shotTex;
        byte[] textureBytes = shotTex.EncodeToJPG();
        string fileName = string.Format("IMG_{0}{1}{2}_{3}{4}{5}.jpg",DateTime.Now.Year,DateTime.Now.Month,
            DateTime.Now.Day,DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        Debug.Log($"图片已保存：{path}/{fileName}");
        File.WriteAllBytes($"{ path}/{fileName}", textureBytes);
        if (File.Exists($"{path}/{fileName}"))
        {
            Debug.Log("找到照片");
        }
        else
        {
            Debug.Log("未找到");
        }
    }


    /// <summary>
    /// Texture转换成Texture2D
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);

        return texture2D;
    }
    
}
