using System;
using System.Text;
using UnityEngine;

namespace Odin.OdinNetworking
{
    public class OdinNetworkReader
    {
        private byte[] _bytes;
        private int _cursor = 0;
        
        public OdinNetworkReader(byte[] bytes)
        {
            _bytes = bytes;
            _cursor = 0;
        }

        public byte ReadByte()
        {
            if (_cursor >= _bytes.Length)
            {
                return 0;
            }
            
            byte aByte = _bytes[_cursor];
            _cursor++;
            return aByte;
        }

        public ushort ReadUShort()
        {
            if (_cursor + sizeof(ushort) >= _bytes.Length)
            {
                return 0;
            }
            
            ushort value = BitConverter.ToUInt16(_bytes, _cursor);
            _cursor += sizeof(ushort);
            return value;
        }

        public OdinPrimitive ReadPrimitiveType()
        {
            return (OdinPrimitive)ReadByte();
        }
        
        public OdinMessageType ReadMessageType()
        {
            return (OdinMessageType)ReadByte();
        }

        public void ReadTransform(Transform transform)
        {
            transform.localPosition = ReadVector3();
            transform.localRotation = ReadQuaternion();
            transform.localScale = ReadVector3();
        }

        public int ReadInt()
        {
            if (_cursor + sizeof(int) >= _bytes.Length)
            {
                return 0;
            }
            
            int value = BitConverter.ToInt32(_bytes, _cursor);
            _cursor += sizeof(int);
            return value;
        }
        
        public bool ReadBoolean()
        {
            if (_cursor + sizeof(bool) >= _bytes.Length)
            {
                return false;
            }
            
            bool value = BitConverter.ToBoolean(_bytes, _cursor);
            _cursor += sizeof(bool);
            return value;
        }
        
        public float ReadFloat()
        {
            if (_cursor + sizeof(float) >= _bytes.Length)
            {
                return 0;
            }
            
            float value = BitConverter.ToSingle(_bytes, _cursor);
            _cursor += sizeof(float);
            return value;
        }
        
        public double ReadDouble()
        {
            if (_cursor + sizeof(double) >= _bytes.Length)
            {
                return 0;
            }
            
            double value = BitConverter.ToDouble(_bytes, _cursor);
            _cursor += sizeof(double);
            return value;
        }

        public Vector2 ReadVector2()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            return new Vector2(x, y);
        }
        
        public Vector3 ReadVector3()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();
            return new Vector3(x, y, z);
        }
        
        public Vector4 ReadVector4()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();
            float w = ReadFloat();
            return new Vector4(x, y, z, w);
        }
        
        public Quaternion ReadQuaternion()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();
            float w = ReadFloat();
            return new Quaternion(x, y, z, w);
        }

        public string ReadString()
        {
            ushort length = ReadUShort();
            string aString = Encoding.UTF8.GetString(_bytes, _cursor, length);
            _cursor += length;
            return aString;
        }

        public object ReadObject()
        {
            OdinPrimitive primitive = (OdinPrimitive)ReadByte();
            if (primitive == OdinPrimitive.String)
            {
                return ReadString();
            }
            else if (primitive == OdinPrimitive.Bool)
            {
                return ReadBoolean();
            }                        
            else if (primitive == OdinPrimitive.Integer)
            {
                return ReadInt();
            }
            else if (primitive == OdinPrimitive.Short)
            {
                return ReadUShort();
            }
            else if (primitive == OdinPrimitive.Float)
            {
                return ReadFloat();
            }
            else if (primitive == OdinPrimitive.Vector2)
            {
                return ReadVector2();
            }
            else if (primitive == OdinPrimitive.Vector3)
            {
                return ReadVector3();
            }
            else if (primitive == OdinPrimitive.Vector4)
            {
                return ReadVector4();
            }
            else if (primitive == OdinPrimitive.Quaternion)
            {
                return ReadQuaternion();
            }
            
            return null;
        }
    }
}