import { FormField, Input, Select, Textarea } from "@mimirorg/component-library";
import { useGetPredicates } from "api/predicate.queries";
import React from "react";
import { RdlPredicate } from "types/attributes/rdlPredicate";
import { DESCRIPTION_LENGTH, NAME_LENGTH } from "types/common/stringLengthConstants";
import { AttributeBaseFormWrapper } from "./AttributeBaseForm.styled";
import { AttributeFormStepProps } from "./AttributeForm";

const AttributeBaseForm = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const { name, predicate, description } = fields;
  const setName = (name: string) => setFields({ ...fields, name });
  const setPredicate = (predicate: RdlPredicate | undefined) => setFields({ ...fields, predicate: predicate ?? null });
  const setDescription = (description: string) => setFields({ ...fields, description });

  const predicateQuery = useGetPredicates();
  const predicateOptions = predicateQuery.data?.map((predicate) => ({
    value: predicate,
    label: predicate.name,
  }));

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <AttributeBaseFormWrapper onSubmit={handleSubmit} ref={ref}>
      <FormField label="Name">
        <Input required={true} maxLength={NAME_LENGTH} value={name} onChange={(event) => setName(event.target.value)} />
      </FormField>
      <FormField label="Predicate">
        <Select
          options={predicateOptions}
          isLoading={predicateQuery.isLoading}
          onChange={(x) => setPredicate(x?.value)}
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
    </AttributeBaseFormWrapper>
  );
});

AttributeBaseForm.displayName = "AttributeBaseForm";

export default AttributeBaseForm;
