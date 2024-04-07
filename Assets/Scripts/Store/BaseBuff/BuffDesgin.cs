using Frag;
using UnityEngine;
/// <summary>
/// buff����ʱ��ö��
/// </summary>
public enum BuffUpdateTimeEnum
{
    Add,
    Replace,
    Keep
}

/// <summary>
/// buff ����������ʽö��
/// </summary>
public enum BuffRemoveStackUpdateEnum
{
    Clear,
    Reduce
}

public enum CallBackPoint {
    //�����ص���
    OnCreate,
    OnRemoved,
    OnTick,
    //�˺��ص���
    OnHit,
    OnBeHurt,
    OnKill,
    OnBeKill
}


public class BuffInfo
{

    public BuffModel buffData;

    public Fighter creator;

    public Fighter Target;

    public float durationTimer;

    public float tickTimer;

    public int curStack = 1;

}
