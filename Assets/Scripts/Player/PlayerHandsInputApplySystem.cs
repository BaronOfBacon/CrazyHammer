using CrazyHammer.Core.Input;
using Leopotam.Ecs;
using UnityEngine;
using System.Linq;

namespace CrazyHammer.Core
{
    public class PlayerHandsInputApplySystem : IEcsRunSystem
    {
        private readonly EcsFilter<CharacterSpot> _characterSpot = null;
        private readonly EcsFilter<LeftScreenSideTouchComponent> _leftTouchFilter = null;
        private readonly EcsFilter<RightScreenSideTouchComponent> _rightTouchFilter = null;

        public void Run()
        {
            foreach (var index in _characterSpot)
            {
                ref var spot = ref _characterSpot.Get1(index);

                if (spot.CharacterEntity.IsNull() || !spot.CharacterEntity.Has<PlayerTag>()) continue;

                EcsEntity touchEntity;
                GameTouchComponent gameTouch;

                switch (spot.Side)
                {
                    case SpotSide.Left when !_rightTouchFilter.IsEmpty():
                        touchEntity = _rightTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    case SpotSide.Right when !_leftTouchFilter.IsEmpty():
                        touchEntity = _leftTouchFilter.GetEntity(0);
                        gameTouch = touchEntity.Get<GameTouchComponent>();
                        break;
                    default:
                        continue;
                }

                ref var characterComponent = ref spot.CharacterEntity.Get<CharacterComponent>();
                ref var handsComponent = ref spot.CharacterEntity.Get<HandsComponent>();

                var points = characterComponent.SplineComputer.GetPoints().Select(p => p.position).ToArray();

                var touchOffset = gameTouch.DeltaPositionLastFrame * characterComponent.Settings.HandsSettings.Sensitivity *
                                  Time.fixedDeltaTime;

                var desiredPosition = handsComponent.DesiredPositionTransform.position + (Vector3)touchOffset;

                if (IsPointInPolygon((Vector2)desiredPosition, points))
                {
                    handsComponent.DesiredPositionTransform.position = desiredPosition;
                }
                else
                {
                    var closestPoint = (Vector3)GetClosestPointOnBoundary((Vector2)desiredPosition, points);
                    var direction = (desiredPosition - closestPoint).normalized;
                    var newPosition = closestPoint + direction * 0.1f;

                    handsComponent.DesiredPositionTransform.position = newPosition;
                }
                
                desiredPosition = handsComponent.DesiredPositionTransform.position;
                
                var clampedPosition = ClampDistanceFromPoint(desiredPosition,
                    handsComponent.RootTransform.position,
                    characterComponent.Settings.HandsSettings.MaxHandsDistance);

                handsComponent.DesiredPositionTransform.position = clampedPosition;
            }
        }

        private Vector2 GetClosestPointOnBoundary(Vector2 point, Vector3[] polygonVertices)
        {
            Vector2 closestPoint = Vector2.zero;
            float closestDistance = float.MaxValue;

            for (int i = 0, j = polygonVertices.Length - 1; i < polygonVertices.Length; j = i++)
            {
                var edgeStart = polygonVertices[i];
                var edgeEnd = polygonVertices[j];

                var closest = GetClosestPointOnLineSegment(point, edgeStart, edgeEnd);
                var distance = Vector2.Distance(point, closest);

                if (distance < closestDistance)
                {
                    closestPoint = closest;
                    closestDistance = distance;
                }
            }

            return closestPoint;
        }

        private Vector2 GetClosestPointOnLineSegment(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            var direction = lineEnd - lineStart;
            var magnitudeSquared = direction.sqrMagnitude;

            if (magnitudeSquared < Mathf.Epsilon)
                return lineStart;

            var t = Mathf.Clamp01(Vector2.Dot(point - lineStart, direction) / magnitudeSquared);
            return lineStart + t * direction;
        }

        private bool IsPointInPolygon(Vector2 point, Vector3[] polygonVertices)
        {
            bool isInside = false;

            for (int i = 0, j = polygonVertices.Length - 1; i < polygonVertices.Length; j = i++)
            {
                if ((polygonVertices[i].y > point.y) != (polygonVertices[j].y > point.y) &&
                    (point.x < (polygonVertices[j].x - polygonVertices[i].x) * (point.y - polygonVertices[i].y) /
                        (polygonVertices[j].y - polygonVertices[i].y) + polygonVertices[i].x))
                {
                    isInside = !isInside;
                }
            }

            return isInside;
        }

        private Vector3 ClampDistanceFromPoint(Vector3 position, Vector3 exactPoint, float maxDistance)
        {
            var direction = position - exactPoint;
            var distance = direction.magnitude;

            if (distance > maxDistance)
            {
                var clampedDistance = Mathf.Clamp(distance, 0f, maxDistance);
                var clampedPosition = exactPoint + direction.normalized * clampedDistance;
                return clampedPosition;
            }
            return position;
        }
    }
}