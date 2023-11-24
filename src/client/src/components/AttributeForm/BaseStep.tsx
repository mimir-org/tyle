import { Box, Flexbox, FormBaseFieldsContainer, FormField, Input, Select, Textarea } from "@mimirorg/component-library";
import { useGetPredicates } from "api/predicate.queries";
import React from "react";
import { useTheme } from "styled-components";
import { AttributeBaseFields } from "./AttributeForm.helpers";

interface BaseStepProps {
  baseFields: AttributeBaseFields;
  setBaseFields: (nextAttributeFormFields: AttributeBaseFields) => void;
}

const BaseStep = React.forwardRef<HTMLFormElement, BaseStepProps>(({ baseFields, setBaseFields }, ref) => {
  const theme = useTheme();

  const [name, setName] = React.useState(baseFields.name);
  const [predicate, setPredicate] = React.useState(baseFields.predicate);
  const [description, setDescription] = React.useState(baseFields.description);

  const predicateQuery = useGetPredicates();
  const predicateOptions = predicateQuery.data?.map((predicate) => ({
    value: predicate,
    label: predicate.name,
  }));

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setBaseFields({ name, predicate, description });
  };

  return (
    <form onSubmit={handleSubmit} id="current-form-step" ref={ref}>
      <Box maxWidth="50rem">
        <FormBaseFieldsContainer>
          <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl}>
            <Box flexGrow="3">
              <FormField label="Name">
                <Input placeholder="Name" value={name} onChange={(event) => setName(event.target.value)} />
              </FormField>
            </Box>
            <Box flexGrow="1">
              <FormField label="Predicate">
                <Select
                  placeholder="Select a predicate"
                  options={predicateOptions}
                  isLoading={predicateQuery.isLoading}
                  onChange={(x) => setPredicate(x?.value ?? null)}
                  value={predicateOptions?.find((x) => x.value.id === predicate?.id)}
                  isClearable={true}
                />
              </FormField>
            </Box>
          </Flexbox>
          <FormField label="Description">
            <Textarea
              placeholder="Additional information about this attribute type can be supplied here."
              value={description}
              onChange={(event) => setDescription(event.target.value)}
            />
          </FormField>
        </FormBaseFieldsContainer>
      </Box>
    </form>
  );
});

BaseStep.displayName = "BaseStep";

export default BaseStep;
