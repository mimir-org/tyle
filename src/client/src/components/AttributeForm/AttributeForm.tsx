import { useGetAttribute } from "api/attribute.queries";
import Loader from "components/Loader";
import TypeFormContainer from "components/TypeFormContainer";
import ValueConstraintForm from "components/ValueConstraintForm";
import { usePrefilledForm } from "helpers/form.helpers";
import React from "react";
import { useParams } from "react-router-dom";
import { AttributeView } from "types/attributes/attributeView";
import { FormMode } from "types/formMode";
import AttributeBaseForm from "./AttributeBaseForm";
import { AttributeFormFields, createEmptyAttributeFormFields, toAttributeFormFields } from "./AttributeForm.helpers";
import QualifiersForm from "./QualifiersForm";
import ReviewAndCreateForm from "./ReviewAndCreateForm";
import ReviewAndUpdateForm from "./ReviewAndUpdateForm";
import UnitsForm from "./UnitsForm";

interface AttributeFormProps {
  mode?: FormMode;
}

export interface AttributeFormStepProps {
  fields: AttributeFormFields;
  setFields: React.Dispatch<React.SetStateAction<AttributeFormFields>>;
}

const AttributeForm = ({ mode }: AttributeFormProps) => {
  const [fields, setFields] = React.useState<AttributeFormFields>(createEmptyAttributeFormFields);

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const { id } = useParams();
  const query = useGetAttribute(id);

  console.log("Modus fra attributtskjema " + mode);

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
    AttributeBaseForm,
    QualifiersForm,
    UnitsForm,
    ValueConstraintForm,
    mode === "edit" ? ReviewAndUpdateForm : ReviewAndCreateForm,
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
