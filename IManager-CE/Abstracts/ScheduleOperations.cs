// Decompiled with JetBrains decompiler
// Type: InventoryManager.Abstract.ScheduleOperations`1

using System.Collections.Generic;

namespace IManager_CE.Abstracts
{
  public abstract class ScheduleOperations<T> where T : class
  {
    public abstract void Add(T schedule);

    public abstract List<T> Load();
  }
}
