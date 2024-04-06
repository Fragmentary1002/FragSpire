using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ֪ʶ��
/// 1.AB����ص�API
/// 2.����ģʽ
/// 3.ί�С���>Lambda���ʽ
/// 4.Э��
/// 5.�ֵ�
/// </summary>
public class ABMgr : SingletonAutoMono<ABMgr>
{
    //AB�������� Ŀ����
    //���ⲿ������Ľ�����Դ����

    //����
    private AssetBundle mainAB = null;
    //��������ȡ�õ������ļ�
    private AssetBundleManifest manifest = null;

    //AB�����ܹ��ظ����� �ظ����ػᱨ��
    //�ֵ� ���ֵ����洢 ���ع���AB�� 
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// ���AB�����·�� �����޸�
    /// </summary>
    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// ������ �����޸�
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }

    /// <summary>
    /// ����AB��
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName)
    {
        //����AB��
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            //���������еĹ̶���Ϣ
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //���ǻ�ȡ�����������Ϣ
        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; i++)
        {
            //�жϰ��Ƿ���ع�
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //������Դ��Դ��
        //���û�м��ع� �ټ���
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    //ͬ������ ��ָ������
    public Object LoadRes(string abName, string resName)
    {
        //����AB��
        LoadAB(abName);
        //Ϊ�����淽�� �ڼ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject
        //����� ֱ��ʵ������ �ٷ��ظ��ⲿ
        Object obj = abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //ͬ������ ����typeָ������
    public Object LoadRes(string abName, string resName, System.Type type)
    {
        //����AB��
        LoadAB(abName);
        //Ϊ�����淽�� �ڼ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject
        //����� ֱ��ʵ������ �ٷ��ظ��ⲿ
        Object obj = abDic[abName].LoadAsset(resName, type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //ͬ������ ���ݷ���ָ������
    public T LoadRes<T>(string abName, string resName) where T : Object
    {
        //����AB��
        LoadAB(abName);
        //Ϊ�����淽�� �ڼ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject
        //����� ֱ��ʵ������ �ٷ��ظ��ⲿ
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //�첽���صķ���
    //������첽���� AB����û��ʹ���첽����
    //֪ʶ��AB���� ������Դʱ ʹ���첽
    //���������첽������Դ
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        //����AB��
        LoadAB(abName);
        //Ϊ�����淽�� �ڼ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject
        //����� ֱ��ʵ������ �ٷ��ظ��ⲿ
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //�첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //����Type�첽������Դ
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, type, callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        //����AB��
        LoadAB(abName);
        //Ϊ�����淽�� �ڼ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject
        //����� ֱ��ʵ������ �ٷ��ظ��ⲿ
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        yield return abr;
        //�첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }


    //���ݷ��� �첽������Դ
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        //����AB��
        LoadAB(abName);
        //Ϊ�����淽�� �ڼ�����Դʱ �ж�һ�� ��Դ�ǲ���GameObject
        //����� ֱ��ʵ������ �ٷ��ظ��ⲿ
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //�첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset) as T);
        else
            callBack(abr.asset as T);
    }

    //������ж��
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    //���а���ж��
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }
}