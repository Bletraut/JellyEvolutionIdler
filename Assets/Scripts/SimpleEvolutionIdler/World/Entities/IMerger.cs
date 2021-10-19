using System;
using System.Collections;

namespace SimpleEvolutionIdler.World
{
    public interface IMerger
    {
        void Merge(IMergeable mergeable);
        void RequestMerge(IMergeable mergeable);

        event Action<IMergeable> MergeRequested;
        event Action MergeSuccessed;
        event Action MergeCancelled;
        event Action MergeFailed;
    }
}