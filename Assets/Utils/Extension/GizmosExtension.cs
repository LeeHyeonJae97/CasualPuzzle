using UnityEngine;

public static class Gizmos
{
	public static Color color { get { return UnityEngine.Gizmos.color; } set { UnityEngine.Gizmos.color = value; } }
	public static Matrix4x4 matrix { get { return UnityEngine.Gizmos.matrix; } set { UnityEngine.Gizmos.matrix = value; } }

	public static float circleDetail = 15;

	public static void DrawRay(Ray r)
	{
		UnityEngine.Gizmos.DrawRay(r);
	}

	public static void DrawRay(Ray r, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawRay(r);
		Gizmos.color = org;
	}

	public static void DrawRay(Vector3 from, Vector3 direction)
	{
		UnityEngine.Gizmos.DrawRay(from, direction);
	}

	public static void DrawRay(Vector3 from, Vector3 direction, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawRay(from, direction);
		Gizmos.color = org;
	}

	public static void DrawLine(Vector3 from, Vector3 to)
	{
		UnityEngine.Gizmos.DrawLine(from, to);
	}

	public static void DrawLine(Vector3 from, Vector3 to, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawLine(from, to);
		Gizmos.color = org;
	}

	public static void DrawCube(Vector3 center, Vector3 size)
	{
		UnityEngine.Gizmos.DrawCube(center, size);
	}

	public static void DrawCube(Vector3 center, Vector3 size, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawCube(center, size);
		Gizmos.color = org;
	}

	public static void DrawWireCube(Vector3 center, Vector3 size)
	{
		UnityEngine.Gizmos.DrawWireCube(center, size);
	}

	public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireCube(center, size);
		Gizmos.color = org;
	}

	public static void DrawWireCuboid(Vector3 min, Vector3 max)
	{
		var p1 = new Vector3(min.x, min.y, min.z);
		var p2 = new Vector3(max.x, min.y, min.z);
		var p3 = new Vector3(max.x, min.y, max.z);
		var p4 = new Vector3(min.x, min.y, max.z);
		var p5 = new Vector3(min.x, max.y, min.z);
		var p6 = new Vector3(max.x, max.y, min.z);
		var p7 = new Vector3(max.x, max.y, max.z);
		var p8 = new Vector3(min.x, max.y, max.z);

		DrawLine(p1, p2);
		DrawLine(p2, p3);
		DrawLine(p3, p4);
		DrawLine(p4, p1);

		DrawLine(p5, p6);
		DrawLine(p6, p7);
		DrawLine(p7, p8);
		DrawLine(p8, p5);

		DrawLine(p1, p5);
		DrawLine(p2, p6);
		DrawLine(p3, p7);
		DrawLine(p4, p8);
	}

	public static void DrawWireCuboid(Vector3 center, float width, float length, float height)
	{
		var hw = width / 2;
		var hl = length / 2;
		var hh = height / 2;

		var p1 = new Vector3(center.x - hw, center.y - hh, center.z - hl);
		var p2 = new Vector3(center.x + hw, center.y - hh, center.z - hl);
		var p3 = new Vector3(center.x + hw, center.y - hh, center.z + hl);
		var p4 = new Vector3(center.x - hw, center.y - hh, center.z + hl);
		var p5 = new Vector3(center.x - hw, center.y + hh, center.z - hl);
		var p6 = new Vector3(center.x + hw, center.y + hh, center.z - hl);
		var p7 = new Vector3(center.x + hw, center.y + hh, center.z + hl);
		var p8 = new Vector3(center.x - hw, center.y + hh, center.z + hl);

		DrawLine(p1, p2);
		DrawLine(p2, p3);
		DrawLine(p3, p4);
		DrawLine(p4, p1);

		DrawLine(p5, p6);
		DrawLine(p6, p7);
		DrawLine(p7, p8);
		DrawLine(p8, p5);

		DrawLine(p1, p5);
		DrawLine(p2, p6);
		DrawLine(p3, p7);
		DrawLine(p4, p8);
	}

	public static void DrawWireCuboid(Bounds bounds)
	{
		var p1 = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
		var p2 = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
		var p3 = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
		var p4 = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
		var p5 = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
		var p6 = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
		var p7 = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
		var p8 = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);

		DrawLine(p1, p2);
		DrawLine(p2, p3);
		DrawLine(p3, p4);
		DrawLine(p4, p1);

		DrawLine(p5, p6);
		DrawLine(p6, p7);
		DrawLine(p7, p8);
		DrawLine(p8, p5);

		DrawLine(p1, p5);
		DrawLine(p2, p6);
		DrawLine(p3, p7);
		DrawLine(p4, p8);
	}

	public static void DrawWireCuboid(Vector3 min, Vector3 max, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireCuboid(min, max);
		Gizmos.color = org;
	}

	public static void DrawWireCuboid(Vector3 center, float width, float length, float height, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireCuboid(center, width, length, height);
		Gizmos.color = org;
	}

	public static void DrawWireCuboid(Bounds bounds, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireCuboid(bounds);
		Gizmos.color = org;
	}

	public static void DrawSphere(Vector3 center, float radius)
	{
		UnityEngine.Gizmos.DrawSphere(center, radius);
	}

	public static void DrawSphere(Vector3 center, float radius, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawSphere(center, radius);
		Gizmos.color = org;
	}

	public static void DrawWireSphere(Vector3 center, float radius)
	{
		UnityEngine.Gizmos.DrawWireSphere(center, radius);
	}

	public static void DrawWireSphere(Vector3 center, float radius, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireSphere(center, radius);
		Gizmos.color = org;
	}

	public static void DrawWireRect(Vector2 center, Vector2 size)
	{
		DrawWireCube(center, size);
	}

	public static void DrawWireRect(Vector2 center, Vector2 size, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireRect(center, size);
		Gizmos.color = org;
	}

	public static void DrawWireRectXZ(Vector3 center, Vector2 size)
	{
		DrawWireCube(center, new Vector3(size.x, 0, size.y));
	}

	public static void DrawWireRectXZ(Vector3 center, Vector2 size, Color color)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireRectXZ(center, size);
		Gizmos.color = org;
	}

	public static void DrawWireCircle(Vector2 center, float radius, float circleDetail = 30)
	{
		float angle = 360 / circleDetail;
		float rad1, rad2;
		Vector2 point1, point2;

		// i to i+1
		for (int i = 0; i < circleDetail - 1; i++)
		{
			rad1 = angle * Mathf.Deg2Rad * i;
			rad2 = angle * Mathf.Deg2Rad * (i + 1);

			point1 = center + new Vector2(Mathf.Cos(rad1), Mathf.Sin(rad1)) * radius;
			point2 = center + new Vector2(Mathf.Cos(rad2), Mathf.Sin(rad2)) * radius;

			DrawLine(point1, point2);
		}

		// last to first
		rad1 = angle * Mathf.Deg2Rad * 0;
		rad2 = angle * Mathf.Deg2Rad * (circleDetail - 1);

		point1 = center + new Vector2(Mathf.Cos(rad1), Mathf.Sin(rad1)) * radius;
		point2 = center + new Vector2(Mathf.Cos(rad2), Mathf.Sin(rad2)) * radius;

		DrawLine(point1, point2);
	}

	public static void DrawWireCircle(Vector2 center, float radius, Color color, float circleDetail = 30)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireCircle(center, radius, circleDetail);
		Gizmos.color = org;
	}

	public static void DrawWireCircleXZ(Vector3 center, float radius, float circleDetail = 30)
	{
		float angle = 360 / circleDetail;
		float rad1, rad2;
		Vector3 point1, point2;

		// i to i+1
		for (int i = 0; i < circleDetail - 1; i++)
		{
			rad1 = angle * Mathf.Deg2Rad * i;
			rad2 = angle * Mathf.Deg2Rad * (i + 1);

			point1 = center + new Vector3(Mathf.Cos(rad1), 0, Mathf.Sin(rad1)) * radius;
			point2 = center + new Vector3(Mathf.Cos(rad2), 0, Mathf.Sin(rad2)) * radius;

			DrawLine(point1, point2);
		}

		// last to first
		rad1 = angle * Mathf.Deg2Rad * 0;
		rad2 = angle * Mathf.Deg2Rad * (circleDetail - 1);

		point1 = center + new Vector3(Mathf.Cos(rad1), 0, Mathf.Sin(rad1)) * radius;
		point2 = center + new Vector3(Mathf.Cos(rad2), 0, Mathf.Sin(rad2)) * radius;

		DrawLine(point1, point2);
	}

	public static void DrawWireCircleXZ(Vector3 center, float radius, Color color, float circleDetail = 30)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawWireCircleXZ(center, radius, circleDetail);
		Gizmos.color = org;
	}

	public static void DrawPoint(Vector2 point, float size = 1)
	{
		DrawLine(new Vector2(point.x - size / 2, point.y), new Vector2(point.x + size / 2, point.y));
		DrawLine(new Vector2(point.x, point.y - size / 2), new Vector2(point.x, point.y + size / 2));
	}

	public static void DrawPoint(Vector2 point, Color color, float size = 1)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawPoint(point, size);
		Gizmos.color = org;
	}

	public static void DrawPoint(Vector3 point, float size = 1)
	{
		DrawLine(new Vector3(point.x - size / 2, point.y, point.z), new Vector3(point.x + size / 2, point.y, point.z));
		DrawLine(new Vector3(point.x, point.y - size / 2, point.z), new Vector3(point.x, point.y + size / 2, point.z));
		DrawLine(new Vector3(point.x, point.y, point.z - size / 2), new Vector3(point.x, point.y, point.z + size / 2));
	}

	public static void DrawPoint(Vector3 point, Color color, float size = 1)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawPoint(point, size);
		Gizmos.color = org;
	}

	public static void DrawArrow(Vector3 from, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		if (direction == default) return;

		DrawRay(from, direction);
		DrawRay(from + direction,
			Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1) * arrowHeadLength);
		DrawRay(from + direction,
			Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1) * arrowHeadLength);
	}

	public static void DrawArrow(Vector3 from, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Color org = Gizmos.color;

		Gizmos.color = color;
		DrawArrow(from, direction, arrowHeadLength, arrowHeadAngle);
		Gizmos.color = org;
	}
}