using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CustomArray<T> : IEnumerable<T> where T : ISpawnable {
    private T[] array;
    private byte size = 1;

    public CustomArray(T item) {
        array = new T[1];
        array[0] = item;
    }

    public void Add(T item) {
        size++;
        Array.Resize(ref array, size);
        array[size - 1] = item;
    }

    public IEnumerator<T> GetEnumerator() {
        for (int i = 0; i < size; i++) {
            yield return array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public GameObject CheckArray() {
        GameObject item = null;

        foreach (var i in array) {
            if (i.IsAvailable()) {
                item = i.gameObject;
                i.isAvailable = false;
                break;
            }
        }
        return item;
    }

    public void Freeze() {
        foreach (var i in array) {
            i.speed = 0;
        }
    }

    public int Length() {
        return array.Length;
    }
}