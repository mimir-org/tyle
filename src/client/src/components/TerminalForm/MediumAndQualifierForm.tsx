import FormField from "components/FormField";
import Select from "components/Select";
import React from "react";
import { RdlMedium } from "types/terminals/rdlMedium";
import { useGetMedia } from "../../api/medium.queries";
import { Direction } from "../../types/terminals/direction";
import { getOptionsFromEnum } from "../../utils";
import {
  MediumAndQualifierFormWrapper,
  MediumSelectWrapper,
  QualifierSelectWrapper,
} from "./MediumAndQualifiersForm.styled";
import { TerminalFormStepProps } from "./TerminalForm";

const MediumAndQualifierForm = React.forwardRef<HTMLFormElement, TerminalFormStepProps>(
  ({ fields, setFields }, ref) => {
    const { medium, qualifier } = fields;

    const setMedium = (medium: RdlMedium | undefined) => setFields({ ...fields, medium: medium ?? null });
    const setQualifier = (qualifier: Direction) => setFields({ ...fields, qualifier: qualifier });

    const mediumQuery = useGetMedia();
    const mediumOptions = mediumQuery.data?.map((medium) => ({
      value: medium,
      label: medium.name,
    }));
    const qualifierOptions = getOptionsFromEnum<Direction>(Direction);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
      event.preventDefault();
    };

    return (
      <MediumAndQualifierFormWrapper onSubmit={handleSubmit} ref={ref}>
        <MediumSelectWrapper>
          <FormField label="Medium">
            <Select
              options={mediumOptions}
              isLoading={mediumQuery.isLoading}
              onChange={(x) => {
                setMedium(x?.value);
              }}
              value={mediumOptions?.find((x) => x.value.id === medium?.id)}
              isClearable={true}
            />
          </FormField>
        </MediumSelectWrapper>
        <QualifierSelectWrapper>
          <FormField label="Qualifier">
            <Select
              options={qualifierOptions}
              onChange={(x) => {
                // @ts-expect-error: Cannot be null. Qualifier is a static enum.
                setQualifier(x.value);
              }}
              value={qualifierOptions?.find((x) => x.value === qualifier)}
              isClearable={true}
            />
          </FormField>
        </QualifierSelectWrapper>
      </MediumAndQualifierFormWrapper>
    );
  },
);

MediumAndQualifierForm.displayName = "MediumAndQualifierForm";

export default MediumAndQualifierForm;
