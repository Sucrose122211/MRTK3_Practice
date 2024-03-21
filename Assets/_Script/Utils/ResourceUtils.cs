using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Utils
{
    public static class Resource
    {
        public static T Load<T>(string path) where T : Object
        {
            T resource;
            try
            {
                if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    GameObject temp = Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
                    resource = temp.GetComponent<T>();
                }
                else
                {
                    resource = Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
                }
            }
            catch
            {
                Debug.Log("Resource not found: " + path);
                return null;
            }
            return resource;
        }

        public static T Load<T>(AssetReference assetRef) where T : Object
        {
            T resource;
            try
            {
                if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    GameObject temp = Addressables.LoadAssetAsync<GameObject>(assetRef).WaitForCompletion();
                    resource = temp.GetComponent<T>();
                }
                else
                {
                    resource = Addressables.LoadAssetAsync<T>(assetRef).WaitForCompletion();
                }
            }
            catch
            {
                Debug.Log("Resource not found: " + assetRef);
                return null;
            }
            return resource;
        }
        public static T[] LoadAll<T>(string path) where T : Object
        {
            T[] resources;

            try
            {
                if (typeof(T).IsSubclassOf(typeof(Component)))
                {
                    resources = Addressables.LoadAssetsAsync<GameObject>(path, obj => { }).WaitForCompletion().
                                Select(x => x.GetComponent<T>()).Where(x => x != null).ToArray();
                }
                else
                {
                    resources = Addressables.LoadAssetsAsync<T>(path, obj => { }).WaitForCompletion().ToArray();
                }
            }
            catch
            {
                Debug.Log("Label not found");
                return null;
            }

            return resources;
        }
        public static GameObject Instantiate(string path, Transform parent = null)
        {
            GameObject go;
            var async = InstantiateAsync(path);
            go = async.WaitForCompletion();
            if (go == null) return null;
            
            if (parent != null) go.transform.SetParent(parent);

            int index = go.name.IndexOf("(Clone)");
            if (index > 0)
                go.name = go.name[..index];
            return go;
        }
        public static GameObject Instantiate(AssetReference assetRef, Transform parent = null)
        {
            GameObject go;
            try
            {
                go = InstantiateAsync(assetRef).WaitForCompletion();
            }
            catch
            {
                Debug.Log("어드레서블 로딩 실패");
                return null;
            }

            if (parent != null) go.transform.SetParent(parent);

            int index = go.name.IndexOf("(Clone)");
            if (index > 0)
                go.name = go.name[..index];
            return go;
        }

        public static AsyncOperationHandle<GameObject> InstantiateAsync(string path)
        {
            var handler = Addressables.InstantiateAsync(path);
            handler.Completed += (obj) =>
            {
                obj.Result.AddComponent<SelfCleanup>();
            };

            return handler;
        }

        public static AsyncOperationHandle<GameObject> InstantiateAsync(AssetReference assetRef)
        {
            var handler = Addressables.InstantiateAsync(assetRef);
            handler.Completed += (obj) =>
            {
                obj.Result.AddComponent<SelfCleanup>();
            };

            return handler;
        }
    }
}