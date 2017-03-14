using UnityEngine;

[System.Serializable]
public class Tick {
    protected const int SECONDS_TO_MILLISECONDS     = 60;
    protected const int MINIMUM_THRESHOLD           = 0;
    protected const int MAXIMUM_THRESHOLD           = 500;

    protected float _timeOfLastTick = 0f;

    [SerializeField][Range(MINIMUM_THRESHOLD, MAXIMUM_THRESHOLD)]
    protected float _timeSinceLastTick = 0f;

    [SerializeField][Range(MINIMUM_THRESHOLD, MAXIMUM_THRESHOLD)]
    protected float _timeOutThreshold = 100f;

    public virtual bool GetIsTimedOut() {
        return _timeSinceLastTick >= _timeOutThreshold;
    }
    public virtual void OnTick() {
        _timeSinceLastTick = Mathf.Clamp(Time.time * SECONDS_TO_MILLISECONDS - _timeOfLastTick, MINIMUM_THRESHOLD, MAXIMUM_THRESHOLD);
    }
    public virtual void OnTock() {
        _timeSinceLastTick  = 0f;
        _timeOfLastTick     = Time.time * SECONDS_TO_MILLISECONDS;
    }
    public virtual void OnReset() {
        _timeSinceLastTick = 0f;
        _timeOfLastTick = 0f;
    }
}