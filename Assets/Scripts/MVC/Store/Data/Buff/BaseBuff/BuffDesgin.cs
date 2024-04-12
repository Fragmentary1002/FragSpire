using Frag;
using UnityEngine;
/// <summary>
/// buff更新时间枚举
/// </summary>
public enum BuffUpdateTimeEnum
{
    Add,
    Replace,
    Keep
}

/// <summary>
/// buff 层数消除方式枚举
/// </summary>
public enum BuffRemoveStackUpdateEnum
{
    Clear,
    Reduce
}

public enum CallBackPoint {
    //基础回调点
    OnCreate,
    OnRemoved,
    OnTick,
    //伤害回调点
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
