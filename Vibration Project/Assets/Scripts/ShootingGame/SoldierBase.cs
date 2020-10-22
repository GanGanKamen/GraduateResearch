using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public enum Character
    {
        Player,
        Enemy
    }

    public class SoldierBase : MonoBehaviour
    {
        public int HP { get { return hp; } }
        public int MaxHp { get { return maxHp; } }
        public GameObject Body { get { return body; } }
        public GameObject Weapon { get { return weapon; } }
        public bool IsAim { get { return isAim; } }
        public bool IsShoot { get { return isShoot; } }
        public Character Character { get { return character; } }
        [SerializeField] private int maxHp;
        [SerializeField] private float runSpeed;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject weapon;
        [SerializeField] private Transform hitPoint;
        [SerializeField] private Transform muzzle;
        [SerializeField] private Animator soldierAnimator;
        [SerializeField] private bool isAim = false;
        [SerializeField] private IKManager iK;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float shootCoolDownTime;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private GameObject deadPrefab;
        [SerializeField] private HitManager hitManager;
        private int hp = 100;
        private Character character;
        private bool isShoot = false;
        private float shootCoolDownCount = 0;

        public void Init(Character _character)
        {
            hp = maxHp;
            character = _character;
        }

        public void CharacterMove(Vector3 _direction)
        {
            if (isAim) return;
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            body.transform.localRotation = Quaternion.LookRotation(direction);
            var move = Mathf.Abs(_direction.x) + Mathf.Abs(_direction.z);
            if (move > 1) move = 1;
            transform.Translate(direction * Time.deltaTime * runSpeed * move);
            soldierAnimator.SetFloat("Move", move);
        }

        public void CharacterStand()
        {
            soldierAnimator.SetFloat("Move", 0);
            soldierAnimator.SetFloat("Move_X", 0);
            soldierAnimator.SetFloat("Move_Y", 0);
        }

        public void SetTransformRotation(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.localRotation = Quaternion.LookRotation(direction);
            body.transform.localEulerAngles = Vector3.zero;
        }

        public void SetBodyRotation(Vector3 _direction)
        {
            var direction = new Vector3(_direction.x, 0, _direction.z).normalized;
            transform.localEulerAngles = Vector3.zero;
            body.transform.localRotation = Quaternion.LookRotation(direction);
        }

        public void ResetRotation()
        {
            var rot = transform.localEulerAngles;
            transform.localEulerAngles = Vector3.zero;
            body.transform.localEulerAngles = rot;
        }

        public void SetAimRotation()
        {
            var rot = body.transform.localEulerAngles;
            body.transform.localEulerAngles = Vector3.zero;
            transform.localEulerAngles = rot;
        }

        public void SetAiming()
        {
            isAim = true;
            //SetAimRotation();
            iK.SetIK();
            soldierAnimator.SetBool("IsAim", true);
        }

        public void SetAimHight(Transform target)
        {
            iK.SetAimHight(target);
        }

        public void CancelAiming()
        {
            isAim = false;
            ResetRotation();
            iK.ResetIK();
            soldierAnimator.SetBool("IsAim", false);
        }

        public void AimMove(Vector2 inputVec)
        {
            if (isAim == false) return;
            var moveVec = transform.forward * inputVec.y + transform.right * inputVec.x;
            transform.position += moveVec * Time.deltaTime * runSpeed * 0.4f;
            soldierAnimator.SetFloat("Move_X", inputVec.x);
            soldierAnimator.SetFloat("Move_Y", inputVec.y);
        }

        public void PlayerAimRotate(float horizontal)
        {
            if (isAim == false) return;
            transform.localEulerAngles += new Vector3(0, horizontal * Time.deltaTime * 360f, 0);
        }

        public void Shoot()
        {
            if (isAim == false) isAim = true;
                isShoot = true;
            if (shootCoolDownCount >= shootCoolDownTime)
            {
                shootCoolDownCount = 0;
                GameObject bulletObj = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
                bulletObj.GetComponent<BulletMain>().Init(hitPoint.position, bulletSpeed, this);
                //bulletObj.GetComponent<Bullet>().Init(hitPoint.position, bulletSpeed, this);
                if (fireEffect.activeSelf == false) fireEffect.SetActive(true);
            }
            else
            {
                shootCoolDownCount += Time.deltaTime;
            }
        }

        public void ShootOver()
        {
            if (isShoot == false) return;
            shootCoolDownCount = 0;
            fireEffect.SetActive(false);
            isShoot = false;
        }

        public void GetDamaged(int damage,Vector3 muzzle)
        {
            if (hp <= 0) return;
            hp -= damage;
            if(hp > 50) hitManager.GetDamage(muzzle, false);
            else hitManager.GetDamage(muzzle, true);

            if (hp <= 0)
            {
                hp = 0;
                Dead();
            }
        }

        public void Dead()
        {
            switch (character)
            {
                case Character.Enemy:
                    var stageManager = 
                        GameObject.Find("StageManager").GetComponent<StageManager>();
                    var ai = GetComponent<AIManager>();
                    stageManager.EnemyDead(ai.InstancePoint);
                    var deadObj = Instantiate(deadPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                    break;
                case Character.Player:
                    UnityEngine.SceneManagement.Scene loadScene 
                        = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                    UnityEngine.SceneManagement.SceneManager.LoadScene(loadScene.name);
                    break;
            }
        }
    }
}

