using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    public class Enemy : MonoBehaviour
    {
        // ���˵��ж��б�  
        public List<EnemyAction> enemyActions;
        // ��ǰ�غϵ��˵��ж��б�  
        public List<EnemyAction> turns = new List<EnemyAction>();
        // ��ǰ�غ���  
        public int turnNumber;
        // �Ƿ���Ҫ������ҵ��˵��ж�  
        public bool shuffleActions;
        // ���˵�Fighter���  
        public Fighter thisEnemy;

        // UI���  
        [Header("UI")]
        public Image intentIcon;
        public TMP_Text intentAmount;
        public BuffUI intentUI;

        // ���˵���������  
        [Header("Specifics")]
        public int goldDrop; // ���˵���������  
        public bool bird; // �Ƿ����������  
        public bool nob; // �Ƿ���nob�����  
        public bool wiggler; // �Ƿ���wiggler�����  
        public GameObject wigglerBuff; // wiggler����˵�BuffЧ������  
        public GameObject nobBuff; // nob����˵�BuffЧ������  
        // ս������������  
        BattleSceneManager battleSceneManager;
        // ���Fighter���  
        Fighter player;
        // ���˵�Animator���  
        Animator animator;
        // �Ƿ��ڻغ��м�״̬  
        public bool midTurn;

        // ��Ϸ��ʼʱ����  
        private void Start()
        {
            // ���Ҳ���ȡBattleSceneManager����  
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            // ��ȡ��ҵ�Fighter���  
            player = battleSceneManager.player;
            // ��ȡ��ǰ���˵�Fighter���  
            thisEnemy = GetComponent<Fighter>();
            // ��ȡ��ǰ���˵�Animator���  
            animator = GetComponent<Animator>();

            // �����Ҫ�����ж��������ɴ��Һ�Ļغ��б�  
            if (shuffleActions)
                GenerateTurns();
        }

        // ���ص��˵ķ���  
        private void LoadEnemy()
        {
            // ��Start�������ƣ����»�ȡBattleSceneManager�������ҵ�Fighter���  
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            player = battleSceneManager.player;
            thisEnemy = GetComponent<Fighter>();

            // �����Ҫ�����ж��������ɴ��Һ�Ļغ��б�  
            if (shuffleActions)
                GenerateTurns();
        }

        // ִ�е��˻غϵĺ���  
        public void TakeTurn()
        {
            // ���ŵ�����ͼ�����Ķ���  
            intentUI.animator.Play("IntentFade");

            // ���ݵ��˵�ǰ�غϵ���ͼ���ͽ��в�ͬ�Ĳ���  
            switch (turns[turnNumber].intentType)
            {
                case EnemyAction.IntentType.Attack:
                    // ������ҵ�Э��  
                    StartCoroutine(AttackPlayer());
                    break;
                case EnemyAction.IntentType.Block:
                    // ִ���赲����  
                    PerformBlock();
                    // Ӧ������Ч����Э��  
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.StrategicBuff:
                    // ���Լ�Ӧ������Ч��  
                    ApplyBuffToSelf(turns[turnNumber].buffType);
                    // Ӧ������Ч����Э��  
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.StrategicDebuff:
                    // �����Ӧ�ü���Ч��  
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    // Ӧ������Ч����Э�̣��˴��������߼�����Ӧ����һ��ApplyDebuff��Э�̣�  
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.AttackDebuff:
                    // �����Ӧ�ü���Ч��  
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    // ������ҵ�Э��  
                    StartCoroutine(AttackPlayer());
                    break;
                default:
                    // Ĭ����������������Ϣ  
                    Debug.Log("����ô�����������");
                    break;
            }
        }

        // ���ɵ��˻غϵĺ���  
        public void GenerateTurns()
        {
            // �������˵������ж�  
            foreach (EnemyAction eA in enemyActions)
            {
                // �����ж��ĳ��ּ��������Ӧ�������ж����غ��б���  
                for (int i = 0; i < eA.chance; i++)
                {
                    turns.Add(eA);
                }
            }
            // ���һغ��б��˳��  
            turns.Shuffle();
        }

        // ������ҵ�Э�̺���  
        private IEnumerator AttackPlayer()
        {
            // ���Ź�������  
            animator.Play("Attack");
            if (bird)
                // ���birdΪ�棬�򲥷���ͼ��Ĺ�������  
                battleSceneManager.birdIcon.GetComponent<Animator>().Play("Attack");

            // �������˺�ֵ�����������˺�ֵ����������Ч��  
            int totalDamage = turns[turnNumber].amount + thisEnemy.strength.buffValue;
            if (player.vulnerable.buffValue > 0)
            {
                // �����Ҵ�������״̬���������˺�ֵ  
                float a = totalDamage * 1.5f;
                // Debug.Log("�˺�ֵ�� "+totalDamage+" ���ӵ� "+(int)a); // ע�͵��ĵ�����Ϣ  
                totalDamage = (int)a;
            }
            // �ȴ�0.5��  
            yield return new WaitForSeconds(0.5f);
            // ���������˺�  
            player.TakeDamage(totalDamage);
            // �ٵȴ�0.5��  
            yield return new WaitForSeconds(0.5f);
            // ������ǰ�غ�  
            WrapUpTurn();
        }
        // ����һ��Э�̷�����������һ��ʱ���Ӧ������Ч��  
        private IEnumerator ApplyBuff()
        {
            // �ȴ�1��  
            yield return new WaitForSeconds(1f);
            // �����غϴ���  
            WrapUpTurn();
        }

        // �����غϵĴ�����  
        private void WrapUpTurn()
        {
            // �غ�������  
            turnNumber++;
            // ����غ��������ܻغ�����������Ϊ0  
            if (turnNumber == turns.Count)
                turnNumber = 0;

            // ���birdΪ�棬��غ�������Ϊ1  
            if (bird)
                turnNumber = 1;

            // ���nobΪ���һغ���Ϊ0����غ�������Ϊ1  
            if (nob && turnNumber == 0)
                turnNumber = 1;

            // �����غϽ���ʱ������Ч��  
            thisEnemy.EvaluateBuffsAtTurnEnd();
            // ����midTurnΪfalse����ʾ��ǰ���ǻغ���  
            midTurn = false;
        }

        // ������Ӧ������Ч��  
        private void ApplyBuffToSelf(Buff.Type t)
        {
            // ���������ָ�����ͺ�����������Ч��  
            thisEnemy.AddBuff(t, turns[turnNumber].amount);
        }

        // �����ʩ�Ӽ���Ч��  
        private void ApplyDebuffToPlayer(Buff.Type t)
        {
            // ������Ϊ�գ�����ص��ˣ�������Ϊ�˳�ʼ����Ҷ���  
            if (player == null)
                LoadEnemy();

            // ��������ָ�����ͺ������ļ���Ч��  
            player.AddBuff(t, turns[turnNumber].debuffAmount);
        }

        // ִ���赲����  
        private void PerformBlock()
        {
            // ����������ָ���������赲ֵ  
            thisEnemy.AddBlock(turns[turnNumber].amount);
        }

        // ��ʾ���˵���ͼ  
        public void DisplayIntent()
        {
            // ����غ��б�Ϊ�գ�����ص���  
            if (turns.Count == 0)
                LoadEnemy();

            // ������ͼͼ��  
            intentIcon.sprite = turns[turnNumber].icon;

            // �жϵ�ǰ�غ���ͼ�Ƿ��ǹ���  
            if (turns[turnNumber].intentType == EnemyAction.IntentType.Attack)
            {
                // ����ֵ���ӵ��˵���������Ч��  
                int totalDamage = turns[turnNumber].amount + thisEnemy.strength.buffValue;
                // ������������������Ч�����򹥻�ֵ����50%  
                if (player.vulnerable.buffValue > 0)
                {
                    totalDamage = (int)(totalDamage * 1.5f);
                }
                // ��ʾ���˺�ֵ  
                intentAmount.text = totalDamage.ToString();
            }
            else
            {
                // ������ʾ��ͼ����ֵ  
                intentAmount.text = turns[turnNumber].amount.ToString();
            }

            // ������ͼ���ֵĶ���  
            intentUI.animator.Play("IntentSpawn");
        }

        // ���˾�������  
        public void CurlUP()
        {
            // ����wigglerBuff��������ĳ����Ϸ�����UIԪ�أ�  
            wigglerBuff.SetActive(false);
            // ����������5����赲ֵ  
            thisEnemy.AddBlock(5);
        }

        // ��Ľ�������  
    }
    // �����ռ�Ľ�������  
}