import {
  Box,
  Button,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Select,
  Textarea,
} from "@mimirorg/component-library";
import { useGetPurposes } from "api/purpose.queries";
import { useTheme } from "styled-components";
import { Aspect } from "types/common/aspect";
import { getOptionsFromEnum } from "utils";
import { BlockFormFields } from "./BlockForm.helpers";

interface BaseStepProps {
  blockFormFields: BlockFormFields;
  setBlockFormFields: (nextBlockFormFields: BlockFormFields) => void;
}

const BaseStep = ({ blockFormFields, setBlockFormFields }: BaseStepProps) => {
  const theme = useTheme();

  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);
  const purposeQuery = useGetPurposes();
  const purposeOptions = purposeQuery.data?.map((purpose) => ({
    value: purpose,
    label: purpose.name,
  }));

  return (
    <Box maxWidth="50rem">
      <FormBaseFieldsContainer>
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
          <Box flexGrow="3">
            <FormField label="Name">
              <Input
                value={blockFormFields.name}
                onChange={(event) => setBlockFormFields({ ...blockFormFields, name: event.target.value })}
              />
            </FormField>
          </Box>
          <Box flexGrow="1">
            <FormField label="Notation">
              <Input
                value={blockFormFields.notation}
                onChange={(event) => setBlockFormFields({ ...blockFormFields, notation: event.target.value })}
              />
            </FormField>
          </Box>
        </Flexbox>
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
          <Box flexGrow="1">
            <FormField label="Aspect">
              <Select
                placeholder="Select an aspect"
                options={aspectOptions}
                onChange={(x) => {
                  setBlockFormFields({
                    ...blockFormFields,
                    aspect: x?.value ?? null,
                  });
                }}
                value={aspectOptions.find((x) => x.value === blockFormFields.aspect)}
                isClearable={true}
              />
            </FormField>
          </Box>
          <Box flexGrow="1">
            <FormField label="Purpose">
              <Select
                placeholder="Select a purpose"
                options={purposeOptions}
                isLoading={purposeQuery.isLoading}
                onChange={(x) => {
                  setBlockFormFields({
                    ...blockFormFields,
                    purpose: x?.value ?? null,
                  });
                }}
                value={purposeOptions?.find((x) => x.value === blockFormFields.purpose)}
                isClearable={true}
              />
            </FormField>
          </Box>
        </Flexbox>
        <FormField label="Description">
          <Textarea
            value={blockFormFields.description}
            onChange={(event) => setBlockFormFields({ ...blockFormFields, description: event.target.value })}
          />
        </FormField>
      </FormBaseFieldsContainer>
      <Button onClick={() => console.log(blockFormFields)}>Log state</Button>
    </Box>
  );
};

export default BaseStep;
