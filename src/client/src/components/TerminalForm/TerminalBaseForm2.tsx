import React from "react";
import { TerminalFormStepProps } from "./TerminalForm2";
import { Aspect } from "../../types/common/aspect";
import { RdlPurpose } from "../../types/common/rdlPurpose";
import { getOptionsFromEnum } from "../../utils";
import { useGetPurposes } from "../../api/purpose.queries";
import { TerminalBaseFormWrapper } from "./TerminalBaseForm2.styled";
import {
  AspectSelectWrapper, DescriptionInputWrapper,
  NameInputWrapper,
  NotationInputWrapper,
  PurposeSelectWrapper
} from "../BlockForm/BlockBaseForm.styled";
import { FormField, Input, Select, Textarea } from "@mimirorg/component-library";
import { DESCRIPTION_LENGTH, NAME_LENGTH, NOTATION_LENGTH } from "../../types/common/stringLengthConstants";

const TerminalBaseForm2 = React.forwardRef<HTMLFormElement, TerminalFormStepProps>(({ fields, setFields }, ref) => {
  const { name, notation, aspect, purpose, description } = fields;
  const setName = (name: string) => setFields({ ...fields, name });
  const setNotation = (notation: string) => setFields({ ...fields, notation });
  const setAspect = (aspect: Aspect | undefined) => setFields({ ...fields, aspect: aspect ?? null });
  const setPurpose = (purpose: RdlPurpose | undefined) => setFields({ ...fields, purpose: purpose ?? null });
  const setDescription = (description: string) => setFields({ ...fields, description });


  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);

  const purposeQuery = useGetPurposes();
  const purposeOptions = purposeQuery.data?.map((purpose) => ({
    value: purpose,
    label: purpose.name
  }));

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };
  return (
    <TerminalBaseFormWrapper onSubmit={handleSubmit} ref={ref}>
      <NameInputWrapper>
        <FormField label="Name">
          <Input
            required={true}
            maxLength={NAME_LENGTH}
            value={name}
            onChange={(event) => setName(event.target.value)}
          />
        </FormField>
      </NameInputWrapper>
      <NotationInputWrapper>
        <FormField label="Notation">
          <Input maxLength={NOTATION_LENGTH} value={notation} onChange={(event) => setNotation(event.target.value)} />
        </FormField>
      </NotationInputWrapper>
      <AspectSelectWrapper>
        <FormField label="Aspect">
          <Select
            options={aspectOptions}
            onChange={(x) => {
              setAspect(x?.value);
            }}
            value={aspectOptions.find((x) => x.value === aspect)}
            isClearable={true}
          />
        </FormField>
      </AspectSelectWrapper>
      <PurposeSelectWrapper>
        <FormField label="Purpose">
          <Select
            options={purposeOptions}
            isLoading={purposeQuery.isLoading}
            onChange={(x) => {
              setPurpose(x?.value);
            }}
            value={purposeOptions?.find((x) => x.value.id === purpose?.id)}
            isClearable={true}
          />
        </FormField>
      </PurposeSelectWrapper>
      <DescriptionInputWrapper>
        <FormField label="Description">
          <Textarea
            maxLength={DESCRIPTION_LENGTH}
            value={description}
            onChange={(event) => setDescription(event.target.value)}
          />
        </FormField>
      </DescriptionInputWrapper>
    </TerminalBaseFormWrapper>
  );
});

TerminalBaseForm2.displayName = "TerminalBaseForm";

export default TerminalBaseForm2;