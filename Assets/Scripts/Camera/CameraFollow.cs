using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private float dampTime = 0.15f;
   private Vector3 velocity = Vector3.zero;
   private Transform target;

   private Camera cam;

   void Start()
   {
      target = FindObjectOfType<Player>().transform;
      cam = GetComponent<Camera>();
   }

   public void Update()
   {
      if (target)
      {
         Vector3 point = cam.WorldToViewportPoint(target.position);
         Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
         Vector3 destination = transform.position + delta;
         transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
      }
   }

}
