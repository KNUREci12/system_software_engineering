using UnityEngine;
using System.Collections;

public class moving : MonoBehaviour {


    public float shift = 0.1f;

    public float minimumX = -60F;
    public float maximumX = 60F;

    public float minimumY = 1F;
    public float maximumY = 100F;

    public float minimumZ = -25F;
    public float maximumZ = 25F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A))
        {
            var quant = this.transform.rotation;
            this.transform.position += quant * new Vector3(-shift, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            var quant = this.transform.rotation;
            this.transform.position += quant * new Vector3(0, 0, shift);
        }

        if (Input.GetKey(KeyCode.D))
        {
            var quant = this.transform.rotation;
            this.transform.position += quant * new Vector3(shift, 0, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            var quant = this.transform.rotation;
            this.transform.position += quant * new Vector3(0, 0, -shift);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            shift *= 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (shift > 0.0001)
                shift /= 2;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            var y = Input.GetAxis(@"Mouse ScrollWheel") * 5;

            var yAfter = y + this.transform.position.y;
            if (yAfter > minimumY && yAfter < maximumY)
                this.transform.position += new Vector3(0, y, 0);

        }

        correctionPosition();

	}

    private void correctionPosition()
    {
        var corPos = new Vector3();

        var x = this.transform.position.x;

        if( x < minimumX )
            corPos += new Vector3( minimumX - x, 0f, 0f );
        else if( x > maximumX )
            corPos += new Vector3( maximumX - x, 0f, 0f );

        var y = this.transform.position.y;

        if( y < minimumY )
            corPos += new Vector3( 0f, minimumY - y, 0f);
        else if( y > maximumY )
            corPos += new Vector3( 0f, maximumY - y, 0f);

        var z = this.transform.position.z;

        if( z < minimumZ )
            corPos += new Vector3( 0f, 0f, minimumZ - z );
        else if( z > maximumZ )
            corPos += new Vector3( 0f, 0f, maximumZ - z );

        this.transform.position += corPos;

    }

}
