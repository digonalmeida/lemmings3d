using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sensor<T> where T: Component {
    private static int DefaultMaxColliders = 5;

    public Type type = Type.Ray;
    public Vector3 Origin = new Vector3();
    public Vector3 Direction = new Vector3();
    public float Distance = 1.0f;
    public LayerMask LayerMask = 0;
    public List<T> Ignore = new List<T>();

    public T[] Sensations;
    private int _CollidersCount = 0;
    public int SensationsCount = 0;
    RaycastHit[] _Hits;
    Collider[] _Colliders;
    Vector3 _Position;
    Color debugColor = new Color(0, 1, 0, 0.2f);

    private void ClearSensations()
    {
        SensationsCount = 0;
    }
    private void AddSensation(T sensation)
    {
        Sensations[SensationsCount] = sensation;
        SensationsCount++;
    }

    public Sensor()
    {
        Init(DefaultMaxColliders);
    }

    public Sensor(int MaxColliders)
    {
        Init(MaxColliders);
    }

    public void Init(int MaxColliders)
    {
        _Colliders = new Collider[MaxColliders];
        Sensations = new T[MaxColliders];
        _Hits = new RaycastHit[MaxColliders];
    }

    public enum Type
    {
        Ray,
        Sphere
    }

    public T SenseFirst(Vector3 position)
    {
        Sense(position);
        
        if(SensationsCount > 0)
        {
            return Sensations[0];
        }

        return null;
    }

    public virtual void Sense(Vector3 position)
    {
        ClearSensations();
        _Position = position;
        
        switch (type)
        {
            case Type.Ray:
                SenseCollidersRay();
                break;
            case Type.Sphere:
                SenseCollidersSphere();
                break;
            default:
                break;
        }

        for (int i = 0; i < _CollidersCount; i++)
        {
            T sensation = _Colliders[i].GetComponent<T>();
            if(sensation == null)
            {
                Debug.Log("trying to find comopnent in " + _Colliders[i].name, _Colliders[i]);
                continue;
            }

            //because List<T>.Contains() allocs memory
            for(int j = 0; j < Ignore.Count; j++)
            {
                if(Ignore[j] == sensation)
                {
                    continue;
                }
            }
            
            if(!CheckSensationIsValid(sensation))
            {
                continue;
            }

            AddSensation(sensation);
        }
    }
    
    protected virtual bool CheckSensationIsValid(T sensation)
    {
        return true;
    }

    void SenseCollidersRay()
    {
        _CollidersCount = Physics.RaycastNonAlloc(_Position + Origin, Direction.normalized, _Hits, LayerMask);
        for(int i = 0; i < _CollidersCount; i++)
        {
            _Colliders[i] = _Hits[i].collider;
        }
    }

    void SenseCollidersSphere()
    {
        _CollidersCount = Physics.OverlapSphereNonAlloc(_Position + Origin, Distance, _Colliders, LayerMask);
    }

    public void DrawGizmos()
    {
        Gizmos.color = debugColor;
        switch (type)
        {
            case Type.Ray:
                GizmosDrawRay();
                break;
            case Type.Sphere:
                GizmosDrawSphere();
                break;
            default:
                break;
        }
    }

    public void GizmosDrawRay()
    {
        Gizmos.DrawLine(_Position + Origin, _Position + Origin + (Direction.normalized * Distance));
    }

    void GizmosDrawSphere()
    {
        Gizmos.DrawSphere(_Position + Origin, Distance);
    }
    
}
