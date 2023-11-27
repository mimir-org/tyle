import { FormField, Input, Select, Textarea } from "@mimirorg/component-library";
import { useGetPredicates } from "api/predicate.queries";
import React from "react";
import { DESCRIPTION_LENGTH, NAME_LENGTH } from "types/common/stringLengthConstants";
import { AttributeFormStepProps } from "./AttributeForm";
import { BaseStepWrapper } from "./BaseStep.styled";

const BaseStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const [name, setName] = React.useState(fields.name);
  const [predicate, setPredicate] = React.useState(fields.predicate);
  const [description, setDescription] = React.useState(fields.description);

  const predicateQuery = useGetPredicates();
  const predicateOptions = predicateQuery.data?.map((predicate) => ({
    value: predicate,
    label: predicate.name,
  }));

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setFields({ ...fields, name, predicate, description });
  };

  return (
    <BaseStepWrapper onSubmit={handleSubmit} ref={ref}>
      <FormField label="Name">
        <Input required={true} maxLength={NAME_LENGTH} value={name} onChange={(event) => setName(event.target.value)} />
      </FormField>
      <FormField label="Predicate">
        <Select
          options={predicateOptions}
          isLoading={predicateQuery.isLoading}
          onChange={(x) => setPredicate(x?.value ?? null)}
          value={predicateOptions?.find((x) => x.value.id === predicate?.id)}
          isClearable={true}
        />
      </FormField>
      <FormField label="Description">
        <Textarea
          maxLength={DESCRIPTION_LENGTH}
          value={description}
          onChange={(event) => setDescription(event.target.value)}
        />
      </FormField>
    </BaseStepWrapper>
  );
});

BaseStep.displayName = "BaseStep";

export default BaseStep;
