using UnityEngine;

namespace KermansUtility.Patterns.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static bool created = false;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType(typeof(T)) as T;

                return instance;
            }
        }
        private static volatile T instance;

        //Sahne ge�i�lerinde objenin yok olmamas� i�in kullan�labilinir
        protected void SetDestronOnLoadObejct()
        {
            if (!created)
            {
                DontDestroyOnLoad(gameObject);
                created = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}