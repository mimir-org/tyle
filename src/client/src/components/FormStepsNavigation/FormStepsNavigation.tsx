import { Button } from "@mimirorg/component-library";
import { FormStep } from "components/AttributeForm/AttributeForm";
import React from "react";
import { StepWrapper } from "./FormStepsNavigation.styled";

interface FormStepsNavigationProps {
  steps: FormStep[];
  activeStep: number;
  setActiveStep: (nextStep: number) => void;
  formRef: React.RefObject<HTMLFormElement>;
}

const FormStepsNavigation = ({ steps, activeStep, setActiveStep, formRef }: FormStepsNavigationProps) => {
  const handleClick = (index: number) => {
    formRef.current?.requestSubmit();

    setActiveStep(index);
  };

  return (
    <div>
      {steps.map((step, index) => (
        <StepWrapper key={step.id}>
          <Button variant={activeStep === index ? "filled" : "text"} onClick={() => handleClick(index)}>
            {index + 1}. {step.description}
          </Button>
        </StepWrapper>
      ))}
    </div>
  );
};

export default FormStepsNavigation;
