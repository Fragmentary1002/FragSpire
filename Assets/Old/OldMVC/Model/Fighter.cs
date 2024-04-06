using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    // ս�����࣬�̳��� MonoBehaviour
    public class Fighter : MonoBehaviour
    {
        // ��ǰ����ֵ
        public int currentHealth;
        // �������ֵ
        public int maxHealth;
        // ��ǰ�赲ֵ��Ĭ��Ϊ0
        public int currentBlock = 0;
        // ս��������������
        public FighterHealthBar fighterHealthBar;

        // ����Ч��
        [Header("Buffs")]
        public Buff vulnerable;  // ����
        public Buff weak;        // ����
        public Buff strength;    // ǿ��
        public Buff ritual;      // ��ʽ
        public Buff enrage;      // ��ŭ
        public GameObject buffPrefab;  // ����Ч��Ԥ����
        public Transform buffParent;    // ����Ч���ĸ�����
        public bool isPlayer;           // �Ƿ�Ϊ���
        Enemy enemy;                    // ���˶���
        BattleSceneManager battleSceneManager;  // ս������������
        GameManager gameManager;                // ��Ϸ������
        public GameObject damageIndicator;       // �˺�ָʾ��

        // �ڶ��󱻻���ʱ����
        private void Awake()
        {
            // ��ȡ���˺�ս������������������
            enemy = GetComponent<Enemy>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            gameManager = FindObjectOfType<GameManager>();

            // ���ó�ʼ����ֵ
            currentHealth = maxHealth;
            // ���������������ֵ�͵�ǰֵ
            fighterHealthBar.healthSlider.maxValue = maxHealth;
            fighterHealthBar.DisplayHealth(currentHealth);
            // �������ң�������Ϸ����������ʾ��ǰ����ֵ���������ֵ
            if (isPlayer)
                gameManager.DisplayHealth(currentHealth, currentHealth);
        }

        // �ܵ��˺��ķ���
        public void TakeDamage(int amount)
        {
            // ������赲ֵ�������˺�
            if (currentBlock > 0)
                amount = BlockDamage(amount);

            // ����ǵ����Ҵ����������ֵʱ��ִ�е��˵Ķ���
            if (enemy != null && enemy.wiggler && currentHealth == maxHealth)
                enemy.CurlUP();

            // ��ӡ��ɵ��˺�ֵ
            Debug.Log($"��� {amount} ���˺�");

            // ʵ�����˺�ָʾ��������һ��ʱ�������
            DamageIndicator di = Instantiate(damageIndicator, this.transform).GetComponent<DamageIndicator>();
            di.DisplayDamage(amount);
            Destroy(di, 2f);

            // ���ٵ�ǰ����ֵ������������ֵUI
            currentHealth -= amount;
            UpdateHealthUI(currentHealth);

            // �������ֵС�ڵ���0������ս��
            if (currentHealth <= 0)
            {
                if (enemy != null)
                    battleSceneManager.EndFight(true);
                else
                    battleSceneManager.EndFight(false);

                Destroy(gameObject);
            }
        }

        // ��������ֵUI�ķ���
        public void UpdateHealthUI(int newAmount)
        {
            currentHealth = newAmount;
            // ��������ֵUI
            fighterHealthBar.DisplayHealth(newAmount);

            // �������ң��������Ϸ�������е�����ֵ��ʾ
            if (isPlayer)
                gameManager.DisplayHealth(newAmount, maxHealth);
        }

        // �����赲ֵ�ķ���
        public void AddBlock(int amount)
        {
            // �����赲ֵ�����������������赲ֵ��ʾ
            currentBlock += amount;
            fighterHealthBar.DisplayBlock(currentBlock);
        }

        // ��������
        private void Die()
        {
            // ���õ�ǰ��Ϸ����
            this.gameObject.SetActive(false);
        }

        // �����˺��ķ���
        private int BlockDamage(int amount)
        {
            if (currentBlock >= amount)
            {
                // ȫ���赲
                currentBlock -= amount;
                amount = 0;
            }
            else
            {
                // �޷�ȫ���赲
                amount -= currentBlock;
                currentBlock = 0;
            }

            // �����赲ֵUI
            fighterHealthBar.DisplayBlock(currentBlock);
            return amount;
        }

        // �������Ч���ķ���
        public void AddBuff(Buff.Type type, int amount)
        {
            if (type == Buff.Type.vulnerable)
            {
                if (vulnerable.buffValue <= 0)
                {
                    // �����µ�����Ч������
                    vulnerable.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // ���Ӵ���Ч����ֵ������������Ч��UI
                vulnerable.buffValue += amount;
                vulnerable.buffGO.DisplayBuff(vulnerable);
            }
            else if (type == Buff.Type.weak)
            {
                if (weak.buffValue <= 0)
                {
                    // �����µ�����Ч������
                    weak.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // ��������Ч����ֵ������������Ч��UI
                weak.buffValue += amount;
                weak.buffGO.DisplayBuff(weak);
            }
            else if (type == Buff.Type.strength)
            {
                if (strength.buffValue <= 0)
                {
                    // �����µ�����Ч������
                    strength.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // ����ǿ��Ч����ֵ������������Ч��UI
                strength.buffValue += amount;
                strength.buffGO.DisplayBuff(strength);
            }
            else if (type == Buff.Type.ritual)
            {
                if (ritual.buffValue <= 0)
                {
                    // �����µ�����Ч������
                    ritual.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // ������ʽЧ����ֵ������������Ч��UI
                ritual.buffValue += amount;
                ritual.buffGO.DisplayBuff(ritual);
            }
            else if (type == Buff.Type.enrage)
            {
                Debug.Log("��Ӽ�ŭЧ��");
                if (enrage.buffValue <= 0)
                {
                    // �����µ�����Ч������
                    enrage.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // ���Ӽ�ŭЧ����ֵ������������Ч��UI
                enrage.buffValue += amount;
                enrage.buffGO.DisplayBuff(enrage);
            }
        }

        // �غϽ���ʱ��������Ч���ķ���
        public void EvaluateBuffsAtTurnEnd()
        {
            // �������Ч������0
            if (vulnerable.buffValue > 0)
            {
                // ���ٴ���Ч����ֵ������������Ч��UI
                vulnerable.buffValue -= 1;
                vulnerable.buffGO.DisplayBuff(vulnerable);

                // �������Ч��ֵС�ڵ���0����������Ч������
                if (vulnerable.buffValue <= 0)
                    Destroy(vulnerable.buffGO.gameObject);
            }
            // �������Ч������0
            else if (weak.buffValue > 0)
            {
                // ��������Ч����ֵ������������Ч��UI
                weak.buffValue -= 1;
                weak.buffGO.DisplayBuff(weak);

                // �������Ч��ֵС�ڵ���0����������Ч������
                if (weak.buffValue <= 0)
                    Destroy(weak.buffGO.gameObject);
            }
            // �����ʽЧ������0
            else if (ritual.buffValue > 0)
            {
                // ���ǿ��Ч����ֵΪ��ʽЧ����ֵ
                AddBuff(Buff.Type.strength, ritual.buffValue);
            }
        }

        // ��������Ч���ķ���
        public void ResetBuffs()
        {
            // �������Ч������0
            if (vulnerable.buffValue > 0)
            {
                // ���ô���Ч����ֵ������������Ч������
                vulnerable.buffValue = 0;
                Destroy(vulnerable.buffGO.gameObject);
            }
            // �������Ч������0
            else if (weak.buffValue > 0)
            {
                // ��������Ч����ֵ������������Ч������
                weak.buffValue = 0;
                Destroy(weak.buffGO.gameObject);
            }
            // ���ǿ��Ч������0
            else if (strength.buffValue > 0)
            {
                // ����ǿ��Ч����ֵ������������Ч������
                strength.buffValue = 0;
                Destroy(strength.buffGO.gameObject);
            }

            // �����赲ֵΪ0�����������������赲ֵ��ʾ
            currentBlock = 0;
            fighterHealthBar.DisplayBlock(0);
        }
    }
}
