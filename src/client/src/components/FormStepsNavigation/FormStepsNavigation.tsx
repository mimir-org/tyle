import { Button } from "@mimirorg/component-library";
import { StepWrapper } from "./FormStepsNavigation.styled";

interface FormStepsNavigationProps {
  steps: string[];
  activeStep: number;
  setActiveStep: (nextActiveStep: number) => void;
}

const FormStepsNavigation = ({ steps, activeStep, setActiveStep }: FormStepsNavigationProps) => {
  return (
    <div>
      {steps.map((step, index) => (
        <StepWrapper key={index}>
          <Button onClick={() => setActiveStep(index)} variant={activeStep === index ? "filled" : "text"}>
            {index + 1}. {step}
          </Button>
        </StepWrapper>
      ))}
    </div>
  );
};

export default FormStepsNavigation;
