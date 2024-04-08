using Frag;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCommand : AbstractCommand
{

    DamageInfo damageInfo = null;

    public HurtCommand(DamageInfo damageInfo)  
    {
        this.damageInfo = damageInfo;
    }
    
    protected override void OnExecute()
    {
        this.SendCommand<DamageCommand>(new DamageCommand(damageInfo));
    }
}
