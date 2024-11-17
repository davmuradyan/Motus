using System;
using System.Collections;
using System.Collections.Generic;

public class CustomArray<T> : IEnumerable<T> {
    T[] array;
    byte size = 0;

    public CustomArray(T item) {
        array = new T[size];
        Add(item);
    }

    public void Add(T item) { 
        size++;
        Array.Resize(ref array, size);
        array[size-1] = item;
    }

    public IEnumerator<T> GetEnumerator() {
        for (int i = 0; i < size; i++) {
            yield return array[i];
        }
    }
    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}