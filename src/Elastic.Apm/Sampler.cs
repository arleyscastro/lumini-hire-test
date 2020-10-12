
using System;
using System.Text;

namespace Elastic.Apm
{
	internal readonly struct Sampler
	{
		private readonly long _higherBound;
		private readonly long _lowerBound;
		private readonly double _rate;

		internal Sampler(double rate)
		{
			if (!IsValidRate(rate)) throw new ArgumentOutOfRangeException($"Invalid rate: {rate} - it must be between 0 and 1 (including both)");

			_rate = rate;
			switch (_rate)
			{
				case 0:
					Constant = false;
					_higherBound = 0;
					_lowerBound = 0;
					break;
				case 1:
					Constant = true;
					_higherBound = long.MaxValue;
					_lowerBound = long.MinValue;
					break;
				default:
					_higherBound = (long) (long.MaxValue * rate);
					_lowerBound = -_higherBound;
					Constant = null;
					break;
			}
		}

		internal bool? Constant { get; }


		internal bool DecideIfToSample(byte[] randomBytes)
		{
			var longVal = BitConverter.ToInt64(randomBytes, 0);
			return Constant ?? longVal > _lowerBound && longVal < _higherBound;
		}

		internal static bool IsValidRate(double rate) => 0 <= rate && rate <= 1.0;

		public override string ToString()
		{
			var retVal = new StringBuilder();
			retVal.Append(nameof(Sampler));
			retVal.Append("{ ");
			if (Constant.HasValue)
				retVal.Append($"constant: {Constant}");
			else
				retVal.Append($"rate: {_rate}");
			retVal.Append(" }");
			return retVal.ToString();
		}
	}
}
