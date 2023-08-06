using System.Collections.Generic;

namespace AtominaCraft.Data {
    /// <summary>
    ///     "Named Data Tag" Compound. A mixture of different values that are accessible
    ///     <para>
    ///         Shouldn't really be used on blocks because this is a lot of data for 1 block, entities maybe
    ///     </para>
    /// </summary>
    public class NDTCompound {
        public Dictionary<string, object> Data;

        public NDTCompound() {
            this.Data = new Dictionary<string, object>();
        }

        public void SetString(string key, string value) {
            this.Data.Add(key, value);
        }

        public void SetFloat(string key, float value) {
            this.Data.Add(key, value);
        }

        public void SetInteger(string key, int value) {
            this.Data.Add(key, value);
        }

        public string GetString(string key) {
            if (this.Data.TryGetValue(key, out object value))
                return (string) value;
            return "";
        }

        public float GetFloat(string key) {
            if (this.Data.TryGetValue(key, out object value))
                return (float) value;
            return 0.0f;
        }

        public int GetInteger(string key) {
            if (this.Data.TryGetValue(key, out object integer))
                return (int) integer;
            return 0;
        }
    }
}