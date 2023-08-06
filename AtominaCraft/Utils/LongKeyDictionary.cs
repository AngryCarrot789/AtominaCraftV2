using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using REghZy.Utils;

namespace AtominaCraft.Utils {
    public class LongKeyDictionary<V> : IEnumerable<LKDEntry<V>> {
        private static readonly long EMPTY_KEY = -9223372036854775808L;
        private static readonly int BUCKET_SIZE = 4096;
        private readonly long[][] keys = new long[4096][];
        private readonly V[][] values = new V[4096][];
        private FlatMap<V> flat = new FlatMap<V>();
        private int modCount;
        private int count;

        public int Count => this.count;

        public LongKeyDictionary() {

        }

        public V this[long key] {
            get => TryGet(key, out V value) ? value : throw new KeyNotFoundException(key.ToString());
            set => Put(key, value);
        }

        public bool IsEmpty() {
            return this.count == 0;
        }

        public bool ContainsKey(long key) {
            if (this.count == 0) {
                return false;
            }
            else if (this.flat.ContainsKey(key)) {
                return true;
            }
            else {
                int index = (int) (KeyIndex(key) & 4095L);
                long[] inner = this.keys[index];
                if (inner == null) {
                    return false;
                }
                else {
                    for (int i = 0, len = inner.Length; i < len; ++i) {
                        long innerKey = inner[i];
                        if (innerKey == -9223372036854775808L) {
                            return false;
                        }

                        if (innerKey == key) {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        public void Clear() {
            if (this.count != 0) {
                ++this.modCount;
                this.count = 0;
                for (int i = 0; i < 4096; i++) {
                    this.keys[i] = null;
                    this.values[i] = null;
                }

                this.flat = new FlatMap<V>();
            }
        }

        public bool TryGet(long key, out V value) {
            if (this.count == 0) {
                value = default;
                return false;
            }
            else if (this.flat.TryGet(key, out value)) {
                return true;
            }
            else {
                int index = (int) (KeyIndex(key) & 4095L);
                long[] inner = this.keys[index];
                if (inner == null) {
                    return false;
                }
                else {
                    for (int i = 0, len = inner.Length; i < len; ++i) {
                        long innerKey = inner[i];
                        if (innerKey == -9223372036854775808L) {
                            return false;
                        }

                        if (innerKey == key) {
                            value = this.values[index][i];
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        public bool Put(long key, V value) {
            this.flat.Put(key, value);
            int index = (int) (KeyIndex(key) & 4095L);
            long[] innerKeys = this.keys[index];
            V[] innerValues = this.values[index];
            ++this.modCount;
            if (innerKeys == null) {
                this.keys[index] = innerKeys = new long[8];
                Arrays.Fill(innerKeys, -9223372036854775808L);
                this.values[index] = innerValues = new V[8];
                innerKeys[0] = key;
                innerValues[0] = value;
                ++this.count;
            }
            else {
                int i;
                for (i = 0; i < innerKeys.Length; ++i) {
                    if (innerKeys[i] == -9223372036854775808L) {
                        ++this.count;
                        innerKeys[i] = key;
                        innerValues[i] = value;
                        return false;
                    }

                    if (innerKeys[i] == key) {
                        innerKeys[i] = key;
                        innerValues[i] = value;
                        return true;
                    }
                }

                this.keys[index] = innerKeys = innerKeys.CopyOf(i << 1);
                Arrays.Fill(innerKeys, i, innerKeys.Length, -9223372036854775808L);
                this.values[index] = innerValues = innerValues.CopyOf(i << 1);
                innerKeys[i] = key;
                innerValues[i] = value;
                ++this.count;
            }

            return false;
        }

        public bool Remove(long key) {
            this.flat.Remove(key);
            int index = (int) (KeyIndex(key) & 4095L);
            long[] inner = this.keys[index];
            if (inner == null) {
                return false;
            }
            else {
                for (int i = 0; i < inner.Length && inner[i] != -9223372036854775808L; ++i) {
                    if (inner[i] == key) {
                        ++i;

                        while (i < inner.Length && inner[i] != -9223372036854775808L) {
                            inner[i - 1] = inner[i];
                            this.values[index][i - 1] = this.values[index][i];
                            ++i;
                        }

                        inner[i - 1] = -9223372036854775808L;
                        this.values[index][i - 1] = default;
                        --this.count;
                        ++this.modCount;
                        return true;
                    }
                }

                return false;
            }
        }

        private static long KeyIndex(long key) {
            key ^= (long) ((ulong) key >> 33);
            key *= -49064778989728563L;
            key ^= (long) ((ulong) key >> 33);
            key *= -4265267296055464877L;
            key ^= (long) ((ulong) key >> 33);
            return key;
        }

        private struct Enumerator : IEnumerator<LKDEntry<V>>, IEnumerator {
                private readonly LongKeyDictionary<V> map;
                private int count;
                private int index;
                private int innerIndex;
                private long currentKey;
                private V currentValue;

                public Enumerator(LongKeyDictionary<V> map) {
                    this.map = map;
                    this.index = 0;
                    this.currentKey = -9223372036854775808L;
                    this.currentValue = default;
                    this.count = 0;
                    this.innerIndex = 0;
                }

                public void Dispose() {
                    this.currentValue = default;
                }

                public bool MoveNext() {
                    if (this.count >= this.map.count) {
                        return false;
                    }

                    long[][] keys = this.map.keys;
                    ++this.count;
                    if (this.currentKey != -9223372036854775808L) {
                        ++this.innerIndex;
                    }

                    for(; this.index < keys.Length; ++this.index) {
                        if (keys[this.index] != null) {
                            if (this.innerIndex < keys[this.index].Length) {
                                long key = keys[this.index][this.innerIndex];
                                V value = this.map.values[this.index][this.innerIndex];
                                if (key != -9223372036854775808L) {
                                    this.currentKey = key;
                                    this.currentValue = value;
                                    return true;
                                }
                            }

                            this.innerIndex = 0;
                        }
                    }

                    return false;
                }

                public LKDEntry<V> Current => new LKDEntry<V>(this.currentKey, this.currentValue);

                object IEnumerator.Current => this.currentKey;

                void IEnumerator.Reset() {
                    this.index = 0;
                    this.currentKey = -9223372036854775808L;
                    this.currentValue = default;
                }
            }

        private class FlatMap<T> {
            private readonly T[][] flatLookup = new T[1024][];
            private readonly bool[][] containment = new bool[1024][];

            public FlatMap() {
                for (int i = 0; i < 1024; i++) {
                    this.flatLookup[i] = new T[1024];
                    this.containment[i] = new bool[1024];
                }
            }

            public void Put(long msw, long lsw, T value) {
                long acx = Math.Abs(msw);
                long acz = Math.Abs(lsw);
                if (acx < 512L && acz < 512L) {
                    this.flatLookup[(int)(msw + 512L)][(int)(lsw + 512L)] = value;
                    this.containment[(int) (msw + 512L)][(int) (lsw + 512L)] = true;
                }
            }

            public void Put(long key, T value) {
                Put(LongHash.MSW(key), LongHash.LSW(key), value);
            }

            public void Remove(long key) {
                Put(key, default);
            }

            public void Remove(long msw, long lsw) {
                Put(msw, lsw, default);
            }

            public bool ContainsKey(long msw, long lsw) {
                return Math.Abs(msw) < 512L && Math.Abs(lsw) < 512L && this.containment[(int) (msw + 512L)][(int) (lsw + 512L)];
            }

            public bool ContainsKey(long key) {
                long msw = LongHash.MSW(key);
                long lsw = LongHash.LSW(key);
                return Math.Abs(msw) < 512L && Math.Abs(lsw) < 512L && this.containment[(int) (msw + 512L)][(int) (lsw + 512L)];
            }

            public bool TryGet(long msw, long lsw, out T value) {
                long acx = Math.Abs(msw);
                long acz = Math.Abs(lsw);
                if (acx < 512L && acz < 512L && this.containment[(int) (msw + 512L)][(int) (lsw + 512L)]) {
                    value = this.flatLookup[(int) (msw + 512L)][(int) (lsw + 512L)];
                    return true;
                }
                else {
                    value = default;
                    return false;
                }
            }

            public bool TryGet(long key, out T value)  {
                return TryGet(LongHash.MSW(key), LongHash.LSW(key), out value);
            }
        }

        public IEnumerator<LKDEntry<V>> GetEnumerator() {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new Enumerator(this);
        }
    }
}