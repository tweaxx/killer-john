using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

namespace TweaxxGames.JamRaid
{
    public static class Utilities
    {
        public static bool simulateNoInternet;

        public static bool HasInternet()
        {
            return !simulateNoInternet && Application.internetReachability != NetworkReachability.NotReachable;
        }

        public static Sequence DoActionDelayed(Action call, float delay, bool useUnscaledTime = false)
        {
            if (call == null)
                return null;

            Sequence sequence = DOTween.Sequence();
            sequence.SetUpdate(useUnscaledTime);
            sequence.SetDelay(delay);
            sequence.OnComplete(() =>
            {
                call();
            });
            sequence.Play();

            return sequence;
        }

        //public static Coroutine CallDelayed(Action call, float delay, bool useUnscaledTime = false)
        //{
        //    IEnumerator Routine()
        //    {
        //        if (useUnscaledTime)
        //            yield return new WaitForSecondsRealtime(delay);
        //        else
        //            yield return new WaitForSeconds(delay);

        //        call?.Invoke();
        //    }

        //    return App.Instance.StartCoroutine(Routine());
        //}

        //public static Coroutine DoActionNextFrame(Action call, int framesToSkip = 1)
        //{
        //    IEnumerator<int> Routine()
        //    {
        //        for (int i = 0; i < framesToSkip; i++)
        //        {
        //            yield return 0;
        //        }
        //        call?.Invoke();
        //    }

        //    return App.Instance.StartCoroutine(Routine());
        //}

        //public static Coroutine DoActionEndOfFrame(Action call)
        //{
        //    IEnumerator<WaitForEndOfFrame> Routine()
        //    {
        //        yield return new WaitForEndOfFrame();
        //        call?.Invoke();
        //    }

        //    return App.Instance.StartCoroutine(Routine());
        //}

        /// <summary>
        /// Kills the sequence and returns null
        /// </summary>
        public static Sequence KillSequence(Sequence sequence)
        {
            if (sequence != null) sequence.Kill();

            return null;
        }

        //public static Coroutine StartCoroutine(IEnumerator coroutine)
        //{
        //    if (coroutine != null)
        //        return App.Instance.StartCoroutine(coroutine);

        //    return null;
        //}

        ///// <summary>
        ///// Kills the coroutine and returns null
        ///// </summary>
        //public static Coroutine KillCoroutine(Coroutine coroutine)
        //{
        //    if (coroutine != null)
        //        App.Instance.StopCoroutine(coroutine);

        //    return null;
        //}

        public static string GenerateID(string sourceUrl)
        {
            return string.Format("{0}_{1:N}", sourceUrl, Guid.NewGuid());
        }

        public static string GetFormattedTime(int totalSeconds, bool skipHours = true)
        {
            int hours = Mathf.FloorToInt(totalSeconds / 60 / 60);
            int minutes = Mathf.FloorToInt(totalSeconds / 60) - hours * 60;
            int seconds = Mathf.FloorToInt(totalSeconds) - minutes * 60 - hours * 60;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-EN");

            StringBuilder builder = new StringBuilder();
            if (hours > 0 || !skipHours)
                builder.AppendFormat(culture, "{0,2:N0}", hours).Append("h ");

            builder.AppendFormat(culture, "{0,2:N0}", minutes).Append("m ");
            builder.AppendFormat(culture, "{0,2:N0}", seconds).Append("s");

            return builder.ToString().Replace("  ", " ").TrimStart(' ');
        }

        public static string GetTimeToNextDay()
        {
            var currentDate = DateTime.UtcNow;
            var hours = 23 - currentDate.Hour;
            var minutes = 59 - currentDate.Minute;

            return $"{hours}h {minutes}m";
        }

        public static Vector3 GetRandomVector3InRange(float range)
        {
            Vector3 vec;
            vec.x = Random.Range(0f, range);
            vec.y = Random.Range(0f, range);
            vec.z = Random.Range(0f, range);

            return vec;
        }

        public static void SetAnimatorBoolForOneFrame(string name, Animator animator, MonoBehaviour coroutineTarget)
        {
            if (animator != null && coroutineTarget != null)
            {
                coroutineTarget.StartCoroutine(CancelBool());
                IEnumerator CancelBool()
                {
                    animator.SetBool(name, true);

                    yield return null;

                    if (animator != null)
                        animator.SetBool(name, false);
                }
            }
        }
    }
}
