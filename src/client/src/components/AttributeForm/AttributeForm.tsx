import { useGetAttribute } from "api/attribute.queries";
import Loader from "components/Loader";
import TypeFormContainer from "components/TypeFormContainer";
import { usePrefilledForm } from "helpers/form.helpers";
import React from "react";
import { useParams } from "react-router-dom";
import { AttributeView } from "types/attributes/attributeView";
import { FormMode } from "types/formMode";
import { AttributeFormFields, createEmptyAttributeFormFields, toAttributeFormFields } from "./AttributeForm.helpers";
import BaseStep from "./BaseStep";
import QualifiersStep from "./QualifiersStep";
import ReviewAndCreateStep from "./ReviewAndCreateStep";
import ReviewAndUpdateStep from "./ReviewAndUpdate";
import UnitsStep from "./UnitsStep";
import ValueConstraintStep from "./ValueConstraintStep";

interface AttributeFormProps {
  mode?: FormMode;
}

export interface AttributeFormStepProps {
  fields: AttributeFormFields;
  setFields: (fields: AttributeFormFields) => void;
}

const AttributeForm = ({ mode }: AttributeFormProps) => {
  const [fields, setFields] = React.useState<AttributeFormFields>(createEmptyAttributeFormFields);

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const { id } = useParams();

  const query = useGetAttribute(id);

  const mapper = (source: AttributeView) => toAttributeFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, setFields);

  const steps = [
    "Define base characteristics",
    "Choose qualifiers",
    "Add units",
    "Add value constraint",
    "Review and submit",
  ];

  const stepComponents = [
    BaseStep,
    QualifiersStep,
    UnitsStep,
    ValueConstraintStep,
    mode === "edit" ? ReviewAndUpdateStep : ReviewAndCreateStep,
  ];

  const FormStep = stepComponents[activeStep];

  return (
    <>
      {isLoading && <Loader />}
      {!isLoading && (
        <TypeFormContainer
          title={mode === "edit" ? "Edit attribute type" : "Create attribute type"}
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

export default AttributeForm;
