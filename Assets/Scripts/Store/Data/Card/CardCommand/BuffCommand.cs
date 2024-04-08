using Frag;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCommand : AbstractCommand
{
    Fighter target;
    BuffInfo buffInfo;
    public BuffCommand(Fighter target,BuffInfo buffInfo)
    {
        this.target = target;
        this.buffInfo = buffInfo;
    }
    protected override void OnExecute()
    {
        target.buffHandler.AddBuff(buffInfo);
    }

}
