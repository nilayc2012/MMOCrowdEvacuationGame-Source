#pragma strict

// AH-1G Simple Demo – this script is just for demonstration purposes.

// Required for this demo (set in the inspector):
var DemoCamera : Camera; // set in the inspector
var LookTarget : Transform; // set in the inspector

var DemoSphereVietnam : GameObject; // set in the inspector
var DemoSphereDesert : GameObject; // set in the inspector
var DemoSphereAlaska : GameObject; // set in the inspector
var DemoSphereNYC : GameObject; // set in the inspector

var HelicoptersParent : Transform; // set in the inspector

var AH_MOBILE_Army : GameObject; // set in the inspector
var AH_MOBILE_ArmyCamo1 : GameObject; // set in the inspector
var AH_MOBILE_ArmyCamo2 : GameObject; // set in the inspector
var AH_MOBILE_ArmyCamo3 : GameObject; // set in the inspector
var AH_MOBILE_Desert : GameObject; // set in the inspector
var AH_MOBILE_Arctic : GameObject; // set in the inspector
var AH_MOBILE_NASA : GameObject; // set in the inspector
var AH_MOBILE_MARINES : GameObject; // set in the inspector

var AH_MOBILE_SIMPLE_Army : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_ArmyCamo1 : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_ArmyCamo2 : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_ArmyCamo3 : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_Desert : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_Arctic : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_NASA : GameObject; // set in the inspector
var AH_MOBILE_SIMPLE_MARINES : GameObject; // set in the inspector

/////////////////////////////

private var LookAtParent : Transform;
private var Cam : Transform;
private var CamSubParent : Transform;
private var CamParent : Transform;
private var DemoSphere : GameObject;
private var HelicopterHelperScript : AH_AnimationHelper;
private var HelicopterHelperScripts : AH_AnimationHelper[];
private var AH1 : Transform;
private var Timetime : float;
private var PitchModulation : float;
private var hSliderValue : float;
private var CamTime : float;
private var CamDeltaTime : float;
private var toggle : boolean = true;
private var expand : boolean = false;

//private var Guns : boolean = false;
private var CameraRotation : boolean = true;
private var dontExecute : boolean = false;
private	var toolbarInt : int = 4;
private var toolbarStrings : String[] = ["Army", "Army Camo 1", "Army Camo 2", "Army Camo 3", "Desert", "Arctic", "NASA", "Marines"];
private var OldSelection : int =-1;
private	var toolbar2Int : int = 0;
//private var toolbar2Strings : String[] = ["Desktop", "Mobile Standard", "Mobile Bumped Specular"];
private var toolbar2Strings : String[] = ["Mobile Standard", "Mobile Bumped Specular"];
private var OldPlatform : int = -1;

function Start () 
{
	ControlSetup();
}

function Update () 
{
	if(dontExecute==false)
	{	
		if(OldSelection!=toolbarInt || OldPlatform!=toolbar2Int) ChangeDemoDomeAndHelicopter(toolbarInt, toolbar2Int);

		// example of how you could use AH_AnimationHelper.js:

		// engine and RPM
		HelicopterHelperScript.engineOn = toggle;
		HelicopterHelperScript.targetRPM = Mathf.Clamp(hSliderValue, 0.0, 1.0);
		

		// Demo Camera rotations & co
		if(CameraRotation)
		{
			CamTime=CamTime+Time.deltaTime;
			CamDeltaTime=Time.deltaTime;
		}
		else
		{
			CamDeltaTime=0.0;
		}	

		AH1.localEulerAngles=Vector3(Mathf.SmoothStep(0, 8.0, Mathf.PingPong(CamTime*1.78/8.0, 1.0))+2.5, 0.0, Mathf.SmoothStep(0, 30.0, Mathf.PingPong(CamTime*1.5/30.0, 1.0))-15.0);
		LookAtParent.localEulerAngles=Vector3(Mathf.SmoothStep(0, 30.0, Mathf.PingPong(CamTime*1.5/30.0, 1.0))-15.0,  0.0, Mathf.SmoothStep(0, 8.0, Mathf.PingPong(CamTime*1.78/8.0, 1.0))+2.5);

		if(OldSelection!=6 && OldSelection!=7) 
		{
			DemoCamera.fieldOfView = Mathf.SmoothStep(0, 28.0, Mathf.PingPong(CamTime*0.3/28.0, 1.0))+15.0;
			LookTarget.localPosition=Vector3(0.0, -0.4, Mathf.SmoothStep(0, 6.0, Mathf.PingPong(CamTime*0.4/6.0, 1.0))-3.0);			
			CamParent.Rotate(Vector3(0.0, 15.0 * CamDeltaTime, 0.0), Space.Self);
			CamParent.localPosition = Vector3(0.0, Mathf.SmoothStep(0, 10.0, Mathf.PingPong(CamTime*0.33/10.0, 1.0))-2.5, 0.0);			
		}
		else
		{
			DemoCamera.fieldOfView = Mathf.SmoothStep(0, 28.0, Mathf.PingPong(CamTime*0.3/28.0, 1.0))+20.0;
			LookTarget.localPosition=Vector3(0.0, -0.4, Mathf.SmoothStep(0, 6.0, Mathf.PingPong(CamTime*0.4/6.0, 1.0))-3.0);		
			CamParent.Rotate(Vector3(0.0, 15.0 * CamDeltaTime, 0.0), Space.Self);
			CamParent.localPosition = Vector3(0.0, Mathf.PingPong(CamTime*0.33, 5.0) -1.5, 0.0);
			CamParent.localPosition = Vector3(0.0, Mathf.SmoothStep(0, 5.0, Mathf.PingPong(CamTime*0.33/5.0, 1.0))-1.5, 0.0);			
		}	

		HelicoptersParent.Rotate(Vector3(0.0, CamDeltaTime*-10.0, 0.0));
		CamSubParent.localPosition = Vector3(0.0, 0.0, Mathf.SmoothStep(0, 13.0, Mathf.PingPong(CamTime*0.75/14.0, 1.0))+10.0);	
		CamSubParent.LookAt(LookTarget); 		
		Cam.localEulerAngles=Vector3(0.0,  0.0, Mathf.SmoothStep(0, 30.0, Mathf.PingPong(CamTime/30.0, 1.0))-15.0);

		if(OldSelection!=6) DemoSphere.transform.Rotate(Vector3(0.0, -25.0 * CamDeltaTime, 0.0), Space.Self);
	}
}

function ChangeDemoDomeAndHelicopter(Nr : int, PlatformNr : int)
{			
	DemoSphere.SetActive(false);
	if(Nr<4) DemoSphere=DemoSphereVietnam;
	if(Nr==4) DemoSphere=DemoSphereDesert;
	if(Nr==5) DemoSphere=DemoSphereAlaska;
	if(Nr==6) DemoSphere=DemoSphereNYC;
	if(Nr==7) DemoSphere=DemoSphereNYC;
	DemoSphere.SetActive(true);

	HelicopterHelperScripts[Nr+PlatformNr*8.0].engineOn=HelicopterHelperScript.engineOn;
	HelicopterHelperScripts[Nr+PlatformNr*8.0].targetRPM=HelicopterHelperScript.targetRPM;
	HelicopterHelperScripts[Nr+PlatformNr*8.0].currentRPM=HelicopterHelperScript.currentRPM;
	
	
	this.HelicopterHelperScript.gameObject.SetActive(false);
	HelicopterHelperScript = HelicopterHelperScripts[Nr+PlatformNr*8.0];
	this.HelicopterHelperScript.gameObject.SetActive(true);
	AH1 = this.HelicopterHelperScript.GetComponent.<Transform>();	

	OldSelection=Nr;
	OldPlatform=PlatformNr;
}

function OnGUI () 
{
	if(dontExecute==false)
	{	
		expand = GUI.Toggle(Rect(25, 10, 100, 15), expand, " Expand GUI");
		if(expand)
		{	
			toolbar2Int = GUI.Toolbar (Rect (25, 35, 800, 20), toolbar2Int, toolbar2Strings);
			toolbarInt = GUI.Toolbar (Rect (25, 65, 800, 20), toolbarInt, toolbarStrings);
			CameraRotation = GUI.Toggle(Rect(25, 100, 170, 25), CameraRotation, " Camera Animation");
			toggle = GUI.Toggle(Rect(25, 130, 60, 20), toggle, " Engine");
			if(toggle)
			{
				hSliderValue = GUI.HorizontalSlider (Rect (100, 135, 100, 30), hSliderValue, 0.0, 1.0);
			}	
			
		}
	}	
}

function ControlSetup()
{
	//if(AH_Army && AH_ArmyCamo1 && AH_ArmyCamo2 && AH_ArmyCamo3 && AH_Desert && AH_Arctic && AH_NASA && AH_MARINES && AH_MOBILE_Army && AH_MOBILE_ArmyCamo1 && AH_MOBILE_ArmyCamo2 && AH_MOBILE_ArmyCamo3 && AH_MOBILE_Desert && AH_MOBILE_Arctic && AH_MOBILE_NASA && AH_MOBILE_MARINES && AH_MOBILE_SIMPLE_Army && AH_MOBILE_SIMPLE_ArmyCamo1 && AH_MOBILE_SIMPLE_ArmyCamo2 && AH_MOBILE_SIMPLE_ArmyCamo3 && AH_MOBILE_SIMPLE_Desert && AH_MOBILE_SIMPLE_Arctic && AH_MOBILE_SIMPLE_NASA && AH_MOBILE_SIMPLE_MARINES && DemoCamera && LookTarget && DemoSphereVietnam && DemoSphereAlaska && DemoSphereDesert && DemoSphereNYC)
	//{
	if(AH_MOBILE_Army && AH_MOBILE_ArmyCamo1 && AH_MOBILE_ArmyCamo2 && AH_MOBILE_ArmyCamo3 && AH_MOBILE_Desert && AH_MOBILE_Arctic && AH_MOBILE_NASA && AH_MOBILE_MARINES && AH_MOBILE_SIMPLE_Army && AH_MOBILE_SIMPLE_ArmyCamo1 && AH_MOBILE_SIMPLE_ArmyCamo2 && AH_MOBILE_SIMPLE_ArmyCamo3 && AH_MOBILE_SIMPLE_Desert && AH_MOBILE_SIMPLE_Arctic && AH_MOBILE_SIMPLE_NASA && AH_MOBILE_SIMPLE_MARINES && DemoCamera && LookTarget && DemoSphereVietnam && DemoSphereAlaska && DemoSphereDesert && DemoSphereNYC)
	{	
		hSliderValue=0.845;
		DemoSphere=DemoSphereVietnam;
		

		HelicopterHelperScripts = new AH_AnimationHelper[16];
		HelicopterHelperScripts[0] = AH_MOBILE_Army.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[1] = AH_MOBILE_ArmyCamo1.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[2] = AH_MOBILE_ArmyCamo2.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[3] = AH_MOBILE_ArmyCamo3.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[4] = AH_MOBILE_Desert.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[5] = AH_MOBILE_Arctic.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[6] = AH_MOBILE_NASA.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[7] = AH_MOBILE_MARINES.GetComponent(AH_AnimationHelper);

		HelicopterHelperScripts[8] = AH_MOBILE_SIMPLE_Army.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[9] = AH_MOBILE_SIMPLE_ArmyCamo1.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[10] = AH_MOBILE_SIMPLE_ArmyCamo2.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[11] = AH_MOBILE_SIMPLE_ArmyCamo3.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[12] = AH_MOBILE_SIMPLE_Desert.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[13] = AH_MOBILE_SIMPLE_Arctic.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[14] = AH_MOBILE_SIMPLE_NASA.GetComponent(AH_AnimationHelper);
		HelicopterHelperScripts[15] = AH_MOBILE_SIMPLE_MARINES.GetComponent(AH_AnimationHelper);

		for(var i : int=0;i<16;i++)
		{
			if(HelicopterHelperScripts[i]==null)
			{
				dontExecute=true;
			}
			else
			{
				HelicopterHelperScripts[i].useAcceleration=true;
			}	
		}	

		if(dontExecute==true)
		{
			Debug.Log("Can't execute Attack Helicopter demo - please attach and setup the script AH_AnimationHelper.js to each of the Demo Helicopters");
		}
		else
		{	
			HelicopterHelperScript = HelicopterHelperScripts[0];
			AH1 = this.HelicopterHelperScript.GetComponent.<Transform>();
			HelicopterHelperScript.useAcceleration=true;
			HelicopterHelperScript.engineOn=true;

			Cam = DemoCamera.transform;
			CamSubParent = Cam.parent;
			CamParent = CamSubParent.parent;
			LookAtParent = LookTarget.parent;
			CamTime=64.0;

			hSliderValue=0.845;
			DemoSphere=DemoSphereVietnam;
		}	
	}
	else
	{	
		Debug.Log("Can't execute Attack Helicopter demo - please manually set all required references in the inspector");
		dontExecute=true;
	}
}	