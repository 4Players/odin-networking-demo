using System;
using System.Text;
using OdinNative.Odin;
using UnityEngine;

namespace Odin.OdinNetworking
{
    public class OdinNetworkWriter: IUserData
    {
        private byte[] _bytes = new byte[1500];
        public int Cursor { get; private set; }

        public byte GetByteAt(int index)
        {
            if (index >= _bytes.Length)
            {
                return 0;
            }

            return _bytes[index];
        }

        public void Write(byte value)
        {
            _bytes[Cursor] = value;
            Cursor++;
        }

        public void Write(int value)
        {
            foreach (var aByte in BitConverter.GetBytes(value))
            {
                Write(aByte);
            }
        }

        public void Write(ushort value)
        {
            foreach (var aByte in BitConverter.GetBytes(value))
            {
                Write(aByte);
            }
        }
        
        public void Write(ulong value)
        {
            foreach (var aByte in BitConverter.GetBytes(value))
            {
                Write(aByte);
            }
        }
        
        public void Write(bool value)
        {
            foreach (var aByte in BitConverter.GetBytes(value))
            {
                Write(aByte);
            }
        }

        public void Write(string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            Write((ushort)bytes.Length);
            foreach (var aByte in bytes)
            {
                Write(aByte);
            }
        }
        
        public void Write(float value)
        {
            foreach (var aByte in BitConverter.GetBytes(value))
            {
                Write(aByte);
            }
        }

        public void Write(Vector2 value)
        {
            Write(value.x);
            Write(value.y);
        }

        public void Write(Vector3 value)
        {
            Write(value.x);
            Write(value.y);
            Write(value.z);
        }

        public void Write(Quaternion value)
        {
            Write(value.x);
            Write(value.y);
            Write(value.z);
            Write(value.w);
        }

        public void Write(Vector4 value)
        {
            Write(value.x);
            Write(value.y);
            Write(value.z);
            Write(value.w);
        }

        public void Write(object value)
        {
            if (value is string s)
            {
                Write((byte)OdinPrimitive.String);
                Write(s);
            } 
            else if (value is int i)
            {
                Write((byte)OdinPrimitive.Integer);
                Write(i);
            }
            else if (value is ushort us)
            {
                Write((byte)OdinPrimitive.Short);
                Write(us);
            }           
            else if (value is bool b)
            {
                Write((byte)OdinPrimitive.Bool);
                Write(b);
            }                       
            else if (value is float f)
            {
                Write((byte)OdinPrimitive.Float);
                Write(f);
            }          
            else if (value is Vector2 v2)
            {
                Write((byte)OdinPrimitive.Vector2);
                Write(v2);
            }          
            else if (value is Vector3 v3)
            {
                Write((byte)OdinPrimitive.Vector3);
                Write(v3);
            }              
            else if (value is Vector4 v4)
            {
                Write((byte)OdinPrimitive.Vector4);
                Write(v4);
            }           
            else if (value is Quaternion q)
            {
                Write((byte)OdinPrimitive.Quaternion);
                Write(q);
            }                       
            else
            {
                Debug.LogWarning("Could not write object as the type is unknown");
            }
        }

        public byte[] ToBytes()
        {
            var finalBytes = new byte[Cursor];
            Buffer.BlockCopy(_bytes, 0, finalBytes, 0, Cursor);
            return finalBytes;
        }

        public bool IsEqual(OdinNetworkWriter writer)
        {
            if (writer == null)
            {
                return false;
            }
            
            if (Cursor != writer.Cursor)
            {
                return false;
            }

            for (int i = 0; i < Cursor; i++)
            {
                if (GetByteAt(i) != writer.GetByteAt(i))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
