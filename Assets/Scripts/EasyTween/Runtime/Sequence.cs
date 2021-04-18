using System.Collections.Generic;

namespace EasyTween
{
    public sealed class Sequence : ITweenable
    {
        List<BaseTween[]> list;
        public List<BaseTween[]> List
        {
            get
            {
                if (list == null)
                    list = new List<BaseTween[]>(capacity: 4);
                return list;
            }
        }
        
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

        
        public Sequence Append(params BaseTween[] parallelTweens)
        {
            List.Add(parallelTweens);
            return this;
        }

        public Sequence Append(Sequence sequence)
        {
            List.AddRange(sequence.List);
            return this;
        }

        public Sequence Prepend(params BaseTween[] parallelTweens)
        {
            return Insert(0, parallelTweens);
        }

        public Sequence Prepend(Sequence sequence)
        {
            return Insert(0, sequence);
        }

        public Sequence Insert(int queuePosition, params BaseTween[] parallelTweens)
        {
            List.Insert(queuePosition, parallelTweens);
            return this;
        }

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