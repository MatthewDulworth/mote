using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour
{
   [SerializeField] private float speed;
   private Animator animator;
   private p_TV tv;
   private p_FrontFacingDoor exit;
   private bool moveTrigger = false;
   private MonoBehaviour target;

   public void init(p_TV tv, p_FrontFacingDoor exit)
   {
      this.animator = GetComponent<Animator>();
      this.tv = tv;
      this.exit = exit;
   }

   // makes the dad move
   public void move()
   {
      this.getTarget();

      if (moveTrigger)
      {
         transform.position = Vector2.MoveTowards(transform.position, HorizontalTarget(), speed * Time.deltaTime);
         animator.SetTrigger("Wake");

         if (transform.position == HorizontalTarget() && target == exit)
         {
            animator.SetTrigger("Unlock");
         }
         else if (transform.position == HorizontalTarget() && target == tv)
         {
            tv.PowerOff();
         }
      }
   }

   // gets the target object
   private void getTarget()
   {
      if (tv.IsOn)
      {
         if (!moveTrigger)
         {
            moveTrigger = true;
         }
         target = tv;
      }
      else
      {
         target = exit;
      }
   }

   public void DadExit() {
      exit.Unlock();
      exit.Open();
      gameObject.SetActive(false);
   }

   private IEnumerator DoorClose() {
      yield return new WaitForSeconds(0.5f);

   
   }

   // gets the target coordinates 
   private Vector3 HorizontalTarget()
   {
      return new Vector3(target.transform.position.x, transform.position.y, 0);
   }
}
