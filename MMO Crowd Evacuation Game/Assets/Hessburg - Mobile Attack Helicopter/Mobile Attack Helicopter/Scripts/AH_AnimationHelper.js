#pragma strict
/* 
Description:

	Helper Script for the Attack Helicopter 1G Model to control the rotation of the Rotors (and optionally their acceleration), 
	plays a roughly synchronized helicopter sound and particle system.

Usage:

	– Mobile Model: Add AH_AnimationHelper.js to a Attack Helicopter 1G mobile model.
		Set the transforms of the Main and Tail Rotor to the script in the inspector. 
		The Mobile Model does not have any moveable parts (except of the Rotors) and no LODs.
	– Access the script instance from another script to control the rotors. 
		(C# Users: Place the AH_AnimationHelper.js in Standard Assets or Plugins folder if you want to access the script from C#)

Variables:

	AH_AnimationHelper.engineOn : boolean
	– turns helicopter engine on (true) and off (false)

	AH_AnimationHelper.targetRPM : float
	– range: 0.3 (low/idle RPM) to 1.0 (full 1460 RPM)

	AH_AnimationHelper.useAcceleration : boolean
	- a very basic but useful rotor acceleration and decelleration

	AH_AnimationHelper.currentRPM() : float
	range: 0.0 (zero RPM) to 1.0 (full 1460 RPM) - set to force a desired RPM when useAcceleration is true. Or read it if you need to know the current RPM (which can differ from targetRPM if useAcceleration is true)
	
Support:
	
	assets.support@hessburg.com

*/

// attach in Inspector:
var mainRotor : Transform; // set in the inspector
var tailRotor : Transform; // set in the inspector
var helicopterSound : AudioSource; // set in the inspector
var exhaustParticles : ParticleSystem; // set in the inspector

// Set/Change from another script:
// Rotor rotation:
public var targetRPM : float; // 0.0 (low RPM) to 1.0 (full 1460 RPM)
public var engineOn : boolean;
public var useAcceleration : boolean; // offers a very basic rotor acceleration
public var currentRPM : float; // differs from targetRPM due to acceleration

private var disableAudio = false;
private var disableParticles = false;
private var clampedRPM : float;
private var accelerationRate : float;
private var initialVolume : float;
//private var WindowAccelerationRate : float;
private var hasRotors : boolean;
private var dontExecute : boolean = false;


function Awake()
{
	accelerationRate=0.2; // acceleration speed of the rotors
	ControlSetup();
}	

function Update() 
{
	if(hasRotors) HelicopterRotor();	
}

function HelicopterRotor()
{
	if(engineOn || (currentRPM>0.0 && useAcceleration))
	{	
		clampedRPM=Mathf.Clamp(targetRPM*0.7+0.3, 0.3, 1.0); // clamps the targetRPM to avoid unrealistically low continiuos RPM

		if(useAcceleration)
		{	
			if(engineOn)
			{	
				if(currentRPM<=clampedRPM)
				{
					currentRPM = Mathf.Clamp(currentRPM + (accelerationRate * Time.deltaTime), 0.0, clampedRPM);
				}
				else
				{					
					currentRPM = Mathf.Clamp(currentRPM - (accelerationRate * Time.deltaTime), 0.0, 1.0);
				}	
			}
			else
			{
				currentRPM = Mathf.Clamp(currentRPM - (accelerationRate * Time.deltaTime), 0.0, 1.0);
			}	
		}
		else
		{
			currentRPM=clampedRPM;
		}		

		mainRotor.Rotate(Vector3(0.0, -1460.0 * currentRPM * Time.deltaTime, 0.0), Space.Self);
		
		tailRotor.Rotate(Vector3(1460.0 * currentRPM * Time.deltaTime, 0.0, 0.0), Space.Self);

		// rotors spinning - synchronize particles and audio to rotor speed
		if(disableAudio==false)
		{
			if(helicopterSound.isPlaying==false) helicopterSound.Play();
			helicopterSound.pitch = 0.1+currentRPM;			
			helicopterSound.volume=Mathf.Clamp(initialVolume * Mathf.Clamp(currentRPM*3.333, 0.0, 1.0), 0.0, initialVolume);
		}

		if(disableParticles==false)
		{
			if(exhaustParticles.isStopped) exhaustParticles.Play();
			exhaustParticles.startSpeed=4.0*currentRPM;
			exhaustParticles.maxParticles=100.0*currentRPM;
		}	
	}
	else
	{
		// rotors not spinning - turn off particles and audio
		if(disableParticles==false)
		{
			if(exhaustParticles.isPlaying) exhaustParticles.Stop();
		}	

		if(disableAudio==false) helicopterSound.Stop();
	}	
}


function ControlSetup()
{
	hasRotors=true;
	if(!mainRotor)
	{
		hasRotors=false;
		Debug.Log("AH-1: "+this.name+" – won't rotate rotors – manually attach the transform of the Main Rotor in the Inspector.");
	}

	if(!tailRotor) 
	{
		hasRotors=false;
		Debug.Log("AH-1: "+this.name+" –  won't rotate rotors – manually attach the transform of the Tail Rotor in the Inspector.");
	}

	if(!exhaustParticles) disableParticles=true;

	if(!helicopterSound)
	{
		disableAudio=true;
	}
	else
	{
		initialVolume=helicopterSound.volume;
	}	
}	