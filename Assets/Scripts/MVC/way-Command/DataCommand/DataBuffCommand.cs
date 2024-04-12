using Frag;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBuffCommand : AbstractCommand
{
    Fighter target;
    BuffInfo buffInfo;
    public DataBuffCommand(Fighter target,BuffInfo buffInfo)
    {
        this.target = target;
        this.buffInfo = buffInfo;
    }
    protected override void OnExecute()
    {
        target.buffHandler.AddBuff(buffInfo);
    }

}
