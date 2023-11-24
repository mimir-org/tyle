import Loader from "components/Loader";
import TypeFormContainer from "components/TypeFormContainer";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import React from "react";
import { AttributeView } from "types/attributes/attributeView";
import { FormMode } from "types/formMode";
import {
  toAttributeFormFields,
  toAttributeTypeRequest,
  useAttributeMutation,
  useAttributeQuery,
} from "./AttributeForm.helpers";
import BaseStep from "./BaseStep";
import QualifiersStep from "./QualifiersStep";
import ReviewAndSubmitStep from "./ReviewAndSubmitStep";
import UnitsStep from "./UnitsStep";
import ValueConstraintStep from "./ValueConstraintStep";
import { useAttributeFormState } from "./useAttributeFormState";

interface AttributeFormProps {
  mode?: FormMode;
}

export interface FormStep {
  id: string;
  description: string;
  component: React.ReactNode;
}

const AttributeForm = ({ mode }: AttributeFormProps) => {
  const [formFields, setFormFields, setBaseFields, setQualifiers, setUnitRequirement, setUnits, setValueConstraint] =
    useAttributeFormState();

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const steps = [
    {
      id: "base-step",
      description: "Define base characteristics",
      component: <BaseStep baseFields={formFields.base} setBaseFields={setBaseFields} ref={currentStepFormRef} />,
    },
    {
      id: "qualifiers-step",
      description: "Choose qualifiers",
      component: <QualifiersStep qualifiers={formFields.qualifiers} setQualifiers={setQualifiers} />,
    },
    {
      id: "units-step",
      description: "Add units",
      component: (
        <UnitsStep
          unitRequirement={formFields.unitRequirement}
          setUnitRequirement={setUnitRequirement}
          units={formFields.units}
          setUnits={setUnits}
        />
      ),
    },
    {
      id: "value-constraint-step",
      description: "Add value constraint",
      component: (
        <ValueConstraintStep valueConstraint={formFields.valueConstraint} setValueConstraint={setValueConstraint} />
      ),
    },
    {
      id: "review-step",
      description: "Review and submit",
      component: <ReviewAndSubmitStep mode={mode} attributeFormFields={formFields} />,
    },
  ];

  const query = useAttributeQuery();
  const mapper = (source: AttributeView) => toAttributeFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, setFormFields);

  const mutation = useAttributeMutation(query.data?.id, mode);

  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toAttributeTypeRequest(formFields), mutation.mutateAsync, toast);
  };

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
          {steps[activeStep].component}
        </TypeFormContainer>
      )}
    </>
  );
};

export default AttributeForm;
