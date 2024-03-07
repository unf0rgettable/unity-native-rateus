using System.Collections;
#if UNITY_ANDROID
using Google.Play.Review;
#endif
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
        #if UNITY_ANDROID
        private readonly ReviewManager _reviewManager;
        private PlayReviewInfo _playReviewInfo;
        #endif

        public RateUsService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
#if UNITY_ANDROID
            _reviewManager = new ReviewManager();
#endif
        }
#if UNITY_ANDROID
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
#endif
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
