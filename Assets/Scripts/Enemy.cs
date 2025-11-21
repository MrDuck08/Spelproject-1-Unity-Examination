using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 #region Configurable parameters

 [Header("Movement")]
 [SerializeField] float movementSpeed = 5f;
 [SerializeField] float linearDrag = 2f;

 [Header("Health & Shield")]
 [SerializeField] int health = 1;
 [SerializeField] int shield = 3;
 [SerializeField] float shieldGenerationTimer = 3f;
 [SerializeField] GameObject shieldSprite;
 [SerializeField] Collider2D shieldCollider;
 [SerializeField] LayerMask damageLayer;

 [Header("Death")]
 [SerializeField] GameObject deathEffect;
 [SerializeField] AudioClip deathSound;

 [Header("Wall, Ground, Enemy and player-check")]
 [SerializeField] Vector2 wallCheckPosition;
 [SerializeField] Vector2 groundCheckPosition;
 [SerializeField] Vector2 behindCheckPosition;
 [SerializeField] Vector2 detectionAreaSize;
 [SerializeField] Vector2 detectionAreaOffset;
 [SerializeField] float checkRadius = 1f;
 [SerializeField] LayerMask flipLayer;
 [SerializeField] LayerMask player;
 [SerializeField] GameObject questionMark;
 [SerializeField] GameObject exclamationMark;

 [Header("Shooting")]
 [SerializeField] Vector2 projectileSpawnOffset;
 [SerializeField] float projectileSpawnRadius;
 [SerializeField] float shootingCooldown;
 [SerializeField] float fullyDetectTimer = 1f;
 [SerializeField] GameObject projectile;
 [SerializeField] AudioClip[] shootingSound;

 #endregion
 #region Private variables

 int horizontalDirection = 1;
 int selectedShieldValue;
 int shootCounter;
 bool canShoot = true;
 bool isDead = false;
 bool isGenerating = false;
 Coroutine currentShieldGenerationRoutine;
 Coroutine currentShootRoutine;

 #endregion
 #region Chached references

 Rigidbody2D enemyRigidbody;


 PlayerScript playerScript;

 #endregion
 #region Functions

 private void Start()
 {

     enemyRigidbody = GetComponent<Rigidbody2D>();
     
     playerScript = FindObjectOfType<PlayerScript>();
 }

 private void FixedUpdate()
 {
     if (isDead) { return; }

     Move();
     Flip();
     DetectPlayer();
     
     bool isRunning = Mathf.Abs(enemyRigidbody.linearVelocity.x) > 0.2f; //Mathf.Abs return the absolut value of f.
 }

 void Move()
 {
     float velocityX = horizontalDirection * movementSpeed;
     enemyRigidbody.linearVelocity = new Vector2(velocityX, enemyRigidbody.linearVelocity.y);
 }

 void Flip()
 {
     Vector2 relativeWallAndEnemyCheckPosition = (Vector2)transform.position + new Vector2(horizontalDirection * wallCheckPosition.x, wallCheckPosition.y);
     bool wallChecked = Physics2D.OverlapCircle(relativeWallAndEnemyCheckPosition, checkRadius, flipLayer);

     Vector2 relativeGroundCheckPosition = (Vector2)transform.position + new Vector2(horizontalDirection * groundCheckPosition.x, groundCheckPosition.y);
     bool groundChecked = Physics2D.OverlapCircle(relativeGroundCheckPosition, checkRadius, flipLayer);

     Vector2 relativeBehindCheck = (Vector2)transform.position + new Vector2(horizontalDirection * behindCheckPosition.x, behindCheckPosition.y);
     bool behindChecked = Physics2D.OverlapCircle(relativeBehindCheck, checkRadius, player);

     if (wallChecked || !groundChecked || behindChecked)
     {
         horizontalDirection = -horizontalDirection;
         transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * horizontalDirection, transform.localScale.y);
     }
 }

 void DetectPlayer()
 {
     Vector2 relativeDetectionArea = (Vector2)transform.position + new Vector2(horizontalDirection * detectionAreaOffset.x, detectionAreaOffset.y);
     bool playerDetected = Physics2D.OverlapBox(relativeDetectionArea, detectionAreaSize, 0, player);

     if (playerDetected)
     {

         Shoot();

     }

 }
 
 
 void Shoot()
 {

     

     Vector2 projectileSpawnPosition = (Vector2)transform.position + new Vector2(projectileSpawnOffset.x * horizontalDirection, projectileSpawnOffset.y);
     GameObject newProjectile = Instantiate(projectile);
     newProjectile.transform.position = projectileSpawnPosition;
     newProjectile.GetComponent<EnemyBullet>().GiveDirektion(-horizontalDirection);
     
 }

 


 #endregion
 #region Message functions
 

 private void OnDrawGizmosSelected()
 {
     Gizmos.color = Color.red;

     Gizmos.DrawWireSphere((Vector2)transform.position + wallCheckPosition, checkRadius);
     Gizmos.DrawWireSphere((Vector2)transform.position + groundCheckPosition, checkRadius);
     Gizmos.DrawWireSphere((Vector2)transform.position + behindCheckPosition, checkRadius);

     Gizmos.DrawWireCube((Vector2)transform.position + detectionAreaOffset, detectionAreaSize);

     Gizmos.DrawWireSphere((Vector2)transform.position + projectileSpawnOffset, projectileSpawnRadius);
 }

 #endregion



}
