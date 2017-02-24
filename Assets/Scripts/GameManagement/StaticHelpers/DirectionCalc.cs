using UnityEngine;
using System.Collections;
using System;

public static class DirectionCalc {

    // This returns the enemy's dodge colision check vectors to be used with OverlapAreaAll
    public static Vector2[] GetEnemyCollisionVectors(Vector2 dir)
    {
        float angle = (float)(Math.Atan2(dir.y, dir.x) / (2 * Math.PI) * 360f);
        return GetEnemyCollisionVectors(angle);
    }

    public static Vector2[] GetEnemyCollisionVectors(float angle)
    {
        Vector2[] vectors = new Vector2[2];
        if (angle > 45 && angle <= 135)
        {
            // Up
            vectors[0] = new Vector2(-2f, -10f);
            vectors[1] = new Vector2(2f, -8f);
        }
        else if (angle > 135 || angle <= -135)
        {
            // Left
            vectors[0] = new Vector2(10f, -2f);
            vectors[1] = new Vector2(8f, 2f);
        }
        else if (angle < -45 && angle > -135)
        {
            // Down
            vectors[0] = new Vector2(-2f, 10f);
            vectors[1] = new Vector2(2f, 8f);
        }
        else
        {
            // Right
            vectors[0] = new Vector2(-10f, -2f);
            vectors[1] = new Vector2(-8f, 2f);
        }

        return vectors;
	}

    public static Vector2 GetEnemyDodgeDirection(Vector2 dir)
    {
        float angle = (float)(Math.Atan2(dir.y, dir.x) / (2 * Math.PI) * 360f);
        if (angle > 45 && angle <= 135)
        {
            // Up
            if (angle > 90)
                return Vector2.right;
            else
                return Vector2.left;
        }
        else if (angle > 135 || angle <= -135)
        {
            // Left
            if (angle < 0)
                return Vector2.up;
            else
                return Vector2.down;
        }
        else if (angle < -45 && angle > -135)
        {
            // Down
            if (angle > -90)
                return Vector2.right;
            else
                return Vector2.left;
        }
        else
        {
            // Right
            if (angle < 0)
                return Vector2.up;
            else
                return Vector2.down;
        }

        return Vector2.up;
    }
	
    public static Vector2 GetPlayerShotDirection(Vector2 dir)
    {
        Vector2 shotDir = Vector2.left;

        return shotDir;
    }
}
