using BuilderGame.Infrastructure.Services.Ads;
using BuilderGame.Infrastructure.Services.Ads.Fake;
using BuilderGame.Infrastructure.Services.Input;
using DG.Tweening;
using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

        private bool isActive = true;

        [Inject]
        public void Construct(IAdvertiser advertiser)
        {
            this.advertiser = advertiser;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isActive == false)
                return;

            if (other.tag == Constants.PlayerTag) 
            {
                isActive = false;
                ShowReward();
            }
        }

        private async void ShowReward() 
        {
            var adv = advertiser.ShowRewardedAd("Full Screen");
            var result = await adv;

            if (result == AdWatchResult.Watched)
                ActivateWorker();
            else
                isActive = true;
        }

        private void ActivateWorker() 
        {
            triggerField.DOScale(Vector3.zero, scaleTime)
                .SetEase(Ease.Linear).OnComplete(() =>
                {
                    triggerField.gameObject.SetActive(false);
                });

            worker.Activate();
        }
    }
}
