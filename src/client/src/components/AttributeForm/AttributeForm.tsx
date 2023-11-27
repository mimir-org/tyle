import Loader from "components/Loader";
import TypeFormContainer from "components/TypeFormContainer";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import React from "react";
import { AttributeView } from "types/attributes/attributeView";
import { FormMode } from "types/formMode";
import {
  AttributeFormFields,
  createEmptyAttributeFormFields,
  toAttributeFormFields,
  toAttributeTypeRequest,
  useAttributeMutation,
  useAttributeQuery,
} from "./AttributeForm.helpers";
import BaseStep from "./BaseStep";
import QualifiersStep from "./QualifiersStep";
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
    activeStep === 0 ? BaseStep : BaseStep,
  ];

  const query = useAttributeQuery();
  const mapper = (source: AttributeView) => toAttributeFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, setFields);

  const mutation = useAttributeMutation(query.data?.id, mode);

  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toAttributeTypeRequest(fields), mutation.mutateAsync, toast);
  };

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
