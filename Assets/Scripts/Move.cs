using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode leftKey;
	public KeyCode rightKey;

	public float _speed = 16;

	public GameObject _wallPrefab;
	Collider2D _wall;
	Vector2 _lastWallEnd;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * _speed;
		SpawnWall ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (upKey)) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.up * _speed;
			SpawnWall ();
		} else if (Input.GetKeyDown (downKey)) {
			GetComponent<Rigidbody2D> ().velocity = -Vector2.up * _speed;
			SpawnWall ();
		} else if (Input.GetKeyDown (leftKey)) {
			GetComponent<Rigidbody2D> ().velocity = -Vector2.right * _speed;
			SpawnWall ();
		} else if (Input.GetKeyDown (rightKey)) {
			GetComponent<Rigidbody2D> ().velocity = Vector2.right * _speed;
			SpawnWall ();
		}

		FitColliderBetween (_wall, _lastWallEnd, transform.position);
	
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col != _wall) {
//			print("Player lost:" + _na
			Destroy(gameObject);
		}
	}

	void SpawnWall() {
		_lastWallEnd = transform.position;
		GameObject g = (GameObject)Instantiate (_wallPrefab, transform.position, Quaternion.identity);
		_wall = g.GetComponent<Collider2D> ();
	}

	void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b) {
	
		co.transform.position = a + (b - a) * 0.5f;

		float dist = Vector2.Distance (a, b);
		if (a.x != b.x)
			co.transform.localScale = new Vector2 (dist + 1, 1);
		else
			co.transform.localScale = new Vector2 (1, dist + 1);
	}

}
