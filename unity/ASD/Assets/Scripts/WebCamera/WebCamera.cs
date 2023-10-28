using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
/// <summary>
/// �򵥵�����ͷ�����࣬�����ڳ���������
/// </summary>
public class WebCamera : MonoBehaviour
{    
    public static WebCamera Instance;
    /// <summary>
    /// ��ǰ����ͷ�±꣬���ڶ������ͷ�豸ʱ�����л�����
    /// </summary>
    private int curCamIndex = 0;
    /// <summary>
    /// ��������ͷ�豸�б�
    /// </summary>
    private WebCamDevice[] devices;
    /// <summary>
    /// ����ͷ��Ⱦ����
    /// </summary>
    private WebCamTexture webCamTex;
    /// <summary>
    /// ��ǰ�豸������
    /// </summary>
    public string deviceName { get; private set; }
    /// <summary>
    /// ����ͷ�Ƿ��
    /// </summary>
    public bool CameraIsOpen { get; private set; }
    /// <summary>
    /// ������Ⱦ����
    /// </summary>
    public Texture renderTex { get; private set; }

    /// <summary>
    /// ���µĽ�ͼ
    /// </summary>
    public Texture2D lastShotText { get; private set; }
    /// <summary>
    /// ����Ŀ��
    /// </summary>
    private int width,height;
    /// <summary>
    /// ֡��
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
    /// ������ͷ
    /// </summary>
    public void OpenCamera()
    {
        //�û���Ȩ
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //��ʾ������豸����Ҫ�򿪵�����ͷ
            devices = WebCamTexture.devices;
            if (devices.Length <= 0)
            {
                Debug.LogError("û�м�⵽����ͷ������豸�Ƿ�����"); return;
            }
            deviceName = devices[curCamIndex].name;
            webCamTex = new WebCamTexture(deviceName, width,height,fps);

            renderTex = webCamTex;
            //��������ͷ
            webCamTex.Play();
            CameraIsOpen = true;
        }
    }

    /// <summary>
    /// �ر�����ͷ
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
    /// �л�����ͷ
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
        Debug.Log($"ͼƬ�ѱ��棺{path}/{fileName}");
        File.WriteAllBytes($"{ path}/{fileName}", textureBytes);
        if (File.Exists($"{path}/{fileName}"))
        {
            Debug.Log("�ҵ���Ƭ");
        }
        else
        {
            Debug.Log("δ�ҵ�");
        }
    }


    /// <summary>
    /// Textureת����Texture2D
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
