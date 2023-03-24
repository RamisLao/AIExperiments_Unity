using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoal : Agent
{
    [SerializeField] private Goal _goal;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Material _winMat;
    [SerializeField] private Material _loseMat;
    [SerializeField] private MeshRenderer _floorMeshRenderer;

    public override void OnEpisodeBegin() {

        transform.localPosition = _spawnPos.localPosition;
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(_goal.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions) {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        Vector3 direction = new(moveX, 0, moveZ);
        transform.localPosition += direction * Time.deltaTime * _moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Goal goal)) {
            SetReward(1f);
            _floorMeshRenderer.material = _winMat;
            EndEpisode();
        }
        if (other.TryGetComponent(out Wall wall)) {
            SetReward(-1f);
            _floorMeshRenderer.material = _loseMat;
            EndEpisode();
        }
    }
}
