using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Evo
{
    public class HP : MonoBehaviour
    {
        [System.Serializable] public class OnDieEvent : UnityEvent { }
        [System.Serializable] public class OnDamageEvent : UnityEvent<float> { }

        [Header("Config")]
        [SerializeField] float maxHP = 100;
        [SerializeField] float lowHpThreshold = 25;

        [Header("Variabes")]
        [SerializeField] float hp = 100;
        [SerializeField] float regenBySecond = 0f;

        [Header("Events")]
        public OnDamageEvent onDamageEvent;
        public OnDieEvent onDieEvent;

        bool isDead = false;
        bool isLowHp = false;

        public int Hp { get => (int)hp; set => hp = value; }
        public float HpNorm { get => (hp / maxHP); }
        public int MaxHp { get => (int)maxHP; set => maxHP = value; }
        public float Regen { get => regenBySecond; set => regenBySecond = value; }
        public bool IsDead { get => isDead; }

        #region unity
        private void Update()
        {
            if (!isDead)
            {
                hp = Mathf.Clamp(hp + regenBySecond * Time.deltaTime, 0, maxHP);
            }
        }
        #endregion

        #region core
        public void Die()
        {
            Debug.Log("Die");
            isDead = true;
            onDieEvent.Invoke();
        }
        #endregion

        #region public
        public void Reset()
        {
            hp = maxHP;
            isDead = false;
            isLowHp = false;
        }

        public void Heal(float amount)
        {
            if (isDead) return;
            hp -= amount;
            hp = Mathf.Clamp(hp, 0, maxHP);

            if (isLowHp && hp > lowHpThreshold)
            {
                isLowHp = false;
            }
        }
        public void Damage(float amount)
        {
            if (isDead) return;
            hp -= amount;
            hp = Mathf.Clamp(hp, 0, maxHP);
            onDamageEvent.Invoke(amount);

            if (hp == 0)
            {
                Die();
            }

            if (!isLowHp && hp < lowHpThreshold)
            {
                isLowHp = true;
            }
        }
        public void SetMaxHp(float _maxHp)
        {
            bool isFull = hp == maxHP;
            this.maxHP = _maxHp;
            if(isFull)
            {
                hp = maxHP;
            }
        }
        #endregion
    }
}

