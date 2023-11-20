import { Box, Flexbox, FormBaseFieldsContainer, FormField, Input, Select, Textarea } from "@mimirorg/component-library";
import { useGetPredicates } from "api/predicate.queries";
import { useTheme } from "styled-components";
import { AttributeFormFields } from "./AttributeForm.helpers";

interface BaseStepProps {
  attributeFormFields: AttributeFormFields;
  setAttributeFormFields: (nextAttributeFormFields: AttributeFormFields) => void;
}

const BaseStep = ({ attributeFormFields, setAttributeFormFields }: BaseStepProps) => {
  const theme = useTheme();

  const predicateQuery = useGetPredicates();
  const predicateOptions = predicateQuery.data?.map((predicate) => ({
    value: predicate,
    label: predicate.name,
  }));

  return (
    <Box maxWidth="50rem">
      <FormBaseFieldsContainer>
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
          <Box flexGrow="3">
            <FormField label="Name">
              <Input
                placeholder="Name"
                value={attributeFormFields.name}
                onChange={(event) => setAttributeFormFields({ ...attributeFormFields, name: event.target.value })}
              />
            </FormField>
          </Box>
          <Box flexGrow="1">
            <FormField label="Predicate">
              <Select
                placeholder="Select a predicate"
                options={predicateOptions}
                isLoading={predicateQuery.isLoading}
                onChange={(x) => {
                  setAttributeFormFields({
                    ...attributeFormFields,
                    predicate: x?.value ?? null,
                  });
                }}
                value={predicateOptions?.find((x) => x.value.id === attributeFormFields.predicate?.id)}
                isClearable={true}
              />
            </FormField>
          </Box>
        </Flexbox>
        <FormField label="Description">
          <Textarea
            placeholder="Additional information about this attribute type can be supplied here."
            value={attributeFormFields.description}
            onChange={(event) => setAttributeFormFields({ ...attributeFormFields, description: event.target.value })}
          />
        </FormField>
      </FormBaseFieldsContainer>
    </Box>
  );
};

export default BaseStep;
