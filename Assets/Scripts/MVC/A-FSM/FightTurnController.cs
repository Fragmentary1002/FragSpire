using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Frag
{

    //战斗状态
    public enum FightType
    {
        None,
        RoleInit,
        BattleInit,
        Player,
        Enemy,
        Win,
        Loss
    }

    /// <summary>
    /// 战斗管理类 状态模式 对于战斗状态的切换 利用了FightType切换
    /// </summary>
    public class FightTurnController :MonoSingleton<FightTurnController> ,IController
    {

        public FightUnit fightUnit;//战斗单元

        //command
        // public FighterCommand fighterCommand = new FighterCommand();

        //viewModel
      //  public FightModel model=new FightModel();

        /// <summary>
        /// 传入战斗状态默认为init
        /// </summary>
        /// <param name="fightType"></param>
        public void Init(FightType fightType = FightType.None)
        {
           
            ChangeType(fightType);
            return;
        }

        /// <summary>
        /// 状态模式切换对战回合
        /// </summary>
        /// <param name="type"></param>
        public void ChangeType(FightType type)
        {

            Debug.Log($"开始{type}回合");
            switch (type)
            {
                case FightType.None:
                    break;
                case FightType.RoleInit:
                    fightUnit = new Fight_RoleInit();
                    break;
                case FightType.BattleInit:
                    fightUnit = new Fight_BattleInit();
                    break;
                case FightType.Player:
                    fightUnit = new Fight_PlayerTurn();
                    break;
                case FightType.Enemy:
                    fightUnit = new Fight_EnemyTurn();
                    break;
                case FightType.Win:
                    fightUnit = new Fight_Win();
                    break;
                case FightType.Loss:
                    fightUnit = new Fight_Loss();
                    break;
                default:
                    Debug.Log("FightType 没有找到状态");
                    break;
            }
            fightUnit.Init();
        }

        private void Update()
        {
            fightUnit?.OnUpdate();
           
        }
        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }

}