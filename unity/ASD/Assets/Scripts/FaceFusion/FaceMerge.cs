using FaceFusion;
using Newtonsoft.Json.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace FaceFusion
{
    public class FaceMerge : MonoBehaviour
    {
        public static FaceMerge Instance;
        private void Awake()
        {
            Instance = this;
        }

        //�����ں�

        public void PostFaceMerge(string json, UnityAction<string> sucessResponse, UnityAction<string> errorRes = null)
        {
            StartCoroutine(IPostFaceMerge(json, sucessResponse, errorRes));
        }

        private IEnumerator IPostFaceMerge(string json, UnityAction<string> sucessResponse, UnityAction<string> errorRes = null)
        {
            string token = AccessToken.GetAssessToken();
            string url = "https://aip.baidubce.com/rest/2.0/face/v1/merge?access_token=" + token;
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                Encoding encoding = Encoding.Default;
                byte[] buffer = encoding.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(buffer);
                request.downloadHandler = new DownloadHandlerBuffer();
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                {
                    sucessResponse?.Invoke(request.downloadHandler.text);
                    request.Abort();
                }
                else
                {
                    errorRes?.Invoke(request.downloadHandler.text);
                    request.Abort();
                }
            }
        }

        public Texture2D Base64ToTexture2D(int width,int height,string base64Str)
        {
            Texture2D pic = new Texture2D(width, height, TextureFormat.RGBA32, false);
            byte[] data = System.Convert.FromBase64String(base64Str);
            pic.LoadImage(data);
            return pic;
        }

        public string Texture2DToBase64(Texture2D tex2d)
        {
            byte[] bytes = tex2d.EncodeToJPG();           
            string strBase64 = Convert.ToBase64String(bytes);
            return strBase64;
        }
    }
}

