using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.U2D;

namespace Assets.Scripts
{
    /// <summary>
    /// Spawns hazards at random positions.
    /// </summary>
    public class HazardSpawner : MonoBehaviour
    {
        [SerializeField]
        private SpriteShapeController spriteShapeController;

        [SerializeField]
        private Transform spawnAreaStart;

        [SerializeField]
        private Transform spawnAreaEnd;

        [SerializeField]
        [Min(0)]
        private int hazardCount;

        [SerializeField]
        private GameObject[] hazards;

        [SerializeField]
        private float hazardHeightFromGround = 1f;

        private IEnumerable<Vector2> spawnPointsOnEditor;

        //[SerializeField]
        //private EdgeCollider2D spawnArea;

        private void Start()
        {
            Spawn();
        }

        private void OnDrawGizmos()
        {
            if (spawnAreaStart != null && spawnAreaEnd != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(spawnAreaStart.position, spawnAreaEnd.position);

                if (spawnPointsOnEditor == null) spawnPointsOnEditor = GenerateSpawnPoints();
                foreach (Vector2 spawnPoint in spawnPointsOnEditor)
                {
                    Gizmos.DrawSphere(spawnPoint, 1f);
                }
            }
        }

        private void Spawn()
        {
            foreach (Vector2 spawnPoint in GenerateSpawnPoints())
            {
                GameObject objectToSpawn = hazards[Random.Range(0, hazards.Length - 1)];
                Instantiate(objectToSpawn, spawnPoint, Quaternion.identity);
            }

            //Path path = new Path(spawnArea.points);
            //spriteShapeController.edgeCollider
        }

        private IEnumerable<Vector2> GenerateSpawnPoints()
        {
            Vector2 startPoint = spawnAreaStart.position;
            Vector2 endPoint = spawnAreaEnd.position;
            Vector2 spawnLine = endPoint - startPoint;
            float spawnAreaLength = spawnLine.magnitude;

            IList<Vector2> spawnPoints = new List<Vector2>(hazardCount);
            for (int i = 0; i < hazardCount; i++)
            {
                float t = Random.Range(0, spawnAreaLength);
                Vector2 raycastOrigin = startPoint + (spawnLine.normalized * t);
                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.down);
                Vector2 spawnPoint = hit.point + (hit.normal * hazardHeightFromGround);
                spawnPoints.Add(spawnPoint);
            }

            return spawnPoints;
        }

        //private class Path
        //{
        //    private float length;
        //    private readonly IList<Tuple<float, Vector2>> accumulativeLengths = new List<Tuple<float, Vector2>>();

        //    public Path(IList<Vector2> points)
        //    {
        //        length = 0;
        //        accumulativeLengths...
        //        for (int i = 1; i < points.Count(); i++)
        //        {
        //            length += (points[i] - points[i - 1]).magnitude;
        //        }
        //    }
        //}
    }
}
