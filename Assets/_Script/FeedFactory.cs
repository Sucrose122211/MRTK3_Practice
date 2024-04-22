using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Side{
    RIGHT, LEFT, TOP, BOTTOM,
}

public class FeedFactory
{
    private Vector3 _pCenter;
    private float _pWidth;
    private float _pHeight;

    private float[] rangex;
    private float[] rangey;

    public FeedFactory(GameObject plane)
    {
        _pCenter = plane == null ? Vector3.zero : plane.transform.position;
        var collider = plane == null ? null : plane.transform.GetComponentInChildren<Collider>();
        
        _pHeight = collider == null ? 0 : collider.bounds.size.y;
        _pWidth = collider == null ? 0 : collider.bounds.size.x;

        rangex = new float[2] {_pCenter.x - _pWidth/2f, _pCenter.x + _pWidth/2f};
        rangey = new float[2] {_pCenter.y - _pHeight/2f, _pCenter.y + _pHeight/2f};
    }

    public void GenerateRandom()
    {
        float x = Random.Range(rangex[0], rangex[1]);
        float y = Random.Range(rangey[0], rangey[1]);
        float z = _pCenter.z;
        float deg = Random.Range(0, 360f);
        float speed = Random.Range(0.8f, 1.5f);

        Vector3 pos = new(x, y, z);
        Vector3 dir = Quaternion.AngleAxis(deg, Vector3.forward) * Vector3.right;

        GameObject go = Utils.Resource.Instantiate("Feed");
        go.transform.position = pos;
        
        var script = go.GetComponent<Feed>();
        script.OnInstantiate(dir, speed);
    }
}
