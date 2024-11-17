using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomArray : MonoBehaviour, IEnumerable<PipePrefabScript> {
    PipePrefabScript[] array;
    byte size = 0;

    public CustomArray(PipePrefabScript item) {
        array = new PipePrefabScript[size];
        Add(item);
    }

    public void Add(PipePrefabScript item) { 
        size++;
        Array.Resize(ref array, size);
        array[size-1] = item;
    }

    public IEnumerator<PipePrefabScript> GetEnumerator() {
        for (int i = 0; i < size; i++) {
            yield return array[i];
        }
    }

    internal void Clear() {
        foreach (var item in array)
        {
            Destroy(item.gameObject);
        }
        size = 0;
        array = new PipePrefabScript[size];
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}