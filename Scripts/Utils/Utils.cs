using UnityEditor;
using UnityEngine;

static class Utils
{
	public static void Log(params object[] objects)
	{
		int N = objects.Length;
		string str = "";
		for (int i = 0; i < N; i++)
		{
			str += objects[i].ToString();
			if (i != N - 1)
				str += " ";
		}
		Debug.Log(str);

	}

	//***
	//*** Gizmoz
	//***
	public static void DrawArrow(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color))
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);

		Vector3 perp = Vector2.Perpendicular(to - from).normalized;
		Vector3 vect = (to - from).normalized;

		float halfSize = 0.3f;
		float h = halfSize * Mathf.Sqrt(3);

		Vector3 headpoint = to;

		Vector3 triangle = headpoint - vect * h;


		DrawLine(triangle - perp * halfSize, triangle + perp * halfSize, thichness * 25, color);
		DrawLine(headpoint, triangle + perp * halfSize, thichness * 25, color);
		DrawLine(triangle - perp * halfSize, headpoint, thichness * 25, color);




		DrawLine(from, to, thichness, color);


	}
	public static void DrawLine(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color))
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);
		Gizmos.color = color;
		Vector3 perp = Vector2.Perpendicular(to - from);
		Gizmos.DrawLine(from, to);
		for (int i = 1; i < thichness + 1; i++)
		{
			Gizmos.DrawLine(from + perp * 0.0001f * i, to + perp * 0.0001f * i);
			Gizmos.DrawLine(from - perp * 0.0001f * i, to - perp * 0.0001f * i);
		}
	}


	//***
	//*** DEBUG
	//***
	public static void DrawArrowDebug(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color), float duration = 0)
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);

		Vector3 perp = Vector2.Perpendicular(to - from).normalized;
		Vector3 vect = (to - from).normalized;

		float halfSize = 0.3f;
		float h = halfSize * Mathf.Sqrt(3);

		Vector3 headpoint = to;

		Vector3 triangle = headpoint - vect * h;


		DrawLineDebug(triangle - perp * halfSize, triangle + perp * halfSize, thichness * 25, color, duration);
		DrawLineDebug(headpoint, triangle + perp * halfSize, thichness * 25, color, duration);
		DrawLineDebug(triangle - perp * halfSize, headpoint, thichness * 25, color, duration);


		DrawLineDebug(from, to, thichness, color, duration);


	}
	public static void DrawLineDebug(Vector3 from, Vector3 to, int thichness = 1, Color color = default(Color), float duration = 0)
	{
		if (color == default(Color)) color = new Color32(0, 1, 0, 1);

		Vector3 perp = Vector2.Perpendicular(to - from);

		if (duration == 0)
		{
			Debug.DrawLine(from, to, color);
			for (int i = 1; i < thichness + 1; i++)
			{
				Debug.DrawLine(from + perp * 0.0001f * i, to + perp * 0.0001f * i, color);
				Debug.DrawLine(from - perp * 0.0001f * i, to - perp * 0.0001f * i, color);
			}
		}

		else
		{
			Debug.DrawLine(from, to, color, duration);
			for (int i = 1; i < thichness + 1; i++)
			{
				Debug.DrawLine(from + perp * 0.0001f * i, to + perp * 0.0001f * i, color, duration);
				Debug.DrawLine(from - perp * 0.0001f * i, to - perp * 0.0001f * i, color, duration);
			}
		}
	}
	public static void DrawCone(Vector3 from, Vector3 to, float degree, float radius, bool drawRadius = false)
	{
		if (drawRadius)
		{

			var vector1 = from + Quaternion.Euler(0, 0, degree / 2) * to * radius;
			var vector2 = from + Quaternion.Euler(0, 0, -degree / 2) * to * radius;

			Gizmos.DrawLine(from, vector1);
			Gizmos.DrawLine(from, vector2);
		}
		Handles.DrawWireArc(from, Vector3.forward, to, degree / 2, radius);
		Handles.DrawWireArc(from, Vector3.forward, to, -degree / 2, radius);
	}




}
