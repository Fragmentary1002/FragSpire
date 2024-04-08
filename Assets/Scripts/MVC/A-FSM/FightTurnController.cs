using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Frag
{

    //ս��״̬
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
    /// ս�������� ״̬ģʽ ����ս��״̬���л� ������FightType�л�
    /// </summary>
    public class FightTurnController :MonoSingleton<FightTurnController> ,IController
    {

        public FightUnit fightUnit;//ս����Ԫ

        //command
        // public FighterCommand fighterCommand = new FighterCommand();

        //viewModel
      //  public FightModel model=new FightModel();

        /// <summary>
        /// ����ս��״̬Ĭ��Ϊinit
        /// </summary>
        /// <param name="fightType"></param>
        public void Init(FightType fightType = FightType.None)
        {
           
            ChangeType(fightType);
            return;
        }

        /// <summary>
        /// ״̬ģʽ�л���ս�غ�
        /// </summary>
        /// <param name="type"></param>
        public void ChangeType(FightType type)
        {

            Debug.Log($"��ʼ{type}�غ�");
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
                    Debug.Log("FightType û���ҵ�״̬");
                    break;
            }
            fightUnit.Init();
        }

        private void Update()
        {
            fightUnit?.OnUpdate();
           
        }
        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }

}