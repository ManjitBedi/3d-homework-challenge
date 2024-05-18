using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Got this code from the internet.
/// Have a need to queue coroutines to go one after another to create animation sequences.
/// </summary>
public class QueueManager : MonoBehaviour
{
	private readonly Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();

	public void AddToQueue(IEnumerator coroutine)
	{
		_coroutineQueue.Enqueue(coroutine);
		if (_coroutineQueue.Count ==1) //no previous items in queue
			StartCoroutine(CoroutineCoordinator());
	}

	private IEnumerator CoroutineCoordinator()
	{
		while (true)
		{
			while (_coroutineQueue.Count > 0)
				{
				yield return StartCoroutine(_coroutineQueue.Peek());
				_coroutineQueue.Dequeue();
			}
			if (_coroutineQueue.Count == 0)
				yield break;
		}
	}
}