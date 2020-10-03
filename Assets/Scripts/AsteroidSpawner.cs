using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public const float SpawnDistance = 17f;

    public void StartSpawning(int totalTime)
    {
        this.totalTime = totalTime;
        this.elapsed = 0f;
        this.autoSpawning = true;
        this.lastSpawnTime = float.MinValue;
        this.currentCooldown = 0f;
    }

    public void StopSpawning()
    {
        this.autoSpawning = false;
    }

    [SerializeField]
    Asteroid asteroidPrefab = default;
    
    float totalTime, elapsed;
    float lastSpawnTime;
    float currentCooldown;
    bool autoSpawning = false;


    void Update()
    {
        if (autoSpawning)
        {
            elapsed = Mathf.Clamp(elapsed + Time.deltaTime, 0, totalTime);
            var progress = elapsed / totalTime;
            SpawnParams args = GetSpawnBasedOnProgress(progress);

            if (lastSpawnTime + currentCooldown <= Time.time)
            {
                Spawn(args);
                lastSpawnTime = Time.time;
                currentCooldown = Random.Range(args.MinCooldown, args.MaxCooldown);
            }
        }
    }

    void Spawn(SpawnParams arg)
    {
        var position = Quaternion.Euler(0, 0, Random.Range(arg.AngleFrom, arg.AngleTo)) * Vector2.up * SpawnDistance;
        var initialForce = Random.Range(arg.MinForce, arg.MaxForce);
        var towardsPlanet = (-position).normalized;
        var zRotation = Random.Range(arg.MinAngleTrajDeg, arg.MaxAngleTrajDeg) * (Random.Range(-1, 1) < 0 ? -1 : 1);
        Vector2 v = Quaternion.Euler(0, 0, zRotation) * towardsPlanet * initialForce;


        var asteroid = Instantiate(asteroidPrefab, this.transform);
        asteroid.Initialize(
            position: position,
            forceVector: v,
            gravitySource: new Vector2(0, 0),
            gravityForceMag: arg.GravityForceMag);
    }

    SpawnParams GetSpawnBasedOnProgress(float progress)
    {
        if (progress > 0.9f)
            return SpawnParams.Diff5;
        else if (progress > 0.7f)
            return SpawnParams.Diff4;
        else if (progress > 0.5f)
            return SpawnParams.Diff3;
        else if (progress > 0.2f)
            return SpawnParams.Diff2;
        else
            return SpawnParams.Diff1;
    }

    struct SpawnParams
    {
        float angleFrom, angleTo;
        float minAngleTrajDeg, maxAngleTrajDeg;
        float minForce, maxForce;
        float minCooldown;
        float maxCooldown;

        public static SpawnParams Safe => new SpawnParams
        {
            angleFrom = 45f,
            angleTo = 135f,
            minAngleTrajDeg = 50, // 50f default
            maxAngleTrajDeg = 50,
            minForce = 1.5f, // 1.5f default 
            maxForce = 2f,
            minCooldown = 1f,
            maxCooldown = 3f
        };

        public static SpawnParams Diff1 => new SpawnParams
        {
            angleFrom = 85f,
            angleTo = 95f,
            minAngleTrajDeg = 40, // 48 default
            maxAngleTrajDeg = 50,
            minForce = 0.7f, // 1f default
            maxForce = 1.8f,
            minCooldown = 1.5f,
            maxCooldown = 2f
        };

        public static SpawnParams Diff2 => new SpawnParams
        {
            angleFrom = 70f,
            angleTo = 110f,
            minAngleTrajDeg = 15, // 22 default
            maxAngleTrajDeg = 29,
            minForce = 1.5f, // 2f default
            maxForce = 2.5f,
            minCooldown = 0.7f,
            maxCooldown = 1.7f
        };

        public static SpawnParams Diff3 => new SpawnParams
        {
            angleFrom = 60f,
            angleTo = 120f,
            minAngleTrajDeg = 10, // 15 default
            maxAngleTrajDeg = 20,
            minForce = 3f, // 4f default
            maxForce = 5f,
            minCooldown = 0.3f,
            maxCooldown = 1f
        };

        public static SpawnParams Diff4 => new SpawnParams
        {
            angleFrom = 50f,
            angleTo = 130f,
            minAngleTrajDeg = 8, // 10 default
            maxAngleTrajDeg = 12,
            minForce = 4f, // 6f default
            maxForce = 6f,
            minCooldown = 0.2f,
            maxCooldown = 0.5f
        };

        public static SpawnParams Diff5 => new SpawnParams
        {
            angleFrom = 50f,
            angleTo = 130f,
            minAngleTrajDeg = 7, // 10 default
            maxAngleTrajDeg = 14,
            minForce = 4f, // 6f default
            maxForce = 8f,
            minCooldown = 0.1f,
            maxCooldown = 0.2f
        };

        public float MinAngleTrajDeg { get => minAngleTrajDeg; private set => minAngleTrajDeg = value; }
        public float MaxAngleTrajDeg { get => maxAngleTrajDeg; private set => maxAngleTrajDeg = value; }
        public float MinForce { get => minForce; private set => minForce = value; }
        public float MaxForce { get => maxForce; private set => maxForce = value; }
        public float MinCooldown { get => minCooldown; private set => minCooldown = value; }
        public float MaxCooldown { get => maxCooldown; private set => maxCooldown = value; }
        public float GravityForceMag => 60f;

        public float AngleFrom { get => angleFrom; set => angleFrom = value; }
        public float AngleTo { get => angleTo; set => angleTo = value; }
    }
}