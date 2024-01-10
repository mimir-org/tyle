import Button from "components/Button";
import React from "react";
import { StepWrapper } from "./FormStepsNavigation.styled";

interface FormStepsNavigationProps {
  steps: string[];
  activeStep: number;
  setActiveStep: (nextStep: number) => void;
  formRef: React.RefObject<HTMLFormElement>;
}

const FormStepsNavigation = ({ steps, activeStep, setActiveStep, formRef }: FormStepsNavigationProps) => {
  const handleClick = (index: number) => {
    if (activeStep === index) return;

    if (formRef.current?.reportValidity()) {
      // Submits form on navigation except for the last step (which is the final submit)
      if (activeStep !== steps.length - 1) {
        formRef.current?.requestSubmit();
      }

      setActiveStep(index);
    }
  };

  return (
    <nav>
      {steps.map((step, index) => (
        <StepWrapper key={index}>
          <Button variant={activeStep === index ? "filled" : "text"} onClick={() => handleClick(index)}>
            {index + 1}. {step}
          </Button>
        </StepWrapper>
      ))}
    </nav>
  );
};

export default FormStepsNavigation;
