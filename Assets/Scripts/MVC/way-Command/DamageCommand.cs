using QFramework;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;

namespace Frag
{
    public class DamageCommand : AbstractCommand
    {
        private DamageInfo damageInfo;
        public DamageCommand(DamageInfo damageInfo)
        {
            this.damageInfo = damageInfo;
        }

        protected override void OnExecute()
        {
            SubmitDamage();
        }



        public void SubmitDamage()
        {
            if (damageInfo == null) return;

            Fighter creator = damageInfo.creator;
            Fighter target = damageInfo.target;


            target.DoBeDamage(damageInfo.GetDamage());


            CallBackFight(creator, CallBackPoint.OnHit);

            CallBackFight(target, CallBackPoint.OnBeHurt);


            if (target.IsCanBeKill())
            {
                CallBackFight(creator, CallBackPoint.OnKill);
                CallBackFight(target, CallBackPoint.OnBeKill);
            }

            if (target.IsCanBeKill())
            {
                CallBackFight(creator, CallBackPoint.OnKill);
                CallBackFight(target, CallBackPoint.OnBeKill);
            }

        }


        private void CallBackFight(Fighter fighter, CallBackPoint callBackPoint)
        {
            if (fighter != null) { return; }

            LinkedList<BuffInfo> infoList = fighter.buffHandler.buffList;

            if (infoList.Count > 0)
            {
                foreach (var info in infoList)
                {
                    EventCenter.GetInstance().EventTrigger<BuffInfo>($"{callBackPoint}", info);
                }
            }

        }
    }



}