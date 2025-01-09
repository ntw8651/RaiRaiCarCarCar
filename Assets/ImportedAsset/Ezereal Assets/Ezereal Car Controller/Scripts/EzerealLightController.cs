using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace Ezereal
{
    public class EzerealLightController : MonoBehaviour // This system uses Input System and has no references. Some methods here are called from other scripts.
    {
        [Header("Beam Lights")]

        [SerializeField] LightBeam currentBeam = LightBeam.off;

        [SerializeField] GameObject[] lowBeamHeadlights;
        [SerializeField] GameObject[] highBeamHeadlights;
        [SerializeField] GameObject[] lowBeamSpotlights;
        [SerializeField] GameObject[] highBeamSpotlights;
        [SerializeField] GameObject[] rearLights;

        [Header("Brake Lights")]
        [SerializeField] GameObject[] brakeLights;

        [Header("Handbrake Light")]
        [SerializeField] GameObject[] handbrakeLight;

        [Header("Reverse Lights")]
        [SerializeField] GameObject[] reverseLights;

        [Header("Turn Lights")]
        [SerializeField] GameObject[] leftTurnLights;
        [SerializeField] GameObject[] rightTurnLights;

        [Header("Misc Lights")]
        [Tooltip("Any additional lights. Interior lights.")]
        [SerializeField] GameObject[] miscLights;

        [Header("Settings")]
        [SerializeField] float lightBlinkDelay = 0.5f;

        [Header("Debug")]
        [SerializeField] bool leftTurnActive = false;
        [SerializeField] bool rightTurnActive = false;
        [SerializeField] bool hazardLightsActive = false;

        private void Start()
        {
            return;
            AllLightsOff();
        }

        public void AllLightsOff()
        {
            return;
            AllBeamsOff();
            ReverseLightsOff();
            TurnLightsOff();
            BrakeLightsOff();
            HandbrakeLightOff();
            //MiscLightsOff();
        }

        void OnLowBeamLight()
        {
            return;
            switch (currentBeam)
            {
                case LightBeam.off:
                    LowBeamOn();
                    break;
                case LightBeam.low:
                    AllBeamsOff();
                    break;
                case LightBeam.high:
                    AllBeamsOff();
                    break;
            }
        }

        void OnHighBeamLight()
        {
            return;
            switch (currentBeam)
            {
                case LightBeam.off:
                    HighBeamOn();
                    break;
                case LightBeam.low:
                    HighBeamOn();
                    break;
                case LightBeam.high:
                    AllBeamsOff();
                    break;
            }
        }
        void OnLeftTurnSignal()
        {
            return;
            if (!hazardLightsActive)
            {
                StopAllCoroutines();
                TurnLightsOff();
                rightTurnActive = false;
                leftTurnActive = !leftTurnActive;

                if (leftTurnActive)
                {
                    StartCoroutine(TurnSignalController(leftTurnLights, leftTurnActive));
                }
            }
        }

        void OnRightTurnSignal()
        {
            return;
            if (!hazardLightsActive)
            {
                StopAllCoroutines();
                TurnLightsOff();
                leftTurnActive = false;
                rightTurnActive = !rightTurnActive;

                if (rightTurnActive)
                {
                    StartCoroutine(TurnSignalController(rightTurnLights, rightTurnActive));
                }
            }
        }

        void OnHazardLights()
        {
            return;
            StopAllCoroutines();
            TurnLightsOff();
            leftTurnActive = false;
            rightTurnActive = false;
            hazardLightsActive = !hazardLightsActive;

            if (hazardLightsActive)
            {
                StartCoroutine(HazardLightsController());
            }
        }

        IEnumerator TurnSignalController(GameObject[] turnLights, bool isActive)
        {
            while (isActive)
            {
                SetLight(turnLights, true);
                yield return new WaitForSeconds(lightBlinkDelay);
                SetLight(turnLights, false);
                yield return new WaitForSeconds(lightBlinkDelay);
            }
        }

        IEnumerator HazardLightsController()
        {
            
            while (hazardLightsActive)
            {
                TurnLightsOn();
                yield return new WaitForSeconds(lightBlinkDelay);
                TurnLightsOff();
                yield return new WaitForSeconds(lightBlinkDelay);
            }
        }
        void SetLight(GameObject[] lights, bool isActive)
        {
            return;
            if (isActive)
            {
                foreach (var light in lights)
                {
                    light.SetActive(true);
                }
            }
            else
            {
                foreach (var light in lights)
                {
                    light.SetActive(false);
                }
            }
        }

        void AllBeamsOff()
        {
            return;
            SetLight(lowBeamHeadlights, false);
            SetLight(lowBeamSpotlights, false);
            SetLight(rearLights, false);

            SetLight(highBeamHeadlights, false);
            SetLight(highBeamSpotlights, false);

            currentBeam = LightBeam.off;
        }

        void LowBeamOn()
        {
            return;
            SetLight(lowBeamHeadlights, true);
            SetLight(lowBeamSpotlights, true);
            SetLight(rearLights, true);

            SetLight(highBeamHeadlights, false);
            SetLight(highBeamSpotlights, false);

            currentBeam = LightBeam.low;
        }

        void HighBeamOn()
        {
            return;
            SetLight(lowBeamHeadlights, true);
            SetLight(lowBeamSpotlights, false);
            SetLight(rearLights, true);

            SetLight(highBeamHeadlights, true);
            SetLight(highBeamSpotlights, true);

            currentBeam = LightBeam.high;
        }

        void TurnLightsOff()
        {
            return;
            SetLight(leftTurnLights, false);
            SetLight(rightTurnLights, false);
        }

        void TurnLightsOn()
        {
            return;
            SetLight(leftTurnLights, true);
            SetLight(rightTurnLights, true);
        }

        void SetHazardLightsOn()
        {
            return;
            SetLight(leftTurnLights, true);
            SetLight(rightTurnLights, true);
        }

        public void BrakeLightsOff()
        {
            return;
            SetLight(brakeLights, false);
        }

        public void BrakeLightsOn()
        {
            return;
            SetLight(brakeLights, true);
        }

        public void HandbrakeLightOff()
        {
            return;
            SetLight(handbrakeLight, false);
        }

        public void HandbrakeLightOn()
        {
            return;
            SetLight(handbrakeLight, true);
        }

        public void ReverseLightsOff()
        {
            return;
            SetLight(reverseLights, false);
        }

        public void ReverseLightsOn()
        {
            return;
            SetLight(reverseLights, true);
        }

        public void MiscLightsOff() // Interior Lights
        {
            return;
            SetLight(miscLights, false);
        }

        public void MiscLightsOn() // Interior Lights
        {
            return;
            //SetLight(miscLights, true);
        }
    }
}
