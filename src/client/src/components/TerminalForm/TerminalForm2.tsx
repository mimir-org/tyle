import { FormMode } from "../../types/formMode";
import {
  createEmptyTerminalFormFields,
  TerminalFormFields,
  toTerminalFormFields,
  useTerminalQuery
} from "./TerminalForm.helpers";
import React from "react";
import { useParams } from "react-router-dom";
import { TerminalView } from "../../types/terminals/terminalView";
import { usePrefilledForm } from "../../helpers/form.helpers";
import Loader from "../Loader";
import TypeFormContainer from "../TypeFormContainer";
import TerminalBaseForm2 from "./TerminalBaseForm2";
import MediumAndQualifierForm from "./MediumAndQualifierForm";

interface TerminalForm2Props {
  mode?: FormMode;
}
export interface TerminalFormStepProps {
  fields: TerminalFormFields;
  setFields: React.Dispatch<React.SetStateAction<TerminalFormFields>>;
}

const TerminalForm2 = ({mode}: TerminalForm2Props) => {
  const [fields, setFields] = React.useState(createEmptyTerminalFormFields);

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const {id} = useParams();
  const query = useTerminalQuery();

  const mapper = (source: TerminalView) => toTerminalFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, setFields);

  const steps = [
    "Define base characteristics",
    "Add medium and qualifiers",
    "Add classifiers",
    "Add attributes",
    "Review and submit",
  ];

  const stepComponents = [
    TerminalBaseForm2,
    MediumAndQualifierForm
  ];

  const FormStep = stepComponents[activeStep];

  return (
    <>
      {isLoading && <Loader />}
      {!isLoading && (
        <TypeFormContainer
          title={mode === "edit" ? "Edit terminal type" : "Create terminal type"}
          steps={steps}
          activeStep={activeStep}
          setActiveStep={setActiveStep}
          formRef={currentStepFormRef}
        >
          <FormStep fields={fields} setFields={setFields} ref={currentStepFormRef} />
        </TypeFormContainer>
      )}
    </>
  )
}

export default TerminalForm2;