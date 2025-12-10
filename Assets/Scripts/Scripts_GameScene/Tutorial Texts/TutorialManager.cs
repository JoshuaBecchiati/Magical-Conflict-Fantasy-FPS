using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Riferimenti")]
    public TMP_Text tutorialText;

    private int currentStep = 0;

    // Elenco degli step del tutorial
    private string[] tutorialSteps = new string[]
    {
        "Premi W per muoverti avanti",
        "Premi S per muoverti indietro",
        "Premi A oppure D per muoverti lateralmente",
        "Premi SPACE per bloccare gli attacchi",
        "Premi E per attaccare"
    };

    private bool stepCompleted = false;

    public static class TutorialState
    {
        public static bool tutorialCompleted = false;
    }


    void Start()
    {
        ShowCurrentStep();
    }

    void Update()
    {
        if (stepCompleted) return;

        switch (currentStep)
        {
            case 0: // Premi W
                if (Keyboard.current.wKey.wasPressedThisFrame)
                    CompleteStep();
                break;

            case 1: // Premi S
                if (Keyboard.current.sKey.wasPressedThisFrame)
                    CompleteStep();
                break;

            case 2: // Premi A o D
                if (Keyboard.current.aKey.wasPressedThisFrame ||
                    Keyboard.current.dKey.wasPressedThisFrame)
                    CompleteStep();
                break;

            case 3: // Premi SPACE
                if (Keyboard.current.spaceKey.wasPressedThisFrame)
                    CompleteStep();
                break;

            case 4: // Premi E
                if (Keyboard.current.eKey.wasPressedThisFrame)
                    CompleteStep();
                break;
        }
    }

    void ShowCurrentStep()
    {
        if (tutorialText != null && currentStep < tutorialSteps.Length)
        {
            tutorialText.text = "Tutorial: " + tutorialSteps[currentStep];
        }
    }

    void CompleteStep()
    {
        stepCompleted = true;
        currentStep++;

        if (currentStep < tutorialSteps.Length)
        {
            stepCompleted = false;
            ShowCurrentStep();
        }
        else
        {
             tutorialText.text = "Tutorial completato!";
             TutorialState.tutorialCompleted = true; // ⬅️ AGGIUNTA
        }

        }
    }

