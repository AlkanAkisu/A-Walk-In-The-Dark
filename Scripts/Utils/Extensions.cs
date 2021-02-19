using System.Collections.Generic;
using System;
using UnityEngine;
static class Extensions
{


  public static bool down(this KeyCode key) => Input.GetKeyDown(key);
  public static bool up(this KeyCode key) => Input.GetKeyUp(key);
  public static bool key(this KeyCode key) => Input.GetKey(key);

  public static K[] map<T, K>(this T[] arr, Func<T, K> mapFunc)
  {
    int N = arr.Length;
    K[] rv = new K[N];

    for (int i = 0; i < N; i++)
    {
      rv[i] = mapFunc(arr[i]);
    }

    return rv;

  }


  public static T FindFirst<T>(this T[] arr, Func<T, bool> boolFunc)
  {

    for (int i = 0; i < arr.Length; i++)
    {
      if (boolFunc(arr[i]))
        return arr[i];
    }
    return default(T);

  }

  public static Dictionary<K, L> reduce<T, K, L>(this T[] arr, Func<T, KeyValuePair<K, L>> dictFunc)
  {
    int N = arr.Length;
    Dictionary<K, L> dict = new Dictionary<K, L>();


    for (int i = 0; i < N; i++)
    {
      var keyValue = dictFunc(arr[i]);
      dict.Add(keyValue.Key, keyValue.Value);

    }

    return dict;

  }
  public static void asd(this MonoBehaviour mb, Component[] components)
  {
    for (int i = 0; i < components.Length; i++)
    {

      string typeStr = components[i].GetType().Name;
      var type = Type.GetType(typeStr).Name;
      components[i] = mb.GetComponent(type);
    }



  }





}
