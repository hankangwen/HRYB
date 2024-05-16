using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BezierCurveScript
{
	public List<Vector3> pathPoints;
	private int segments;
	public int pointCount;

	public BezierCurveScript()
	{
		pathPoints = new List<Vector3>();
		pointCount = 100;
	}

	public void DeletePath()
	{
		pathPoints.Clear();
	}

	public Vector3 BezierPathCalculation(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float tt = t * t;
		float ttt = t * tt;
		float u = 1.0f - t;
		float uu = u * u;
		float uuu = u * uu;

		Vector3 B;
		B = uuu * p0;
		B += 3.0f * uu * t * p1;
		B += 3.0f * u * tt * p2;
		B += ttt * p3;

		return B;
	}

	public List<Vector3> RandomVector(Actor self, Vector3 Enemy, int randvalue )
	{
		List<Vector3> ls = new();

		Vector3 target = (self.transform.position - Enemy).normalized;

		Vector3 Middle = Vector3.Lerp( self.transform.position, Enemy, 0.5f);

		ls.Add(self.transform.position + new Vector3(0, 1.2f, 0));
		float rand1 = Random.Range(-randvalue, randvalue);
		float rand2 = Random.Range(-1.2f, randvalue);
		float rand3 = Random.Range(-randvalue, randvalue);

		Vector3 bezierr1 = new Vector3(rand1, rand2, rand3);//new Vector3(rand1 * target.x, rand2 * target.y, rand3 * target.z) ;

		ls.Add(Middle + bezierr1);
		rand1 = Random.Range(-randvalue, randvalue);
		rand2 = Random.Range(-1.2f, randvalue);
		rand3 = Random.Range(-randvalue, randvalue);
		ls.Add(Middle + new Vector3(rand1, rand2, rand3));

		ls.Add(Enemy);

		return ls;
	}
}