import { useGetBlock } from "api/block.queries";
import Loader from "components/Loader";
import TypeFormContainer from "components/TypeFormContainer";
import { usePrefilledForm } from "helpers/form.helpers";
import React from "react";
import { useParams } from "react-router-dom";
import { BlockView } from "types/blocks/blockView";
import { FormMode } from "types/formMode";
import AttributesForm from "./AttributesForm";
import BlockBaseForm from "./BlockBaseForm";
import { BlockFormFields, createEmptyBlockFormFields, toBlockFormFields } from "./BlockForm.helpers";
import ClassifiersForm from "./ClassifiersForm";
import ReviewAndCreateForm from "./ReviewAndCreateForm";
import ReviewAndUpdateForm from "./ReviewAndUpdateForm";
import SymbolForm from "./SymbolForm";
import TerminalsForm from "./TerminalsForm";

interface BlockFormProps {
  mode?: FormMode;
}

export interface BlockFormStepProps {
  fields: BlockFormFields;
  setFields: React.Dispatch<React.SetStateAction<BlockFormFields>>;
}

const BlockForm = ({ mode }: BlockFormProps) => {
  const [fields, setFields] = React.useState(createEmptyBlockFormFields);

  const [activeStep, setActiveStep] = React.useState(0);
  const currentStepFormRef = React.useRef<HTMLFormElement>(null);

  const { id } = useParams();
  const query = useGetBlock(id);

  const mapper = (source: BlockView) => toBlockFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, setFields);

  const steps = [
    "Define base characteristics",
    "Choose classifiers",
    "Add attributes",
    "Add terminals",
    "Select symbol",
    "Review and submit",
  ];

  const stepComponents = [
    BlockBaseForm,
    ClassifiersForm,
    AttributesForm,
    TerminalsForm,
    SymbolForm,
    mode === "edit" ? ReviewAndUpdateForm : ReviewAndCreateForm,
  ];

  const FormStep = stepComponents[activeStep];

  return (
    <>
      {isLoading && <Loader />}
      {!isLoading && (
        <TypeFormContainer
          title={mode === "edit" ? "Edit block type" : "Create block type"}
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

export default BlockForm;
