import { FormStep } from "components/AttributeForm/AttributeForm";
import FormStepsNavigation from "components/FormStepsNavigation";
import React from "react";
import styled from "styled-components/macro";

const Wrapper = styled.div`
  padding: ${(props) => props.theme.mimirorg.spacing.multiple(6)}
    min(${(props) => props.theme.mimirorg.spacing.multiple(11)}, 5vw);
`;

const Header = styled.h1`
  margin-bottom: ${(props) => props.theme.mimirorg.spacing.multiple(6)};
`;

const Body = styled.div`
  display: flex;
  gap: ${(props) => props.theme.mimirorg.spacing.multiple(18)};
`;

interface TypeFormContainerProps {
  title: string;
  steps: FormStep[];
  activeStep: number;
  setActiveStep: (nextStep: number) => void;
  formRef: React.RefObject<HTMLFormElement>;
  children: React.ReactNode;
}

const TypeFormContainer = ({ title, steps, activeStep, setActiveStep, formRef, children }: TypeFormContainerProps) => {
  return (
    <Wrapper>
      <Header>{title}</Header>
      <Body>
        <FormStepsNavigation steps={steps} activeStep={activeStep} setActiveStep={setActiveStep} formRef={formRef} />
        {children}
      </Body>
    </Wrapper>
  );
};

export default TypeFormContainer;
