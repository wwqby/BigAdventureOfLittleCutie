using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollecable
{
   void Collect(Player player);
   bool IsCollected();
}
