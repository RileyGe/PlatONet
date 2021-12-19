using System;
using Nethereum.Hex.HexTypes;
using Newtonsoft.Json;

namespace PlatONet.DTOs
{
	/// <summary>
	/// 程序版本
	/// </summary>
    public class ProgramVersion
	{
		/// <summary>
		/// 代码版本
		/// </summary>
		[JsonProperty("Version")]
		public HexBigInteger Version { get; set; }
		/// <summary>
		/// 代码版本签名
		/// </summary>
		[JsonProperty("Sign")]
		public string Sign { get; set; }
		public override int GetHashCode()
		{
			int prime = 31;
			int result = 1;
			result = prime * result + ((Sign == null) ? 0 : Sign.GetHashCode());
			result = prime * result + ((Version == null) ? 0 : Version.GetHashCode());
			return result;
		}
		public override bool Equals(Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (this.GetType() != obj.GetType())
				return false;
			ProgramVersion other = (ProgramVersion)obj;
			if (Sign == null)
			{
				if (other.Sign != null)
					return false;
			}
			else if (!Sign.Equals(other.Sign))
				return false;
			if (Version == null)
			{
				if (other.Version != null)
					return false;
			}
			else if (!Version.Equals(other.Version))
				return false;
			return true;
		}
		public override string ToString()
		{
			return "ProgramVersion [version=" + Version + ", sign=" + Sign + "]";
		}
	}
}
