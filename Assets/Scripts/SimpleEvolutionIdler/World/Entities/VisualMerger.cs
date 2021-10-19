using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using SimpleEvolutionIdler.Controllers;

namespace SimpleEvolutionIdler.World
{
    public class VisualMerger : IMerger
    {
        public virtual event Action<IMergeable> MergeRequested;
        public virtual event Action MergeSuccessed;
        public virtual event Action MergeCancelled;
        public virtual event Action MergeFailed;

        protected IMergeable enquirer;
        protected IMergeable respondent;

        protected MergeStatuses enquirerStatus = MergeStatuses.NotAvailable;
        protected MergeStatuses respondentStatus = MergeStatuses.NotAvailable;

        protected Planet planet;

        public VisualMerger(Planet planet)
        {
            this.planet = planet;
        }

        public void Merge(IMergeable mergeable)
        {
            Reset();

            enquirer = mergeable;
            respondent = FindRespondent(mergeable);
            if (respondent == null)
            {
                OnMergeFailed();
                return;
            }

            enquirer.MergeStatusChanged += Enquirer_MergeStatusChanged;
            respondent.MergeStatusChanged += Respondent_MergeStatusChanged;
            enquirer.MergeCancelled += CurrentMergeCancelled;
            respondent.MergeCancelled += CurrentMergeCancelled;

            enquirer.Merger.RequestMerge(respondent);
            respondent.Merger.RequestMerge(enquirer);
        }
        private void Enquirer_MergeStatusChanged(MergeStatuses status)
        {
            enquirerStatus = status;
            CheckReadyForMerge();
        }
        private void Respondent_MergeStatusChanged(MergeStatuses status)
        {
            respondentStatus = status;
            CheckReadyForMerge();
        }

        private void CheckReadyForMerge()
        {
            if (enquirerStatus == MergeStatuses.Ready && respondentStatus == MergeStatuses.Ready)
            {
                if (TryMergeAction()) OnMergeSuccessed();
                else
                {
                    Reset();
                    OnMergeFailed();
                }
            }
        }
        private void CurrentMergeCancelled()
        {
            Reset();
            OnMergeCancelled();
        }

        public void RequestMerge(IMergeable mergeable)
        {
            OnMergeRequested(mergeable);
        }
        
        protected virtual bool TryMergeAction()
        {
            var enquirerEntity = enquirer as VisualEntity;
            var respondentEntity = respondent as VisualEntity;

            var mergedData = (enquirerEntity.EntityData as ContainerEntityData).ContainedEntityData;

            if (planet.TryAddEntity(mergedData, out IEntity entity, enquirerEntity, respondentEntity))
            {
                var visualEntity = entity as VisualEntity;
                if (visualEntity != null)
                {
                    var enquirerPos = enquirerEntity.gameObject.transform.localPosition;
                    var respondentPos = respondentEntity.gameObject.transform.localPosition;
                    var middlePoint = enquirerPos + (respondentPos - enquirerPos) / 2;

                    visualEntity.gameObject.transform.position = middlePoint;
                }

                return true;
            }

            return false;
        }

        protected virtual IMergeable FindRespondent(IMergeable enquirer)
        {
            var entity = enquirer as VisualEntity;
            if (entity == null) return null;

            var entities = planet.Entities.Where(e => e != (object)entity && e is IMergeable);
            var respondent = GetSuitableRespondent(entities, entity);

            return respondent as IMergeable;
        }
        protected virtual VisualEntity GetSuitableRespondent(IEnumerable<IEntity> entities, VisualEntity enquirerEntity)
        {
            var mergeRadius = 1.5f;

            var respondent = entities.Where(e => e.EntityData == enquirerEntity.EntityData 
                                                && (e as IMergeable).MergeStatus == MergeStatuses.Available)
                                    .Select(e => e as VisualEntity)
                                    .OrderBy(g => Vector3.Distance(g.transform.position, enquirerEntity.gameObject.transform.position))
                                    .FirstOrDefault();
            if (respondent != null
                && Vector3.Distance(respondent.transform.position, enquirerEntity.gameObject.transform.position) < mergeRadius)
            {
                return respondent;
            }

            return null;
        }

        protected virtual void OnMergeRequested(IMergeable mergeable)
        {
            MergeRequested?.Invoke(mergeable);
        }
        protected virtual void OnMergeSuccessed()
        {
            MergeSuccessed?.Invoke();

            Reset();
        }
        protected virtual void OnMergeCancelled()
        {
            MergeCancelled?.Invoke();
        }
        protected virtual void OnMergeFailed()
        {
            MergeFailed?.Invoke();
        }
        protected virtual void Reset()
        {
            enquirerStatus = MergeStatuses.NotAvailable;
            respondentStatus = MergeStatuses.NotAvailable;

            if (enquirer != null)
            { 
                enquirer.MergeStatusChanged -= Enquirer_MergeStatusChanged;
                enquirer.MergeCancelled -= CurrentMergeCancelled;
            }
            if (respondent != null)
            {
                respondent.MergeStatusChanged -= Respondent_MergeStatusChanged;
                respondent.MergeCancelled -= CurrentMergeCancelled;
            }

            enquirer = respondent = null;
        }
    }
}
