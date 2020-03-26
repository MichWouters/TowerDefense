using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class Missile : MonoBehaviour
    {
        [SerializeField] private int _maximumDamage = 26;
        [SerializeField] private int _minimumDamage = 14;
        [SerializeField] private float _movementSpeed = 50f;

        public GameObject Target { get; set; }

        private void Update()
        {
            MoveTowardsTarget(Target.transform);
        }

        /// <summary>
        /// Play explosion animation and remove missile from the game
        /// </summary>
        private void Explode()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Generate a random value between the minimum and maximum damage
        /// </summary>
        /// <param name="damageMin">The minimum amount of damage this missile can inflict.</param>
        /// <param name="damageMax">The maximum amount of damage this missile can inflict.</param>
        /// <returns></returns>
        private int GetDamage(int damageMin, int damageMax)
        {
            int result = Random.Range(damageMin, damageMax);
            return result;
        }

        /// <summary>
        /// Missile has made contact with an enemy. Do damage.
        /// </summary>
        /// <param name="enemy">The enemy that has been hit</param>
        private void HitTarget(Enemy enemy)
        {
            int damage = GetDamage(_minimumDamage, _maximumDamage);
            enemy.TakeDamage(damage);
            Explode();
        }

        /// <summary>
        /// Target has died before missile was able to hit. Remove missile and do no damage.
        /// </summary>
        private void MissTarget()
        {
            Explode();
        }

        /// <summary>
        /// Move the missile towards the target.
        /// </summary>
        /// <param name="target">The lock-on on target.</param>
        private void MoveTowardsTarget(Transform target)
        {
            if (target == null) MissTarget();
            else
            {
                EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                if (enemyHealth == null) return;

                if (enemyHealth.IsAlive())
                {
                    transform.LookAt(target);
                    Vector3 targetPosition = new Vector3(target.position.x, 1, target.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);
                }
                else
                {
                    MissTarget();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != Tags.Enemy) return;

            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                HitTarget(enemy);
            }
        }
    }
}