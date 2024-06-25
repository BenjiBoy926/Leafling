using System;
using UnityEngine;

namespace Leafling
{
    [Serializable]
    public class FromToCurve
    {
        [SerializeField]
        private float _from;
        [SerializeField]
        private float _to;
        [SerializeField]
        private AnimationCurve _curve;

        public FromToCurve(float from, float to, AnimationCurve curve)
        {
            _from = from;
            _to = to;
            _curve = curve;
        }
        public float Evaluate(float t)
        {
            float eval = _curve.Evaluate(t);
            float inverseEval = _curve.Evaluate(1 - t);
            return _from * inverseEval + _to * eval;
        }
    }
}