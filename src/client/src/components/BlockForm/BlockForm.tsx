import { Box, Flexbox, FormContainer, Text } from "@mimirorg/component-library";
import FormStepsNavigation from "components/FormStepsNavigation";
import Loader from "components/Loader";
import { onSubmitForm, usePrefilledFormTemporary, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import React, { ReactElement, useState } from "react";
import { useTheme } from "styled-components/macro";
import { BlockView } from "types/blocks/blockView";
import { FormMode } from "types/formMode";
import AttributesStep from "./AttributesStep";
import BaseStep from "./BaseStep";
import {
  createEmptyBlockFormFields,
  toBlockFormFields,
  toBlockTypeRequest,
  useBlockMutation,
  useBlockQuery,
} from "./BlockForm.helpers";
import ClassifiersStep from "./ClassifiersStep";
import ConnectTerminalsToSymbolStep from "./ConnectTerminalsToSymbolStep";
import ReviewAndSubmit from "./ReviewAndSubmit";
import SelectSymbolStep from "./SelectSymbolStep";
import TerminalsStep from "./TerminalsStep";

interface BlockFormProps {
  mode?: FormMode;
}

const BlockForm = ({ mode }: BlockFormProps) => {
  const theme = useTheme();

  const [blockFormFields, setBlockFormFields] = React.useState(createEmptyBlockFormFields);

  const query = useBlockQuery();
  const mapper = (source: BlockView) => toBlockFormFields(source);

  const [_, isLoading] = usePrefilledFormTemporary(query, mapper, setBlockFormFields);

  const mutation = useBlockMutation(query.data?.id, mode);

  //useServerValidation(mutation.error, setError);
  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("block");

  const [activeStep, setActiveStep] = useState(0);

  const steps = [
    "Define base characteristics",
    "Choose classifiers",
    "Add attributes",
    "Add terminals",
    "Select symbol",
    "Review and submit",
  ];

  const getFormStep = (step: number): ReactElement => {
    switch (step) {
      case 0:
        return <BaseStep blockFormFields={blockFormFields} setBlockFormFields={setBlockFormFields} />;
      case 1:
        return (
          <ClassifiersStep
            chosenClassifiers={blockFormFields.classifiers}
            setClassifiers={(nextClassifiers) => {
              setBlockFormFields({ ...blockFormFields, classifiers: nextClassifiers });
            }}
          />
        );
      case 2:
        return (
          <AttributesStep
            chosenAttributes={blockFormFields.attributes}
            setAttributes={(nextAttributes) => {
              setBlockFormFields({ ...blockFormFields, attributes: nextAttributes });
            }}
          />
        );
      case 3:
        return (
          <TerminalsStep
            chosenTerminals={blockFormFields.terminals}
            setTerminals={(nextTerminals) => {
              setBlockFormFields({ ...blockFormFields, terminals: nextTerminals });
            }}
          />
        );
      case 4:
        if (blockFormFields.symbol === null) {
          return (
            <SelectSymbolStep
              setSymbol={(nextSymbol) => setBlockFormFields({ ...blockFormFields, symbol: nextSymbol })}
            />
          );
        } else {
          return (
            <ConnectTerminalsToSymbolStep
              symbol={blockFormFields.symbol}
              removeSymbol={handleRemoveSymbol}
              terminals={blockFormFields.terminals}
              setTerminals={(nextTerminals) => {
                setBlockFormFields({ ...blockFormFields, terminals: nextTerminals });
              }}
            />
          );
        }
      case 5:
        return <ReviewAndSubmit mode={mode} blockFormFields={blockFormFields} />;
      default:
        return <></>;
    }
  };

  const handleRemoveSymbol = () => {
    const nextTerminals = blockFormFields.terminals.map((terminal) => ({ ...terminal, connectionPoint: null }));

    setBlockFormFields({ ...blockFormFields, symbol: null, terminals: nextTerminals });
  };

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toBlockTypeRequest(blockFormFields), mutation.mutateAsync, toast);
  };

  return (
    <FormContainer onSubmit={handleSubmit}>
      {isLoading && <Loader />}
      {!isLoading && (
        <Box width="100%">
          <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.multiple(6)}>
            <Text as="h1">{mode === "edit" ? "Edit block type" : "Create block type"}</Text>

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

export default BlockForm;
