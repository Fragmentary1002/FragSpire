using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Frag
{

    public enum CreatGoType
    {
        player,
        enemy
    }

    /// <summary>
    /// ��ʼ���
    /// </summary>
    public class FightPanel : BasePanel
    {
        //Ѱ�ҿؼ����߼��������ݸ���
        /// <summary>
        /// ·��
        /// </summary>
        static readonly string path = "Prefabs/UI/Panel/FightPanel";

        //view
        FighterHealthBarCell healthBarPlayer;
        FighterHealthBarCell healthBarEnemy;

        Transform playerParent;
        Transform enemyParent;


        Transform buffParentPlayer;
        Transform buffParentEnemy;

        private TMP_Text energyText;// ����ֵ�ı���ʾ���
        private TMP_Text drawPileText;
        private TMP_Text discardPileText;
        private TMP_Text cemeteryText; //�ȴ�����
        private TMP_Text BannerText;

        private Image playerIcon;

        private Button endTurn;
        public Animator banner; // UI���������������չʾʤ����ʧ����Ϣ��  

        //model

        /// <summary>
        /// ��ʼ���
        /// </summary>
        public FightPanel() : base(new UIType(path))
        {

        }

        protected override void InitEvent()
        {
            InitE();

            InitUIName();

            InitClick();

        }
        private void InitE()
        {
            EventCenter.GetInstance().AddEventListener<FightModel>("FightData", UpdateInfo);

            EventCenter.GetInstance().AddEventListener<FightModel>("FightData", InitPrefabs);

            EventCenter.GetInstance().AddEventListener("Unit", BannerOut);

        }

        private void InitUIName()
        {
            energyText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("Energy/EnergyText");
            drawPileText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("DrawPile/DrawPileText");
            discardPileText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("DiscardPile/DiscardPileText");
            BannerText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("Banner/BannerText");

            banner = ActivePanel.GetOrAddComponentInChildren<Animator>("Banner");

            endTurn = ActivePanel.GetOrAddComponentInChildren<Button>("EndTurn");
        }
        private void InitClick()
        {
            //����Player�غϵĽ����¼�

            endTurn.GetComponent<Button>().onClick.AddListener(EndTurnClick);

        }

        private void InitPrefabs(FightModel fightModel)
        {
            playerParent = ActivePanel.GetOrAddComponentInChildren<Transform>("PlayerParent");
            enemyParent = ActivePanel.GetOrAddComponentInChildren<Transform>("EnemyParent");
            try
            {
                ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Fighter/BasePlayer", (go) =>
                {
                    if (playerParent != null)
                    {
                        go.transform.SetParent(playerParent);
                        healthBarPlayer = go.transform.GetOrAddComponentInChildren<FighterHealthBarCell>("FighterHealthBarCell");
                        buffParentPlayer = go.transform.GetOrAddComponentInChildren<Transform>("FighterHealthBarCell/BuffParent");
                    }
                }
                );
                ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Fighter/BaseEnemy", (go) =>
                {
                    if (enemyParent != null)
                    {
                        go.transform.SetParent(enemyParent);
                        healthBarEnemy = go.transform.GetOrAddComponentInChildren<FighterHealthBarCell>("FighterHealthBarCell");

                        buffParentEnemy = go.transform.GetOrAddComponentInChildren<Transform>("FighterHealthBarCell/BuffParent");
                    }
                }
                );
            }
            catch
            {
                return;
            }

            EventCenter.GetInstance().RemoveEventListener<FightModel>("FightData", InitPrefabs);
        }


        private void EndTurnClick()
        {
            Debug.Log("EndTurnClick");
            if (FightTurnController.Instance.fightUnit == null) return;

            FightTurnController.Instance.fightUnit.OnDestroy();

            BannerOut();
        }

        private void BannerOut()
        {
            switch (FightTurnController.Instance.fightUnit)
            {
                case Fight_PlayerTurn:
                    BannerText.text = "Player's Turn";
                    //endTurn.enabled = false;
                    break;
                case Fight_EnemyTurn:
                    BannerText.text = "Enemy's Turn";
                    //endTurn.enabled = true;
                    break;
                default:
                    break;
            }

            banner.Play("bannerOut");
        }



        public void UpdateInfo(FightModel fightModel)
        {
            if (fightModel == null)
            {
                Debug.Log("fightModel������");
                return;
            }

            try
            {
                //����Ѫ����ʾ

                healthBarPlayer?.DisplayHealth(fightModel.healthCount, fightModel.maxHealthCount);

                healthBarEnemy?.DisplayHealth(fightModel.healthCountEnemy, fightModel.maxHealthCountEnemy);

                //���·�����ʾ
                healthBarPlayer?.DisplayBlock(fightModel.blockNumberCount);

                healthBarEnemy?.DisplayHealth(fightModel.healthCountEnemy, fightModel.maxHealthCountEnemy);


                ////��������
                energyText.text = fightModel.enegryCount.ToString();

                //���³��ƶ�
                drawPileText.text = fightModel.drawPileCount.ToString();

                //�������ƶ�

                discardPileText.text = fightModel.discardPileCount.ToString();

                ////����Ĺ��
                //cemeteryText.text = fightModel.cemeteryCount.ToString();
            }
            catch
            {
                Debug.LogWarning("Model���´���");
            }

        }


        public override void OnDestroyOrSetActive(bool isDestroy = false)
        {
            base.OnDestroyOrSetActive();
            EventCenter.GetInstance().RemoveEventListener<FightModel>("FightData", UpdateInfo);

        }

    }
}