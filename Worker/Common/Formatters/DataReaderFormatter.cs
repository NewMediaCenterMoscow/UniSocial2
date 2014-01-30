using Collector.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Common.Formatters
{
	public class DataReaderFormatter : ObjectFormatter
	{
		public class UniSocialObjectsDataReader : IDataReader
		{
			protected List<object> items = new List<object>();
			protected int currentIndex = -1;

			protected int fieldCount;
			protected Func<object, int, object> getValue;

			public UniSocialObjectsDataReader(int FieldCount, Func<object, int, object> GetValue)
			{
				fieldCount = FieldCount;
				getValue = GetValue;
			}

			public virtual void AddItem(object NewItem)
			{
				items.Add(NewItem);
			}

			#region IDataReader members

			public bool Read()
			{
				currentIndex++;

				if (currentIndex == items.Count)
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			public int FieldCount
			{
				get { return fieldCount; }
			}

			public object GetValue(int i)
			{
				var obj = items[currentIndex];
				var value = getValue(obj,i);

				return value;
			}

			public void Dispose()
			{
				
			}

			#endregion

			#region IDataReader unnecessary members

			public void Close()
			{
				throw new NotImplementedException();
			}

			public int Depth
			{
				get { throw new NotImplementedException(); }
			}

			public DataTable GetSchemaTable()
			{
				throw new NotImplementedException();
			}

			public bool IsClosed
			{
				get { throw new NotImplementedException(); }
			}

			public bool NextResult()
			{
				throw new NotImplementedException();
			}

			public int RecordsAffected
			{
				get { throw new NotImplementedException(); }
			}

			public bool GetBoolean(int i)
			{
				throw new NotImplementedException();
			}

			public byte GetByte(int i)
			{
				throw new NotImplementedException();
			}

			public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
			{
				throw new NotImplementedException();
			}

			public char GetChar(int i)
			{
				throw new NotImplementedException();
			}

			public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
			{
				throw new NotImplementedException();
			}

			public IDataReader GetData(int i)
			{
				throw new NotImplementedException();
			}

			public string GetDataTypeName(int i)
			{
				throw new NotImplementedException();
			}

			public DateTime GetDateTime(int i)
			{
				throw new NotImplementedException();
			}

			public decimal GetDecimal(int i)
			{
				throw new NotImplementedException();
			}

			public double GetDouble(int i)
			{
				throw new NotImplementedException();
			}

			public Type GetFieldType(int i)
			{
				throw new NotImplementedException();
			}

			public float GetFloat(int i)
			{
				throw new NotImplementedException();
			}

			public Guid GetGuid(int i)
			{
				throw new NotImplementedException();
			}

			public short GetInt16(int i)
			{
				throw new NotImplementedException();
			}

			public int GetInt32(int i)
			{
				throw new NotImplementedException();
			}

			public long GetInt64(int i)
			{
				throw new NotImplementedException();
			}

			public string GetName(int i)
			{
				throw new NotImplementedException();
			}

			public int GetOrdinal(string name)
			{
				throw new NotImplementedException();
			}

			public string GetString(int i)
			{
				throw new NotImplementedException();
			}

			public int GetValues(object[] values)
			{
				throw new NotImplementedException();
			}

			public bool IsDBNull(int i)
			{
				throw new NotImplementedException();
			}

			public object this[string name]
			{
				get { throw new NotImplementedException(); }
			}

			public object this[int i]
			{
				get { throw new NotImplementedException(); }
			}
			#endregion

		}

		public class UniSocialVkFriendDataReader : UniSocialObjectsDataReader
		{
			public UniSocialVkFriendDataReader(int FieldCount, Func<object, int, object> GetValue)
				: base(FieldCount, GetValue)
			{
				
			}

			public override void AddItem(object NewItem)
			{
				var uf = NewItem as VkFriends;
				foreach (var f in uf.Friends)
					base.AddItem(new Tuple<long, long>(uf.UserId, f));
			}
		}

		UniSocialObjectsDataReader currentDataReader;

		public DataReaderFormatter()
		{
		}

		private UniSocialObjectsDataReader createReader(Type t)
		{
			if (t == typeof(VkPost))
				return new UniSocialObjectsDataReader(13, (o,i) => {
					var p = o as VkPost;
					var copyHistory = p.CopyHistory == null ? null : p.CopyHistory.FirstOrDefault();

					if (i == 0) return p.Id;
					if (i == 1) return p.FromId;
					if (i == 2) return p.ToId;
					if (i == 3) return p.Date;
					if (i == 4) return p.PostType;
					if (i == 5) return p.Text;
					if (i == 6) return p.Comments.Count;
					if (i == 7) return p.Likes.Count;
					if (i == 8) return p.Reposts.Count;
					if (i == 9) return copyHistory == null ? 0 : copyHistory.Id;
					if (i == 10) return copyHistory == null ? 0 : copyHistory.FromId;
					if (i == 11) return copyHistory == null ? 0 : copyHistory.ToId;
					if (i == 12) return copyHistory == null ? "" : copyHistory.Text;
					return "";
				});
			if (t == typeof(VkFriends))
				return new UniSocialVkFriendDataReader(2, (o, i) =>
				{
					var p = o as Tuple<long, long>;

					if (i == 0) return p.Item1;
					if (i == 1) return p.Item2;
					return "";
				});
			if (t == typeof(VkComment))
				return new UniSocialObjectsDataReader(5, (o, i) =>
				{
					var p = o as VkComment;

					if (i == 0) return p.Id;
					if (i == 1) return p.FromId;
					if (i == 2) return p.Date;
					if (i == 3) return p.Text;
					if (i == 4) return p.Likes.Count;
					return "";
				});

			return null;
		}

		protected override void SetObjectType(Type t)
		{
			currentDataReader = createReader(t);
		}

		protected override void HandleObject(object Obj)
		{
			currentDataReader.AddItem(Obj);
		}

		protected override object GetResult()
		{
			return currentDataReader;
		}

		public override void Dispose()
		{
			currentDataReader.Dispose();
		}
	}
}
