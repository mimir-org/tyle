import { useGetTerminal } from "api/terminal.queries";
import React from "react";
import { useParams } from "react-router-dom";
import { usePrefilledForm } from "../../helpers/form.helpers";
import { FormMode } from "../../types/formMode";
import { TerminalView } from "../../types/terminals/terminalView";
import Loader from "../Loader";
import TypeFormContainer from "../TypeFormContainer";
import AttributesForm from "./AttributesForm";
import ClassifiersForm from "./ClassifiersForm";
import MediumAndQualifierForm from "./MediumAndQualifierForm";
import ReviewAndCreateForm from "./ReviewAndCreateForm";
import ReviewAndUpdateForm from "./ReviewAndUpdateForm";
import TerminalBaseForm from "./TerminalBaseForm";
import { createEmptyTerminalFormFields, TerminalFormFields, toTerminalFormFields } from "./TerminalForm.helpers";

interface TerminalFormProps {
  mode?: FormMode;
}
export interface TerminalFormStepProps {
  fields: TerminalFormFields;
  setFields: React.Dispatch<React.SetStateAction<TerminalFormFields>>;
}

const TerminalForm = ({ mode }: TerminalFormProps) => {
  const [fields, setFields] = React.useState(createEmptyTerminalFormFields);

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const { id } = useParams();
  const query = useGetTerminal(id);

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
    TerminalBaseForm,
    MediumAndQualifierForm,
    ClassifiersForm,
    AttributesForm,
    mode === "edit" ? ReviewAndUpdateForm : ReviewAndCreateForm,
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
  );
};

export default TerminalForm;
