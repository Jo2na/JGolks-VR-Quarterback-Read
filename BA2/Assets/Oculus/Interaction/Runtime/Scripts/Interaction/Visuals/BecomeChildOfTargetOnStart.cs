/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Your use of this SDK or tool is subject to the Oculus SDK License Agreement, available at
https://developer.oculus.com/licenses/oculussdk/

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction
{
    public class BecomeChildOfTargetOnStart : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private bool _keepWorldPosition = true;

        protected virtual void Start()
        {
            Assert.IsNotNull(_target);
            transform.SetParent(_target, _keepWorldPosition);
        }

        #region Inject

        public void InjectAllChildToTransform(Transform target)
        {
            InjectTarget(target);
        }

        public void InjectTarget(Transform target)
        {
            _target = target;
        }

        public void InjectOptionalKeepWorldPosition(bool keepWorldPosition)
        {
            _keepWorldPosition = keepWorldPosition;
        }

        #endregion
    }
}
