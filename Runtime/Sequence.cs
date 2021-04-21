using System.Collections.Generic;

namespace EasyTween
{
    public sealed class Sequence : ITweenable
    {
        List<BaseTween[]> list;
        /// <summary>List of all tweens in this sequence. It contains array of tweens because all tweens in array will be play parallel.</summary>
        List<BaseTween[]> List
        {
            get
            {
                if (list == null)
                    list = new List<BaseTween[]>(capacity: 4);
                return list;
            }
        }
        
        /// <summary>Return all current playing parallel tweens as enumerable.</summary>
        public IEnumerable<BaseTween> CurrentTweens
        {
            get
            {
                BaseTween[] parallelTweens = null;
                while (List.Count > 0 && parallelTweens == null)
                {
                    parallelTweens = TakeCurrent();
                    bool isAnyNotCompleted = false;

                    foreach (var tween in parallelTweens)
                    {
                        if (!tween.IsCompleted)
                        {
                            yield return tween;
                            isAnyNotCompleted = true;
                        }
                    }

                    if (!isAnyNotCompleted)
                    {
                        RemoveCurrent();
                        parallelTweens = null;
                    }
                }
            }
        }

        /// <summary>Add tween / few parallel tweens to end of sequence.</summary>
        /// <param name="parallelTweens">Tweens</param>
        /// <returns>Sequence</returns>
        public Sequence Append(params BaseTween[] parallelTweens)
        {
            List.Add(parallelTweens);
            return this;
        }
        
        /// <summary>Add another sequence to end of this sequence.</summary>
        /// <param name="sequence">Another sequence</param>
        /// <returns>Sequence</returns>
        public Sequence Append(Sequence sequence)
        {
            List.AddRange(sequence.List);
            return this;
        }

        /// <summary>Add tween / few parallel tweens to begin of sequence.</summary>
        /// <param name="parallelTweens">Tweens</param>
        /// <returns>Sequence</returns>
        public Sequence Prepend(params BaseTween[] parallelTweens)
        {
            return Insert(0, parallelTweens);
        }

        /// <summary>Add another sequence to begin of this sequence.</summary>
        /// <param name="sequence">Another sequence</param>
        /// <returns>Sequence</returns>
        public Sequence Prepend(Sequence sequence)
        {
            return Insert(0, sequence);
        }

        /// <summary>Add tween / few parallel tweens to specific position in sequence.</summary>
        /// <param name="queuePosition">Position in sequence.</param>
        /// <param name="parallelTweens">Tweens</param>
        /// <returns>Sequence</returns>
        public Sequence Insert(int queuePosition, params BaseTween[] parallelTweens)
        {
            List.Insert(queuePosition, parallelTweens);
            return this;
        }

        /// <summary>Add another sequence to specific position in this sequence.</summary>
        /// <param name="queuePosition">Position in sequence.</param>
        /// <param name="sequence">Another sequence</param>
        /// <returns>Sequence</returns>
        public Sequence Insert(int queuePosition, Sequence sequence)
        {
            List.InsertRange(queuePosition, sequence.List);
            return this;
        }


        BaseTween[] TakeCurrent()
        {
            return (List.Count > 0) ? List[0] : null;
        }

        void RemoveCurrent()
        {
            if (List.Count > 0)
                List.RemoveAt(0);
        }

    }
}