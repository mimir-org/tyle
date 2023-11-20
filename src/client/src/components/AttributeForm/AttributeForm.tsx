import { Box, Flexbox, FormContainer, Text } from "@mimirorg/component-library";
import FormStepsNavigation from "components/FormStepsNavigation";
import Loader from "components/Loader";
import { onSubmitForm, usePrefilledForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import React from "react";
import { useTheme } from "styled-components";
import { AttributeView } from "types/attributes/attributeView";
import { FormMode } from "types/formMode";
import {
  createEmptyAttributeFormFields,
  toAttributeFormFields,
  toAttributeTypeRequest,
  useAttributeMutation,
  useAttributeQuery,
} from "./AttributeForm.helpers";
import BaseStep from "./BaseStep";
import QualifiersStep from "./QualifiersStep";

interface AttributeFormProps {
  mode?: FormMode;
}

const AttributeForm = ({ mode }: AttributeFormProps) => {
  const theme = useTheme();

  const [attributeFormFields, setAttributeFormFields] = React.useState(createEmptyAttributeFormFields);

  const query = useAttributeQuery();
  const mapper = (source: AttributeView) => toAttributeFormFields(source);

  const [_, isLoading] = usePrefilledForm(query, mapper, setAttributeFormFields);

  const mutation = useAttributeMutation(query.data?.id, mode);

  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute");

  const [activeStep, setActiveStep] = React.useState(0);

  const steps = [
    "Define base characteristics",
    "Choose qualifiers",
    "Add units",
    "Add value constraint",
    "Review and submit",
  ];

  const getFormStep = (step: number) => {
    switch (step) {
      case 0:
        return <BaseStep attributeFormFields={attributeFormFields} setAttributeFormFields={setAttributeFormFields} />;
      case 1:
        return (
          <QualifiersStep attributeFormFields={attributeFormFields} setAttributeFormFields={setAttributeFormFields} />
        );
      default:
        return <></>;
    }
  };

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toAttributeTypeRequest(attributeFormFields), mutation.mutateAsync, toast);
  };

  return (
    <FormContainer onSubmit={handleSubmit}>
      {isLoading && <Loader />}
      {!isLoading && (
        <Box width="100%">
          <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.multiple(6)}>
            <Text as="h1">{mode === "edit" ? "Edit attribute type" : "Create attribute type"}</Text>

            <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.multiple(18)}>
              <FormStepsNavigation steps={steps} activeStep={activeStep} setActiveStep={setActiveStep} />
              <Box flexGrow="1">{getFormStep(activeStep)}</Box>
            </Flexbox>
          </Flexbox>
        </Box>
      )}
    </FormContainer>
  );
};

export default AttributeForm;
