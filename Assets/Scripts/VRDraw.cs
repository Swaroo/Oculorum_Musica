using UnityEngine;
using System.Collections.Generic;
using DilmerGames.Enums;
using DilmerGames.Core.Utilities;
using System;
namespace DilmerGames
{
    public class VRDraw : MonoBehaviour
    {   
        [SerializeField]
        private ControlHand controlHand = ControlHand.NoSet;

        [SerializeField]
        private GameObject objectToTrackMovement;

        private Vector3 prevPointDistance = Vector3.zero;

        [SerializeField, Range(0, 1.0f)]
        private float minDistanceBeforeNewPoint = 0.2f;

        [SerializeField, Range(0, 1.0f)]
        private float minDrawingPressure = 0.8f;

        [SerializeField, Range(0, 1.0f)]
        private float lineDefaultWidth = 0.010f;

        private int positionCount = 0; // 2 by default

        private List<LineRenderer> lines = new List<LineRenderer>();

        public LineRenderer currentLineRender;

        [SerializeField]
        private Color defaultColor = Color.white;

        [SerializeField]
        private GameObject editorObjectToTrackMovement;

        [SerializeField]
        private bool allowEditorControls = true;

        [SerializeField]
        private VRControllerOptions vrControllerOptions;
        
        public VRControllerOptions VRControllerOptions => vrControllerOptions;
        
        private AudioSource aSource;
        
        public float[] samples = new float[64];
        public bool firstEntry = true;

        private Color[] randomColors = new Color[]{Color.cyan, Color.blue, Color.white, Color.magenta, Color.yellow, Color.red, Color.green};

        void Awake() 
        {
            #if UNITY_EDITOR
            
            // if we allow editor controls use the editor object to track movement because oculus
            // blocks the movement of LeftControllerAnchor and RightControllerAnchor
            if(allowEditorControls)
            {
                objectToTrackMovement = editorObjectToTrackMovement != null ? editorObjectToTrackMovement : objectToTrackMovement;
            }

            #endif
            this.aSource = GameObject.FindGameObjectWithTag("DancingAudio").GetComponent<AudioSource>();

            AddNewLineRenderer();
        }

        void AddNewLineRenderer()
        {
            if(lines==null)
                Debug.Log("NoLines");
            positionCount = 0;
            GameObject go = new GameObject($"LineRenderer_{controlHand.ToString()}_{lines.Count}");
            go.transform.parent = objectToTrackMovement.transform.parent;
            go.transform.position = objectToTrackMovement.transform.position;
            LineRenderer goLineRenderer = go.AddComponent<LineRenderer>();
            goLineRenderer.startWidth = lineDefaultWidth;
            goLineRenderer.endWidth = lineDefaultWidth;
            goLineRenderer.useWorldSpace = true;

			//Select a new random color every time a new line rendered is created.
            System.Random rnd = new System.Random();
            int col  = rnd.Next(1,randomColors.Length + 1);

            defaultColor = randomColors[col-1];
            goLineRenderer.material = MaterialUtils.CreateMaterial(defaultColor, $"Material_{controlHand.ToString()}_{lines.Count}");
            print("Color is"+defaultColor);
            goLineRenderer.positionCount = 1;
            goLineRenderer.numCapVertices = 90;
            goLineRenderer.SetPosition(0, objectToTrackMovement.transform.position);

            // send position
            TCPControllerClient.Instance.AddNewLine(objectToTrackMovement.transform.position);

            currentLineRender = goLineRenderer;
            lines.Add(goLineRenderer);
        }

		//Function to delete all stored line Renderers.
        void DeleteAllLines(){
			
            while(lines.Count > 0){
        	   Destroy(lines[lines.Count-1]);
               lines.RemoveAt(lines.Count-1);
            }
            currentLineRender = null;
            AddNewLineRenderer();
        }

		//Function to update 'y' positions in each of the LineRenderers
        void LineDance(){
			
			//Store Previous Spectrum data samples so that they can be reused to adjust the 'y' position back to it's original position, before updating with the new Spectrum data.
            float[] prevsamples = new float[64];
            Array.Copy(this.samples, prevsamples, 64);
			
            aSource.GetSpectrumData(this.samples, 0, FFTWindow.BlackmanHarris);
            //For each sample  
            float prevpos;
            for(int l=0;l<lines.Count;l++){
                for (int i = 0; i < lines[l].positionCount; i++)
                {    
                    Vector3 position = lines[l].GetPosition(i);
                    
                    if(!firstEntry)
                        prevpos = position[1] - (Mathf.Clamp(prevsamples[(i+5)%64]*50, 0, 50))*0.1f;
                    else
                        prevpos = position[1];
                    lines[l].SetPosition(i,new Vector3(position[0], prevpos + (Mathf.Clamp(samples[(i+5)%64]*50, 0, 50))*0.1f ,position[2]));
                }
            }

            if(firstEntry)
                firstEntry = false;
        }

        void Update()
        {
            if(!vrControllerOptions.IsScreenHidden) return;

            LineDance();

    #if !UNITY_EDITOR
            // primary left controller
            //if(controlHand == ControlHand.Right && OVRInput.GetDown(OVRInput.RawButton.B))
            	//DeleteAllLines();
           // else if(controlHand == ControlHand.Left && OVRInput.GetDown(OVRInput.RawButton.Y))
               // DeleteAllLines();
            if(OVRInput.GetDown(OVRInput.RawButton.B) || OVRInput.GetDown(OVRInput.RawButton.Y))
                DeleteAllLines();
            if(controlHand == ControlHand.Left && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > minDrawingPressure)
            {
                //VRStats.Instance.firstText.text = $"Axis1D.PrimaryIndexTrigger: {OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Left && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
            {
                //VRStats.Instance.secondText.text = $"Button.PrimaryIndexTrigger: {Time.deltaTime}";
                AddNewLineRenderer();
            }

            // secondary right controller
            if(controlHand == ControlHand.Right && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > minDrawingPressure)
            {
                //VRStats.Instance.firstText.text = $"Axis1D.SecondaryIndexTrigger: {OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Right && OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            {
                //VRStats.Instance.secondText.text = $"Button.SecondaryIndexTrigger: {Time.deltaTime}";
                AddNewLineRenderer();
            }

    #endif

    #if UNITY_EDITOR
            if(!allowEditorControls) return;

            // left controller
            if(Input.GetKey(KeyCode.C))
            	DeleteAllLines();
            if(controlHand == ControlHand.Left && Input.GetKey(KeyCode.K))
            {
                //VRStats.Instance.firstText.text = $"Input.GetKey(KeyCode.K) {Input.GetKey(KeyCode.K)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Left && Input.GetKeyUp(KeyCode.K))
            {
                //VRStats.Instance.secondText.text = $"Input.GetKeyUp(KeyCode.K) {Input.GetKeyUp(KeyCode.K)}";
                AddNewLineRenderer();
            }

            // right controller
            if(controlHand == ControlHand.Right && Input.GetKey(KeyCode.L))
            {
                //VRStats.Instance.firstText.text = $"Input.GetKey(KeyCode.L): {Input.GetKey(KeyCode.L)}";
                UpdateLine();
            }
            else if(controlHand == ControlHand.Right && Input.GetKeyUp(KeyCode.L))
            {
                //VRStats.Instance.secondText.text = $"Input.GetKeyUp(KeyCode.L): {Input.GetKeyUp(KeyCode.L)}";
                AddNewLineRenderer();
            }
    #endif
        }

        void UpdateLine()
        {
            if(prevPointDistance == null)
            {
                prevPointDistance = objectToTrackMovement.transform.position;
            }

            if(prevPointDistance != null && Mathf.Abs(Vector3.Distance(prevPointDistance, objectToTrackMovement.transform.position)) >= minDistanceBeforeNewPoint)
            {
                Vector3 dir = (objectToTrackMovement.transform.position - Camera.main.transform.position).normalized;
                prevPointDistance = objectToTrackMovement.transform.position;
                AddPoint(prevPointDistance, dir);
            }
        }

        void AddPoint(Vector3 position, Vector3 direction)
        {
            if(currentLineRender==null)
                return;
            currentLineRender.SetPosition(positionCount, position);
            positionCount++;
            currentLineRender.positionCount = positionCount + 1;
            currentLineRender.SetPosition(positionCount, position);
            
            // send position
            TCPControllerClient.Instance.UpdateLine(position);
        }

        public void UpdateLineWidth(float newValue)
        {
            if(currentLineRender==null)
                return;
            currentLineRender.startWidth = newValue;
            currentLineRender.endWidth = newValue;
            lineDefaultWidth = newValue;
        }

        public void UpdateLineColor(Color color)
        {
            // in case we haven't drawn anything
            if(currentLineRender==null)
                return;
            if(currentLineRender.positionCount == 1)
            {
                currentLineRender.material.color = color;
                currentLineRender.material.EnableKeyword("_EMISSION");
                currentLineRender.material.SetColor("_EmissionColor", color);
            }
            defaultColor = color;
        }

        public void UpdateLineMinDistance(float newValue)
        {
            minDistanceBeforeNewPoint = newValue;
        }
    }
}