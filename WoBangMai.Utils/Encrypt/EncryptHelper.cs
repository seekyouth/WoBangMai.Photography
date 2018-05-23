using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WoBangMai.Utils
{
    public class EncryptHelper
    {
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        public EncryptHelper() 
        { 
        } 
        /// <summary> 
        /// 使用缺省密钥字符串加密 
        /// </summary> 
        /// <param name="original">明文</param> 
        /// <returns>密文</returns> 
        public static string Encrypt(string original) 
        { 
            return Encrypt(original,"JASONHEUNG"); 
        } 
        /// <summary> 
        /// 使用缺省密钥解密 
        /// </summary> 
        /// <param name="original">密文</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string original) 
        { 
            return Decrypt(original,"JASONHEUNG",System.Text.Encoding.Default); 
        } 
        /// <summary> 
        /// 使用给定密钥解密 
        /// </summary> 
        /// <param name="original">密文</param> 
        /// <param name="key">密钥</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string original, string key) 
        { 
            return Decrypt(original,key,System.Text.Encoding.Default); 
        } 
        /// <summary> 
        /// 使用缺省密钥解密,返回指定编码方式明文 
        /// </summary> 
        /// <param name="original">密文</param> 
        /// <param name="encoding">编码方式</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string original,Encoding encoding) 
        { 
            return Decrypt(original,"JASONHEUNG",encoding); 
        } 
        /// <summary> 
        /// 使用给定密钥加密 
        /// </summary> 
        /// <param name="original">原始文字</param> 
        /// <param name="key">密钥</param> 
        /// <param name="encoding">字符编码方案</param> 
        /// <returns>密文</returns> 
        public static string Encrypt(string original, string key) 
        { 
            byte[] buff = System.Text.Encoding.Default.GetBytes(original); 
            byte[] kb = System.Text.Encoding.Default.GetBytes(key); 
            return Convert.ToBase64String(Encrypt(buff,kb)); 
        } 

        /// <summary> 
        /// 使用给定密钥解密 
        /// </summary> 
        /// <param name="encrypted">密文</param> 
        /// <param name="key">密钥</param> 
        /// <param name="encoding">字符编码方案</param> 
        /// <returns>明文</returns> 
        public static string Decrypt(string encrypted, string key,Encoding encoding) 
        { 
            byte[] buff = Convert.FromBase64String(encrypted); 
            byte[] kb = System.Text.Encoding.Default.GetBytes(key); 
            return encoding.GetString(Decrypt(buff,kb)); 
        } 
        /// <summary> 
        /// 生成MD5摘要 
        /// </summary> 
        /// <param name="original">数据源</param> 
        /// <returns>摘要</returns> 
        public static byte[] MakeMD5(byte[] original) 
        { 
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider(); 
            byte[] keyhash = hashmd5.ComputeHash(original); 
            hashmd5 = null; 
            return keyhash; 
        } 

        /// <summary> 
        /// 使用给定密钥加密 
        /// </summary> 
        /// <param name="original">明文</param> 
        /// <param name="key">密钥</param> 
        /// <returns>密文</returns> 
        public static byte[] Encrypt(byte[] original, byte[] key) 
        { 
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider(); 
            des.Key = MakeMD5(key); 
            des.Mode = CipherMode.ECB; 

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length); 
        } 

        /// <summary> 
        /// 使用给定密钥解密数据 
        /// </summary> 
        /// <param name="encrypted">密文</param> 
        /// <param name="key">密钥</param> 
        /// <returns>明文</returns> 
        public static byte[] Decrypt(byte[] encrypted, byte[] key) 
        { 
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider(); 
            des.Key = MakeMD5(key); 
            des.Mode = CipherMode.ECB; 

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length); 
        } 

        /// <summary> 
        /// 使用给定密钥加密 
        /// </summary> 
        /// <param name="original">原始数据</param> 
        /// <param name="key">密钥</param> 
        /// <returns>密文</returns> 
        public static byte[] Encrypt(byte[] original) 
        { 
            byte[] key = System.Text.Encoding.Default.GetBytes("JASONHEUNG"); 
            return Encrypt(original,key); 
        } 

        /// <summary> 
        /// 使用缺省密钥解密数据 
        /// </summary> 
        /// <param name="encrypted">密文</param> 
        /// <param name="key">密钥</param> 
        /// <returns>明文</returns> 
        public static byte[] Decrypt(byte[] encrypted) 
        { 
            byte[] key = System.Text.Encoding.Default.GetBytes("JASONHEUNG"); 
            return Decrypt(encrypted,key); 
        }


        /// <summary>  
        /// 对用户传进来的字符串进行不可逆(MD5)加密  
        /// </summary>  
        /// <param name="str">需要加密的字符串</param>  
        /// <returns>返回值是已经加密的字符串</returns>  
        public static string EncMd5(string str)
        {
            //获取加密服务    
            System.Security.Cryptography.MD5CryptoServiceProvider md5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();

            //获取要加密的字段，并转化为Byte[]数组    
            byte[] strEncrypt = System.Text.Encoding.UTF8.GetBytes(str);

            //加密Byte[]数组    
            byte[] resultEncrypt = md5CSP.ComputeHash(strEncrypt);

            //将加密后的数组转化为字段(普通加密)   
            //string EncStr = System.Text.Encoding.UTF8.GetString(resultEncrypt);  

            string EncStr = "";
            for (int i = 0; i < resultEncrypt.Length; i++)
            {
                EncStr = EncStr + resultEncrypt[i].ToString("x").PadLeft(2, '0');
            }

            return EncStr;

        }


         

    } 
} 