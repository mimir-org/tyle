import FormStepsNavigation from "components/FormStepsNavigation";
import React from "react";
import { TypeFormContainerBody, TypeFormContainerHeader, TypeFormContainerWrapper } from "./TypeFormContainer.styled";

interface TypeFormContainerProps {
  title: string;
  steps: string[];
  activeStep: number;
  setActiveStep: (nextStep: number) => void;
  formRef: React.RefObject<HTMLFormElement>;
  children: React.ReactNode;
}

const TypeFormContainer = ({ title, steps, activeStep, setActiveStep, formRef, children }: TypeFormContainerProps) => {
  return (
    <TypeFormContainerWrapper>
      <TypeFormContainerHeader>{title}</TypeFormContainerHeader>
      <TypeFormContainerBody>
        <FormStepsNavigation steps={steps} activeStep={activeStep} setActiveStep={setActiveStep} formRef={formRef} />
        {children}
      </TypeFormContainerBody>
    </TypeFormContainerWrapper>
  );
};

export default TypeFormContainer;
