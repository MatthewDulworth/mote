using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionController : MonoBehaviour
{
   // ------------------------------------------------------
   // Member Vars
   // ------------------------------------------------------
   [SerializeField] private LayerMask targetLayer;
   [SerializeField] private LayerMask obstacleLayer;

   private PossessableContainer currentTarget;
   private PossessableContainer possessedContainer;
   private List<PossessableContainer> visiblePossessables;
   private List<PossessableContainer> possessables;


   // ------------------------------------------------------
   // Struct
   // ------------------------------------------------------
   private class PossessableContainer
   {
      public Possessable possessable;
      public bool isVisible;
      public bool isPossessed;
      public bool isTargeted;

      public PossessableContainer(Possessable p)
      {
         this.Possessable = p;
         this.IsVisible = false;
         this.IsPossessed = false;
         this.IsTargeted = false;
      }

      public Possessable Possessable
      {
         get { return possessable; }
         set { possessable = value; }
      }

      public bool IsVisible
      {
         get { return isVisible; }
         set { isVisible = value; }
      }

      public bool IsPossessed
      {
         get { return isPossessed; }
         set { isPossessed = value; }
      }

      public bool IsTargeted
      {
         get { return isTargeted; }
         set { isTargeted = value; }
      }
   }


   // ------------------------------------------------------
   // Mono Methods
   // ------------------------------------------------------
   public void OnStart()
   {
      visiblePossessables = new List<PossessableContainer>();
      possessables = new List<PossessableContainer>();

      Possessable[] pos = FindObjectsOfType<Possessable>();
      foreach (Possessable p in pos)
      {
         possessables.Add(new PossessableContainer(p));
      }
   }

   public void OnUpdate(Player player, InputController io)
   {

      foreach (PossessableContainer pc in possessables)
      {
         if (pc != possessedContainer)
         {
            pc.Possessable.NotPossessedUpdate(io);
         }
      }

      if (!CurrentlyPossessing())
      {
         GetVisiblePossessables(player.transform.position, player.Range);
         GetTargetedPossessable(io);

         OnPossessableVisible();
         OnPossessableTargeted();

         HandlePossessions(io, player);
      }
      else
      {
         GetVisiblePossessables(possessedContainer.possessable.transform.position, player.Range);
         GetTargetedPossessable(io);

         OnPossessableVisible();
         OnPossessableTargeted();

         HandlePossessedPossession(io, player);
         HandleUnpossessions(io, player);
      }
   }


   // ------------------------------------------------------
   // On Updates
   // ------------------------------------------------------
   private void GetVisiblePossessables(Vector3 origin, float range)
   {
      this.visiblePossessables.Clear();
      Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(origin, range, targetLayer);

      foreach (Collider2D targetCollider in targetsInRange)
      {

         Possessable pos = targetCollider.gameObject.GetComponent<Possessable>();
         if (pos != null)
         {

            Vector2 direction = (targetCollider.transform.position - origin).normalized;
            float distance = Vector2.Distance(targetCollider.transform.position, origin);

            if (!Physics2D.Raycast(origin, direction, distance, obstacleLayer))
            {
               this.visiblePossessables.Add(GetPossessableContainer(pos));
            }
         }
      }
   }

   private void GetTargetedPossessable(InputController io)
   {

      List<Transform> t = new List<Transform>();
      foreach (PossessableContainer p in visiblePossessables)
      {
         t.Add(p.Possessable.transform);
      }
      Transform[] targets = t.ToArray();

      Transform targetTransform = Targeting.GetClosestTarget(targets, io.MousePosition);
      Possessable possess = (targetTransform != null) ? targetTransform.GetComponent<Possessable>() : null;
      currentTarget = (possess != null) ? GetPossessableContainer(possess) : null;
   }

   private void OnPossessableVisible()
   {
      foreach (PossessableContainer p in possessables)
      {
         if (IsVisible(p) && !p.IsVisible)
         {
            p.IsVisible = true;
            p.Possessable.OnEnterRange();
         }
         else if (!IsVisible(p) && p.IsVisible)
         {
            p.IsVisible = false;
            p.Possessable.OnExitRange();
         }
      }
   }

   private void OnPossessableTargeted()
   {
      foreach (PossessableContainer p in possessables)
      {
         if (IsTargeted(p) && !p.IsTargeted)
         {
            p.IsTargeted = true;
            p.Possessable.OnTargetEnter();
         }
         else if (!IsTargeted(p) && p.IsTargeted)
         {
            p.IsTargeted = false;
            p.Possessable.OnTargetExit();
         }
      }
   }


   // ------------------------------------------------------
   // Possession
   // ------------------------------------------------------
   private void HandlePossessions(InputController io, Player player)
   {
      if (io.PossessionKeyPressed && currentTarget != null)
      {
         PossessObject(currentTarget);
         player.gameObject.SetActive(false);

         foreach (PossessableContainer p in possessables)
         {
            if (p.IsVisible)
            {
               p.IsVisible = false;
               p.Possessable.OnExitRange();
            }
            if (p.IsTargeted)
            {
               p.IsTargeted = false;
               p.Possessable.OnTargetExit();
            }
         }
      }
   }

   private void HandlePossessedPossession(InputController io, Player player)
   {
      if (io.PossessionKeyPressed && currentTarget != null)
      {
         UnpossessObject();
         player.gameObject.SetActive(true);
         PossessObject(currentTarget);
         player.gameObject.SetActive(false);

         foreach (PossessableContainer p in possessables)
         {
            if (p.IsVisible)
            {
               p.IsVisible = false;
               p.Possessable.OnExitRange();
            }
            if (p.IsTargeted)
            {
               p.IsTargeted = false;
               p.Possessable.OnTargetExit();
            }
         }
      }
   }

   private void HandleUnpossessions(InputController io, Player player)
   {
      if (io.UnpossessionKeyPressed)
      {
         player.gameObject.SetActive(true);
         player.transform.position = possessedContainer.Possessable.transform.position;
         UnpossessObject();
      }
   }

   private void PossessObject(PossessableContainer posContainer)
   {
      possessedContainer = posContainer;
      possessedContainer.IsPossessed = true;
      possessedContainer.Possessable.OnPossessionEnter();
   }

   private void UnpossessObject()
   {
      possessedContainer.IsPossessed = false;
      possessedContainer.Possessable.OnPossessionExit();
      possessedContainer = null;
   }


   // ------------------------------------------------------
   // Private Methods
   // ------------------------------------------------------
   private PossessableContainer GetPossessableContainer(Possessable pos)
   {
      PossessableContainer posCon = null;

      foreach (PossessableContainer p in possessables)
      {
         if (pos == p.Possessable)
         {
            posCon = p;
         }
      }

      return posCon;
   }

   private bool IsVisible(PossessableContainer p)
   {
      return (visiblePossessables.Contains(p));
   }

   private bool IsTargeted(PossessableContainer p)
   {
      return (p == currentTarget);
   }

   private bool IsPossessed(PossessableContainer p)
   {
      return (p == possessedContainer);
   }


   // ------------------------------------------------------
   // Public Methods
   // ------------------------------------------------------
   public bool CurrentlyPossessing()
   {
      return (possessedContainer != null);
   }

   public void ForcedUnpossession(Player player)
   {
      player.gameObject.SetActive(true);
      player.transform.position = possessedContainer.Possessable.transform.position;

      possessedContainer.IsPossessed = false;
      possessedContainer.Possessable.OnPossessionExit();
      possessedContainer = null;
   }

   // ------------------------------------------------------
   // Getters
   // ------------------------------------------------------
   public Possessable PossessedObject()
   {
      if (possessedContainer == null)
      {
         return null;
      }
      else
      {
         return possessedContainer.Possessable;
      }
   }
}
