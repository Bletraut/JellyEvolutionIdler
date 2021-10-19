using System;
using System.Collections;

namespace SimpleEvolutionIdler.World
{
    public interface IMergeable
    {
        IMerger Merger { get; }
        MergeStatuses MergeStatus { get; }

        event Action<MergeStatuses> MergeStatusChanged;
        event Action MergeCancelled;
    }

    public enum MergeStatuses
    {
        NotAvailable,
        Available,
        Preparing,
        Ready
    }
}