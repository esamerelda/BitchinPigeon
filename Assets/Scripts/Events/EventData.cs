using UnityEngine;

public class EventData {
    protected bool          _isLocal;
    protected GameObject    _eventSender;
    protected GameObject    _eventObserver;

    public EventData() {
        _isLocal        = false;
        _eventSender    = null;
        _eventObserver  = null;
    }
    public EventData(GameObject eventSender, bool isLocal = false) {
        _isLocal        = isLocal;
        _eventSender    = eventSender;
        _eventObserver  = isLocal ? _eventSender : null;
    } 
    public EventData(GameObject eventObserver, GameObject eventSender) {
        _isLocal        = true;
        _eventSender    = eventSender;
        _eventObserver  = eventObserver;
    }
    public virtual bool GetIsLocal() {
        return _isLocal;
    }
    public virtual GameObject GetEventSender() {
        return _eventSender;
    }
    public virtual GameObject GetEventObserver() {
        return _eventObserver;
    }
}