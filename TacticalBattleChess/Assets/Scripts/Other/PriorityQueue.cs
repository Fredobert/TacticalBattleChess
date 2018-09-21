using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PriorityQueue<T> where T : class
{
    private T[] data;
    private int[] prios;
    private int count = 0;

    public PriorityQueue(int maxsize)
    {
        data = new T[maxsize];
        prios = new int[maxsize];
    }

    public void Add(T value, int prio)
    {
        if (count == data.Length)
        {
            Debug.Log("Heap is full");
        }
        data[count] = value;
        prios[count] = prio;
        count++;
        BubbleUp(count - 1);
    }

    private void BubbleUp(int startPos)
    {
        int pos = startPos;
        int z = prios[startPos];
        T k = data[startPos];
        while (pos > 0)
        {
            int parent = (pos - 1) / 2;
            if (prios[parent] > z)
            {
                data[pos] = data[parent];
                prios[pos] = prios[parent];
                pos = parent;
            }
            else
            {
                break;
            }
            prios[pos] = z;
            data[pos] = k;
        }
    }

    public T Poll()
    {
        if (count > 0)
        {
            T min = data[0];
            int prio = prios[0];
            data[0] = data[count - 1];
            prios[0] = prios[count - 1];
            count--;
            SiftDown(0);
            return min;
        }
        Debug.Log("Heap is empty");
        return null;
         
    }

    public int Contains(T value, int prio)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == value)
            {
                return i;
            }
        }
        return -1;
    }

    public void DecreasePrio(int pos, int newPrio)
    {
        prios[pos] = newPrio;
        BubbleUp(pos);
    }

    public void SiftDown(int startPos)
    {
        int pos = startPos;
        while (2 * pos + 1 < count)
        {
            int childLeft = 2 * pos + 1;
            int childRight = childLeft + 1;
            int minChild = childLeft;
            if (childRight < count && prios[childRight] < prios[childLeft])
            {
                minChild = childRight;
            }
            if (prios[pos] > prios[minChild])
            {
                T z = data[pos];
                int p = prios[pos];
                data[pos] = data[minChild];
                prios[pos] = prios[minChild];
                data[minChild] = z;
                prios[minChild] = p;
                pos = minChild;
            }
            else
            {
                break;
            }

        }

    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public int Count()
    {
        return count;
    }
}
