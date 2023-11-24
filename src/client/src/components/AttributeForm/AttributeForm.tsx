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

export interface FormStepProps {
  fields: AttributeFormFields;
  setFields: (nextAttributeFormFields: AttributeFormFields) => void;
}

export interface FormStep {
  id: string;
  description: string;
  component: React.ForwardRefExoticComponent<FormStepProps>;
}

const AttributeForm = ({ mode }: AttributeFormProps) => {
  const [fields, setFields] = React.useState<AttributeFormFields>(createEmptyAttributeFormFields);

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const steps = [
    {
      id: "base-step",
      description: "Define base characteristics",
      component: BaseStep,
    },
    {
      id: "qualifiers-step",
      description: "Choose qualifiers",
      component: QualifiersStep,
    },
    {
      id: "units-step",
      description: "Add units",
      component: UnitsStep,
    },
    {
      id: "value-constraint-step",
      description: "Add value constraint",
      component: ValueConstraintStep,
    },
    {
      id: "review-step",
      description: "Review and submit",
      component: activeStep === 0 ? BaseStep : BaseStep,
    },
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

  const FormStep = steps[activeStep].component;

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
