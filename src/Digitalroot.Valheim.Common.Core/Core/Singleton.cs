using JetBrains.Annotations;
using System;

namespace Digitalroot.Valheim.Common.Core
{
  public abstract class Singleton<TSingletonSubClass>  where TSingletonSubClass : Singleton<TSingletonSubClass>, new()
  {
    // ReSharper disable once MemberCanBeProtected.Global
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
          throw new Exception($"Failed while initializing singleton of type: {typeof(TSingletonSubClass).FullName}: {ex.Message}", ex);
        }
        return instance;
      }
    }
  }
}
