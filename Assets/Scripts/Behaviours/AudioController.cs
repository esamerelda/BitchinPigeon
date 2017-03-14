using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
	public class AudioController : AbstractBehaviour
	{
		protected AudioSource source;
		public AudioClip chaseLoop;
		public AudioClip ChaseLoop
		{
			get { return chaseLoop; }
		}

		/*public AudioClip dieClip;
		public AudioClip DieClip
		{
			get { return dieClip; }
		}*/

		public AudioClip hitClip;
		public AudioClip HitClip
		{
			get { return hitClip; }
		}
		public AudioClip idleLoop;
		public AudioClip IdleLoop
		{
			get { return idleLoop; }
		}
		public AudioClip walkLoop;
		public AudioClip WalkLoop
		{
			get { return walkLoop; }
		}

		protected override void SetInitialReferences()
		{
			base.SetInitialReferences();
			source = this.GetComponent<AudioSource>();
		}

		public void PlaySoundOnce(AudioClip clip)
		{
			StopLoop();
			if (clip != null)
			{
				source.PlayOneShot(clip);
			}
			else { Debug.Log("Audio clip is null"); }
		}

		public void StartLoop(AudioClip loop)
		{
			if (loop != null)
			{
				source.loop = true;
				source.clip = loop;
				source.Play(0);
			}
			else { Debug.Log("Audio Loop is null"); }
		}

		private void StopLoop()
		{
			source.loop = false;
			source.Stop();
		}
	}
}

