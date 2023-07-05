using BuilderGame.Infrastructure.Services.Ads;
using DG.Tweening;
using UnityEngine;
using Zenject;
using System.Threading.Tasks;

namespace BuilderGame.Gameplay.Unit.Worker
{
    public class WorkerTrigger : MonoBehaviour
    {
        [SerializeField]
        private Worker worker;

        [Space]
        [SerializeField]
        private Transform triggerField;
        [SerializeField]
        private float scaleTime;

        private IAdvertiser advertiser;

        private bool workerActivated;
        private Task<AdWatchResult> ad;

        [Inject]
        public void Construct(IAdvertiser advertiser)
        {
            this.advertiser = advertiser;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(workerActivated) return;

            if (other.CompareTag(Constants.PlayerTag))
            {
                TryActivateWorker();
            }
        }

        //AndriiCodeReview: Changed ad showing logic
        private async void TryActivateWorker()
        {
            if(ad is { Status: TaskStatus.Running }) return;
            
            ad = advertiser.ShowRewardedAd("Full Screen");
            var result = await ad;

            if (result == AdWatchResult.Watched)
            {
                ActivateWorker();
            }
        }

        private void ActivateWorker()
        {
            HideTriggerField();
            worker.Activate();
        }

        private void HideTriggerField()
        {
            triggerField.DOScale(Vector3.zero, scaleTime)
                .SetEase(Ease.Linear).OnComplete(() => { triggerField.gameObject.SetActive(false); });
        }
    }
}
