using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XFramework;

namespace Frag
{
    /// <summary>
    /// ��Ϸ������ ��������������Ĺ���
    /// </summary>
    public class GameApp : MonoSingleton<GameApp>, IController
    {
        // Start is called before the first frame update

        public Character character;
        public BaseMonster monster;

        //protected override void Awake()
        //{
        //    base.Awake();
        //    OnInit();
        //}

        private void Start()
        {
            OnInit();

        }
        private void Update()
        {
        }

        public void OnInit()
        {
            Debug.Log("GameManager start");

            //������
            AudioManager.Instance.Init();

            SceneManager.Instance.Init(new FightScene());

            //�߼��� �����߼�
            FightCardManager.Instance.Init(character);

            FightFSM.Instance.Init(FightType.BattleInit);


            Debug.Log("GameManager end");
        }


        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}