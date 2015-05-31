using UnityEngine;
using System.Collections;

using System.Windows.Forms;

using System.IO;

public class main : MonoBehaviour {

	// Use this for initialization
	void Start () {

        try
        {
            var curveList = AudioRecorderUnity.Restore.RestoreCurveList(@"3d.multifft");

            for( int i = 0; i < curveList.Count; ++i )
                createGraph( curveList[ i ], i );
        }
        catch( System.Exception _exception )
        {
            Debug.Log( _exception.Message );
        }


	}
	
	// Update is called once per frame
	void Update () {

	}

    void createGraph(
            ZedGraph.CurveItem _curve
        ,   System.Single _shiftZ
    )
    {
        int z = 1;
        int counter = 1;

        for (int i = 0; i < _curve.Points.Count - 1; i += counter)
        {
            var baseAmpl = _curve[i].Y;

            counter = 1;

            for (int j = i; j < _curve.Points.Count - 1; ++j)
                if( baseAmpl == _curve[ j ].Y)
                    ++counter;


            var ampl = System.Convert.ToSingle(baseAmpl * System.Math.Pow(10, 9));
            var rect = GameObject.CreatePrimitive(PrimitiveType.Cube);

            rect.transform.localScale = new Vector3(0.002f * counter, 0.001f * ampl, 0.3f);
            rect.transform.position = new Vector3( -30f + i * 0.002f, 1 + 0.001f * ampl / 2, _shiftZ * 0.3f);

            ++z;

            var color = _curve.Color;
            rect.GetComponent<Renderer>().material.color = new Color( color.R, color.G, color.B, color.A );
        }

        Debug.Log( z.ToString() );

        //for (int i = 0; i < 22100; )
        //{
        //    var baseAmpl = AudioRecorderUnity.AmplitudeGetter.getAmplitude( fft, i );
        //
        //    int counter = 1;
        //    while( baseAmpl == AudioRecorderUnity.AmplitudeGetter.getAmplitude(fft, ++i) )
        //        ++counter;
        //
        //    var ampl = System.Convert.ToSingle(baseAmpl * System.Math.Pow(10, 8) * 5);
        //    var rect = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //    rect.transform.localScale = new Vector3(0.002f * counter, 0.001f * ampl, 0.3f);
        //    rect.transform.position = new Vector3(i * 0.002f, 1 + 0.001f * ampl / 2, 0);
        //
        //    rect.GetComponent<Renderer>().material.color = Color.yellow;
        //}
    }

}
