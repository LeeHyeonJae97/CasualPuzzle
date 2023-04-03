using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public static class TaskExtension
{
    public static IEnumerator ToWaitUntil(this Task task)
    {
        yield return new WaitUntil(() => task.IsCompleted);
    }
}