using System;
using System.Collections.Generic;
using System.Text;

namespace AtominaCraft.Data
{
    /// <summary>
    /// "Named Data Tag" Compound. A mixture of different values that are accessible
    /// <para>
    ///     Shouldn't really be used on blocks because this is a lot of data for 1 block, entities maybe
    /// </para>
    /// </summary>
    public class NDTCompound
    {
        public Dictionary<string, object> Data { get; set; }

        public NDTCompound()
        {
            Data = new Dictionary<string, object>();
        }

        public void SetString(string key, string value)
        {
            Data.Add(key, value);
        }

        public void SetFloat(string key, float value)
        {
            Data.Add(key, value);
        }

        public void SetInteger(string key, int value)
        {
            Data.Add(key, value);
        }

        public string GetString(string key)
        {
            if (Data.TryGetValue(key, out object value))
            {
                return (string)value;
            }
            else
            {
                return "";
            }
        }

        public float GetFloat(string key)
        {
            if (Data.TryGetValue(key, out object value))
            {
                return (float)value;
            }
            else
            {
                return 0.0f;
            }
        }

        public int GetInteger(string key)
        {
            if (Data.TryGetValue(key, out object integer))
            {
                return (int)integer;
            }
            else
            {
                return 0;
            }
        }
    }
}
