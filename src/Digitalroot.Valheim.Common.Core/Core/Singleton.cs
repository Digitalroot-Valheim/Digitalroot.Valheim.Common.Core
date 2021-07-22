using JetBrains.Annotations;
using System;

namespace Digitalroot.Valheim.Common.Core
{
  public abstract class Singleton<TSingletonSubClass> where TSingletonSubClass : Singleton<TSingletonSubClass>, new()
  {
    public static TSingletonSubClass Instance => Nested._instance;

    [UsedImplicitly]
    private class Nested
    {
      static Nested()
      {
      }

      // ReSharper disable once InconsistentNaming
      internal static readonly TSingletonSubClass _instance = InstantiateInstance();

      private static TSingletonSubClass InstantiateInstance()
      {
        TSingletonSubClass instance;
        try
        {
          instance = new TSingletonSubClass();
        }
        catch (Exception ex)
        {
          Log.Error(ex);
          Log.Error($"Failed while initializing singleton of type: {typeof(TSingletonSubClass).FullName}: {ex.Message}");
          throw;
        }
        return instance;
      }
    }
  }
}
