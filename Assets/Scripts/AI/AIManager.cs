﻿using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class AIManager : MonoBehaviour
{
    private static AIManager _instance;
    public static AIManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    public Transform Target;
    public float RadiusAroundTarget = 0.5f;
    public List<AIUnit> Units = new List<AIUnit>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set instance if it doesn't exist
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void Update()
    {
        MakeAgentsCircleTarget(); // Update AI units positions around the target
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 50), "Move To Target"))
        {
            MakeAgentsCircleTarget(); // Trigger movement around the target
        }
    }

    private void MakeAgentsCircleTarget()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            // Calculate position on circle around the target
            Vector3 circlePosition = new Vector3(
                Target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / Units.Count),
                Target.position.y,
                Target.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / Units.Count)
            );

            // Move AI unit to the calculated position
            Units[i].MoveTo(circlePosition);

            // Calculate direction from AI unit to target (player)
            Vector3 directionToTarget = (Target.position - Units[i].transform.position).normalized;

            // Calculate rotation to face the target (player)
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);

            // Apply rotation to the AI unit (excluding y-axis rotation)
            Units[i].transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
        }
    }
}
