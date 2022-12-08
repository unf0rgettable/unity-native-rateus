using System.Collections;
using Google.Play.Review;
using LittleBit.Modules.CoreModule;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Unfo.Modules.RateUsModule
{
    public class RateUsService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;

        public RateUsService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _reviewManager = new ReviewManager();
        }

        private IEnumerator GetPlayReviewInfoWithShow()
        {
            var requestFlowOperation = _reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError(requestFlowOperation.Error.ToString());
                yield break;
            }
            _playReviewInfo = requestFlowOperation.GetResult();
            Debug.LogWarning(_playReviewInfo.ToString());

            var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
            
            yield return launchFlowOperation;
            
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                Debug.LogError(launchFlowOperation.Error.ToString());
                yield break;
            }
            Debug.LogWarning("Showing Rate Us");
        }

        public void ShowReviewWindowWithRequest()
        {
#if UNITY_ANDROID
            _coroutineRunner.StartCoroutine(GetPlayReviewInfoWithShow());
#elif UNITY_IOS
            Device.RequestStoreReview();
#endif
        }
    }
}