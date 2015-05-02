using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audio_recorder.Spectrum_Analyzer
{
	class WindowSignal
	{
		private const double Q = 0.5;

		public static double Rectangle(double n, double frameSize)
		{
			return 1;
		}

		public static double Gausse(double n, double frameSize)
		{
			var a = (frameSize - 1) / 2;
			var t = (n - a) / (Q * a);
			t = t * t;
			return Math.Exp(-t / 2);
		}

		public static double Hamming(double n, double frameSize)
		{
			return 0.54 - 0.46 * Math.Cos((2 * Math.PI * n) / (frameSize - 1));
		}

		public static double Hann(double n, double frameSize)
		{
			return 0.5 * (1 - Math.Cos((2 * Math.PI * n) / (frameSize - 1)));
		}

		public static double BlackmannHarris(double n, double frameSize)
		{
			return 0.35875 - (0.48829 * Math.Cos((2 * Math.PI * n) / (frameSize - 1))) +
				   (0.14128 * Math.Cos((4 * Math.PI * n) / (frameSize - 1))) - (0.01168 * Math.Cos((4 * Math.PI * n) / (frameSize - 1)));
		}
	}
}
