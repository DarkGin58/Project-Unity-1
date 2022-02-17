using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

	public GameObject enemyPrefab;
	public GameObject BOSSPrefab;
	float spawnDistance = 12f;
	float lookboss = 0;
	float enemyRate = 5;
	float nextEnemy = 1;
	bool qpx=true;

	// Update is called once per frame
	void Update()
	{
		if (lookboss > 3&&qpx )
		{


			Vector3 offset = Random.onUnitSphere;

			offset.z = 0;

			offset = offset.normalized * spawnDistance;

			Instantiate(BOSSPrefab, transform.position + offset, Quaternion.identity);
			qpx = false;

		}
		else if (qpx)
		{
			nextEnemy -= Time.deltaTime;

			if (nextEnemy <= 0)
			{
				nextEnemy = enemyRate;
				enemyRate *= 0.9f;
				if (enemyRate < 2)
					enemyRate = 2;

				Vector3 offset = Random.onUnitSphere;

				offset.z = 0;

				offset = offset.normalized * spawnDistance;

				Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity);

				lookboss++;
			}
		}
	}
}
