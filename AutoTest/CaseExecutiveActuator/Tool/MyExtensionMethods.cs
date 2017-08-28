using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaseExecutiveActuator.Tool
{
    public static class MyExtensionMethods
    {
        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void MyAdd(this Dictionary<string, ICaseExecutionDevice> dc, string yourKey, ICaseExecutionDevice yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                dc[yourKey] = yourValue;
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }

        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">Key</param>
        /// <param name="yourValue">Value</param>
        public static void MyAdd(this Dictionary<string, IRunTimeStaticData> dc, string yourKey, IRunTimeStaticData yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                dc[yourKey] = yourValue;
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }

        /// <summary>
        /// 添加键值，若遇到已有key则覆盖
        /// </summary>
        /// <typeparam name="T">T Type</typeparam>
        /// <param name="dc">Dictionary</param>
        /// <param name="yourKey">yourKey</param>
        /// <param name="yourValue">yourValue</param>
        public static void MyAdd<T>(this Dictionary<string, T> dc, string yourKey, T yourValue)
        {
            if (dc.ContainsKey(yourKey))
            {
                dc[yourKey] = yourValue;
            }
            else
            {
                dc.Add(yourKey, yourValue);
            }
        }



        /// <summary>
        /// 返回对象的深度克隆(泛型数据，要求T必须为值类型，或类似string的特殊引用类型[因为虽然使用string的克隆会有相同的引用，但对string进行修改的时都会创建新的对象])
        /// </summary>
        /// <typeparam name="TKey">TKey</typeparam>
        /// <typeparam name="TValue">TKey</typeparam>
        /// <param name="dc">目标Dictionary</param>
        /// <returns>对象的深度克隆</returns>
        public static Dictionary<TKey, TValue> MyClone<TKey, TValue>(this Dictionary<TKey, TValue> dc)
        {
            Dictionary<TKey, TValue> cloneDc = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> tempKvp in dc)
            {
                cloneDc.Add(tempKvp.Key, tempKvp.Value);
            }
            return cloneDc;
        }

        /// <summary>
        /// 返回对象的深度克隆(泛型数据，要求T必须为值类型，或类似string的特殊引用类型[因为虽然使用string的克隆会有相同的引用，但对string进行修改的时都会创建新的对象](该重载可以约束支持clone的TValue))
        /// </summary>
        /// <typeparam name="TKey">TKey</typeparam>
        /// <typeparam name="TValue">TKey</typeparam>
        /// <param name="dc">目标Dictionary</param>
        /// <returns>对象的深度克隆</returns>
        public static Dictionary<TKey, TValue> MyDeepClone<TKey, TValue>(this Dictionary<TKey, TValue> dc)  where TValue:ICloneable
        {
            Dictionary<TKey, TValue> cloneDc = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> tempKvp in dc)
            {
                cloneDc.Add(tempKvp.Key, (TValue)tempKvp.Value.Clone());
            }
            return cloneDc;
        }

        //public static Dictionary<string, ICaseExecutionDevice> MyClone(this Dictionary<string, ICaseExecutionDevice> dc)
        //{
        //    Dictionary<string, ICaseExecutionDevice> cloneDc = new Dictionary<string, ICaseExecutionDevice>();
        //    foreach (KeyValuePair<string, ICaseExecutionDevice> tempKvp in dc)
        //    {
        //        cloneDc.Add(tempKvp.Key, (ICaseExecutionDevice)tempKvp.Value.Clone());
        //    }
        //    return cloneDc;
        //}

        public static List<IRunTimeStaticData> MyClone(this List<IRunTimeStaticData> lt)
        {
            List<IRunTimeStaticData> cloneLt = new List<IRunTimeStaticData>();
            foreach (IRunTimeStaticData tempKvp in lt)
            {
                cloneLt.Add((IRunTimeStaticData)tempKvp.Clone());
            }
            return cloneLt;
        }
    }

}
