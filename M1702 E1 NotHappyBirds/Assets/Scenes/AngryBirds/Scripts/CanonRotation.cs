using UnityEngine;
public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed = 8;
    public float MinSpeed = 1;
    public GameObject PotencyBar;
    private float initialScaleX;

    public GameObject bullets;
    public Vector3 mousePosition;


    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);   

        Vector2 direction = mousePos-transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI + offset;
        if (angle < _maxRotation.z && angle > _minRotation.z)
            transform.rotation = Quaternion.Euler(0, 0, angle);
   
        if (Input.GetMouseButton(0))
        {
            if (ProjectileSpeed >= MaxSpeed) return;
            else ProjectileSpeed += 4 * Time.deltaTime * MinSpeed;
        }
        if(Input.GetMouseButtonUp(0))
        {
            GameObject projectile = Instantiate(Bullet, ShootPoint.transform);
            projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed  * direction.normalized;
            projectile.transform.SetParent(bullets.transform,true);
            ProjectileSpeed = 0f;
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            transform.localScale.y,
            transform.localScale.z);
    }
}
