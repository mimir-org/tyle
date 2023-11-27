import { FormField, Select } from "@mimirorg/component-library";
import React from "react";
import { ProvenanceQualifier } from "types/attributes/provenanceQualifier";
import { RangeQualifier } from "types/attributes/rangeQualifier";
import { RegularityQualifier } from "types/attributes/regularityQualifier";
import { ScopeQualifier } from "types/attributes/scopeQualifier";
import { getOptionsFromEnum } from "utils";
import { AttributeFormStepProps } from "./AttributeForm";
import { QualifierSelectWrapper, QualifiersStepWrapper } from "./QualifiersStep.styled";

const QualifiersStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields, setFields }, ref) => {
  const [provenanceQualifier, setProvenanceQualifier] = React.useState(fields.provenanceQualifier);
  const [rangeQualifier, setRangeQualifier] = React.useState(fields.rangeQualifier);
  const [regularityQualifier, setRegularityQualifier] = React.useState(fields.regularityQualifier);
  const [scopeQualifier, setScopeQualifier] = React.useState(fields.scopeQualifier);

  const provenanceQualifierOptions = getOptionsFromEnum<ProvenanceQualifier>(ProvenanceQualifier);
  const rangeQualifierOptions = getOptionsFromEnum<RangeQualifier>(RangeQualifier);
  const regularityQualifierOptions = getOptionsFromEnum<RegularityQualifier>(RegularityQualifier);
  const scopeQualifierOptions = getOptionsFromEnum<ScopeQualifier>(ScopeQualifier);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    setFields({ ...fields, provenanceQualifier, rangeQualifier, regularityQualifier, scopeQualifier });
  };

  return (
    <QualifiersStepWrapper onSubmit={handleSubmit} ref={ref}>
      <QualifierSelectWrapper>
        <FormField label="Provenance qualifier">
          <Select
            options={provenanceQualifierOptions}
            onChange={(x) => setProvenanceQualifier(x?.value ?? null)}
            value={provenanceQualifierOptions.find((x) => x.value === provenanceQualifier)}
            isClearable={true}
          />
        </FormField>
      </QualifierSelectWrapper>
      <QualifierSelectWrapper>
        <FormField label="Range qualifier">
          <Select
            options={rangeQualifierOptions}
            onChange={(x) => setRangeQualifier(x?.value ?? null)}
            value={rangeQualifierOptions.find((x) => x.value === rangeQualifier)}
            isClearable={true}
          />
        </FormField>
      </QualifierSelectWrapper>
      <QualifierSelectWrapper>
        <FormField label="Regularity qualifier">
          <Select
            options={regularityQualifierOptions}
            onChange={(x) => setRegularityQualifier(x?.value ?? null)}
            value={regularityQualifierOptions.find((x) => x.value === regularityQualifier)}
            isClearable={true}
          />
        </FormField>
      </QualifierSelectWrapper>
      <QualifierSelectWrapper>
        <FormField label="Scope qualifier">
          <Select
            options={scopeQualifierOptions}
            onChange={(x) => setScopeQualifier(x?.value ?? null)}
            value={scopeQualifierOptions.find((x) => x.value === scopeQualifier)}
            isClearable={true}
          />
        </FormField>
      </QualifierSelectWrapper>
    </QualifiersStepWrapper>
  );
});

QualifiersStep.displayName = "QualifiersStep";

export default QualifiersStep;
